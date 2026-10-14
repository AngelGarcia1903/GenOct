using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //casilla de verificacion como .form-check-input: cuadro redondeado que se pinta coral al marcarse
    public class CasillaVerificacion : CheckBox
    {
        public CasillaVerificacion()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Cursor = Cursors.Hand;
            AutoSize = false;
            Font = Tema.FuenteSemibold(9f);
            Size = new Size(160, 26);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            base.OnPaintBackground(e);
            Tema.Suavizar(g);

            var cuadro = new RectangleF(1, (Height - 17) / 2f, 17, 17);
            using (GraphicsPath path = Tema.Redondeado(cuadro, 4))
            {
                if (Checked)
                {
                    using (var b = new SolidBrush(Tema.Coral))
                        g.FillPath(b, path);

                    using (var pen = new Pen(Color.White, 2f) { StartCap = LineCap.Round, EndCap = LineCap.Round, LineJoin = LineJoin.Round })
                        g.DrawLines(pen, new[]
                        {
                            new PointF(cuadro.X + 4, cuadro.Y + 9),
                            new PointF(cuadro.X + 7.5f, cuadro.Y + 12.5f),
                            new PointF(cuadro.X + 13, cuadro.Y + 5.5f)
                        });
                }
                else
                {
                    using (var b = new SolidBrush(Color.White))
                        g.FillPath(b, path);
                    using (var pen = new Pen(Color.FromArgb(110, Tema.Gris)))
                        g.DrawPath(pen, path);
                }

                if (Focused)
                {
                    using (var pen = new Pen(Color.FromArgb(60, Tema.Coral), 3f))
                        g.DrawPath(pen, path);
                }
            }

            var areaTexto = new Rectangle(26, 0, Width - 26, Height);
            TextRenderer.DrawText(g, Text, Font, areaTexto, Tema.GrisMedio,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }
    }
}
