using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaNegocios;
using CapaEntidades;


namespace CamionesWeb
{
    public partial class InicioWeb : System.Web.UI.Page
    {

        private N_Ruta objNegocio = new N_Ruta();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDashboard();
            }
        }


        //se usa desde Page_Load (primera vez) y desde el boton Refrescar
        private void CargarDashboard()
        {
            try
            {
                //1. pedir los datos a la capa de negocios
                List<E_Camion> camiones = new N_Camion().ListarCamiones();
                List<E_Chofer> choferes = new N_Chofer().ListarChoferes();
                List<E_Ruta> rutas = new N_Ruta().ListarRutas();

                //2. calcular con LINQ
                int camionesDisp = camiones.Count(c => c.Disponibilidad);
                int choferesDisp = choferes.Count(c => c.Disponibilidad);
                int rutasATiempo = rutas.Count(r => r.ATiempo);
                double kmTotales = rutas.Sum(r => r.Distancia);
                double kmPromedio = rutas.Count > 0 ? rutas.Average(r => r.Distancia) : 0;

                //3. mostrar en los controles
                lblTotalCamiones.Text = camiones.Count.ToString();
                lblCamionesDisp.Text = camionesDisp.ToString();

                lblTotalChoferes.Text = choferes.Count.ToString();
                lblChoferesDisp.Text = choferesDisp.ToString();

                lblTotalRutas.Text = rutas.Count.ToString();
                lblRutasATiempo.Text = rutasATiempo.ToString();

                lblKmTotales.Text = kmTotales.ToString("N0");
                lblKmPromedio.Text = kmPromedio.ToString("N1");

                //barras de porcentaje
                MostrarPorcentaje(camionesDisp, camiones.Count, lblPctCamiones, barCamiones);
                MostrarPorcentaje(choferesDisp, choferes.Count, lblPctChoferes, barChoferes);
                MostrarPorcentaje(rutasATiempo, rutas.Count, lblPctATiempo, barATiempo);

                //ultimas 5 rutas (las de fecha de salida mas reciente)
                List<E_Ruta> ultimas = rutas.OrderByDescending(r => r.FechaSalida).Take(5).ToList();
                rptUltimasRutas.DataSource = ultimas;
                rptUltimasRutas.DataBind();
                lblSinRutas.Visible = ultimas.Count == 0;

                lblActualizado.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                pnlMensaje.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar el resumen: " + ex.Message, "alert-danger");
            }
        }


        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
            pnlMensaje.CssClass = $"alert {tipo}";
        }


        //calcula el porcentaje, lo escribe en el Label y ajusta el ancho de la barra
        private void MostrarPorcentaje(int parte, int total, Label lbl, Panel barra)
        {
            int porcentaje = total > 0 ? (int)Math.Round(parte * 100.0 / total) : 0;
            lbl.Text = porcentaje + "%";
            barra.Style["width"] = porcentaje + "%";
        }


        protected void btnRefrescar_Click(object sender, EventArgs e)
        {
            CargarDashboard();
            //si CargarDashboard no mostro un error, se confirma que se actualizo
            if (!pnlMensaje.Visible)
                MostrarMensaje($"Datos actualizados a las {DateTime.Now:HH:mm:ss}", "alert-success");
        }

    }
}
