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
    public partial class Camiones : System.Web.UI.Page
    {
        private N_Camion objNegocio = new N_Camion();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack) 
            
            {
                CargarCamiones();
                InicializarControles();
            
            }

        }//finde la pagina


        private void InicializarControles()
        {

            //esto le inyecta la clase directamente al elemento input generado
            chkDisponible.InputAttributes.Add("class", "form-check-input");

            rvModelo.MaximumValue = (DateTime.Now.Year + 1).ToString();
            rvModelo.ErrorMessage = $"El modelo debe estar entre {rvModelo.MinimumValue} y {rvModelo.MaximumValue}";


        }


        //devuelve cuantos camiones se mostraron (-1 si hubo error) para que los botones den un mensaje
        private int CargarCamiones()
        {
            try
            {

                bool? disponibilidad = null;
                if (ddFiltro.SelectedValue == "1")
                    disponibilidad = true;
                else if (ddFiltro.SelectedValue == "2")
                    disponibilidad = false;

                //obtener lista de camiones
                List<E_Camion> lista = objNegocio.ListarCamiones(disponibilidad);

                //Asignar GridView
                gvCamiones.DataSource = lista;
                gvCamiones.DataBind();

                if(lista == null || lista.Count == 0)
                {
                    MostrarMensaje("No se encontraron camiones con el filtro seleccionado", "alert-info");
                
                }else
                {
                    pnlMensaje.Visible = false;
                }

                return lista == null ? 0 : lista.Count;

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar camiones:" + ex.Message, "alert-danger");
                return -1;
            }

        }


        //tambien se ejecuta al cambiar el DropDownList (AutoPostBack en el .aspx)
        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            int total = CargarCamiones();
            if (total > 0)
                MostrarMensaje($"Filtro aplicado: {total} camión(es) — {ddFiltro.SelectedItem.Text}", "alert-info");
        }


        protected void btnAgregar_Click(Object sender, EventArgs e)
        {

            hfIdCamion.Value = "0";
            limpiarFormulario();
            string script = @"mostrarModalFormulario(true)";
            ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(), "MostrarModalAgregar", script, true);

        }

        protected E_Camion generarCamion() 
        {

            return new E_Camion
            {
                IdCamion = Convert.ToInt32(hfIdCamion.Value),
                Matricula = txtMatricula.Text,
                TipoCamion = ddlTipoCamion.SelectedValue,
                Modelo = Convert.ToInt32(txtModelo.Text),
                Marca = txtMarca.Text,
                Capacidad = Convert.ToInt32(txtCapacidad.Text),
                Kilometraje = Convert.ToDouble(txtKilometraje.Text),
                Disponibilidad = chkDisponible.Checked,
                UrlFoto = txtUrlFoto.Text
            };

        }


        protected void limpiarFormulario() 
        {
            
            txtMatricula.Text = string.Empty;
            ddlTipoCamion.SelectedValue = string.Empty;
            txtModelo.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtCapacidad.Text = string.Empty;
            txtKilometraje.Text = string.Empty;
            chkDisponible.Checked = false;
            txtUrlFoto.Text = string.Empty;
 
        }

        protected void llenarFormulario(E_Camion camion)
        {
            txtMatricula.Text = camion.Matricula;
            //si el tipo guardado en la BD no esta en la lista, se agrega para que SelectedValue no falle
            if (ddlTipoCamion.Items.FindByValue(camion.TipoCamion) == null)
                ddlTipoCamion.Items.Add(camion.TipoCamion);
            ddlTipoCamion.SelectedValue = camion.TipoCamion;
            txtModelo.Text = camion.Modelo.ToString();
            txtMarca.Text = camion.Marca.ToString();
            txtCapacidad.Text = camion.Capacidad.ToString();
            txtKilometraje.Text = camion.Kilometraje.ToString();
            chkDisponible.Checked = camion.Disponibilidad;
            txtUrlFoto.Text = camion.UrlFoto.ToString();

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                //mostrar el modal usando funciones de js
                string script = @"mostrarModalFormulario(true)";
                ScriptManager.RegisterStartupScript((Control) sender, sender.GetType(), "MostrarModalEditar", script, true);

                //obtener el ID del camion que se esta editando
                LinkButton btnEditar = (LinkButton)sender;
                hfIdCamion.Value = btnEditar.CommandArgument;
                int idCamionEditado = Convert.ToInt32(hfIdCamion.Value);

                //cargar los datos del camion seleccionado
                E_Camion camion = objNegocio.ObtenerCamionPorID(idCamionEditado);
                llenarFormulario(camion);

            }
            catch(Exception ex) 
            {
                MostrarMensaje("Error al editar el camion"+ex.Message, "alert-danger");
            }



        }



        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            ddFiltro.SelectedIndex = 0;
            int total = CargarCamiones();
            if (total > 0)
                MostrarMensaje($"Lista actualizada: {total} camión(es) a las {DateTime.Now:HH:mm:ss}", "alert-success");
        }


        protected void btnGuardar_Click(Object sender, EventArgs e)
        {

            try
            {

                if (!Page.IsValid)
                {
                    return;
                }

                E_Camion entidadCamion = generarCamion();
                string resultado;
                if (entidadCamion.IdCamion == 0)
                {
                    resultado = objNegocio.InsertarCamion(entidadCamion);
                }
                else
                {
                    resultado = objNegocio.ActualizarCamion(entidadCamion);
                }
                if (resultado.ToUpper() != "OK")
                {
                    throw new Exception(resultado);
                }

                CargarCamiones();
                MostrarMensaje("Datos del camion actualizados correctamente", "alert-success");

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar el camion" + ex.Message, "alert-danger");


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
            ScriptManager.RegisterStartupScript((Control)sender, sender.GetType(),"MostrarModalEliminar", script, true);

            //obtener el ID del camion que se desea eliminar
            LinkButton btnEliminar = (LinkButton)sender;
            hfIdCamion.Value = btnEliminar.CommandArgument;


        }


        protected  void btnConfirmarEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idCamionPorEliminar = Convert.ToInt32(hfIdCamion.Value);
                string resultado = objNegocio.EliminarCamion(idCamionPorEliminar);
                CargarCamiones();
                if (resultado.ToUpper() == "OK") MostrarMensaje("Camion eliminado correctamente", "alert-success");
                else MostrarMensaje($"Error: integridad referencial- {resultado}", "alert-warning");

            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar el camion" + ex.Message, "alert-danger");


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
    }//fin de clase
}