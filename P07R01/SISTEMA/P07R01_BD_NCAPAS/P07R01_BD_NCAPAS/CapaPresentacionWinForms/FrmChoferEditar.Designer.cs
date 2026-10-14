namespace CapaPresentacionWinForms
{
    partial class FrmChoferEditar
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.cjNombre = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cjApPaterno = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtApPaterno = new System.Windows.Forms.TextBox();
            this.cjApMaterno = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtApMaterno = new System.Windows.Forms.TextBox();
            this.cjTelefono = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.cjFechaNacimiento = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.dtpFechaNacimiento = new System.Windows.Forms.DateTimePicker();
            this.cjLicencia = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtLicencia = new System.Windows.Forms.TextBox();
            this.cjUrlFoto = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtUrlFoto = new System.Windows.Forms.TextBox();
            this.chkDisponible = new CapaPresentacionWinForms.Controles.CasillaVerificacion();
            this.alerta = new CapaPresentacionWinForms.Controles.Alerta();
            this.btnCancelar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.btnGuardar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.cjNombre.SuspendLayout();
            this.cjApPaterno.SuspendLayout();
            this.cjApMaterno.SuspendLayout();
            this.cjTelefono.SuspendLayout();
            this.cjFechaNacimiento.SuspendLayout();
            this.cjLicencia.SuspendLayout();
            this.cjUrlFoto.SuspendLayout();
            this.SuspendLayout();
            //
            // cjNombre
            //
            this.cjNombre.Controls.Add(this.txtNombre);
            this.cjNombre.Location = new System.Drawing.Point(24, 76);
            this.cjNombre.Name = "cjNombre";
            this.cjNombre.Size = new System.Drawing.Size(452, 62);
            this.cjNombre.TabIndex = 0;
            this.cjNombre.Titulo = "Nombre";
            //
            // txtNombre
            //
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(420, 18);
            this.txtNombre.TabIndex = 0;
            //
            // cjApPaterno
            //
            this.cjApPaterno.Controls.Add(this.txtApPaterno);
            this.cjApPaterno.Location = new System.Drawing.Point(24, 142);
            this.cjApPaterno.Name = "cjApPaterno";
            this.cjApPaterno.Size = new System.Drawing.Size(452, 62);
            this.cjApPaterno.TabIndex = 1;
            this.cjApPaterno.Titulo = "Apellido paterno";
            //
            // txtApPaterno
            //
            this.txtApPaterno.MaxLength = 100;
            this.txtApPaterno.Name = "txtApPaterno";
            this.txtApPaterno.Size = new System.Drawing.Size(420, 18);
            this.txtApPaterno.TabIndex = 0;
            //
            // cjApMaterno
            //
            this.cjApMaterno.Controls.Add(this.txtApMaterno);
            this.cjApMaterno.Location = new System.Drawing.Point(24, 208);
            this.cjApMaterno.Name = "cjApMaterno";
            this.cjApMaterno.Size = new System.Drawing.Size(452, 62);
            this.cjApMaterno.TabIndex = 2;
            this.cjApMaterno.Titulo = "Apellido materno";
            //
            // txtApMaterno
            //
            this.txtApMaterno.MaxLength = 100;
            this.txtApMaterno.Name = "txtApMaterno";
            this.txtApMaterno.Size = new System.Drawing.Size(420, 18);
            this.txtApMaterno.TabIndex = 0;
            //
            // cjTelefono
            //
            this.cjTelefono.Controls.Add(this.txtTelefono);
            this.cjTelefono.Location = new System.Drawing.Point(24, 274);
            this.cjTelefono.Name = "cjTelefono";
            this.cjTelefono.Size = new System.Drawing.Size(452, 62);
            this.cjTelefono.TabIndex = 3;
            this.cjTelefono.Titulo = "Teléfono";
            //
            // txtTelefono
            //
            this.txtTelefono.MaxLength = 10;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(420, 18);
            this.txtTelefono.TabIndex = 0;
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            //
            // cjFechaNacimiento
            //
            this.cjFechaNacimiento.Controls.Add(this.dtpFechaNacimiento);
            this.cjFechaNacimiento.Location = new System.Drawing.Point(24, 340);
            this.cjFechaNacimiento.Name = "cjFechaNacimiento";
            this.cjFechaNacimiento.Size = new System.Drawing.Size(452, 62);
            this.cjFechaNacimiento.TabIndex = 4;
            this.cjFechaNacimiento.Titulo = "Fecha de nacimiento";
            //
            // dtpFechaNacimiento
            //
            this.dtpFechaNacimiento.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFechaNacimiento.MinDate = new System.DateTime(1930, 1, 1, 0, 0, 0, 0);
            this.dtpFechaNacimiento.Name = "dtpFechaNacimiento";
            this.dtpFechaNacimiento.Size = new System.Drawing.Size(420, 25);
            this.dtpFechaNacimiento.TabIndex = 0;
            //
            // cjLicencia
            //
            this.cjLicencia.Controls.Add(this.txtLicencia);
            this.cjLicencia.Location = new System.Drawing.Point(24, 406);
            this.cjLicencia.Name = "cjLicencia";
            this.cjLicencia.Size = new System.Drawing.Size(452, 62);
            this.cjLicencia.TabIndex = 5;
            this.cjLicencia.Titulo = "Licencia";
            //
            // txtLicencia
            //
            this.txtLicencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLicencia.MaxLength = 50;
            this.txtLicencia.Name = "txtLicencia";
            this.txtLicencia.Size = new System.Drawing.Size(420, 18);
            this.txtLicencia.TabIndex = 0;
            //
            // cjUrlFoto
            //
            this.cjUrlFoto.Controls.Add(this.txtUrlFoto);
            this.cjUrlFoto.Location = new System.Drawing.Point(24, 472);
            this.cjUrlFoto.Name = "cjUrlFoto";
            this.cjUrlFoto.Size = new System.Drawing.Size(452, 62);
            this.cjUrlFoto.TabIndex = 6;
            this.cjUrlFoto.Titulo = "URL de la foto";
            //
            // txtUrlFoto
            //
            this.txtUrlFoto.MaxLength = 255;
            this.txtUrlFoto.Name = "txtUrlFoto";
            this.txtUrlFoto.Size = new System.Drawing.Size(420, 18);
            this.txtUrlFoto.TabIndex = 0;
            //
            // chkDisponible
            //
            this.chkDisponible.Checked = true;
            this.chkDisponible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisponible.Location = new System.Drawing.Point(28, 542);
            this.chkDisponible.Name = "chkDisponible";
            this.chkDisponible.Size = new System.Drawing.Size(200, 26);
            this.chkDisponible.TabIndex = 7;
            this.chkDisponible.Text = "Disponible";
            //
            // alerta
            //
            this.alerta.Centrado = false;
            this.alerta.Location = new System.Drawing.Point(10, 590);
            this.alerta.Name = "alerta";
            this.alerta.Size = new System.Drawing.Size(232, 64);
            this.alerta.TabIndex = 10;
            //
            // btnCancelar
            //
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Estilo = CapaPresentacionWinForms.Controles.EstiloBoton.Secundario;
            this.btnCancelar.Location = new System.Drawing.Point(246, 597);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Pastilla = false;
            this.btnCancelar.Size = new System.Drawing.Size(112, 46);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            //
            // btnGuardar
            //
            this.btnGuardar.Location = new System.Drawing.Point(364, 597);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Pastilla = false;
            this.btnGuardar.Size = new System.Drawing.Size(112, 46);
            this.btnGuardar.TabIndex = 8;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            //
            // FrmChoferEditar
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(500, 656);
            this.Controls.Add(this.alerta);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.chkDisponible);
            this.Controls.Add(this.cjUrlFoto);
            this.Controls.Add(this.cjLicencia);
            this.Controls.Add(this.cjFechaNacimiento);
            this.Controls.Add(this.cjTelefono);
            this.Controls.Add(this.cjApMaterno);
            this.Controls.Add(this.cjApPaterno);
            this.Controls.Add(this.cjNombre);
            this.Name = "FrmChoferEditar";
            this.Text = "Datos del chofer";
            this.Titulo = "Datos del chofer";
            this.cjNombre.ResumeLayout(false);
            this.cjNombre.PerformLayout();
            this.cjApPaterno.ResumeLayout(false);
            this.cjApPaterno.PerformLayout();
            this.cjApMaterno.ResumeLayout(false);
            this.cjApMaterno.PerformLayout();
            this.cjTelefono.ResumeLayout(false);
            this.cjTelefono.PerformLayout();
            this.cjFechaNacimiento.ResumeLayout(false);
            this.cjLicencia.ResumeLayout(false);
            this.cjLicencia.PerformLayout();
            this.cjUrlFoto.ResumeLayout(false);
            this.cjUrlFoto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CapaPresentacionWinForms.Controles.CajaRedonda cjNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjApPaterno;
        private System.Windows.Forms.TextBox txtApPaterno;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjApMaterno;
        private System.Windows.Forms.TextBox txtApMaterno;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjTelefono;
        private System.Windows.Forms.TextBox txtTelefono;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjFechaNacimiento;
        private System.Windows.Forms.DateTimePicker dtpFechaNacimiento;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjLicencia;
        private System.Windows.Forms.TextBox txtLicencia;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjUrlFoto;
        private System.Windows.Forms.TextBox txtUrlFoto;
        private CapaPresentacionWinForms.Controles.CasillaVerificacion chkDisponible;
        private CapaPresentacionWinForms.Controles.Alerta alerta;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnCancelar;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnGuardar;
    }
}
