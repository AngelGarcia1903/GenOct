using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //enlace del menu lateral (.sidebar-nav .nav-link):
    //normal = texto gris; mouse encima = fondo blanco, texto coral y barra de color a la izquierda;
    //activo = pastilla con degradado coral -> naranja (.nav-link-active)
    public class BotonMenu : Button
    {
        private bool activo;
        private bool encima;

        public BotonMenu()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor = Cursors.Hand;
            AccessibleRole = AccessibleRole.PushButton;   //lectores de pantalla y UI Automation lo ven como boton
            Font = Tema.FuenteSemibold(10f);
            Size = new Size(220, 54);
        }

        [Category("Apariencia"), DefaultValue(false), Description("Marca la seccion que esta abierta.")]
        public bool Activo
        {
            get => activo;
            set { activo = value; Invalidate(); }
        }

        public override void NotifyDefault(bool value)
        {
            base.NotifyDefault(false);
        }

        protected override bool ShowFocusCues => false;

        protected override void OnMouseEnter(EventArgs e) { encima = true; Invalidate(); base.OnMouseEnter(e); }
        protected override void OnMouseLeave(EventArgs e) { encima = false; Invalidate(); base.OnMouseLeave(e); }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            base.OnPaintBackground(e);
            Tema.Suavizar(g);

            var r = new RectangleF(2, 4, Width - 5, Height - 12);
            Color colorTexto = Tema.GrisMedio;
            Font fuente = Font;
            float sangria = 0;

            using (GraphicsPath path = Tema.Redondeado(r, 16))
            {
                if (activo)
                {
                    Tema.DibujarSombra(g, r, 16, Tema.Coral, 6, 12, 5);
                    using (Brush b = Tema.Degradado(r, Tema.Coral, Tema.Naranja))
                        g.FillPath(b, path);
                    colorTexto = Color.White;
                    fuente = Tema.Fuente(10f, FontStyle.Bold);
                }
                else if (encima)
                {
                    sangria = 4;   //translateX(4px)
                    using (var b = new SolidBrush(Color.FromArgb(155, Color.White)))
                        g.FillPath(b, path);

                    g.SetClip(path);
                    var barra = new RectangleF(r.X, r.Y + r.Height * 0.2f, 4, r.Height * 0.6f);
                    using (GraphicsPath pb = Tema.Redondeado(barra, 2))
                    using (var b = new LinearGradientBrush(barra, Tema.Coral, Tema.Naranja, LinearGradientMode.Vertical))
                        g.FillPath(b, pb);
                    g.ResetClip();
                    colorTexto = Tema.Coral;
                }
            }

            var areaTexto = new Rectangle((int)(r.X + 20 + sangria), (int)r.Y, (int)r.Width - 24, (int)r.Height);
            TextRenderer.DrawText(g, Text, fuente, areaTexto, colorTexto,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.NoPadding);
        }
    }
}
