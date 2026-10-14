using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CapaPresentacionWinForms.Utilidades
{
    //muestra una ventana de dialogo con la ventana principal difuminada detras,
    //como .modal-backdrop con backdrop-filter: blur(6px) de la web
    internal static class Dialogos
    {
        public static DialogResult MostrarModal(Form dialogo, Control desde)
        {
            Form ventana = desde?.TopLevelControl as Form ?? Form.ActiveForm;
            if (ventana == null || !ventana.Visible || ventana.WindowState == FormWindowState.Minimized)
                return dialogo.ShowDialog();

            using (var velo = new Velo(ventana))
            {
                velo.Show(ventana);
                return dialogo.ShowDialog(velo);
            }
        }


        //captura lo que se ve de la ventana, lo difumina y lo oscurece un poco
        private class Velo : Form
        {
            public Velo(Form ventana)
            {
                FormBorderStyle = FormBorderStyle.None;
                ShowInTaskbar = false;
                StartPosition = FormStartPosition.Manual;
                Bounds = ventana.Bounds;
                BackColor = Tema.Tinta;
                BackgroundImageLayout = ImageLayout.None;
                BackgroundImage = CapturaDifuminada(ventana.Bounds);
            }

            private static Bitmap CapturaDifuminada(Rectangle area)
            {
                var resultado = new Bitmap(area.Width, area.Height);
                try
                {
                    using (var captura = new Bitmap(area.Width, area.Height))
                    {
                        using (Graphics g = Graphics.FromImage(captura))
                            g.CopyFromScreen(area.Location, Point.Empty, area.Size);

                        //difuminar = reducir mucho la imagen y volver a agrandarla suavizando
                        int w = Math.Max(1, area.Width / 12), h = Math.Max(1, area.Height / 12);
                        using (var chica = new Bitmap(w, h))
                        {
                            using (Graphics g = Graphics.FromImage(chica))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                                g.DrawImage(captura, 0, 0, w, h);
                            }
                            using (Graphics g = Graphics.FromImage(resultado))
                            {
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                g.PixelOffsetMode = PixelOffsetMode.Half;
                                g.DrawImage(chica, new Rectangle(0, 0, area.Width, area.Height));
                                using (var tinte = new SolidBrush(Color.FromArgb(64, Tema.Tinta)))
                                    g.FillRectangle(tinte, 0, 0, area.Width, area.Height);
                            }
                        }
                    }
                }
                catch
                {
                    using (Graphics g = Graphics.FromImage(resultado))
                        g.Clear(Color.FromArgb(120, 120, 125));
                }
                return resultado;
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing) BackgroundImage?.Dispose();
                base.Dispose(disposing);
            }
        }
    }
}
