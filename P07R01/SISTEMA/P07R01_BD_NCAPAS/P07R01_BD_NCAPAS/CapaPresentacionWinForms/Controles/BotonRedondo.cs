using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //mismos colores que .btn-primary, .btn-success, .btn-danger, .btn-warning,
    //.btn-outline-secondary y .btn-secondary de camionesWeb.css / Bootstrap
    public enum EstiloBoton
    {
        Primario,      //degradado coral -> naranja
        Exito,         //verde
        Peligro,       //coral
        Advertencia,   //naranja
        Contorno,      //blanco translucido con texto gris
        Secundario     //gris (Cancelar)
    }


    //boton dibujado a mano: redondeado (o en forma de pastilla), con degradado,
    //sombra de color y que "sube" 2px al pasar el mouse (transform: translateY(-2px))
    public class BotonRedondo : Button
    {
        private EstiloBoton estilo = EstiloBoton.Primario;
        private bool pastilla = true;
        private bool encima;
        private bool presionado;

        public BotonRedondo()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            AccessibleRole = AccessibleRole.PushButton;   //lectores de pantalla y UI Automation lo ven como boton
            Font = Tema.FuenteSemibold(9.75f);
            Size = new Size(120, 42);
        }

        [Category("Apariencia"), DefaultValue(EstiloBoton.Primario)]
        public EstiloBoton Estilo
        {
            get => estilo;
            set { estilo = value; Invalidate(); }
        }

        [Category("Apariencia"), DefaultValue(true), Description("true = forma de pastilla (rounded-pill); false = esquinas de 8px.")]
        public bool Pastilla
        {
            get => pastilla;
            set { pastilla = value; Invalidate(); }
        }

        //sin el borde de "boton predeterminado" que Windows dibuja al tener el foco
        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        protected override bool ShowFocusCues => false;

        protected override void OnMouseEnter(EventArgs e) { encima = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { encima = false; presionado = false; Invalidate(); base.OnMouseLeave(e); }
        protected override void OnMouseDown(MouseEventArgs e) { presionado = true; Invalidate(); base.OnMouseDown(e); }
        protected override void OnMouseUp(MouseEventArgs e) { presionado = false; Invalidate(); base.OnMouseUp(e); }
        protected override void OnEnabledChanged(EventArgs e) { Invalidate(); base.OnEnabledChanged(e); }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            base.OnPaintBackground(e);   //fondo transparente: se ve lo que hay detras
            Tema.Suavizar(g);

            bool hover = encima && Enabled && !presionado;
            float subir = hover ? 2f : 0f;
            var r = new RectangleF(3, 3 - subir, Width - 7, Height - 10);
            float radio = pastilla ? r.Height / 2f : 8f;

            Color texto = Color.White;
            Color sombra = Color.Empty;

            using (GraphicsPath path = Tema.Redondeado(r, radio))
            {
                Brush fondo;
                switch (estilo)
                {
                    case EstiloBoton.Primario:
                        fondo = hover ? Tema.Degradado(r, Tema.CoralOscuro, Tema.Coral) : Tema.Degradado(r, Tema.Coral, Tema.Naranja);
                        sombra = Tema.Coral;
                        break;
                    case EstiloBoton.Exito:
                        fondo = new SolidBrush(hover ? Tema.VerdeOscuro : Tema.Verde);
                        sombra = Tema.Verde;
                        break;
                    case EstiloBoton.Peligro:
                        fondo = new SolidBrush(hover ? Tema.CoralOscuro : Tema.Coral);
                        break;
                    case EstiloBoton.Advertencia:
                        fondo = new SolidBrush(hover ? Tema.NaranjaOscuro : Tema.Naranja);
                        break;
                    case EstiloBoton.Contorno:
                        fondo = new SolidBrush(Color.FromArgb(hover ? 255 : 180, Color.White));
                        texto = hover ? Tema.Coral : Tema.GrisMedio;
                        break;
                    default:
                        fondo = new SolidBrush(hover ? Tema.GrisBotonOscuro : Tema.GrisBoton);
                        break;
                }

                if (!sombra.IsEmpty && Enabled)
                    Tema.DibujarSombra(g, r, radio, sombra, 4, hover ? 16 : 10, 3);

                g.FillPath(fondo, path);
                fondo.Dispose();

                if (estilo == EstiloBoton.Contorno)
                {
                    using (var borde = new Pen(Color.FromArgb(230, Color.White)))
                        g.DrawPath(borde, path);
                }

                if (!Enabled)
                {
                    using (var velo = new SolidBrush(Color.FromArgb(110, Color.White)))
                        g.FillPath(velo, path);
                }
            }

            TextRenderer.DrawText(g, Text, Font, Rectangle.Round(r), texto,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis | TextFormatFlags.NoPadding);
        }
    }
}
