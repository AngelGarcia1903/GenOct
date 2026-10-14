using System;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;

namespace CapaPresentacionWinForms
{
    //ventana para agregar (sin ruta) o editar (con ruta) una ruta; igual que el modal "Datos de la ruta"
    public partial class FrmRutaEditar : DialogoBase
    {
        private N_Ruta objNegocio = new N_Ruta();
        private E_Ruta rutaEditada;   //null = nueva ruta

        public FrmRutaEditar() : this(null)
        {
        }

        public FrmRutaEditar(E_Ruta ruta)
        {
            InitializeComponent();
            rutaEditada = ruta;

            DateTime ahora = DateTime.Now;
            dtpFechaSalida.Value = ahora;
            dtpFechaLlegada.Value = ahora.AddHours(2);
        }


        //los combos se llenan en Load: SelectedValue solo funciona cuando el ComboBox ya esta enlazado
        private void FrmRutaEditar_Load(object sender, EventArgs e)
        {
            if (!CargarCombos())
                return;

            if (rutaEditada != null)
                LlenarFormulario(rutaEditada);
        }


        //llena los ComboBox de chofer y camion con datos de sus capas de negocio
        private bool CargarCombos()
        {
            try
            {
                cboChofer.DataSource = new N_Chofer().ListarChoferes();
                cboChofer.DisplayMember = "NombreCompleto";   //lo que ve el usuario
                cboChofer.ValueMember = "IdChofer";           //lo que se guarda
                cboChofer.SelectedIndex = -1;

                cboCamion.DataSource = new N_Camion().ListarCamiones();
                cboCamion.DisplayMember = "Descripcion";
                cboCamion.ValueMember = "IdCamion";
                cboCamion.SelectedIndex = -1;
                return true;
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al cargar choferes y camiones: " + ex.Message, TipoAlerta.Error);
                btnGuardar.Enabled = false;
                return false;
            }
        }


        private void LlenarFormulario(E_Ruta ruta)
        {
            cboChofer.SelectedValue = ruta.IdChofer;
            cboCamion.SelectedValue = ruta.IdCamion;
            txtOrigen.Text = ruta.Origen;
            txtDestino.Text = ruta.Destino;
            dtpFechaSalida.Value = ruta.FechaSalida;
            dtpFechaLlegada.Value = ruta.FechaLlegada;
            nudDistancia.Value = Math.Max(nudDistancia.Minimum, Math.Min(nudDistancia.Maximum, (decimal)ruta.Distancia));
            chkATiempo.Checked = ruta.ATiempo;
        }


        private E_Ruta GenerarRuta()
        {
            return new E_Ruta
            {
                IdRuta = rutaEditada?.IdRuta ?? 0,
                IdChofer = (int)cboChofer.SelectedValue,
                IdCamion = (int)cboCamion.SelectedValue,
                Origen = txtOrigen.Text.Trim(),
                Destino = txtDestino.Text.Trim(),
                FechaSalida = dtpFechaSalida.Value,
                FechaLlegada = dtpFechaLlegada.Value,
                Distancia = (double)nudDistancia.Value,
                ATiempo = chkATiempo.Checked
            };
        }


        //revisa los campos obligatorios y escribe el error junto a la etiqueta (como los validadores de la web)
        private bool ValidarFormulario()
        {
            cjChofer.Error = cboChofer.SelectedIndex < 0 ? "Selecciona un chofer" : "";
            cjCamion.Error = cboCamion.SelectedIndex < 0 ? "Selecciona un camión" : "";
            cjOrigen.Error = string.IsNullOrWhiteSpace(txtOrigen.Text) ? "El origen es obligatorio" : "";
            cjDestino.Error = string.IsNullOrWhiteSpace(txtDestino.Text) ? "El destino es obligatorio" : "";
            cjFechaLlegada.Error = dtpFechaLlegada.Value < dtpFechaSalida.Value ? "No puede ser antes de la salida" : "";

            return cjChofer.Error == "" && cjCamion.Error == "" && cjOrigen.Error == ""
                && cjDestino.Error == "" && cjFechaLlegada.Error == "";
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            try
            {
                E_Ruta ruta = GenerarRuta();
                string resultado = ruta.IdRuta == 0
                    ? objNegocio.InsertarRuta(ruta)
                    : objNegocio.ActualizarRuta(ruta);

                if (resultado.ToUpper() == "OK")
                    DialogResult = DialogResult.OK;   //cierra la ventana y avisa a FrmRutas que recargue
                else
                    alerta.Mostrar(resultado, TipoAlerta.Advertencia);   //la ventana sigue abierta para corregir
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al guardar la ruta: " + ex.Message, TipoAlerta.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }//fin de clase
}//fin de namespace
