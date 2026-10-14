using System;
using System.Drawing;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //pie de pagina de vidrio: "© 2026 - Control de Camiones" (la marca en cursiva coral, .footer-brand)
    public class PieDePagina : PanelVidrio
    {
        public PieDePagina()
        {
            Radio = 16;
            Sombra = false;
            Size = new Size(800, 48);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            string inicio = $"© {DateTime.Now.Year} - ";
            Font fuente = Tema.Fuente(9f);
            Size tam = TextRenderer.MeasureText(g, inicio, fuente, Size.Empty, TextFormatFlags.NoPadding);
            int y = (Height - tam.Height) / 2;

            TextRenderer.DrawText(g, inicio, fuente, new Point(24, y), Tema.Gris, TextFormatFlags.NoPadding);
            TextRenderer.DrawText(g, "Control de Camiones", Tema.Fuente(9.5f, FontStyle.Italic | FontStyle.Bold, Tema.FuenteElegante),
                new Point(24 + tam.Width, y - 1), Tema.Coral, TextFormatFlags.NoPadding);
        }
    }
}
