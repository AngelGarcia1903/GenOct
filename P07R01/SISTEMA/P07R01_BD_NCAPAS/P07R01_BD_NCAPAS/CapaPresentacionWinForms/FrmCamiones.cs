using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms
{
    //camiones en tarjetas con filtro y botones para agregar, editar y eliminar (igual que Camiones.aspx)
    public partial class FrmCamiones : FormularioSeccion
    {
        private N_Camion objNegocio = new N_Camion();
        private bool cargando = true;   //evita el mensaje de "filtro aplicado" al abrir

        public FrmCamiones()
        {
            InitializeComponent();
        }


        private void FrmCamiones_Load(object sender, EventArgs e)
        {
            cboFiltro.SelectedIndex = 0;   //"Todos"
            CargarCamiones();
            cargando = false;
        }


        //devuelve cuantos camiones se mostraron (-1 si hubo error) para que los botones den un mensaje
        private int CargarCamiones()
        {
            try
            {
                bool? disponibilidad = null;
                if (cboFiltro.SelectedIndex == 1)
                    disponibilidad = true;
                else if (cboFiltro.SelectedIndex == 2)
                    disponibilidad = false;

                List<E_Camion> lista = objNegocio.ListarCamiones(disponibilidad);
                lstCamiones.Mostrar(lista.Select(CrearTarjeta), Tarjeta_Editar, Tarjeta_Eliminar);

                if (lista.Count == 0)
                    alerta.Mostrar("No se encontraron camiones con el filtro seleccionado", TipoAlerta.Info);
                else
                    alerta.Ocultar();

                return lista.Count;
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al cargar camiones: " + ex.Message, TipoAlerta.Error);
                return -1;
            }
        }


        //una tarjeta por camion, con los mismos datos que la tarjeta de la web
        private static TarjetaItem CrearTarjeta(E_Camion c)
        {
            return new TarjetaItem
            {
                UrlImagen = c.UrlFoto,
                Insignia = c.Disponibilidad ? "Disponible" : "No disponible",
                InsigniaPositiva = c.Disponibilidad,
                Categoria = c.TipoCamion,
                Titulo = $"{c.Marca} {c.Modelo}",
                Descripcion = $"Matrícula: {c.Matricula}\nCapacidad: {c.Capacidad} · Kilometraje: {c.Kilometraje:N0} km",
                Numero = $"#{c.IdCamion}",
                Unidad = "ID",
                Dato = c
            };
        }


        //tambien se ejecuta al cambiar el filtro (como el AutoPostBack del DropDownList)
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            int total = CargarCamiones();
            if (total > 0)
                alerta.Mostrar($"Filtro aplicado: {total} camión(es) — {cboFiltro.Text}", TipoAlerta.Info);
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargando)
                btnFiltrar_Click(sender, e);
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargando = true;
            cboFiltro.SelectedIndex = 0;
            cargando = false;

            int total = CargarCamiones();
            if (total > 0)
                alerta.Mostrar($"Lista actualizada: {total} camión(es) a las {DateTime.Now:HH:mm:ss}", TipoAlerta.Exito);
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmCamionEditar())
            {
                if (Dialogos.MostrarModal(frm, this) == System.Windows.Forms.DialogResult.OK)
                {
                    CargarCamiones();
                    alerta.Mostrar("Camión agregado correctamente", TipoAlerta.Exito);
                }
            }
        }


        private void Tarjeta_Editar(object sender, EventArgs e)
        {
            var seleccionado = (E_Camion)((TarjetaItem)sender).Dato;

            try
            {
                //se vuelve a pedir a la BD para editar los datos actuales
                E_Camion camion = objNegocio.ObtenerCamionPorID(seleccionado.IdCamion);

                using (var frm = new FrmCamionEditar(camion))
                {
                    if (Dialogos.MostrarModal(frm, this) == System.Windows.Forms.DialogResult.OK)
                    {
                        CargarCamiones();
                        alerta.Mostrar("Datos del camión actualizados correctamente", TipoAlerta.Exito);
                    }
                }
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al editar el camión: " + ex.Message, TipoAlerta.Error);
            }
        }


        private void Tarjeta_Eliminar(object sender, EventArgs e)
        {
            var camion = (E_Camion)((TarjetaItem)sender).Dato;

            if (!Mensajes.ConfirmarEliminar("Eliminar camión", $"¿Seguro que desea eliminar el camión {camion.Descripcion}?", this))
                return;

            string resultado = objNegocio.EliminarCamion(camion.IdCamion);
            CargarCamiones();

            if (resultado.ToUpper() == "OK")
                alerta.Mostrar("Camión eliminado correctamente", TipoAlerta.Exito);
            else
                alerta.Mostrar(Mensajes.Traducir(resultado), TipoAlerta.Advertencia);
        }

    }//fin de clase
}//fin de namespace
