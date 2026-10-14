using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //panel de "vidrio" (glassmorphism): blanco semitransparente, esquinas redondeadas, borde claro y sombra
    //equivale a .sidebar-gourmet, .dash-panel, .dash-card y footer de camionesWeb.css
    public class PanelVidrio : Panel
    {
        private int radio = 24;
        private int opacidad = 140;
        private bool sombra = true;
        private bool brilloEsquina = false;

        public PanelVidrio()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Padding = new Padding(20);
        }

        [Category("Vidrio"), DefaultValue(24), Description("Radio de las esquinas (border-radius).")]
        public int Radio
        {
            get => radio;
            set { radio = value; Invalidate(); }
        }

        [Category("Vidrio"), DefaultValue(140), Description("Opacidad del blanco, de 0 a 255 (0.55 en la web = 140).")]
        public int Opacidad
        {
            get => opacidad;
            set { opacidad = value; Invalidate(); }
        }

        [Category("Vidrio"), DefaultValue(true)]
        public bool Sombra
        {
            get => sombra;
            set { sombra = value; Invalidate(); }
        }

        [Category("Vidrio"), DefaultValue(false), Description("Brillo coral en la esquina superior derecha (.dash-card::before).")]
        public bool BrilloEsquina
        {
            get => brilloEsquina;
            set { brilloEsquina = value; Invalidate(); }
        }

        //rectangulo de la tarjeta: se deja un margen para que quepa la sombra
        protected RectangleF AreaTarjeta
        {
            get
            {
                int margen = sombra ? 6 : 0;
                return new RectangleF(margen, margen, Width - margen * 2 - 1, Height - margen * 2 - 4);
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);
            RectangleF r = AreaTarjeta;
            if (r.Width <= 0 || r.Height <= 0) return;

            if (sombra)
                Tema.DibujarSombra(g, r, radio);

            using (GraphicsPath path = Tema.Redondeado(r, radio))
            {
                using (var fondo = new SolidBrush(Color.FromArgb(opacidad, Color.White)))
                    g.FillPath(fondo, path);

                if (brilloEsquina)
                {
                    g.SetClip(path);
                    var circulo = new RectangleF(r.Right - 100, r.Y - 70, 160, 160);
                    using (var p = new GraphicsPath())
                    {
                        p.AddEllipse(circulo);
                        using (var brillo = new PathGradientBrush(p))
                        {
                            brillo.CenterColor = Color.FromArgb(64, Tema.Coral);
                            brillo.SurroundColors = new[] { Color.FromArgb(0, Tema.Coral) };
                            g.FillPath(brillo, p);
                        }
                    }
                    g.ResetClip();
                }

                using (var borde = new Pen(Color.FromArgb(180, Color.White), 1f))
                    g.DrawPath(borde, path);
            }

            base.OnPaint(e);
        }
    }
}
