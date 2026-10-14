using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using CapaEntidades;
using CapaNegocios;

namespace CamionesWeb
{
    public partial class ChoferesWeb : System.Web.UI.Page
    {
        private N_Chofer objNegocio = new N_Chofer();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                CargarChoferes();
                InicializarControles();

            }

        }


        private void InicializarControles()
        {

            //esto le inyecta la clase directamente al elemento input generado
            chkDisponible.InputAttributes.Add("class", "form-check-input");

            //el calendario no deja elegir una fecha de alguien menor de 18 años
            txtFechaNacimiento.Attributes["max"] = DateTime.Today.AddYears(-18).ToString("yyyy-MM-dd");


        }


        //devuelve cuantos choferes se mostraron (-1 si hubo error) para que los botones den un mensaje
        private int CargarChoferes()
        {
            try
            {

                bool? disponibilidad = null;
                if (ddFiltro.SelectedValue == "1")
                    disponibilidad = true;
                else if (ddFiltro.SelectedValue == "2")
                    disponibilidad = false;

                //obtener lista de choferes
                List<E_Chofer> lista = objNegocio.ListarChoferes(disponibilidad);

                //Asignar GridView
                gvChoferes.DataSource = lista;
                gvChoferes.DataBind();

                if (lista == null || lista.Count == 0)
                {
                    MostrarMensaje("No se encontraron choferes con el filtro seleccionado", "alert-info");

                }
                else
                {
                    pnlMensaje.Visible = false;
                }

                return lista == null ? 0 : lista.Count;

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar choferes:" + ex.Message, "alert-danger");
                return -1;
            }

        }


        //tambien se ejecuta al cambiar el DropDownList (AutoPostBack en el .aspx)
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            int total = CargarChoferes();
            if (total > 0)
                MostrarMensaje($"Filtro aplicado: {total} chofer(es) — {ddFiltro.SelectedItem.Text}", "alert-info");
        }


        protected void btnAgregar_Click(Object sender, EventArgs e)
        {

            hfIdChofer.Value = "0";
            limpiarFormulario();
            string script = @"mostrarModalFormulario(true)";
            ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "MostrarModalAgregar", script, true);

        }

        protected E_Chofer generarChofer()
        {

            return new E_Chofer
            {
                IdChofer = Convert.ToInt32(hfIdChofer.Value),
                Nombre = txtNombre.Text,
                ApPaterno = txtApPaterno.Text,
                ApMaterno = txtApMaterno.Text,
                Telefono = txtTelefono.Text,
                FechaNacimiento = Convert.ToDateTime(txtFechaNacimiento.Text),
                Licencia = txtLicencia.Text,
                Disponibilidad = chkDisponible.Checked,
                UrlFoto = txtUrlFoto.Text
            };

        }


        protected void limpiarFormulario()
        {

            txtNombre.Text = string.Empty;
            txtApPaterno.Text = string.Empty;
            txtApMaterno.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtLicencia.Text = string.Empty;
            chkDisponible.Checked = false;
            txtUrlFoto.Text = string.Empty;

        }

        protected void llenarFormulario(E_Chofer chofer)
        {
            txtNombre.Text = chofer.Nombre;
            txtApPaterno.Text = chofer.ApPaterno;
            txtApMaterno.Text = chofer.ApMaterno;
            txtTelefono.Text = chofer.Telefono;
            //el TextBox con TextMode="Date" solo acepta el formato yyyy-MM-dd
            txtFechaNacimiento.Text = chofer.FechaNacimiento.ToString("yyyy-MM-dd");
            txtLicencia.Text = chofer.Licencia;
            chkDisponible.Checked = chofer.Disponibilidad;
            txtUrlFoto.Text = chofer.UrlFoto.ToString();

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //mostrar el modal usando funciones de js
                string script = @"mostrarModalFormulario(true)";
                ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "MostrarModalEditar", script, true);

                //obtener el ID del chofer que se esta editando
                LinkButton btnEditar = (LinkButton)sender;
                hfIdChofer.Value = btnEditar.CommandArgument;
                int idChoferEditado = Convert.ToInt32(hfIdChofer.Value);

                //cargar los datos del chofer seleccionado
                E_Chofer chofer = objNegocio.ObtenerChoferPorID(idChoferEditado);
                llenarFormulario(chofer);

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al editar el chofer" + ex.Message, "alert-danger");
            }



        }



        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ddFiltro.SelectedIndex = 0;
            int total = CargarChoferes();
            if (total > 0)
                MostrarMensaje($"Lista actualizada: {total} chofer(es) a las {DateTime.Now:HH:mm:ss}", "alert-success");
        }


        protected void btnGuardar_Click(Object sender, EventArgs e)
        {

            try
            {

                if (!Page.IsValid)
                {
                    return;
                }

                E_Chofer entidadChofer = generarChofer();
                string resultado;
                if (entidadChofer.IdChofer == 0)
                {
                    resultado = objNegocio.InsertarChofer(entidadChofer);
                }
                else
                {
                    resultado = objNegocio.ActualizarChofer(entidadChofer);
                }
                if (resultado.ToUpper() != "OK")
                {
                    throw new Exception(resultado);
                }

                CargarChoferes();
                MostrarMensaje("Datos del chofer actualizados correctamente", "alert-success");

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar el chofer" + ex.Message, "alert-danger");


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

            //obtener el ID del chofer que se desea eliminar
            LinkButton btnEliminar = (LinkButton)sender;
            hfIdChofer.Value = btnEliminar.CommandArgument;


        }


        protected void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idChoferPorEliminar = Convert.ToInt32(hfIdChofer.Value);
                string resultado = objNegocio.EliminarChofer(idChoferPorEliminar);
                CargarChoferes();
                if (resultado.ToUpper() == "OK") MostrarMensaje("Chofer eliminado correctamente", "alert-success");
                else MostrarMensaje($"Error: integridad referencial- {resultado}", "alert-warning");

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar el chofer" + ex.Message, "alert-danger");


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
