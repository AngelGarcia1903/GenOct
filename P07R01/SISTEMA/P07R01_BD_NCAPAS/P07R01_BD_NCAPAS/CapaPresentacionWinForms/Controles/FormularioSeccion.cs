using System.Drawing;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //base de las secciones que se abren dentro de FrmPrincipal (Inicio, Camiones, Choferes, Rutas):
    //pinta su pedazo del fondo aurora para que se vea continuo con el menu
    public class FormularioSeccion : Form
    {
        public FormularioSeccion()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            BackColor = Tema.Crema;
            Font = Tema.Fuente(9.75f);
            ForeColor = Tema.Tinta;
            Padding = new Padding(36, 28, 36, 8);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Tema.PintarAurora(e.Graphics, this);
        }

        //al cambiar de tamaño la aurora se recalcula para toda la ventana
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);
            Invalidate(true);
        }
    }
}
