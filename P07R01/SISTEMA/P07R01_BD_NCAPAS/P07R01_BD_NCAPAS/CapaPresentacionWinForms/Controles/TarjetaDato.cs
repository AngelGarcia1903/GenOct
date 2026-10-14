using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //tarjeta de indicador del Inicio (.dash-card): etiqueta naranja, numero grande y detalle gris.
    //Si se le asigna el evento Click, se comporta como enlace: sube y muestra "Ver detalle →"
    public class TarjetaDato : PanelVidrio
    {
        private string etiqueta = "ETIQUETA";
        private string valor = "0";
        private string detalle = "detalle";
        private bool encima;

        public TarjetaDato()
        {
            BrilloEsquina = true;
            Size = new Size(240, 140);
        }

        [Category("Dato"), DefaultValue("ETIQUETA")]
        public string Etiqueta { get => etiqueta; set { etiqueta = value; Invalidate(); } }

        [Category("Dato"), DefaultValue("0")]
        public string Valor { get => valor; set { valor = value; Invalidate(); } }

        [Category("Dato"), DefaultValue("detalle")]
        public string Detalle { get => detalle; set { detalle = value; Invalidate(); } }

        [Category("Dato"), DefaultValue(false), Description("Muestra 'Ver detalle' al pasar el mouse.")]
        public bool EsEnlace { get; set; }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!EsEnlace) return;
            encima = true;
            Cursor = Cursors.Hand;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            encima = false;
            Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            //al pasar el mouse la tarjeta sube un poco (translateY(-5px))
            if (encima) e.Graphics.TranslateTransform(0, -3);
            base.OnPaint(e);

            Graphics g = e.Graphics;
            RectangleF r = AreaTarjeta;
            int x = (int)r.X + 22;
            int ancho = (int)r.Width - 40;

            TextRenderer.DrawText(g, (etiqueta ?? "").ToUpper(), Tema.Fuente(7.5f, FontStyle.Bold),
                new Rectangle(x, (int)r.Y + 20, ancho, 32), Tema.Naranja, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);

            Font fuenteValor = Tema.Fuente(26f, FontStyle.Bold);
            int yValor = (int)r.Y + (etiqueta != null && etiqueta.Length > 18 ? 48 : 36);
            TextRenderer.DrawText(g, valor, fuenteValor, new Point(x - 3, yValor), Tema.Tinta, TextFormatFlags.NoPadding);

            TextRenderer.DrawText(g, detalle, Tema.Fuente(8.75f),
                new Rectangle(x, yValor + 48, ancho, 36), Tema.Gris, TextFormatFlags.Left | TextFormatFlags.WordBreak | TextFormatFlags.NoPadding);

            if (encima)
                TextRenderer.DrawText(g, "Ver detalle →", Tema.Fuente(8f, FontStyle.Bold),
                    new Rectangle(x, (int)r.Bottom - 30, ancho, 20), Tema.Coral, TextFormatFlags.Right | TextFormatFlags.NoPadding);
        }
    }
}
