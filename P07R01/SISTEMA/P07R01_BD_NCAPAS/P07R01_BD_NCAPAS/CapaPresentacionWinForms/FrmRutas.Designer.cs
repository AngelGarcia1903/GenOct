namespace CapaPresentacionWinForms
{
    partial class FrmRutas
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
            this.btnActualizar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.btnAgregar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.lstRutas = new CapaPresentacionWinForms.Controles.ListaTarjetas();
            this.pnlFiltros.SuspendLayout();
            this.barraFiltros.SuspendLayout();
            this.SuspendLayout();
            //
            // tituloPagina
            //
            this.tituloPagina.Dock = System.Windows.Forms.DockStyle.Top;
            this.tituloPagina.Location = new System.Drawing.Point(36, 28);
            this.tituloPagina.Name = "tituloPagina";
            this.tituloPagina.Resaltado = "rutas";
            this.tituloPagina.Size = new System.Drawing.Size(916, 100);
            this.tituloPagina.Subtitulo = "Viajes";
            this.tituloPagina.TabIndex = 0;
            this.tituloPagina.Titulo = "Nuestras";
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
            this.barraFiltros.Controls.Add(this.btnActualizar);
            this.barraFiltros.Controls.Add(this.btnAgregar);
            this.barraFiltros.Location = new System.Drawing.Point(316, 6);
            this.barraFiltros.Name = "barraFiltros";
            this.barraFiltros.Size = new System.Drawing.Size(284, 70);
            this.barraFiltros.TabIndex = 0;
            //
            // btnActualizar
            //
            this.btnActualizar.Estilo = CapaPresentacionWinForms.Controles.EstiloBoton.Contorno;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(104, 44);
            this.btnActualizar.TabIndex = 0;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            //
            // btnAgregar
            //
            this.btnAgregar.Estilo = CapaPresentacionWinForms.Controles.EstiloBoton.Exito;
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(130, 44);
            this.btnAgregar.TabIndex = 1;
            this.btnAgregar.Text = "Agregar ruta";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            //
            // lstRutas
            //
            this.lstRutas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstRutas.Location = new System.Drawing.Point(36, 288);
            this.lstRutas.Name = "lstRutas";
            this.lstRutas.Size = new System.Drawing.Size(916, 396);
            this.lstRutas.TabIndex = 3;
            //
            // FrmRutas
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 692);
            this.Controls.Add(this.lstRutas);
            this.Controls.Add(this.pnlFiltros);
            this.Controls.Add(this.alerta);
            this.Controls.Add(this.tituloPagina);
            this.Name = "FrmRutas";
            this.Text = "Rutas";
            this.Load += new System.EventHandler(this.FrmRutas_Load);
            this.pnlFiltros.ResumeLayout(false);
            this.barraFiltros.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CapaPresentacionWinForms.Controles.TituloPagina tituloPagina;
        private CapaPresentacionWinForms.Controles.Alerta alerta;
        private System.Windows.Forms.Panel pnlFiltros;
        private CapaPresentacionWinForms.Controles.BarraFiltros barraFiltros;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnActualizar;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnAgregar;
        private CapaPresentacionWinForms.Controles.ListaTarjetas lstRutas;
    }
}
