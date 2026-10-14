using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //tarjeta de un camion, chofer o ruta, igual que .row-card de la web:
    //foto arriba con etiqueta de estado, categoria, titulo, descripcion y pie con el numero.
    //Los botones Editar / Eliminar aparecen al pasar el mouse (hover reveal).
    public class TarjetaItem : Control
    {
        private const int AltoImagen = 160;
        private const int Radio = 24;

        private string urlImagen;
        private Image imagen;
        private bool encima;
        private RectangleF areaEditar, areaEliminar;
        private int botonEncima;   //0 ninguno, 1 editar, 2 eliminar

        public event EventHandler EditarClick;
        public event EventHandler EliminarClick;

        public TarjetaItem()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(320, 372);
        }

        // ---------- Datos que se muestran ----------
        public string Insignia { get; set; }               //"Disponible", "A tiempo"...
        public bool InsigniaPositiva { get; set; } = true;  //verde (true) o coral (false)
        public string Categoria { get; set; }               //texto naranja en mayusculas
        public string Titulo { get; set; }
        public string Descripcion { get; set; }             //admite saltos de linea \n
        public string Numero { get; set; }                  //"#8" o "156.6"
        public string Unidad { get; set; }                  //"ID" o "km"
        public string Subtexto { get; set; }                //"Ruta #1" (opcional)

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Dato { get; set; }                    //entidad que representa la tarjeta

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UrlImagen
        {
            get => urlImagen;
            set
            {
                urlImagen = value;
                imagen = null;
                CargarImagen(value);
            }
        }


        private async void CargarImagen(string url)
        {
            Image descargada = await CargadorImagenes.ObtenerAsync(url);
            if (IsDisposed || url != urlImagen) return;   //la tarjeta ya cambio o se cerro
            imagen = descargada;
            Invalidate();
        }


        // ---------- Mouse ----------
        protected override void OnMouseEnter(EventArgs e) { encima = true; Invalidate(); base.OnMouseEnter(e); }

        protected override void OnMouseLeave(EventArgs e)
        {
            encima = false;
            botonEncima = 0;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            int nuevo = areaEditar.Contains(e.Location) ? 1 : areaEliminar.Contains(e.Location) ? 2 : 0;
            if (nuevo != botonEncima)
            {
                botonEncima = nuevo;
                Invalidate();
            }
            Cursor = nuevo != 0 ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left || !encima) return;
            if (areaEditar.Contains(e.Location)) EditarClick?.Invoke(this, EventArgs.Empty);
            else if (areaEliminar.Contains(e.Location)) EliminarClick?.Invoke(this, EventArgs.Empty);
        }

        //doble clic en cualquier parte de la tarjeta = editar
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (!areaEliminar.Contains(e.Location))
                EditarClick?.Invoke(this, EventArgs.Empty);
        }


        // ---------- Dibujo ----------
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);

            float subir = encima ? 5 : 0;   //translateY(-6px) al pasar el mouse
            var tarjeta = new RectangleF(8, 8 - subir, Width - 17, Height - 26);

            Tema.DibujarSombra(g, tarjeta, Radio, Tema.Tinta, encima ? 12 : 8, encima ? 9 : 8, encima ? 9 : 5);

            using (GraphicsPath forma = Tema.Redondeado(tarjeta, Radio))
            {
                using (var vidrio = new SolidBrush(Color.FromArgb(150, Color.White)))
                    g.FillPath(vidrio, forma);

                DibujarImagen(g, forma, tarjeta);

                using (var borde = new Pen(Color.FromArgb(180, Color.White)))
                    g.DrawPath(borde, forma);
            }

            DibujarInsignia(g, tarjeta);
            DibujarCuerpo(g, tarjeta);
        }


        private void DibujarImagen(Graphics g, GraphicsPath forma, RectangleF tarjeta)
        {
            var area = new RectangleF(tarjeta.X, tarjeta.Y, tarjeta.Width, AltoImagen);
            g.SetClip(forma);
            g.IntersectClip(area);

            if (imagen != null)
            {
                //object-fit: cover -> se recorta la imagen para llenar el area sin deformarla
                float zoom = encima ? 1.06f : 1f;
                float escala = Math.Max(area.Width / imagen.Width, area.Height / imagen.Height) * zoom;
                float w = imagen.Width * escala, h = imagen.Height * escala;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(imagen, area.X + (area.Width - w) / 2, area.Y + (area.Height - h) / 2, w, h);

                if (encima)
                {
                    using (var velo = new LinearGradientBrush(area, Color.FromArgb(0, Tema.Tinta), Color.FromArgb(115, Tema.Tinta), LinearGradientMode.Vertical))
                        g.FillRectangle(velo, area);
                }
            }
            else
            {
                //sin foto: el mismo degradado de respaldo que .row-card-image-wrapper
                using (var respaldo = new LinearGradientBrush(area, Color.FromArgb(46, Tema.Coral), Color.FromArgb(46, Tema.Violeta), LinearGradientMode.ForwardDiagonal))
                    g.FillRectangle(respaldo, area);
            }

            g.ResetClip();
        }


        private void DibujarInsignia(Graphics g, RectangleF tarjeta)
        {
            if (string.IsNullOrEmpty(Insignia)) return;

            Font fuente = Tema.Fuente(7.5f, FontStyle.Bold);
            Size tam = TextRenderer.MeasureText(Insignia, fuente, Size.Empty, TextFormatFlags.NoPadding);
            var r = new RectangleF(tarjeta.X + 14, tarjeta.Y + 14, tam.Width + 22, tam.Height + 10);
            Color color = InsigniaPositiva ? Color.FromArgb(225, Tema.Verde) : Color.FromArgb(230, Tema.Coral);

            Tema.DibujarSombra(g, r, r.Height / 2, Color.Black, 3, 10, 2);
            using (GraphicsPath p = Tema.Redondeado(r, r.Height / 2))
            using (var b = new SolidBrush(color))
                g.FillPath(b, p);

            TextRenderer.DrawText(g, Insignia, fuente, Rectangle.Round(r), Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }


        private void DibujarCuerpo(Graphics g, RectangleF tarjeta)
        {
            int x = (int)tarjeta.X + 22;
            int ancho = (int)tarjeta.Width - 44;
            int y = (int)tarjeta.Y + AltoImagen + 16;

            TextRenderer.DrawText(g, (Categoria ?? "").ToUpper(), Tema.Fuente(7.5f, FontStyle.Bold),
                new Rectangle(x, y, ancho, 16), Tema.Naranja, TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);

            TextRenderer.DrawText(g, Titulo, Tema.Fuente(12.5f, FontStyle.Bold),
                new Rectangle(x, y + 18, ancho, 28), Tema.Tinta, TextFormatFlags.Left | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);

            TextRenderer.DrawText(g, Descripcion, Tema.Fuente(8.75f),
                new Rectangle(x, y + 50, ancho, 56), Tema.Gris, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);

            //pie: linea punteada, numero grande coral y, al pasar el mouse, los botones
            float yPie = tarjeta.Bottom - 62;
            using (var linea = new Pen(Color.FromArgb(76, Tema.Gris)) { DashStyle = DashStyle.Dash })
                g.DrawLine(linea, x, yPie, x + ancho, yPie);

            Font fuenteNumero = Tema.Fuente(15f, FontStyle.Bold);
            int yNumero = (int)yPie + (string.IsNullOrEmpty(Subtexto) ? 16 : 9);
            Size tamNumero = TextRenderer.MeasureText(Numero ?? "", fuenteNumero, Size.Empty, TextFormatFlags.NoPadding);
            TextRenderer.DrawText(g, Numero, fuenteNumero, new Point(x, yNumero), Tema.Coral, TextFormatFlags.NoPadding);
            TextRenderer.DrawText(g, Unidad, Tema.Fuente(8f), new Point(x + tamNumero.Width + 4, yNumero + tamNumero.Height - 15),
                Color.FromArgb(160, 165, 176), TextFormatFlags.NoPadding);

            if (!string.IsNullOrEmpty(Subtexto))
                TextRenderer.DrawText(g, Subtexto, Tema.FuenteSemibold(8f), new Point(x, yNumero + tamNumero.Height + 2),
                    Tema.Naranja, TextFormatFlags.NoPadding);

            float yBoton = yPie + 15;
            areaEliminar = new RectangleF(x + ancho - 78, yBoton, 78, 30);
            areaEditar = new RectangleF(areaEliminar.X - 6 - 66, yBoton, 66, 30);

            if (encima)
            {
                DibujarBotonChico(g, areaEditar, "Editar", botonEncima == 1 ? Tema.NaranjaOscuro : Tema.Naranja);
                DibujarBotonChico(g, areaEliminar, "Eliminar", botonEncima == 2 ? Tema.CoralOscuro : Tema.Coral);
            }
        }


        //.btn-sm con rounded-pill
        private static void DibujarBotonChico(Graphics g, RectangleF r, string texto, Color color)
        {
            using (GraphicsPath p = Tema.Redondeado(r, r.Height / 2))
            using (var b = new SolidBrush(color))
                g.FillPath(b, p);

            TextRenderer.DrawText(g, texto, Tema.FuenteSemibold(8.5f), Rectangle.Round(r), Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
        }
    }
}
