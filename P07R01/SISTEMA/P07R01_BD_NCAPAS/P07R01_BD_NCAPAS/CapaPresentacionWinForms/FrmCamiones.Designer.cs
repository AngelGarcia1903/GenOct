namespace CapaPresentacionWinForms
{
    partial class FrmCamiones
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
            this.tituloPagina = new CapaPresentacionWinForms.Controles.TituloPagina();
            this.alerta = new CapaPresentacionWinForms.Controles.Alerta();
            this.pnlFiltros = new System.Windows.Forms.Panel();
            this.barraFiltros = new CapaPresentacionWinForms.Controles.BarraFiltros();
            this.cajaFiltro = new CapaPresentacionWinForms.Controles.CajaRedonda();
            this.cboFiltro = new System.Windows.Forms.ComboBox();
            this.btnFiltrar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.btnActualizar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.btnAgregar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.lstCamiones = new CapaPresentacionWinForms.Controles.ListaTarjetas();
            this.pnlFiltros.SuspendLayout();
            this.barraFiltros.SuspendLayout();
            this.cajaFiltro.SuspendLayout();
            this.SuspendLayout();
            //
            // tituloPagina
            //
            this.tituloPagina.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPagina.Location = new System.Drawing.Point(36, 28);
            this.tituloPagina.Name = "tituloPagina";
            this.tituloPagina.Resaltado = "camiones";
            this.tituloPagina.Size = new System.Drawing.Size(916, 100);
            this.tituloPagina.Subtitulo = "Flotilla";
            this.tituloPagina.TabIndex = 0;
            this.tituloPagina.Titulo = "Nuestros";
            //
            // alerta
            //
            this.alerta.Dock = System.Windows.Forms.DockStyle.Top;
            this.alerta.Location = new System.Drawing.Point(36, 128);
            this.alerta.Name = "alerta";
            this.alerta.Size = new System.Drawing.Size(916, 68);
            this.alerta.TabIndex = 1;
            //
            // pnlFiltros
            //
            this.pnlFiltros.BackColor = System.Drawing.Color.Transparent;
            this.pnlFiltros.Controls.Add(this.barraFiltros);
            this.pnlFiltros.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFiltros.Location = new System.Drawing.Point(36, 196);
            this.pnlFiltros.Name = "pnlFiltros";
            this.pnlFiltros.Size = new System.Drawing.Size(916, 92);
            this.pnlFiltros.TabIndex = 2;
            //
            // barraFiltros
            //
            this.barraFiltros.Controls.Add(this.cajaFiltro);
            this.barraFiltros.Controls.Add(this.btnFiltrar);
            this.barraFiltros.Controls.Add(this.btnActualizar);
            this.barraFiltros.Controls.Add(this.btnAgregar);
            this.barraFiltros.Location = new System.Drawing.Point(204, 6);
            this.barraFiltros.Name = "barraFiltros";
            this.barraFiltros.Size = new System.Drawing.Size(508, 70);
            this.barraFiltros.TabIndex = 0;
            //
            // cajaFiltro
            //
            this.cajaFiltro.Controls.Add(this.cboFiltro);
            this.cajaFiltro.Name = "cajaFiltro";
            this.cajaFiltro.Pastilla = true;
            this.cajaFiltro.Size = new System.Drawing.Size(170, 44);
            this.cajaFiltro.TabIndex = 0;
            //
            // cboFiltro
            //
            this.cboFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFiltro.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.cboFiltro.FormattingEnabled = true;
            this.cboFiltro.Items.AddRange(new object[] {
            "Todos",
            "Disponibles",
            "No disponibles"});
            this.cboFiltro.Name = "cboFiltro";
            this.cboFiltro.Size = new System.Drawing.Size(140, 23);
            this.cboFiltro.TabIndex = 0;
            this.cboFiltro.SelectedIndexChanged += new System.EventHandler(this.cboFiltro_SelectedIndexChanged);
            //
            // btnFiltrar
            //
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(84, 44);
            this.btnFiltrar.TabIndex = 1;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            //
            // btnActualizar
            //
            this.btnActualizar.Estilo = CapaPresentacionWinForms.Controles.EstiloBoton.Contorno;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(104, 44);
            this.btnActualizar.TabIndex = 2;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // btnAgregar
            //
            this.btnAgregar.Estilo = CapaPresentacionWinForms.Controles.EstiloBoton.Exito;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(140, 44);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar camión";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // lstCamiones
            //
            this.lstCamiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstCamiones.Location = new System.Drawing.Point(36, 288);
            this.lstCamiones.Name = "lstCamiones";
            this.lstCamiones.Size = new System.Drawing.Size(916, 396);
            this.lstCamiones.TabIndex = 3;
            //
            // FrmCamiones
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 692);
            this.Controls.Add(this.lstCamiones);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.alerta);
            this.Controls.Add(this.tituloPagina);
            this.Name = "FrmCamiones";
            this.Text = "Camiones";
            this.Load += new System.EventHandler(this.FrmCamiones_Load);
            this.pnlFiltros.ResumeLayout(false);
            this.barraFiltros.ResumeLayout(false);
            this.cajaFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CapaPresentacionWinForms.Controles.TituloPagina tituloPagina;
        private CapaPresentacionWinForms.Controles.Alerta alerta;
        private System.Windows.Forms.Panel pnlFiltros;
        private CapaPresentacionWinForms.Controles.BarraFiltros barraFiltros;
        private CapaPresentacionWinForms.Controles.CajaRedonda cajaFiltro;
        private System.Windows.Forms.ComboBox cboFiltro;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnFiltrar;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnActualizar;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnAgregar;
        private CapaPresentacionWinForms.Controles.ListaTarjetas lstCamiones;
    }
}
