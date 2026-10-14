namespace CapaPresentacionWinForms
{
    partial class FrmPrincipal
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
            this.pnlMenu = new CapaPresentacionWinForms.Controles.PanelVidrio();
            this.btnSalir = new CapaPresentacionWinForms.Controles.BotonMenu();
            this.btnRutas = new CapaPresentacionWinForms.Controles.BotonMenu();
            this.btnChoferes = new CapaPresentacionWinForms.Controles.BotonMenu();
            this.btnCamiones = new CapaPresentacionWinForms.Controles.BotonMenu();
            this.btnInicio = new CapaPresentacionWinForms.Controles.BotonMenu();
            this.marcaMenu = new CapaPresentacionWinForms.Controles.MarcaMenu();
            this.pnlContenido = new System.Windows.Forms.Panel();
            this.pieDePagina = new CapaPresentacionWinForms.Controles.PieDePagina();
            this.pnlMenu.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlMenu
            //
            this.pnlMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlMenu.Controls.Add(this.btnSalir);
            this.pnlMenu.Controls.Add(this.btnRutas);
            this.pnlMenu.Controls.Add(this.btnChoferes);
            this.pnlMenu.Controls.Add(this.btnCamiones);
            this.pnlMenu.Controls.Add(this.btnInicio);
            this.pnlMenu.Controls.Add(this.marcaMenu);
            this.pnlMenu.Location = new System.Drawing.Point(12, 12);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(24, 30, 24, 26);
            this.pnlMenu.Size = new System.Drawing.Size(272, 736);
            this.pnlMenu.TabIndex = 0;
            //
            // btnSalir
            //
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.Location = new System.Drawing.Point(24, 656);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(224, 54);
            this.btnSalir.TabIndex = 5;
            this.btnSalir.Text = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            //
            // btnRutas
            //
            this.btnRutas.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRutas.Location = new System.Drawing.Point(24, 292);
            this.btnRutas.Name = "btnRutas";
            this.btnRutas.Size = new System.Drawing.Size(224, 54);
            this.btnRutas.TabIndex = 4;
            this.btnRutas.Text = "Rutas";
            this.btnRutas.Click += new System.EventHandler(this.btnRutas_Click);
            //
            // btnChoferes
            //
            this.btnChoferes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnChoferes.Location = new System.Drawing.Point(24, 238);
            this.btnChoferes.Name = "btnChoferes";
            this.btnChoferes.Size = new System.Drawing.Size(224, 54);
            this.btnChoferes.TabIndex = 3;
            this.btnChoferes.Text = "Choferes";
            this.btnChoferes.Click += new System.EventHandler(this.btnChoferes_Click);
            //
            // btnCamiones
            //
            this.btnCamiones.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCamiones.Location = new System.Drawing.Point(24, 184);
            this.btnCamiones.Name = "btnCamiones";
            this.btnCamiones.Size = new System.Drawing.Size(224, 54);
            this.btnCamiones.TabIndex = 2;
            this.btnCamiones.Text = "Camiones";
            this.btnCamiones.Click += new System.EventHandler(this.btnCamiones_Click);
            //
            // btnInicio
            //
            this.btnInicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInicio.Location = new System.Drawing.Point(24, 130);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(224, 54);
            this.btnInicio.TabIndex = 1;
            this.btnInicio.Text = "Inicio";
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            //
            // marcaMenu
            //
            this.marcaMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.marcaMenu.Location = new System.Drawing.Point(24, 30);
            this.marcaMenu.Name = "marcaMenu";
            this.marcaMenu.Size = new System.Drawing.Size(224, 100);
            this.marcaMenu.TabIndex = 0;
            //
            // pnlContenido
            //
            this.pnlContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContenido.BackColor = System.Drawing.Color.Transparent;
            this.pnlContenido.Location = new System.Drawing.Point(292, 0);
            this.pnlContenido.Name = "pnlContenido";
            this.pnlContenido.Size = new System.Drawing.Size(988, 692);
            this.pnlContenido.TabIndex = 1;
            //
            // pieDePagina
            //
            this.pieDePagina.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pieDePagina.Location = new System.Drawing.Point(328, 696);
            this.pieDePagina.Name = "pieDePagina";
            this.pieDePagina.Size = new System.Drawing.Size(916, 48);
            this.pieDePagina.TabIndex = 2;
            //
            // FrmPrincipal
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(249)))), ((int)(((byte)(246)))));
            this.ClientSize = new System.Drawing.Size(1280, 760);
            this.Controls.Add(this.pieDePagina);
            this.Controls.Add(this.pnlContenido);
            this.Controls.Add(this.pnlMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.MinimumSize = new System.Drawing.Size(1180, 720);
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Control de Camiones";
            this.Load += new System.EventHandler(this.FrmPrincipal_Load);
            this.pnlMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CapaPresentacionWinForms.Controles.PanelVidrio pnlMenu;
        private CapaPresentacionWinForms.Controles.MarcaMenu marcaMenu;
        private CapaPresentacionWinForms.Controles.BotonMenu btnInicio;
        private CapaPresentacionWinForms.Controles.BotonMenu btnCamiones;
        private CapaPresentacionWinForms.Controles.BotonMenu btnChoferes;
        private CapaPresentacionWinForms.Controles.BotonMenu btnRutas;
        private CapaPresentacionWinForms.Controles.BotonMenu btnSalir;
        private System.Windows.Forms.Panel pnlContenido;
        private CapaPresentacionWinForms.Controles.PieDePagina pieDePagina;
    }
}
