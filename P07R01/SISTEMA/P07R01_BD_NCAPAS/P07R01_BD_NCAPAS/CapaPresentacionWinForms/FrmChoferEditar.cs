using System;
using System.Linq;
using System.Windows.Forms;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;

namespace CapaPresentacionWinForms
{
    //ventana para agregar (sin chofer) o editar (con chofer) un chofer; igual que el modal "Datos del chofer"
    public partial class FrmChoferEditar : DialogoBase
    {
        private const int EdadMinima = 18;

        private N_Chofer objNegocio = new N_Chofer();
        private int idChofer = 0;   //0 = nuevo; otro valor = se esta editando

        public FrmChoferEditar() : this(null)
        {
        }

        public FrmChoferEditar(E_Chofer chofer)
        {
            InitializeComponent();

            //el calendario no deja elegir una fecha de alguien menor de 18 años
            dtpFechaNacimiento.MaxDate = DateTime.Today.AddYears(-EdadMinima);
            dtpFechaNacimiento.Value = dtpFechaNacimiento.MaxDate;

            if (chofer != null)
            {
                idChofer = chofer.IdChofer;
                LlenarFormulario(chofer);
            }
        }


        private void LlenarFormulario(E_Chofer chofer)
        {
            txtNombre.Text = chofer.Nombre;
            txtApPaterno.Text = chofer.ApPaterno;
            txtApMaterno.Text = chofer.ApMaterno;
            txtTelefono.Text = chofer.Telefono;
            if (chofer.FechaNacimiento >= dtpFechaNacimiento.MinDate && chofer.FechaNacimiento <= dtpFechaNacimiento.MaxDate)
                dtpFechaNacimiento.Value = chofer.FechaNacimiento;
            txtLicencia.Text = chofer.Licencia;
            chkDisponible.Checked = chofer.Disponibilidad;
            txtUrlFoto.Text = chofer.UrlFoto;
        }


        private E_Chofer GenerarChofer()
        {
            return new E_Chofer
            {
                IdChofer = idChofer,
                Nombre = txtNombre.Text.Trim(),
                ApPaterno = txtApPaterno.Text.Trim(),
                ApMaterno = txtApMaterno.Text.Trim(),
                Telefono = txtTelefono.Text.Trim(),
                FechaNacimiento = dtpFechaNacimiento.Value.Date,
                Licencia = txtLicencia.Text.Trim(),
                Disponibilidad = chkDisponible.Checked,
                UrlFoto = txtUrlFoto.Text.Trim()
            };
        }


        //revisa los campos obligatorios y escribe el error junto a la etiqueta (como los validadores de la web)
        private bool ValidarFormulario()
        {
            cjNombre.Error = string.IsNullOrWhiteSpace(txtNombre.Text) ? "El nombre es obligatorio" : "";
            cjApPaterno.Error = string.IsNullOrWhiteSpace(txtApPaterno.Text) ? "El apellido paterno es obligatorio" : "";
            cjApMaterno.Error = string.IsNullOrWhiteSpace(txtApMaterno.Text) ? "El apellido materno es obligatorio" : "";
            cjLicencia.Error = string.IsNullOrWhiteSpace(txtLicencia.Text) ? "La licencia es obligatoria" : "";
            cjUrlFoto.Error = string.IsNullOrWhiteSpace(txtUrlFoto.Text) ? "La URL de la foto es obligatoria" : "";

            //la capa de negocios exige exactamente 10 digitos
            string telefono = txtTelefono.Text.Trim();
            cjTelefono.Error = telefono.Length != 10 || !telefono.All(char.IsDigit) ? "El teléfono debe tener 10 dígitos" : "";

            return new[] { cjNombre, cjApPaterno, cjApMaterno, cjTelefono, cjLicencia, cjUrlFoto }.All(c => c.Error == "");
        }


        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario())
                return;

            try
            {
                E_Chofer chofer = GenerarChofer();
                string resultado = chofer.IdChofer == 0
                    ? objNegocio.InsertarChofer(chofer)
                    : objNegocio.ActualizarChofer(chofer);

                if (resultado.ToUpper() == "OK")
                    DialogResult = DialogResult.OK;   //cierra la ventana y avisa a FrmChoferes que recargue
                else
                    alerta.Mostrar(resultado, TipoAlerta.Advertencia);   //la ventana sigue abierta para corregir
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al guardar el chofer: " + ex.Message, TipoAlerta.Error);
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }


        //el telefono solo acepta numeros
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }

    }//fin de clase
}//fin de namespace
