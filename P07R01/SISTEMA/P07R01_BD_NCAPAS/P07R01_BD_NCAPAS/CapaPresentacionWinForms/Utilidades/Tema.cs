using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CapaPresentacionWinForms.Utilidades
{
    //colores, fuentes y dibujos compartidos; misma paleta que camionesWeb.css (:root)
    internal static class Tema
    {
        // ---------- Paleta ----------
        public static readonly Color Coral = ColorTranslator.FromHtml("#ea4c2a");        //acento principal
        public static readonly Color CoralOscuro = ColorTranslator.FromHtml("#c93a1b");
        public static readonly Color Naranja = ColorTranslator.FromHtml("#ff9800");      //acento secundario
        public static readonly Color NaranjaOscuro = ColorTranslator.FromHtml("#e68900");
        public static readonly Color Verde = ColorTranslator.FromHtml("#00864e");        //disponible / exito
        public static readonly Color VerdeOscuro = ColorTranslator.FromHtml("#006e40");
        public static readonly Color Tinta = ColorTranslator.FromHtml("#1e2229");        //texto principal
        public static readonly Color Crema = ColorTranslator.FromHtml("#fcf9f6");        //fondo
        public static readonly Color Gris = ColorTranslator.FromHtml("#828a99");         //texto secundario
        public static readonly Color GrisMedio = ColorTranslator.FromHtml("#5f6672");
        public static readonly Color Violeta = ColorTranslator.FromHtml("#7b61ff");      //aurora e informacion
        public static readonly Color GrisBoton = ColorTranslator.FromHtml("#6c757d");    //btn-secondary de Bootstrap
        public static readonly Color GrisBotonOscuro = ColorTranslator.FromHtml("#5c636a");

        // ---------- Fuentes ----------
        //la web usa Plus Jakarta Sans y Playfair Display; como no vienen con Windows
        //se usan las equivalentes del sistema: Segoe UI y Georgia
        public const string FuenteTexto = "Segoe UI";
        public const string FuenteElegante = "Georgia";

        private static readonly Dictionary<string, Font> fuentes = new Dictionary<string, Font>();

        //las fuentes se guardan para no crear una nueva en cada repintado
        public static Font Fuente(float tamano, FontStyle estilo = FontStyle.Regular, string familia = FuenteTexto)
        {
            string clave = familia + tamano + estilo;
            if (!fuentes.TryGetValue(clave, out Font fuente))
            {
                fuente = new Font(familia, tamano, estilo);
                fuentes[clave] = fuente;
            }
            return fuente;
        }

        public static Font FuenteSemibold(float tamano)
        {
            return Fuente(tamano, FontStyle.Regular, "Segoe UI Semibold");
        }


        // ---------- Formas ----------

        //rectangulo con esquinas redondeadas (border-radius)
        public static GraphicsPath Redondeado(RectangleF r, float radio)
        {
            radio = Math.Max(0, Math.Min(radio, Math.Min(r.Width, r.Height) / 2f));
            var path = new GraphicsPath();
            if (radio < 0.5f)
            {
                path.AddRectangle(r);
                return path;
            }

            float d = radio * 2;
            path.AddArc(r.X, r.Y, d, d, 180, 90);
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90);
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90);
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }


        //sombra suave (box-shadow): varias capas transparentes que se van haciendo mas grandes
        public static void DibujarSombra(Graphics g, RectangleF r, float radio, Color color, int capas = 8, int alfa = 10, float desplazamientoY = 5)
        {
            for (int i = capas; i >= 1; i--)
            {
                RectangleF capa = r;
                capa.Inflate(i, i);
                capa.Offset(0, desplazamientoY);
                using (GraphicsPath path = Redondeado(capa, radio + i))
                using (var brocha = new SolidBrush(Color.FromArgb(alfa, color)))
                {
                    g.FillPath(brocha, path);
                }
            }
        }


        public static void DibujarSombra(Graphics g, RectangleF r, float radio)
        {
            DibujarSombra(g, r, radio, Tinta);
        }


        //degradado coral -> naranja de .btn-primary, .nav-link-active y los titulos
        public static LinearGradientBrush Degradado(RectangleF r, Color inicio, Color fin)
        {
            if (r.Width < 1) r.Width = 1;
            if (r.Height < 1) r.Height = 1;
            return new LinearGradientBrush(r, inicio, fin, LinearGradientMode.ForwardDiagonal);
        }


        public static void Suavizar(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
        }


        // ---------- Fondo aurora ----------

        //la aurora se dibuja una vez en pequeño y se estira: asi es rapida y queda mas difuminada
        private const int Escala = 6;
        private static Bitmap auroraCache;
        private static Size auroraTamano;

        private static Bitmap Aurora(Size tamano)
        {
            if (auroraCache != null && auroraTamano == tamano)
                return auroraCache;

            int w = Math.Max(1, tamano.Width / Escala);
            int h = Math.Max(1, tamano.Height / Escala);
            var bmp = new Bitmap(w, h);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Crema);

                //mismas posiciones y colores que .aurora-1 ... .aurora-4 (medidas en vw = ancho de la ventana)
                Mancha(g, 0.18f * w, 0.10f * w, 0.48f * w, Coral, 0.50f);
                Mancha(g, w - 0.09f * w, 0.29f * w, 0.42f * w, Naranja, 0.42f);
                Mancha(g, 0.45f * w, h - 0.05f * w, 0.46f * w, Violeta, 0.32f);
                Mancha(g, w - 0.21f * w, h - 0.19f * w, 0.26f * w, Verde, 0.18f);
            }

            auroraCache?.Dispose();
            auroraCache = bmp;
            auroraTamano = tamano;
            return bmp;
        }


        //una mancha de color con el centro fuerte y los bordes transparentes (como filter: blur)
        private static void Mancha(Graphics g, float cx, float cy, float diametro, Color color, float opacidad)
        {
            float radio = diametro * 0.75f;   //un poco mas grande: el blur de la web la extiende
            var rect = new RectangleF(cx - radio, cy - radio, radio * 2, radio * 2);

            using (var path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (var brocha = new PathGradientBrush(path))
                {
                    brocha.CenterColor = Color.FromArgb((int)(255 * opacidad), color);
                    brocha.SurroundColors = new[] { Color.FromArgb(0, color) };
                    brocha.Blend = new Blend
                    {
                        Factors = new[] { 0f, 0.15f, 0.55f, 0.9f, 1f },
                        Positions = new[] { 0f, 0.25f, 0.55f, 0.85f, 1f }
                    };
                    g.FillPath(brocha, path);
                }
            }
        }


        //pinta el pedazo de aurora que le toca a un control, segun su posicion dentro de la ventana;
        //asi el fondo se ve continuo aunque la pantalla este hecha de varios formularios y paneles
        public static void PintarAurora(Graphics g, Control control)
        {
            Control raiz = control.TopLevelControl ?? control;
            Size tamano = raiz.ClientSize;
            if (tamano.Width <= 0 || tamano.Height <= 0)
            {
                g.Clear(Crema);
                return;
            }

            Point posicion = Point.Empty;
            if (raiz != control && control.IsHandleCreated && raiz.IsHandleCreated)
                posicion = raiz.PointToClient(control.PointToScreen(Point.Empty));

            Bitmap aurora = Aurora(tamano);
            var origen = new RectangleF(
                posicion.X / (float)Escala, posicion.Y / (float)Escala,
                control.Width / (float)Escala, control.Height / (float)Escala);

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.DrawImage(aurora, new RectangleF(0, 0, control.Width, control.Height), origen, GraphicsUnit.Pixel);
            g.PixelOffsetMode = PixelOffsetMode.Default;
        }
    }
}
