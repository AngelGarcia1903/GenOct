namespace CapaPresentacionWinForms
{
    partial class FrmRutaEditar
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
            this.cjChofer = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.cboChofer = new System.Windows.Forms.ComboBox();
            this.cjCamion = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.cboCamion = new System.Windows.Forms.ComboBox();
            this.cjOrigen = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtOrigen = new System.Windows.Forms.TextBox();
            this.cjDestino = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.txtDestino = new System.Windows.Forms.TextBox();
            this.cjFechaSalida = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.dtpFechaSalida = new System.Windows.Forms.DateTimePicker();
            this.cjFechaLlegada = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.dtpFechaLlegada = new System.Windows.Forms.DateTimePicker();
            this.cjDistancia = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.nudDistancia = new System.Windows.Forms.NumericUpDown();
            this.chkATiempo = new CapaPresentacionWinForms.Controles.CasillaVerificacion();
            this.alerta = new CapaPresentacionWinForms.Controles.Alerta();
            this.btnCancelar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.btnGuardar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.cjChofer.SuspendLayout();
            this.cjCamion.SuspendLayout();
            this.cjOrigen.SuspendLayout();
            this.cjDestino.SuspendLayout();
            this.cjFechaSalida.SuspendLayout();
            this.cjFechaLlegada.SuspendLayout();
            this.cjDistancia.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDistancia)).BeginInit();
            this.SuspendLayout();
            // 
            // cjChofer
            // 
            this.cjChofer.Controls.Add(this.cboChofer);
            this.cjChofer.Location = new System.Drawing.Point(24, 76);
            this.cjChofer.Name = "cjChofer";
            this.cjChofer.Size = new System.Drawing.Size(452, 62);
            this.cjChofer.TabIndex = 0;
            this.cjChofer.Titulo = "Chofer";
            // 
            // cboChofer
            // 
            this.cboChofer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChofer.FormattingEnabled = true;
            this.cboChofer.Name = "cboChofer";
            this.cboChofer.Size = new System.Drawing.Size(420, 25);
            this.cboChofer.TabIndex = 0;
            // 
            // cjCamion
            // 
            this.cjCamion.Controls.Add(this.cboCamion);
            this.cjCamion.Location = new System.Drawing.Point(24, 142);
            this.cjCamion.Name = "cjCamion";
            this.cjCamion.Size = new System.Drawing.Size(452, 62);
            this.cjCamion.TabIndex = 1;
            this.cjCamion.Titulo = "Camión";
            // 
            // cboCamion
            // 
            this.cboCamion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCamion.FormattingEnabled = true;
            this.cboCamion.Name = "cboCamion";
            this.cboCamion.Size = new System.Drawing.Size(420, 25);
            this.cboCamion.TabIndex = 0;
            // 
            // cjOrigen
            // 
            this.cjOrigen.Controls.Add(this.txtOrigen);
            this.cjOrigen.Location = new System.Drawing.Point(24, 208);
            this.cjOrigen.Name = "cjOrigen";
            this.cjOrigen.Size = new System.Drawing.Size(452, 62);
            this.cjOrigen.TabIndex = 2;
            this.cjOrigen.Titulo = "Origen";
            // 
            // txtOrigen
            // 
            this.txtOrigen.MaxLength = 200;
            this.txtOrigen.Name = "txtOrigen";
            this.txtOrigen.Size = new System.Drawing.Size(420, 18);
            this.txtOrigen.TabIndex = 0;
            // 
            // cjDestino
            // 
            this.cjDestino.Controls.Add(this.txtDestino);
            this.cjDestino.Location = new System.Drawing.Point(24, 274);
            this.cjDestino.Name = "cjDestino";
            this.cjDestino.Size = new System.Drawing.Size(452, 62);
            this.cjDestino.TabIndex = 3;
            this.cjDestino.Titulo = "Destino";
            // 
            // txtDestino
            // 
            this.txtDestino.MaxLength = 200;
            this.txtDestino.Name = "txtDestino";
            this.txtDestino.Size = new System.Drawing.Size(420, 18);
            this.txtDestino.TabIndex = 0;
            // 
            // cjFechaSalida
            // 
            this.cjFechaSalida.Controls.Add(this.dtpFechaSalida);
            this.cjFechaSalida.Location = new System.Drawing.Point(24, 340);
            this.cjFechaSalida.Name = "cjFechaSalida";
            this.cjFechaSalida.Size = new System.Drawing.Size(452, 62);
            this.cjFechaSalida.TabIndex = 4;
            this.cjFechaSalida.Titulo = "Fecha de salida";
            // 
            // dtpFechaSalida
            // 
            this.dtpFechaSalida.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaSalida.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaSalida.Name = "dtpFechaSalida";
            this.dtpFechaSalida.Size = new System.Drawing.Size(420, 25);
            this.dtpFechaSalida.TabIndex = 0;
            // 
            // cjFechaLlegada
            // 
            this.cjFechaLlegada.Controls.Add(this.dtpFechaLlegada);
            this.cjFechaLlegada.Location = new System.Drawing.Point(24, 406);
            this.cjFechaLlegada.Name = "cjFechaLlegada";
            this.cjFechaLlegada.Size = new System.Drawing.Size(452, 62);
            this.cjFechaLlegada.TabIndex = 5;
            this.cjFechaLlegada.Titulo = "Fecha de llegada";
            // 
            // dtpFechaLlegada
            // 
            this.dtpFechaLlegada.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpFechaLlegada.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaLlegada.Name = "dtpFechaLlegada";
            this.dtpFechaLlegada.Size = new System.Drawing.Size(420, 25);
            this.dtpFechaLlegada.TabIndex = 0;
            // 
            // cjDistancia
            // 
            this.cjDistancia.Controls.Add(this.nudDistancia);
            this.cjDistancia.Location = new System.Drawing.Point(24, 472);
            this.cjDistancia.Name = "cjDistancia";
            this.cjDistancia.Size = new System.Drawing.Size(452, 62);
            this.cjDistancia.TabIndex = 6;
            this.cjDistancia.Titulo = "Distancia (km)";
            // 
            // nudDistancia
            // 
            this.nudDistancia.DecimalPlaces = 1;
            this.nudDistancia.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDistancia.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudDistancia.Name = "nudDistancia";
            this.nudDistancia.Size = new System.Drawing.Size(420, 21);
            this.nudDistancia.TabIndex = 0;
            this.nudDistancia.ThousandsSeparator = true;
            this.nudDistancia.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            //
            // chkATiempo
            //
            this.chkATiempo.Location = new System.Drawing.Point(28, 542);
            this.chkATiempo.Name = "chkATiempo";
            this.chkATiempo.Size = new System.Drawing.Size(200, 26);
            this.chkATiempo.TabIndex = 7;
            this.chkATiempo.Text = "Llegó a tiempo";
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
            // FrmRutaEditar
            //
            this.AcceptButton = this.btnGuardar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(500, 656);
            this.Controls.Add(this.alerta);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.chkATiempo);
            this.Controls.Add(this.cjDistancia);
            this.Controls.Add(this.cjFechaLlegada);
            this.Controls.Add(this.cjFechaSalida);
            this.Controls.Add(this.cjDestino);
            this.Controls.Add(this.cjOrigen);
            this.Controls.Add(this.cjCamion);
            this.Controls.Add(this.cjChofer);
            this.Name = "FrmRutaEditar";
            this.Text = "Datos de la ruta";
            this.Titulo = "Datos de la ruta";
            this.Load += new System.EventHandler(this.FrmRutaEditar_Load);
            this.cjChofer.ResumeLayout(false);
            this.cjCamion.ResumeLayout(false);
            this.cjOrigen.ResumeLayout(false);
            this.cjOrigen.PerformLayout();
            this.cjDestino.ResumeLayout(false);
            this.cjDestino.PerformLayout();
            this.cjFechaSalida.ResumeLayout(false);
            this.cjFechaLlegada.ResumeLayout(false);
            this.cjDistancia.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudDistancia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CapaPresentacionWinForms.Controles.CajaRedonda cjChofer;
        private System.Windows.Forms.ComboBox cboChofer;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjCamion;
        private System.Windows.Forms.ComboBox cboCamion;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjOrigen;
        private System.Windows.Forms.TextBox txtOrigen;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjDestino;
        private System.Windows.Forms.TextBox txtDestino;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjFechaSalida;
        private System.Windows.Forms.DateTimePicker dtpFechaSalida;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjFechaLlegada;
        private System.Windows.Forms.DateTimePicker dtpFechaLlegada;
        private CapaPresentacionWinForms.Controles.CajaRedonda cjDistancia;
        private System.Windows.Forms.NumericUpDown nudDistancia;
        private CapaPresentacionWinForms.Controles.CasillaVerificacion chkATiempo;
        private CapaPresentacionWinForms.Controles.Alerta alerta;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnCancelar;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnGuardar;
    }
}
