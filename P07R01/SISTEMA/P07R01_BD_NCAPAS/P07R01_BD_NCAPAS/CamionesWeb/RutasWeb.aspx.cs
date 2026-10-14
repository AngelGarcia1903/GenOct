using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaEntidades;
using CapaNegocios;

namespace CamionesWeb
{
    public partial class RutasWeb : System.Web.UI.Page
    {
        private N_Ruta objNegocio = new N_Ruta();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarCombos();
                CargarRutas();
                InicializarControles();

            }

        }


        private void InicializarControles()
        {

            //esto le inyecta la clase directamente al elemento input generado
            chkATiempo.InputAttributes.Add("class", "form-check-input");


        }


        //llena los DropDownList de chofer y camion con datos de sus capas de negocio
        private void CargarCombos()
        {
            try
            {

                ddlChofer.DataSource = new N_Chofer().ListarChoferes();
                ddlChofer.DataTextField = "NombreCompleto";   //lo que ve el usuario
                ddlChofer.DataValueField = "IdChofer";        //lo que se guarda
                ddlChofer.DataBind();
                ddlChofer.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

                ddlCamion.DataSource = new N_Camion().ListarCamiones();
                ddlCamion.DataTextField = "Descripcion";
                ddlCamion.DataValueField = "IdCamion";
                ddlCamion.DataBind();
                ddlCamion.Items.Insert(0, new ListItem("-- Seleccione --", "0"));

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar choferes y camiones:" + ex.Message, "alert-danger");
            }

        }


        //devuelve cuantas rutas se mostraron (-1 si hubo error) para que los botones den un mensaje
        private int CargarRutas()
        {
            try
            {

                //obtener lista de rutas
                List<E_Ruta> lista = objNegocio.ListarRutas();

                //Asignar GridView
                gvRutas.DataSource = lista;
                gvRutas.DataBind();

                if (lista == null || lista.Count == 0)
                {
                    MostrarMensaje("No se encontraron rutas", "alert-info");

                }
                else
                {
                    pnlMensaje.Visible = false;
                }

                return lista == null ? 0 : lista.Count;

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar rutas:" + ex.Message, "alert-danger");
                return -1;
            }

        }


        protected void btnAgregar_Click(Object sender, EventArgs e)
        {

            hfIdRuta.Value = "0";
            limpiarFormulario();
            string script = @"mostrarModalFormulario(true)";
            ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "MostrarModalAgregar", script, true);

        }

        protected E_Ruta generarRuta()
        {

            return new E_Ruta
            {
                IdRuta = Convert.ToInt32(hfIdRuta.Value),
                IdChofer = Convert.ToInt32(ddlChofer.SelectedValue),
                IdCamion = Convert.ToInt32(ddlCamion.SelectedValue),
                Origen = txtOrigen.Text,
                Destino = txtDestino.Text,
                FechaSalida = Convert.ToDateTime(txtFechaSalida.Text),
                FechaLlegada = Convert.ToDateTime(txtFechaLlegada.Text),
                Distancia = Convert.ToDouble(txtDistancia.Text),
                ATiempo = chkATiempo.Checked
            };

        }


        protected void limpiarFormulario()
        {

            ddlChofer.SelectedValue = "0";
            ddlCamion.SelectedValue = "0";
            txtOrigen.Text = string.Empty;
            txtDestino.Text = string.Empty;
            txtFechaSalida.Text = string.Empty;
            txtFechaLlegada.Text = string.Empty;
            txtDistancia.Text = string.Empty;
            chkATiempo.Checked = false;

        }

        protected void llenarFormulario(E_Ruta ruta)
        {
            ddlChofer.SelectedValue = ruta.IdChofer.ToString();
            ddlCamion.SelectedValue = ruta.IdCamion.ToString();
            txtOrigen.Text = ruta.Origen;
            txtDestino.Text = ruta.Destino;
            //el TextBox con TextMode="DateTimeLocal" solo acepta el formato yyyy-MM-ddTHH:mm
            txtFechaSalida.Text = ruta.FechaSalida.ToString("yyyy-MM-ddTHH:mm");
            txtFechaLlegada.Text = ruta.FechaLlegada.ToString("yyyy-MM-ddTHH:mm");
            txtDistancia.Text = ruta.Distancia.ToString();
            chkATiempo.Checked = ruta.ATiempo;

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //mostrar el modal usando funciones de js
                string script = @"mostrarModalFormulario(true)";
                ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "MostrarModalEditar", script, true);

                //obtener el ID de la ruta que se esta editando
                LinkButton btnEditar = (LinkButton)sender;
                hfIdRuta.Value = btnEditar.CommandArgument;
                int idRutaEditada = Convert.ToInt32(hfIdRuta.Value);

                //cargar los datos de la ruta seleccionada
                E_Ruta ruta = objNegocio.ObtenerRutaPorID(idRutaEditada);
                llenarFormulario(ruta);

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al editar la ruta" + ex.Message, "alert-danger");
            }



        }



        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            //se limpian y recargan los combos por si se agregaron choferes o camiones nuevos
            ddlChofer.Items.Clear();
            ddlCamion.Items.Clear();
            CargarCombos();

            int total = CargarRutas();
            if (total > 0)
                MostrarMensaje($"Lista actualizada: {total} ruta(s) a las {DateTime.Now:HH:mm:ss}", "alert-success");
        }


        protected void btnGuardar_Click(Object sender, EventArgs e)
        {

            try
            {

                if (!Page.IsValid)
                {
                    return;
                }

                E_Ruta entidadRuta = generarRuta();
                string resultado;
                if (entidadRuta.IdRuta == 0)
                {
                    resultado = objNegocio.InsertarRuta(entidadRuta);
                }
                else
                {
                    resultado = objNegocio.ActualizarRuta(entidadRuta);
                }
                if (resultado.ToUpper() != "OK")
                {
                    throw new Exception(resultado);
                }

                CargarRutas();
                MostrarMensaje("Datos de la ruta actualizados correctamente", "alert-success");

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar la ruta" + ex.Message, "alert-danger");


            }
            finally
            {
                string script = @"mostrarModalFormulario(false)";
                ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "OcultarModalFormulario", script, true);

            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            pnlMensaje.Visible = false;
        }

        protected void btnEliminar_Click(Object sender, EventArgs e)
        {

            string script = @"mostrarModalEliminar(true)";
            ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "MostrarModalEliminar", script, true);

            //obtener el ID de la ruta que se desea eliminar
            LinkButton btnEliminar = (LinkButton)sender;
            hfIdRuta.Value = btnEliminar.CommandArgument;


        }


        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idRutaPorEliminar = Convert.ToInt32(hfIdRuta.Value);
                string resultado = objNegocio.EliminarRuta(idRutaPorEliminar);
                CargarRutas();
                if (resultado.ToUpper() == "OK") MostrarMensaje("Ruta eliminada correctamente", "alert-success");
                else MostrarMensaje($"Error: {resultado}", "alert-warning");

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar la ruta" + ex.Message, "alert-danger");


            }
            finally
            {
                string script = @"mostrarModalEliminar(false)";
                ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "OcultarModalEliminar", script, true);

            }

        }

        private void MostrarMensaje(string mensaje, string tipo)
        {

            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            pnlMensaje.CssClass = $"alert {tipo} alert-dismissible fade show";

        }


    }
}
