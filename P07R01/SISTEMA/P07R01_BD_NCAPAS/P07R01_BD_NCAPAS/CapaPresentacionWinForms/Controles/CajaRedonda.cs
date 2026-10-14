using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //contenedor redondeado para UN control de captura (TextBox, ComboBox, NumericUpDown o DateTimePicker),
    //igual que .form-control de los modales: esquinas de 12px, borde gris claro y borde coral al tener el foco.
    //Si tiene Titulo, lo escribe arriba (como el <label>) y el Error a la derecha en coral (como el validador).
    public class CajaRedonda : Panel
    {
        private const int AltoTitulo = 22;
        private const int Anillo = 3;   //espacio para el resplandor de foco (box-shadow 0 0 0 3px)

        private string titulo = "";
        private string error = "";
        private bool pastilla = false;
        private bool conFoco;

        public CajaRedonda()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(450, 62);
        }

        [Category("Campo"), DefaultValue(""), Description("Texto de la etiqueta que va arriba del campo.")]
        public string Titulo
        {
            get => titulo;
            set { titulo = value ?? ""; PerformLayout(); Invalidate(); }
        }

        [Category("Campo"), DefaultValue(""), Description("Mensaje de validacion; si tiene texto el borde se pone coral.")]
        public string Error
        {
            get => error;
            set { error = value ?? ""; Invalidate(); }
        }

        [Category("Campo"), DefaultValue(false), Description("Forma de pastilla, como el filtro de la barra superior.")]
        public bool Pastilla
        {
            get => pastilla;
            set { pastilla = value; Invalidate(); }
        }

        private Control Campo => Controls.Count > 0 ? Controls[0] : null;

        private RectangleF AreaCaja
        {
            get
            {
                int arriba = string.IsNullOrEmpty(titulo) ? 0 : AltoTitulo;
                return new RectangleF(Anillo, arriba + Anillo, Width - Anillo * 2 - 1, Height - arriba - Anillo * 2 - 1);
            }
        }


        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            e.Control.Enter += (s, a) => { conFoco = true; Invalidate(); };
            e.Control.Leave += (s, a) => { conFoco = false; Invalidate(); };
            e.Control.BackColor = Color.White;

            if (e.Control is TextBox caja) caja.BorderStyle = BorderStyle.None;
            if (e.Control is NumericUpDown numero) numero.BorderStyle = BorderStyle.None;
            if (e.Control is ComboBox combo) combo.FlatStyle = FlatStyle.Flat;
        }


        //coloca el control dentro de la caja y le recorta su propio borde con Region
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            Control campo = Campo;
            if (campo == null) return;

            RectangleF caja = AreaCaja;
            int margenX = pastilla ? 16 : 12;
            int ancho = (int)caja.Width - margenX * 2 + (campo is ComboBox || campo is DateTimePicker ? 6 : 0);
            campo.Width = Math.Max(20, ancho);
            campo.Left = (int)caja.X + margenX - (campo is ComboBox || campo is DateTimePicker ? 2 : 0);
            campo.Top = (int)(caja.Y + (caja.Height - campo.Height) / 2);

            int recorte = campo is DateTimePicker ? 2 : campo is ComboBox ? 1 : 0;
            campo.Region = recorte > 0
                ? new Region(new Rectangle(recorte, recorte, campo.Width - recorte * 2, campo.Height - recorte * 2))
                : null;
        }


        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Campo?.Focus();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);

            if (!string.IsNullOrEmpty(titulo))
            {
                var areaTitulo = new Rectangle(Anillo, 0, Width - Anillo * 2, AltoTitulo);
                TextRenderer.DrawText(g, titulo, Tema.FuenteSemibold(9f), areaTitulo, Tema.GrisMedio,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);

                if (!string.IsNullOrEmpty(error))
                    TextRenderer.DrawText(g, error, Tema.Fuente(8.25f), areaTitulo, Tema.Coral,
                        TextFormatFlags.Right | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPadding);
            }

            RectangleF r = AreaCaja;
            float radio = pastilla ? r.Height / 2 : 12;
            bool marcado = conFoco || !string.IsNullOrEmpty(error);

            using (GraphicsPath path = Tema.Redondeado(r, radio))
            {
                if (marcado)
                {
                    var anillo = RectangleF.Inflate(r, Anillo, Anillo);
                    using (GraphicsPath pa = Tema.Redondeado(anillo, radio + Anillo))
                    using (var b = new SolidBrush(Color.FromArgb(38, Tema.Coral)))
                        g.FillPath(b, pa);
                }

                using (var fondo = new SolidBrush(Color.White))
                    g.FillPath(fondo, path);

                Color borde = marcado ? Tema.Coral : Color.FromArgb(64, Tema.Gris);
                using (var pen = new Pen(borde, 1f))
                    g.DrawPath(pen, path);
            }

            if (!string.IsNullOrEmpty(error) && string.IsNullOrEmpty(titulo))
                TextRenderer.DrawText(g, error, Tema.Fuente(8.25f), Rectangle.Round(r), Tema.Coral,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
        }
    }
}
