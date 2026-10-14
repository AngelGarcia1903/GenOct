using System;
using System.Windows.Forms;
using CapaPresentacionWinForms.Controles;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms
{
    //ventana principal, como Site.Master: menu lateral de vidrio a la izquierda,
    //la seccion elegida a la derecha y el pie de pagina abajo, todo sobre el fondo aurora
    public partial class FrmPrincipal : Form
    {
        private Form formularioActual;
        private BotonMenu botonActivo;

        public FrmPrincipal()
        {
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
        }


        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmInicio(), btnInicio);
        }


        //cierra la seccion anterior y muestra la nueva dentro de pnlContenido
        private void AbrirFormulario(Form formulario, BotonMenu boton)
        {
            formularioActual?.Close();

            formularioActual = formulario;
            formulario.TopLevel = false;               //permite meter un Form dentro de un Panel
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            pnlContenido.Controls.Add(formulario);
            formulario.Show();

            if (botonActivo != null) botonActivo.Activo = false;
            botonActivo = boton;
            botonActivo.Activo = true;
            Text = formulario.Text + " - Control de Camiones";
        }


        //las tarjetas del Inicio usan este metodo para abrir su seccion (como los enlaces de InicioWeb)
        public void Navegar(string seccion)
        {
            switch (seccion)
            {
                case "Camiones": btnCamiones.PerformClick(); break;
                case "Choferes": btnChoferes.PerformClick(); break;
                case "Rutas": btnRutas.PerformClick(); break;
                default: btnInicio.PerformClick(); break;
            }
        }


        private void btnInicio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmInicio(), btnInicio);
        }

        private void btnCamiones_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCamiones(), btnCamiones);
        }

        private void btnChoferes_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmChoferes(), btnChoferes);
        }

        private void btnRutas_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmRutas(), btnRutas);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (Mensajes.Confirmar("¿Deseas salir del sistema?", this))
                Application.Exit();
        }


        // ---------- fondo aurora ----------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Tema.PintarAurora(e.Graphics, this);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate(true);
        }

        //sin parpadeo al repintar los controles transparentes
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   //WS_EX_COMPOSITED
                return cp;
            }
        }

    }//fin de clase
}//fin de namespace
