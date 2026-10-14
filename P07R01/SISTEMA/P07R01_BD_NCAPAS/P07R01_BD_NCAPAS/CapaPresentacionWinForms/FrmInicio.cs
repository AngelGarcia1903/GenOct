using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;

namespace CapaPresentacionWinForms
{
    //resumen general: totales, porcentajes y ultimas rutas (igual que InicioWeb)
    public partial class FrmInicio : FormularioSeccion
    {
        public FrmInicio()
        {
            InitializeComponent();
        }


        private void FrmInicio_Load(object sender, EventArgs e)
        {
            CargarResumen();
        }


        //se usa al abrir el formulario y desde el boton Refrescar
        private bool CargarResumen()
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
                tarjetaCamiones.Valor = camiones.Count.ToString();
                tarjetaCamiones.Detalle = $"{camionesDisp} disponibles";

                tarjetaChoferes.Valor = choferes.Count.ToString();
                tarjetaChoferes.Detalle = $"{choferesDisp} disponibles";

                tarjetaRutas.Valor = rutas.Count.ToString();
                tarjetaRutas.Detalle = $"{rutasATiempo} llegaron a tiempo";

                tarjetaKm.Valor = kmTotales.ToString("N0");
                tarjetaKm.Detalle = $"Promedio por ruta: {kmPromedio:N1} km";

                barCamiones.Porcentaje = Porcentaje(camionesDisp, camiones.Count);
                barChoferes.Porcentaje = Porcentaje(choferesDisp, choferes.Count);
                barATiempo.Porcentaje = Porcentaje(rutasATiempo, rutas.Count);

                //ultimas 5 rutas (las de fecha de salida mas reciente)
                lstUltimasRutas.Rutas = rutas.OrderByDescending(r => r.FechaSalida).Take(5).ToList();

                lblActualizado.Text = "Actualizado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                return true;
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al cargar el resumen: " + ex.Message, TipoAlerta.Error);
                return false;
            }
        }


        private static int Porcentaje(int parte, int total)
        {
            return total > 0 ? (int)Math.Round(parte * 100.0 / total) : 0;
        }


        private void btnRefrescar_Click(object sender, EventArgs e)
        {
            if (CargarResumen())
                alerta.Mostrar($"Datos actualizados a las {DateTime.Now:HH:mm:ss}", TipoAlerta.Exito);
        }


        //las tarjetas de camiones, choferes y rutas abren su seccion (como los enlaces de la web)
        private void tarjetaCamiones_Click(object sender, EventArgs e) => (TopLevelControl as FrmPrincipal)?.Navegar("Camiones");
        private void tarjetaChoferes_Click(object sender, EventArgs e) => (TopLevelControl as FrmPrincipal)?.Navegar("Choferes");
        private void tarjetaRutas_Click(object sender, EventArgs e) => (TopLevelControl as FrmPrincipal)?.Navegar("Rutas");

    }//fin de clase
}//fin de namespace
