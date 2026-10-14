using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using P04R01_APIREST.Models;

namespace P04R01_APIREST.Services
{
    //Reglas de la Característica 2. No depende de la BD ni de HTTP: solo recibe datos y regresa resultados.
    public class PersonaService
    {
        //Preposiciones, artículos y conjunciones que pueden aparecer en nombres y apellidos
        private static readonly HashSet<string> Preposiciones = new(StringComparer.OrdinalIgnoreCase)
        {
            "de", "del", "la", "las", "los", "el", "y", "e", "da", "das", "do", "dos", "di", "van", "von", "der"
        };

        private static readonly Regex RegexEmail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]{2,}$", RegexOptions.Compiled);
        private static readonly Regex RegexEspacios = new(@"\s+", RegexOptions.Compiled);
        private static readonly CultureInfo Cultura = new("es-MX");

        public const int EdadMinima = 5;

        //Valida y procesa. Si hay errores regresa (null, errores); si no, (resultado, errores vacíos).
        //"hoy" es parámetro para poder probar con fechas fijas.
        public (PersonaResultado? resultado, Dictionary<string, string> errores) Procesar(PersonaRequest req, DateTime hoy)
        {
            var errores = new Dictionary<string, string>();
            var avisos = new List<string>();

            //---------- Nombre ----------
            var nombreNormalizado = NormalizarNombre(req.nombreCompleto, avisos);
            var palabras = nombreNormalizado.Length == 0 ? new List<string>() : nombreNormalizado.Split(' ').ToList();
            var unidades = AgruparUnidades(palabras);

            if (string.IsNullOrWhiteSpace(req.nombreCompleto))
                errores["nombreCompleto"] = "El nombre completo es obligatorio.";
            else if (unidades.Count < 2)
                errores["nombreCompleto"] = "Escribe tu nombre y al menos un apellido (ej. Juan Pérez López).";
            else if (nombreNormalizado.Length > 100)
                errores["nombreCompleto"] = "El nombre no puede tener más de 100 caracteres.";

            //---------- Hobby ----------
            var hobby = RegexEspacios.Replace(req.hobby ?? string.Empty, " ").Trim();
            if (hobby.Length == 0)
                errores["hobby"] = "El hobby es obligatorio.";
            else if (hobby.Length > 150)
                errores["hobby"] = "El hobby no puede tener más de 150 caracteres.";

            //---------- Fecha de nacimiento ----------
            int edad = 0;
            if (req.fechaNacimiento == null)
                errores["fechaNacimiento"] = "La fecha de nacimiento es obligatoria.";
            else
            {
                var fecha = req.fechaNacimiento.Value.Date;
                edad = CalcularEdad(fecha, hoy.Date);
                if (fecha > hoy.Date)
                    errores["fechaNacimiento"] = "La fecha de nacimiento no puede ser futura.";
                else if (fecha.Year < 1900)
                    errores["fechaNacimiento"] = "La fecha de nacimiento no es válida.";
                else if (edad < EdadMinima)
                    errores["fechaNacimiento"] = $"Debes tener al menos {EdadMinima} años cumplidos.";
            }

            //---------- Email ----------
            var email = (req.email ?? string.Empty).Trim();
            if (email.Length == 0)
                errores["email"] = "El email es obligatorio.";
            else if (email.Length > 100 || !RegexEmail.IsMatch(email))
                errores["email"] = "El email no tiene un formato válido (ej. nombre@dominio.com).";

            if (errores.Count > 0)
                return (null, errores);

            //---------- Partes del nombre: los 2 últimos bloques son apellidos ----------
            string apellidoMaterno = string.Empty;
            string apellidoPaterno;
            List<string> bloquesNombre;
            if (unidades.Count >= 3)
            {
                apellidoMaterno = unidades[^1];
                apellidoPaterno = unidades[^2];
                bloquesNombre = unidades.Take(unidades.Count - 2).ToList();
            }
            else
            {
                apellidoPaterno = unidades[1];
                bloquesNombre = new List<string> { unidades[0] };
                avisos.Add("No se capturó segundo apellido; en el ID se usó 'X' en su lugar.");
            }

            var fechaNac = req.fechaNacimiento!.Value.Date;

            var resultado = new PersonaResultado
            {
                nombreNormalizado = nombreNormalizado,
                nombres = string.Join(" ", bloquesNombre),
                apellidoPaterno = apellidoPaterno,
                apellidoMaterno = apellidoMaterno,
                idUsuario = GenerarId(apellidoPaterno, apellidoMaterno, bloquesNombre[0], fechaNac),
                hobby = hobby,
                email = email.ToLowerInvariant(),
                fechaNacimiento = fechaNac.ToString("yyyy-MM-dd"),
                edad = edad,
                //distintas, en el orden en que aparecen; el frontend se encarga de ordenarlas
                preposiciones = palabras.Where(EsPreposicion).Select(p => p.ToLower(Cultura)).Distinct().ToList(),
                avisos = avisos
            };
            return (resultado, errores);
        }

        //1) Quita caracteres especiales (deja letras, incluidas acentuadas y ñ)
        //2) Quita espacios dobles  3) Primera letra de cada palabra en mayúscula
        public static string NormalizarNombre(string? nombre, List<string>? avisos = null)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return string.Empty;

            var limpio = new StringBuilder();
            var eliminados = new SortedSet<char>();
            foreach (var c in nombre.Normalize(NormalizationForm.FormC))
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c)) limpio.Append(c);
                else if (c == '-' || c == '.' || c == '_') limpio.Append(' '); //separadores comunes -> espacio
                else eliminados.Add(c);
            }
            if (eliminados.Count > 0)
                avisos?.Add($"Se eliminaron caracteres no válidos: {string.Join(" ", eliminados)}");

            var palabras = RegexEspacios.Replace(limpio.ToString(), " ").Trim()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(Capitalizar);

            return string.Join(" ", palabras);
        }

        private static string Capitalizar(string palabra)
        {
            var minusculas = palabra.ToLower(Cultura);
            return char.ToUpper(minusculas[0], Cultura) + minusculas[1..];
        }

        private static bool EsPreposicion(string palabra) => Preposiciones.Contains(palabra);

        //Une cada preposición con la palabra que le sigue: ["Juan","De","La","Cruz","Pérez"] -> ["Juan","De La Cruz","Pérez"]
        //Así un apellido compuesto cuenta como uno solo.
        private static List<string> AgruparUnidades(List<string> palabras)
        {
            var unidades = new List<string>();
            var pendientes = new List<string>();
            foreach (var palabra in palabras)
            {
                pendientes.Add(palabra);
                if (!EsPreposicion(palabra))
                {
                    unidades.Add(string.Join(" ", pendientes));
                    pendientes.Clear();
                }
            }
            //preposiciones sueltas al final ("Juan Pérez De"): se pegan al último bloque
            if (pendientes.Count > 0 && unidades.Count > 0)
                unidades[^1] += " " + string.Join(" ", pendientes);
            return unidades;
        }

        //ID: 2 letras del primer apellido + 1 del segundo + 1 del nombre + fecha ddMMyy
        //Se ignoran las preposiciones ("De La Cruz" -> "CR"), se quitan acentos y la Ñ se toma como N.
        public static string GenerarId(string apellidoPaterno, string apellidoMaterno, string nombre, DateTime fechaNacimiento)
        {
            var paterno = SinAcentos(PalabraPrincipal(apellidoPaterno));
            var materno = SinAcentos(PalabraPrincipal(apellidoMaterno));
            var primerNombre = SinAcentos(PalabraPrincipal(nombre));

            var letrasPaterno = (paterno + "XX")[..2];
            var letraMaterno = materno.Length > 0 ? materno[0].ToString() : "X";
            var letraNombre = primerNombre.Length > 0 ? primerNombre[0].ToString() : "X";

            return (letrasPaterno + letraMaterno + letraNombre).ToUpperInvariant()
                + fechaNacimiento.ToString("ddMMyy", CultureInfo.InvariantCulture);
        }

        private static string PalabraPrincipal(string bloque)
        {
            return bloque.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault(p => !EsPreposicion(p)) ?? string.Empty;
        }

        private static string SinAcentos(string texto)
        {
            var sb = new StringBuilder();
            foreach (var c in texto.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark) sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public static int CalcularEdad(DateTime fechaNacimiento, DateTime hoy)
        {
            var edad = hoy.Year - fechaNacimiento.Year;
            if (fechaNacimiento > hoy.AddYears(-edad)) edad--;
            return edad;
        }
    }//fin de la clase
}//fin del namespace
