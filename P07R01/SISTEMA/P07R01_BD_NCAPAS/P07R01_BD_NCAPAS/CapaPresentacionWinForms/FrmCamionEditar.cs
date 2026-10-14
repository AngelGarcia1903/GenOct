using System;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;

namespace CapaPresentacionWinForms
{
    //ventana para agregar (sin camion) o editar (con camion) un camion; igual que el modal "Datos del camión"
    public partial class FrmCamionEditar : DialogoBase
    {
        private N_Camion objNegocio = new N_Camion();
        private int idCamion = 0;   //0 = nuevo; otro valor = se esta editando

        public FrmCamionEditar() : this(null)
        {
        }

        public FrmCamionEditar(E_Camion camion)
        {
            InitializeComponent();

            //mismo rango que la capa de negocios: de 1900 al año siguiente
            nudModelo.Maximum = DateTime.Now.Year + 1;
            nudModelo.Value = DateTime.Now.Year;

            if (camion != null)
            {
                idCamion = camion.IdCamion;
                LlenarFormulario(camion);
            }
        }


        private void LlenarFormulario(E_Camion camion)
        {
            txtMatricula.Text = camion.Matricula;

            //si el tipo guardado en la BD no esta en la lista, se agrega para poder seleccionarlo
            if (!cboTipoCamion.Items.Contains(camion.TipoCamion))
                cboTipoCamion.Items.Add(camion.TipoCamion);
            cboTipoCamion.SelectedItem = camion.TipoCamion;

            nudModelo.Value = Math.Max(nudModelo.Minimum, Math.Min(nudModelo.Maximum, camion.Modelo));
            txtMarca.Text = camion.Marca;
            nudCapacidad.Value = Math.Max(nudCapacidad.Minimum, Math.Min(nudCapacidad.Maximum, camion.Capacidad));
            nudKilometraje.Value = Math.Min(nudKilometraje.Maximum, (decimal)camion.Kilometraje);
            chkDisponible.Checked = camion.Disponibilidad;
            txtUrlFoto.Text = camion.UrlFoto;
        }


        private E_Camion GenerarCamion()
        {
            return new E_Camion
            {
                IdCamion = idCamion,
                Matricula = txtMatricula.Text.Trim(),
                TipoCamion = cboTipoCamion.Text,
                Modelo = (int)nudModelo.Value,
                Marca = txtMarca.Text.Trim(),
                Capacidad = (int)nudCapacidad.Value,
                Kilometraje = (double)nudKilometraje.Value,
                Disponibilidad = chkDisponible.Checked,
                UrlFoto = txtUrlFoto.Text.Trim()
            };
        }


        //revisa los campos obligatorios y escribe el error junto a la etiqueta (como los validadores de la web)
        private bool ValidarFormulario()
        {
            cjMatricula.Error = string.IsNullOrWhiteSpace(txtMatricula.Text) ? "La matrícula es obligatoria" : "";
            cjTipoCamion.Error = cboTipoCamion.SelectedIndex < 0 ? "Selecciona el tipo de camión" : "";
            cjMarca.Error = string.IsNullOrWhiteSpace(txtMarca.Text) ? "La marca es obligatoria" : "";
            cjKilometraje.Error = nudKilometraje.Value <= 0 ? "El kilometraje debe ser mayor a 0" : "";
            cjUrlFoto.Error = string.IsNullOrWhiteSpace(txtUrlFoto.Text) ? "La URL de la foto es obligatoria" : "";

            return cjMatricula.Error == "" && cjTipoCamion.Error == "" && cjMarca.Error == ""
                && cjKilometraje.Error == "" && cjUrlFoto.Error == "";
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            try
            {
                E_Camion camion = GenerarCamion();
                string resultado = camion.IdCamion == 0
                    ? objNegocio.InsertarCamion(camion)
                    : objNegocio.ActualizarCamion(camion);

                if (resultado.ToUpper() == "OK")
                    DialogResult = DialogResult.OK;   //cierra la ventana y avisa a FrmCamiones que recargue
                else
                    alerta.Mostrar(resultado, TipoAlerta.Advertencia);   //la ventana sigue abierta para corregir
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al guardar el camión: " + ex.Message, TipoAlerta.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }//fin de clase
}//fin de namespace
