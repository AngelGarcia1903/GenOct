namespace CapaPresentacionWinForms
{
    partial class FrmInicio
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
            this.pnlEncabezado = new System.Windows.Forms.Panel();
            this.lblActualizado = new System.Windows.Forms.Label();
            this.btnRefrescar = new CapaPresentacionWinForms.Controles.BotonRedondo();
            this.tituloPagina = new CapaPresentacionWinForms.Controles.TituloPagina();
            this.alerta = new CapaPresentacionWinForms.Controles.Alerta();
            this.tlpTarjetas = new System.Windows.Forms.TableLayoutPanel();
            this.tarjetaCamiones = new CapaPresentacionWinForms.Controles.TarjetaDato();
            this.tarjetaChoferes = new CapaPresentacionWinForms.Controles.TarjetaDato();
            this.tarjetaRutas = new CapaPresentacionWinForms.Controles.TarjetaDato();
            this.tarjetaKm = new CapaPresentacionWinForms.Controles.TarjetaDato();
            this.tlpPaneles = new System.Windows.Forms.TableLayoutPanel();
            this.pnlEstado = new CapaPresentacionWinForms.Controles.PanelVidrio();
            this.barATiempo = new CapaPresentacionWinForms.Controles.BarraProgreso();
            this.barChoferes = new CapaPresentacionWinForms.Controles.BarraProgreso();
            this.barCamiones = new CapaPresentacionWinForms.Controles.BarraProgreso();
            this.lblTituloEstado = new System.Windows.Forms.Label();
            this.pnlUltimasRutas = new CapaPresentacionWinForms.Controles.PanelVidrio();
            this.lstUltimasRutas = new CapaPresentacionWinForms.Controles.ListaUltimasRutas();
            this.lblTituloRutas = new System.Windows.Forms.Label();
            this.pnlEncabezado.SuspendLayout();
            this.tlpTarjetas.SuspendLayout();
            this.tlpPaneles.SuspendLayout();
            this.pnlEstado.SuspendLayout();
            this.pnlUltimasRutas.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlEncabezado
            //
            this.pnlEncabezado.BackColor = System.Drawing.Color.Transparent;
            this.pnlEncabezado.Controls.Add(this.lblActualizado);
            this.pnlEncabezado.Controls.Add(this.btnRefrescar);
            this.pnlEncabezado.Controls.Add(this.tituloPagina);
            this.pnlEncabezado.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEncabezado.Location = new System.Drawing.Point(36, 28);
            this.pnlEncabezado.Name = "pnlEncabezado";
            this.pnlEncabezado.Size = new System.Drawing.Size(916, 104);
            this.pnlEncabezado.TabIndex = 0;
            //
            // lblActualizado
            //
            this.lblActualizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblActualizado.BackColor = System.Drawing.Color.Transparent;
            this.lblActualizado.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.lblActualizado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(165)))), ((int)(((byte)(176)))));
            this.lblActualizado.Location = new System.Drawing.Point(636, 82);
            this.lblActualizado.Name = "lblActualizado";
            this.lblActualizado.Size = new System.Drawing.Size(280, 18);
            this.lblActualizado.TabIndex = 2;
            this.lblActualizado.Text = "Actualizado:";
            this.lblActualizado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // btnRefrescar
            //
            this.btnRefrescar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefrescar.Location = new System.Drawing.Point(792, 36);
            this.btnRefrescar.Name = "btnRefrescar";
            this.btnRefrescar.Size = new System.Drawing.Size(124, 44);
            this.btnRefrescar.TabIndex = 1;
            this.btnRefrescar.Text = "Refrescar";
            this.btnRefrescar.Click += new System.EventHandler(this.btnRefrescar_Click);
            //
            // tituloPagina
            //
            this.tituloPagina.Location = new System.Drawing.Point(0, 0);
            this.tituloPagina.Name = "tituloPagina";
            this.tituloPagina.Resaltado = "general";
            this.tituloPagina.Size = new System.Drawing.Size(560, 100);
            this.tituloPagina.Subtitulo = "Panel";
            this.tituloPagina.TabIndex = 0;
            this.tituloPagina.Titulo = "Resumen";
            //
            // alerta
            //
            this.alerta.Dock = System.Windows.Forms.DockStyle.Top;
            this.alerta.Location = new System.Drawing.Point(36, 132);
            this.alerta.Name = "alerta";
            this.alerta.Size = new System.Drawing.Size(916, 68);
            this.alerta.TabIndex = 1;
            //
            // tlpTarjetas
            //
            this.tlpTarjetas.BackColor = System.Drawing.Color.Transparent;
            this.tlpTarjetas.ColumnCount = 4;
            this.tlpTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTarjetas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTarjetas.Controls.Add(this.tarjetaCamiones, 0, 0);
            this.tlpTarjetas.Controls.Add(this.tarjetaChoferes, 1, 0);
            this.tlpTarjetas.Controls.Add(this.tarjetaRutas, 2, 0);
            this.tlpTarjetas.Controls.Add(this.tarjetaKm, 3, 0);
            this.tlpTarjetas.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpTarjetas.Location = new System.Drawing.Point(36, 200);
            this.tlpTarjetas.Name = "tlpTarjetas";
            this.tlpTarjetas.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.tlpTarjetas.RowCount = 1;
            this.tlpTarjetas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTarjetas.Size = new System.Drawing.Size(916, 160);
            this.tlpTarjetas.TabIndex = 2;
            //
            // tarjetaCamiones
            //
            this.tarjetaCamiones.Detalle = "0 disponibles";
            this.tarjetaCamiones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tarjetaCamiones.EsEnlace = true;
            this.tarjetaCamiones.Etiqueta = "Camiones";
            this.tarjetaCamiones.Location = new System.Drawing.Point(0, 0);
            this.tarjetaCamiones.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tarjetaCamiones.Name = "tarjetaCamiones";
            this.tarjetaCamiones.Size = new System.Drawing.Size(219, 150);
            this.tarjetaCamiones.TabIndex = 0;
            this.tarjetaCamiones.Click += new System.EventHandler(this.tarjetaCamiones_Click);
            //
            // tarjetaChoferes
            //
            this.tarjetaChoferes.Detalle = "0 disponibles";
            this.tarjetaChoferes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tarjetaChoferes.EsEnlace = true;
            this.tarjetaChoferes.Etiqueta = "Choferes";
            this.tarjetaChoferes.Location = new System.Drawing.Point(229, 0);
            this.tarjetaChoferes.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tarjetaChoferes.Name = "tarjetaChoferes";
            this.tarjetaChoferes.Size = new System.Drawing.Size(219, 150);
            this.tarjetaChoferes.TabIndex = 1;
            this.tarjetaChoferes.Click += new System.EventHandler(this.tarjetaChoferes_Click);
            //
            // tarjetaRutas
            //
            this.tarjetaRutas.Detalle = "0 llegaron a tiempo";
            this.tarjetaRutas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tarjetaRutas.EsEnlace = true;
            this.tarjetaRutas.Etiqueta = "Rutas";
            this.tarjetaRutas.Location = new System.Drawing.Point(458, 0);
            this.tarjetaRutas.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tarjetaRutas.Name = "tarjetaRutas";
            this.tarjetaRutas.Size = new System.Drawing.Size(219, 150);
            this.tarjetaRutas.TabIndex = 2;
            this.tarjetaRutas.Click += new System.EventHandler(this.tarjetaRutas_Click);
            //
            // tarjetaKm
            //
            this.tarjetaKm.Detalle = "Promedio por ruta: 0 km";
            this.tarjetaKm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tarjetaKm.Etiqueta = "Kilómetros recorridos";
            this.tarjetaKm.Location = new System.Drawing.Point(687, 0);
            this.tarjetaKm.Margin = new System.Windows.Forms.Padding(0);
            this.tarjetaKm.Name = "tarjetaKm";
            this.tarjetaKm.Size = new System.Drawing.Size(229, 150);
            this.tarjetaKm.TabIndex = 3;
            //
            // tlpPaneles
            //
            this.tlpPaneles.BackColor = System.Drawing.Color.Transparent;
            this.tlpPaneles.ColumnCount = 2;
            this.tlpPaneles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tlpPaneles.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58F));
            this.tlpPaneles.Controls.Add(this.pnlEstado, 0, 0);
            this.tlpPaneles.Controls.Add(this.pnlUltimasRutas, 1, 0);
            this.tlpPaneles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpPaneles.Location = new System.Drawing.Point(36, 360);
            this.tlpPaneles.Name = "tlpPaneles";
            this.tlpPaneles.RowCount = 1;
            this.tlpPaneles.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPaneles.Size = new System.Drawing.Size(916, 324);
            this.tlpPaneles.TabIndex = 3;
            //
            // pnlEstado
            //
            this.pnlEstado.Controls.Add(this.barATiempo);
            this.pnlEstado.Controls.Add(this.barChoferes);
            this.pnlEstado.Controls.Add(this.barCamiones);
            this.pnlEstado.Controls.Add(this.lblTituloEstado);
            this.pnlEstado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlEstado.Location = new System.Drawing.Point(0, 0);
            this.pnlEstado.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlEstado.Name = "pnlEstado";
            this.pnlEstado.Size = new System.Drawing.Size(374, 324);
            this.pnlEstado.TabIndex = 0;
            //
            // barATiempo
            //
            this.barATiempo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barATiempo.ColorFin = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(97)))));
            this.barATiempo.ColorInicio = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.barATiempo.Location = new System.Drawing.Point(28, 186);
            this.barATiempo.Name = "barATiempo";
            this.barATiempo.Size = new System.Drawing.Size(318, 46);
            this.barATiempo.TabIndex = 3;
            this.barATiempo.Text = "Rutas a tiempo";
            //
            // barChoferes
            //
            this.barChoferes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barChoferes.ColorFin = System.Drawing.Color.FromArgb(((int)(((byte)(165)))), ((int)(((byte)(147)))), ((int)(((byte)(255)))));
            this.barChoferes.ColorInicio = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(97)))), ((int)(((byte)(255)))));
            this.barChoferes.Location = new System.Drawing.Point(28, 128);
            this.barChoferes.Name = "barChoferes";
            this.barChoferes.Size = new System.Drawing.Size(318, 46);
            this.barChoferes.TabIndex = 2;
            this.barChoferes.Text = "Choferes disponibles";
            //
            // barCamiones
            //
            this.barCamiones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.barCamiones.ColorFin = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(181)))), ((int)(((byte)(124)))));
            this.barCamiones.ColorInicio = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(78)))));
            this.barCamiones.Location = new System.Drawing.Point(28, 70);
            this.barCamiones.Name = "barCamiones";
            this.barCamiones.Size = new System.Drawing.Size(318, 46);
            this.barCamiones.TabIndex = 1;
            this.barCamiones.Text = "Camiones disponibles";
            //
            // lblTituloEstado
            //
            this.lblTituloEstado.AutoSize = true;
            this.lblTituloEstado.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloEstado.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.lblTituloEstado.Location = new System.Drawing.Point(26, 26);
            this.lblTituloEstado.Name = "lblTituloEstado";
            this.lblTituloEstado.Size = new System.Drawing.Size(177, 21);
            this.lblTituloEstado.TabIndex = 0;
            this.lblTituloEstado.Text = "Estado de la flotilla";
            //
            // pnlUltimasRutas
            //
            this.pnlUltimasRutas.Controls.Add(this.lstUltimasRutas);
            this.pnlUltimasRutas.Controls.Add(this.lblTituloRutas);
            this.pnlUltimasRutas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlUltimasRutas.Location = new System.Drawing.Point(384, 0);
            this.pnlUltimasRutas.Margin = new System.Windows.Forms.Padding(0);
            this.pnlUltimasRutas.Name = "pnlUltimasRutas";
            this.pnlUltimasRutas.Size = new System.Drawing.Size(532, 324);
            this.pnlUltimasRutas.TabIndex = 1;
            //
            // lstUltimasRutas
            //
            this.lstUltimasRutas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstUltimasRutas.Location = new System.Drawing.Point(18, 60);
            this.lstUltimasRutas.Name = "lstUltimasRutas";
            this.lstUltimasRutas.Size = new System.Drawing.Size(494, 248);
            this.lstUltimasRutas.TabIndex = 1;
            //
            // lblTituloRutas
            //
            this.lblTituloRutas.AutoSize = true;
            this.lblTituloRutas.BackColor = System.Drawing.Color.Transparent;
            this.lblTituloRutas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloRutas.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(34)))), ((int)(((byte)(41)))));
            this.lblTituloRutas.Location = new System.Drawing.Point(26, 26);
            this.lblTituloRutas.Name = "lblTituloRutas";
            this.lblTituloRutas.Size = new System.Drawing.Size(120, 21);
            this.lblTituloRutas.TabIndex = 0;
            this.lblTituloRutas.Text = "Últimas rutas";
            //
            // FrmInicio
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 692);
            this.Controls.Add(this.tlpPaneles);
            this.Controls.Add(this.tlpTarjetas);
            this.Controls.Add(this.alerta);
            this.Controls.Add(this.pnlEncabezado);
            this.Name = "FrmInicio";
            this.Text = "Inicio";
            this.Load += new System.EventHandler(this.FrmInicio_Load);
            this.pnlEncabezado.ResumeLayout(false);
            this.tlpTarjetas.ResumeLayout(false);
            this.tlpPaneles.ResumeLayout(false);
            this.pnlEstado.ResumeLayout(false);
            this.pnlEstado.PerformLayout();
            this.pnlUltimasRutas.ResumeLayout(false);
            this.pnlUltimasRutas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlEncabezado;
        private CapaPresentacionWinForms.Controles.TituloPagina tituloPagina;
        private CapaPresentacionWinForms.Controles.BotonRedondo btnRefrescar;
        private System.Windows.Forms.Label lblActualizado;
        private CapaPresentacionWinForms.Controles.Alerta alerta;
        private System.Windows.Forms.TableLayoutPanel tlpTarjetas;
        private CapaPresentacionWinForms.Controles.TarjetaDato tarjetaCamiones;
        private CapaPresentacionWinForms.Controles.TarjetaDato tarjetaChoferes;
        private CapaPresentacionWinForms.Controles.TarjetaDato tarjetaRutas;
        private CapaPresentacionWinForms.Controles.TarjetaDato tarjetaKm;
        private System.Windows.Forms.TableLayoutPanel tlpPaneles;
        private CapaPresentacionWinForms.Controles.PanelVidrio pnlEstado;
        private System.Windows.Forms.Label lblTituloEstado;
        private CapaPresentacionWinForms.Controles.BarraProgreso barCamiones;
        private CapaPresentacionWinForms.Controles.BarraProgreso barChoferes;
        private CapaPresentacionWinForms.Controles.BarraProgreso barATiempo;
        private CapaPresentacionWinForms.Controles.PanelVidrio pnlUltimasRutas;
        private System.Windows.Forms.Label lblTituloRutas;
        private CapaPresentacionWinForms.Controles.ListaUltimasRutas lstUltimasRutas;
    }
}
