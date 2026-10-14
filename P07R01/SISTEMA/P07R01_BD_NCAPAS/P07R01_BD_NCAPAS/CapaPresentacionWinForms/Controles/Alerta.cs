using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    public enum TipoAlerta
    {
        Exito,         //borde verde
        Info,          //borde violeta
        Advertencia,   //borde naranja
        Error          //borde coral
    }


    //mensaje dentro de la pantalla, igual que .alert de la web: vidrio blanco con una franja de color
    //a la izquierda y una "x" para cerrarlo. Los de exito e informacion se ocultan solos.
    public class Alerta : Control
    {
        private const int AnchoMaximo = 760;
        private TipoAlerta tipo = TipoAlerta.Info;
        private readonly Timer temporizador = new Timer { Interval = 6000 };
        private Rectangle areaCerrar;

        public Alerta()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(700, 68);
            Visible = false;
            temporizador.Tick += (s, e) => Ocultar();
        }

        [Category("Alerta"), DefaultValue(TipoAlerta.Info)]
        public TipoAlerta Tipo
        {
            get => tipo;
            set { tipo = value; Invalidate(); }
        }

        [Category("Alerta"), DefaultValue(true), Description("Centrar el mensaje con un ancho maximo de 760px.")]
        public bool Centrado { get; set; } = true;


        public void Mostrar(string mensaje, TipoAlerta tipoAlerta)
        {
            Text = mensaje;
            tipo = tipoAlerta;
            Visible = true;
            Invalidate();

            temporizador.Stop();
            if (tipoAlerta == TipoAlerta.Exito || tipoAlerta == TipoAlerta.Info)
                temporizador.Start();
        }

        public void Ocultar()
        {
            temporizador.Stop();
            Visible = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) temporizador.Dispose();
            base.Dispose(disposing);
        }


        private Color ColorFranja
        {
            get
            {
                switch (tipo)
                {
                    case TipoAlerta.Exito: return Tema.Verde;
                    case TipoAlerta.Advertencia: return Tema.Naranja;
                    case TipoAlerta.Error: return Tema.Coral;
                    default: return Tema.Violeta;
                }
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);

            float ancho = Centrado ? Math.Min(AnchoMaximo, Width - 12) : Width - 12;
            float x = Centrado ? (Width - ancho) / 2 : 6;
            var r = new RectangleF(x, 4, ancho, Height - 18);

            Tema.DibujarSombra(g, r, 16);
            using (GraphicsPath path = Tema.Redondeado(r, 16))
            {
                using (var fondo = new SolidBrush(Color.FromArgb(205, Color.White)))
                    g.FillPath(fondo, path);

                g.SetClip(path);
                using (var franja = new SolidBrush(ColorFranja))
                    g.FillRectangle(franja, r.X, r.Y, 5, r.Height);
                g.ResetClip();

                using (var borde = new Pen(Color.FromArgb(180, Color.White)))
                    g.DrawPath(borde, path);
            }

            areaCerrar = new Rectangle((int)(r.Right - 36), (int)r.Y, 30, (int)r.Height);
            var areaTexto = new Rectangle((int)r.X + 20, (int)r.Y, (int)r.Width - 64, (int)r.Height);
            TextRenderer.DrawText(g, Text, Tema.FuenteSemibold(9.75f), areaTexto, Tema.Tinta,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak | TextFormatFlags.EndEllipsis);
            TextRenderer.DrawText(g, "✕", Tema.Fuente(10f), areaCerrar, Tema.Gris,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Cursor = areaCerrar.Contains(e.Location) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (areaCerrar.Contains(e.Location))
                Ocultar();
        }
    }
}
