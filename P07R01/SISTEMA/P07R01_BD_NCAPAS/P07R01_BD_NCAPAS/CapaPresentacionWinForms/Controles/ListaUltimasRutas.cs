using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaEntidades;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //lista de "Ultimas rutas" del Inicio (.dash-ruta): una fila por ruta con origen → destino,
    //detalle gris, kilometros y etiqueta "A tiempo" / "Con retraso"; la fila se resalta al pasar el mouse
    public class ListaUltimasRutas : Control
    {
        private const int AltoFila = 50;
        private List<E_Ruta> rutas = new List<E_Ruta>();
        private int filaEncima = -1;

        public ListaUltimasRutas()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(500, 320);
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public List<E_Ruta> Rutas
        {
            get => rutas;
            set { rutas = value ?? new List<E_Ruta>(); Invalidate(); }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int fila = e.Y / AltoFila;
            if (fila >= rutas.Count) fila = -1;
            if (fila != filaEncima)
            {
                filaEncima = fila;
                Invalidate();
            }
        }

        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);
            filaEncima = -1;
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);

            if (rutas.Count == 0)
            {
                TextRenderer.DrawText(g, "Aún no hay rutas registradas.", Tema.Fuente(9.5f), new Point(0, 10), Tema.Gris);
                return;
            }

            for (int i = 0; i < rutas.Count; i++)
            {
                E_Ruta ruta = rutas[i];
                var fila = new RectangleF(0, i * AltoFila, Width - 1, AltoFila);
                float sangria = 0;

                if (i == filaEncima)
                {
                    sangria = 4;
                    using (GraphicsPath p = Tema.Redondeado(new RectangleF(fila.X, fila.Y + 2, fila.Width, fila.Height - 4), 14))
                    using (var b = new SolidBrush(Color.FromArgb(180, Color.White)))
                        g.FillPath(b, p);
                }
                else if (i < rutas.Count - 1)
                {
                    using (var linea = new Pen(Color.FromArgb(64, Tema.Gris)) { DashStyle = DashStyle.Dash })
                        g.DrawLine(linea, fila.X + 12, fila.Bottom - 1, fila.Right - 12, fila.Bottom - 1);
                }

                int x = (int)(fila.X + 12 + sangria);
                int ancho = (int)fila.Width - 24;
                TextRenderer.DrawText(g, $"{ruta.Origen} → {ruta.Destino}", Tema.Fuente(10f, FontStyle.Bold),
                    new Rectangle(x, (int)fila.Y + 6, ancho - 120, 22), Tema.Tinta, TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
                TextRenderer.DrawText(g, $"{ruta.NombreChofer} · Camión {ruta.Matricula} · {ruta.FechaSalida:dd/MM/yyyy}", Tema.Fuente(8.25f),
                    new Rectangle(x, (int)fila.Y + 28, ancho - 120, 18), Tema.Gris, TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);

                var derecha = new Rectangle(x, (int)fila.Y + 5, ancho, 22);
                TextRenderer.DrawText(g, $"{ruta.Distancia:N1} km", Tema.Fuente(10f, FontStyle.Bold), derecha, Tema.Tinta,
                    TextFormatFlags.Right | TextFormatFlags.NoPadding);

                string texto = ruta.ATiempo ? "A tiempo" : "Con retraso";
                Font fuente = Tema.Fuente(7.5f, FontStyle.Bold);
                Size tam = TextRenderer.MeasureText(texto, fuente, Size.Empty, TextFormatFlags.NoPadding);
                var insignia = new RectangleF(x + ancho - tam.Width - 14, fila.Y + 27, tam.Width + 14, tam.Height + 6);
                using (GraphicsPath p = Tema.Redondeado(insignia, 5))
                using (var b = new SolidBrush(ruta.ATiempo ? Tema.Verde : Tema.Coral))
                    g.FillPath(b, p);
                TextRenderer.DrawText(g, texto, fuente, Rectangle.Round(insignia), Color.White,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }
        }
    }
}
