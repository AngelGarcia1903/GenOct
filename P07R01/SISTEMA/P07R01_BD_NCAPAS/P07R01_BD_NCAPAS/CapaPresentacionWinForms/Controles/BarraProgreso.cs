using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //barra de porcentaje del Inicio (.dash-barra): texto y porcentaje arriba,
    //barra redondeada de 10px con degradado abajo
    public class BarraProgreso : Control
    {
        private int porcentaje;
        private Color colorInicio = Tema.Verde;
        private Color colorFin = ColorTranslator.FromHtml("#2fb57c");

        public BarraProgreso()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(360, 46);
        }

        [Category("Barra"), DefaultValue(0)]
        public int Porcentaje
        {
            get => porcentaje;
            set { porcentaje = Math.Max(0, Math.Min(100, value)); Invalidate(); }
        }

        [Category("Barra")]
        public Color ColorInicio { get => colorInicio; set { colorInicio = value; Invalidate(); } }

        [Category("Barra")]
        public Color ColorFin { get => colorFin; set { colorFin = value; Invalidate(); } }

        private bool ShouldSerializeColorInicio() => true;
        private bool ShouldSerializeColorFin() => true;

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);

            var filaTexto = new Rectangle(0, 0, Width, 22);
            TextRenderer.DrawText(g, Text, Tema.Fuente(9f), filaTexto, Tema.GrisMedio,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            TextRenderer.DrawText(g, porcentaje + "%", Tema.Fuente(9f, FontStyle.Bold), filaTexto, Tema.Tinta,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

            var pista = new RectangleF(0, 28, Width - 1, 10);
            using (GraphicsPath p = Tema.Redondeado(pista, 5))
            using (var b = new SolidBrush(Color.FromArgb(38, Tema.Gris)))
                g.FillPath(b, p);

            if (porcentaje > 0)
            {
                var lleno = new RectangleF(pista.X, pista.Y, Math.Max(10, pista.Width * porcentaje / 100f), pista.Height);
                using (GraphicsPath p = Tema.Redondeado(lleno, 5))
                using (var b = new LinearGradientBrush(lleno, colorInicio, colorFin, LinearGradientMode.Horizontal))
                    g.FillPath(b, p);
            }
        }
    }
}
