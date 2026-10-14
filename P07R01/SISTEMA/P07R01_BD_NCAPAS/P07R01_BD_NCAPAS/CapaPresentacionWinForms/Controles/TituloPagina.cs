using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //titulo de cada pagina, igual que .menu-subtitle + .menu-main-title de la web:
    //   Flotilla              <- cursiva coral
    //   Nuestros camiones     <- la segunda parte con degradado coral -> naranja
    public class TituloPagina : Control
    {
        private string subtitulo = "Subtitulo";
        private string titulo = "Titulo";
        private string resaltado = "resaltado";
        private bool centrado = false;

        public TituloPagina()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            Size = new Size(600, 96);
        }

        [Category("Titulo"), DefaultValue("Subtitulo")]
        public string Subtitulo { get => subtitulo; set { subtitulo = value; Invalidate(); } }

        [Category("Titulo"), DefaultValue("Titulo")]
        public string Titulo { get => titulo; set { titulo = value; Invalidate(); } }

        [Category("Titulo"), DefaultValue("resaltado"), Description("Parte del titulo que va con degradado.")]
        public string Resaltado { get => resaltado; set { resaltado = value; Invalidate(); } }

        [Category("Titulo"), DefaultValue(false)]
        public bool Centrado { get => centrado; set { centrado = value; Invalidate(); } }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Tema.Suavizar(g);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            Font fuenteSub = Tema.Fuente(14f, FontStyle.Italic | FontStyle.Bold, Tema.FuenteElegante);
            Font fuenteTitulo = Tema.Fuente(30f, FontStyle.Bold);
            StringFormat formato = StringFormat.GenericTypographic;

            SizeF tamSub = g.MeasureString(subtitulo ?? "", fuenteSub, int.MaxValue, formato);
            string parte1 = string.IsNullOrEmpty(titulo) ? "" : titulo + " ";
            SizeF tam1 = g.MeasureString(parte1.Replace(' ', ' '), fuenteTitulo, int.MaxValue, formato);
            SizeF tam2 = g.MeasureString(resaltado ?? "", fuenteTitulo, int.MaxValue, formato);

            float xSub = centrado ? (Width - tamSub.Width) / 2 : 2;
            float xTitulo = centrado ? (Width - tam1.Width - tam2.Width) / 2 : 0;
            float ySub = 4;
            float yTitulo = ySub + tamSub.Height + 4;

            using (var coral = new SolidBrush(Tema.Coral))
                g.DrawString(subtitulo, fuenteSub, coral, xSub, ySub, formato);

            using (var tinta = new SolidBrush(Tema.Tinta))
                g.DrawString(parte1, fuenteTitulo, tinta, xTitulo, yTitulo, formato);

            if (!string.IsNullOrEmpty(resaltado))
            {
                var area = new RectangleF(xTitulo + tam1.Width, yTitulo, tam2.Width + 1, tam2.Height + 1);
                using (var degradado = new LinearGradientBrush(area, Tema.Coral, Tema.Naranja, LinearGradientMode.Horizontal))
                    g.DrawString(resaltado, fuenteTitulo, degradado, area.X, area.Y, formato);
            }
        }
    }
}
