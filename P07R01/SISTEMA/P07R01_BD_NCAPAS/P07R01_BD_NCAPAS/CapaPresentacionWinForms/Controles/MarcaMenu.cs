using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //marca del menu lateral (.sidebar-brand): "Control de" en tinta y "Camiones" con degradado
    public class MarcaMenu : Control
    {
        public MarcaMenu()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(220, 90);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            Font fuente = Tema.Fuente(15f, FontStyle.Bold);
            var formato = new StringFormat { Alignment = StringAlignment.Center };

            using (var tinta = new SolidBrush(Tema.Tinta))
                g.DrawString("Control de", fuente, tinta, new RectangleF(0, 10, Width, 30), formato);

            SizeF tam = g.MeasureString("Camiones", fuente);
            var area = new RectangleF((Width - tam.Width) / 2, 36, tam.Width, tam.Height);
            using (var degradado = new LinearGradientBrush(area, Tema.Coral, Tema.Naranja, LinearGradientMode.Horizontal))
                g.DrawString("Camiones", fuente, degradado, new RectangleF(0, 36, Width, 30), formato);
        }
    }
}
