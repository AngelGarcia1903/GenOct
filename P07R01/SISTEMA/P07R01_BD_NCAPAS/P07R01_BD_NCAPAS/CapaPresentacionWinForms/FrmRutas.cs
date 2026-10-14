using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms
{
    //rutas en tarjetas (con la foto del camion, chofer y fechas) y botones para agregar, editar y eliminar
    public partial class FrmRutas : FormularioSeccion
    {
        private N_Ruta objNegocio = new N_Ruta();

        public FrmRutas()
        {
            InitializeComponent();
        }


        private void FrmRutas_Load(object sender, EventArgs e)
        {
            CargarRutas();
        }


        //devuelve cuantas rutas se mostraron (-1 si hubo error)
        private int CargarRutas()
        {
            try
            {
                List<E_Ruta> lista = objNegocio.ListarRutas();
                lstRutas.Mostrar(lista.Select(CrearTarjeta), Tarjeta_Editar, Tarjeta_Eliminar);

                if (lista.Count == 0)
                    alerta.Mostrar("No se encontraron rutas", TipoAlerta.Info);
                else
                    alerta.Ocultar();

                return lista.Count;
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al cargar rutas: " + ex.Message, TipoAlerta.Error);
                return -1;
            }
        }


        //una tarjeta por ruta, con los mismos datos que la tarjeta de RutasWeb.aspx
        private static TarjetaItem CrearTarjeta(E_Ruta r)
        {
            return new TarjetaItem
            {
                UrlImagen = r.FotoCamion,
                Insignia = r.ATiempo ? "A tiempo" : "Con retraso",
                InsigniaPositiva = r.ATiempo,
                Categoria = "Camión " + r.Matricula,
                Titulo = $"{r.Origen} → {r.Destino}",
                Descripcion = $"Chofer: {r.NombreChofer} (Lic. {r.Licencia})\nSalida: {r.FechaSalida:dd/MM/yyyy HH:mm}\nLlegada: {r.FechaLlegada:dd/MM/yyyy HH:mm}",
                Numero = r.Distancia.ToString("N1"),
                Unidad = "km",
                Subtexto = "Ruta #" + r.IdRuta,
                Dato = r
            };
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            int total = CargarRutas();
            if (total > 0)
                alerta.Mostrar($"Lista actualizada: {total} ruta(s) a las {DateTime.Now:HH:mm:ss}", TipoAlerta.Exito);
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmRutaEditar())
            {
                if (Dialogos.MostrarModal(frm, this) == System.Windows.Forms.DialogResult.OK)
                {
                    CargarRutas();
                    alerta.Mostrar("Ruta agregada correctamente", TipoAlerta.Exito);
                }
            }
        }


        private void Tarjeta_Editar(object sender, EventArgs e)
        {
            var seleccionada = (E_Ruta)((TarjetaItem)sender).Dato;

            try
            {
                //la lista viene de Select_Rutas_Detalle, que no trae IdChofer ni IdCamion;
                //por eso se pide la ruta completa por su ID
                E_Ruta ruta = objNegocio.ObtenerRutaPorID(seleccionada.IdRuta);

                using (var frm = new FrmRutaEditar(ruta))
                {
                    if (Dialogos.MostrarModal(frm, this) == System.Windows.Forms.DialogResult.OK)
                    {
                        CargarRutas();
                        alerta.Mostrar("Datos de la ruta actualizados correctamente", TipoAlerta.Exito);
                    }
                }
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al editar la ruta: " + ex.Message, TipoAlerta.Error);
            }
        }


        private void Tarjeta_Eliminar(object sender, EventArgs e)
        {
            var ruta = (E_Ruta)((TarjetaItem)sender).Dato;

            if (!Mensajes.ConfirmarEliminar("Eliminar ruta", $"¿Seguro que desea eliminar la ruta {ruta.Origen} → {ruta.Destino}?", this))
                return;

            string resultado = objNegocio.EliminarRuta(ruta.IdRuta);
            CargarRutas();

            if (resultado.ToUpper() == "OK")
                alerta.Mostrar("Ruta eliminada correctamente", TipoAlerta.Exito);
            else
                alerta.Mostrar(resultado, TipoAlerta.Advertencia);
        }

    }//fin de clase
}//fin de namespace
