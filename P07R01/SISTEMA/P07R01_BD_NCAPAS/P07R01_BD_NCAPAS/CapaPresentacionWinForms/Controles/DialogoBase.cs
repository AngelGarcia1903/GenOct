using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //base de las ventanas de captura y de mensajes, igual que .modal-content de la web:
    //vidrio claro con esquinas redondeadas, encabezado con titulo en negritas y una "x" para cerrar.
    //El encabezado sirve para arrastrar la ventana.
    public class DialogoBase : Form
    {
        protected const int AltoEncabezado = 60;
        protected const int AltoPie = 74;

        private string titulo = "Titulo";
        private Rectangle areaCerrar;
        private bool encimaCerrar;

        public DialogoBase()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw, true);
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;
            ShowInTaskbar = false;
            KeyPreview = true;
            BackColor = Tema.Crema;
            ForeColor = Tema.Tinta;
            Font = Tema.Fuente(9.75f);
        }

        [Category("Dialogo"), DefaultValue("Titulo"), Description("Texto del encabezado (.modal-title).")]
        public string Titulo
        {
            get => titulo;
            set { titulo = value; Text = value; Invalidate(); }
        }


        // ---------- esquinas redondeadas ----------
        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int atributo, ref int valor, int tamano);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //Windows 11 redondea la ventana con suavizado; en Windows 10 se recorta con Region
            int redondo = 2;   //DWMWCP_ROUND
            bool ok = false;
            try { ok = DwmSetWindowAttribute(Handle, 33, ref redondo, sizeof(int)) == 0; } catch { }
            if (!ok) AplicarRegion();
        }

        private void AplicarRegion()
        {
            using (GraphicsPath p = Tema.Redondeado(new RectangleF(0, 0, Width, Height), 20))
                Region = new Region(p);
        }


        // ---------- dibujo ----------
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.PintarAurora(g, this);

            //vidrio claro sobre la aurora (rgba(255,255,255,.78) en la web)
            using (var vidrio = new SolidBrush(Color.FromArgb(205, Color.White)))
                g.FillRectangle(vidrio, ClientRectangle);

            using (var linea = new Pen(Color.FromArgb(38, Tema.Gris)))
            {
                g.DrawLine(linea, 0, AltoEncabezado, Width, AltoEncabezado);
                g.DrawLine(linea, 0, Height - AltoPie, Width, Height - AltoPie);
            }

            using (var borde = new Pen(Color.FromArgb(50, Tema.Gris)))
                g.DrawRectangle(borde, 0, 0, Width - 1, Height - 1);

            TextRenderer.DrawText(g, titulo, Tema.Fuente(12.5f, FontStyle.Bold),
                new Rectangle(24, 0, Width - 90, AltoEncabezado), Tema.Tinta,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            areaCerrar = new Rectangle(Width - 52, (AltoEncabezado - 32) / 2, 32, 32);
            if (encimaCerrar)
            {
                Tema.Suavizar(g);
                using (GraphicsPath p = Tema.Redondeado(areaCerrar, 8))
                using (var b = new SolidBrush(Color.FromArgb(30, Tema.Gris)))
                    g.FillPath(b, p);
            }
            TextRenderer.DrawText(g, "✕", Tema.Fuente(11f), areaCerrar, Tema.GrisMedio,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }


        // ---------- mouse: cerrar y arrastrar ----------
        [DllImport("user32.dll")] private static extern bool ReleaseCapture();
        [DllImport("user32.dll")] private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool encima = areaCerrar.Contains(e.Location);
            if (encima != encimaCerrar)
            {
                encimaCerrar = encima;
                Cursor = encima ? Cursors.Hand : Cursors.Default;
                Invalidate(areaCerrar);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;

            if (areaCerrar.Contains(e.Location))
            {
                DialogResult = DialogResult.Cancel;
            }
            else if (e.Y < AltoEncabezado)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 2, 0);   //WM_NCLBUTTONDOWN + HTCAPTION: Windows mueve la ventana
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Escape)
                DialogResult = DialogResult.Cancel;
        }


        //sin parpadeo al repintar los controles transparentes
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   //WS_EX_COMPOSITED
                cp.ClassStyle |= 0x00020000; //CS_DROPSHADOW
                return cp;
            }
        }
    }
}
