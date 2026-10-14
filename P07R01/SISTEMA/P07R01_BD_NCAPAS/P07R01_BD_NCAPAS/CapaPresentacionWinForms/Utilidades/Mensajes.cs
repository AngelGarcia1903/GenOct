using System.Windows.Forms;
using CapaPresentacionWinForms.Controles;

namespace CapaPresentacionWinForms.Utilidades
{
    //ventanas de mensaje y confirmacion con el estilo de los modales de la web
    internal static class Mensajes
    {
        public static void Exito(string mensaje, IWin32Window ventana = null)
        {
            FrmMensaje.Mostrar("Listo", mensaje, Tema.Verde, ventana);
        }

        public static void Advertencia(string mensaje, IWin32Window ventana = null)
        {
            FrmMensaje.Mostrar("Atención", mensaje, Tema.Naranja, ventana);
        }

        public static void Error(string mensaje, IWin32Window ventana = null)
        {
            FrmMensaje.Mostrar("Error", mensaje, Tema.Coral, ventana);
        }

        //devuelve true si el usuario elige "Aceptar"
        public static bool Confirmar(string mensaje, IWin32Window ventana = null)
        {
            return FrmMensaje.Confirmar("Confirmar", mensaje, "Aceptar", EstiloBoton.Primario, Tema.Violeta, ventana);
        }

        //igual que el modal "Eliminar camion" de la web: boton rojo "Eliminar"
        public static bool ConfirmarEliminar(string titulo, string mensaje, IWin32Window ventana = null)
        {
            return FrmMensaje.Confirmar(titulo, mensaje, "Eliminar", EstiloBoton.Peligro, Tema.Coral, ventana);
        }


        //la capa de negocios devuelve "Ok" o el texto del error; el SP agrega texto tecnico
        //(por ejemplo, de la llave foranea) que aqui se traduce a algo entendible
        public static string Traducir(string resultado)
        {
            if (resultado.Contains("REFERENCE") || resultado.Contains("FOREIGN KEY"))
                return "No se puede eliminar porque tiene rutas asignadas. Elimina primero sus rutas.";

            return resultado;
        }
    }
}
