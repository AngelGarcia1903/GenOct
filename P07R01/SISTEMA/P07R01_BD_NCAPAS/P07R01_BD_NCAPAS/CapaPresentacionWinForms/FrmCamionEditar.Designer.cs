namespace CapaPresentacionWinForms
{
    partial class FrmCamionEditar
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
            this.cjMatricula = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtMatricula = new System.Windows.Forms.TextBox();
            this.cjTipoCamion = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.cboTipoCamion = new System.Windows.Forms.ComboBox();
            this.cjMarca = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtMarca = new System.Windows.Forms.TextBox();
            this.cjModelo = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.nudModelo = new System.Windows.Forms.NumericUpDown();
            this.cjCapacidad = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.nudCapacidad = new System.Windows.Forms.NumericUpDown();
            this.cjKilometraje = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.nudKilometraje = new System.Windows.Forms.NumericUpDown();
            this.cjUrlFoto = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtUrlFoto = new System.Windows.Forms.TextBox();
            this.chkDisponible = new CapaPresentacionWinForms.Controles.CasillaVerificacion();
            this.alerta = new CapaPresentacionWinForms.Controles.Alerta();
            this.btnCancelar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.btnGuardar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.cjMatricula.SuspendLayout();
            this.cjTipoCamion.SuspendLayout();
            this.cjMarca.SuspendLayout();
            this.cjModelo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudModelo)).BeginInit();
            this.cjCapacidad.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacidad)).BeginInit();
            this.cjKilometraje.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudKilometraje)).BeginInit();
            this.cjUrlFoto.SuspendLayout();
            this.SuspendLayout();
            //
            // cjMatricula
            //
            this.cjMatricula.Controls.Add(this.txtMatricula);
            this.cjMatricula.Location = new System.Drawing.Point(24, 76);
            this.cjMatricula.Name = "cjMatricula";
            this.cjMatricula.Size = new System.Drawing.Size(452, 62);
            this.cjMatricula.TabIndex = 0;
            this.cjMatricula.Titulo = "Matrícula";
            //
            // txtMatricula
            //
            this.txtMatricula.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMatricula.MaxLength = 50;
            this.txtMatricula.Name = "txtMatricula";
            this.txtMatricula.Size = new System.Drawing.Size(420, 18);
            this.txtMatricula.TabIndex = 0;
            //
            // cjTipoCamion
            //
            this.cjTipoCamion.Controls.Add(this.cboTipoCamion);
            this.cjTipoCamion.Location = new System.Drawing.Point(24, 142);
            this.cjTipoCamion.Name = "cjTipoCamion";
            this.cjTipoCamion.Size = new System.Drawing.Size(452, 62);
            this.cjTipoCamion.TabIndex = 1;
            this.cjTipoCamion.Titulo = "Tipo de camión";
            //
            // cboTipoCamion
            //
            this.cboTipoCamion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoCamion.FormattingEnabled = true;
            this.cboTipoCamion.Items.AddRange(new object[] {
            "CAJA SECA",
            "TOLVA",
            "PIPA",
            "REFRIGERADO",
            "TORTON",
            "CAMIONETA"});
            this.cboTipoCamion.Name = "cboTipoCamion";
            this.cboTipoCamion.Size = new System.Drawing.Size(420, 25);
            this.cboTipoCamion.TabIndex = 0;
            //
            // cjMarca
            //
            this.cjMarca.Controls.Add(this.txtMarca);
            this.cjMarca.Location = new System.Drawing.Point(24, 208);
            this.cjMarca.Name = "cjMarca";
            this.cjMarca.Size = new System.Drawing.Size(452, 62);
            this.cjMarca.TabIndex = 2;
            this.cjMarca.Titulo = "Marca";
            //
            // txtMarca
            //
            this.txtMarca.MaxLength = 50;
            this.txtMarca.Name = "txtMarca";
            this.txtMarca.Size = new System.Drawing.Size(420, 18);
            this.txtMarca.TabIndex = 0;
            //
            // cjModelo
            //
            this.cjModelo.Controls.Add(this.nudModelo);
            this.cjModelo.Location = new System.Drawing.Point(24, 274);
            this.cjModelo.Name = "cjModelo";
            this.cjModelo.Size = new System.Drawing.Size(452, 62);
            this.cjModelo.TabIndex = 3;
            this.cjModelo.Titulo = "Modelo (año)";
            //
            // nudModelo
            //
            this.nudModelo.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.nudModelo.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.nudModelo.Name = "nudModelo";
            this.nudModelo.Size = new System.Drawing.Size(420, 21);
            this.nudModelo.TabIndex = 0;
            this.nudModelo.Value = new decimal(new int[] {
            2026,
            0,
            0,
            0});
            //
            // cjCapacidad
            //
            this.cjCapacidad.Controls.Add(this.nudCapacidad);
            this.cjCapacidad.Location = new System.Drawing.Point(24, 340);
            this.cjCapacidad.Name = "cjCapacidad";
            this.cjCapacidad.Size = new System.Drawing.Size(452, 62);
            this.cjCapacidad.TabIndex = 4;
            this.cjCapacidad.Titulo = "Capacidad";
            //
            // nudCapacidad
            //
            this.nudCapacidad.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudCapacidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCapacidad.Name = "nudCapacidad";
            this.nudCapacidad.Size = new System.Drawing.Size(420, 21);
            this.nudCapacidad.TabIndex = 0;
            this.nudCapacidad.ThousandsSeparator = true;
            this.nudCapacidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // cjKilometraje
            //
            this.cjKilometraje.Controls.Add(this.nudKilometraje);
            this.cjKilometraje.Location = new System.Drawing.Point(24, 406);
            this.cjKilometraje.Name = "cjKilometraje";
            this.cjKilometraje.Size = new System.Drawing.Size(452, 62);
            this.cjKilometraje.TabIndex = 5;
            this.cjKilometraje.Titulo = "Kilometraje";
            //
            // nudKilometraje
            //
            this.nudKilometraje.DecimalPlaces = 1;
            this.nudKilometraje.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudKilometraje.Name = "nudKilometraje";
            this.nudKilometraje.Size = new System.Drawing.Size(420, 21);
            this.nudKilometraje.TabIndex = 0;
            this.nudKilometraje.ThousandsSeparator = true;
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
            // FrmCamionEditar
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
            this.Controls.Add(this.cjKilometraje);
            this.Controls.Add(this.cjCapacidad);
            this.Controls.Add(this.cjModelo);
            this.Controls.Add(this.cjMarca);
            this.Controls.Add(this.cjTipoCamion);
            this.Controls.Add(this.cjMatricula);
            this.Name = "FrmCamionEditar";
            this.Text = "Datos del camión";
            this.Titulo = "Datos del camión";
            this.cjMatricula.ResumeLayout(false);
            this.cjMatricula.PerformLayout();
            this.cjTipoCamion.ResumeLayout(false);
            this.cjMarca.ResumeLayout(false);
            this.cjMarca.PerformLayout();
            this.cjModelo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudModelo)).EndInit();
            this.cjCapacidad.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudCapacidad)).EndInit();
            this.cjKilometraje.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudKilometraje)).EndInit();
            this.cjUrlFoto.ResumeLayout(false);
            this.cjUrlFoto.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CapaPresentacionWinForms.Controles.CajaRedonda cjMatricula;
        private System.Windows.Forms.TextBox txtMatricula;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjTipoCamion;
        private System.Windows.Forms.ComboBox cboTipoCamion;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjMarca;
        private System.Windows.Forms.TextBox txtMarca;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjModelo;
        private System.Windows.Forms.NumericUpDown nudModelo;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjCapacidad;
        private System.Windows.Forms.NumericUpDown nudCapacidad;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjKilometraje;
        private System.Windows.Forms.NumericUpDown nudKilometraje;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjUrlFoto;
        private System.Windows.Forms.TextBox txtUrlFoto;
        private CapaPresentacionWinForms.Controles.CasillaVerificacion chkDisponible;
        private CapaPresentacionWinForms.Controles.Alerta alerta;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnCancelar;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnGuardar;
    }
}
