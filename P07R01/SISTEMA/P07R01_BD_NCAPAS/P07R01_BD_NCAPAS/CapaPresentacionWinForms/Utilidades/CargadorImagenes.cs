using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CapaPresentacionWinForms.Utilidades
{
    //descarga las fotos de las URL guardadas en la BD sin congelar la ventana,
    //y las guarda en memoria para no descargar la misma foto dos veces
    internal static class CargadorImagenes
    {
        private static readonly Dictionary<string, Task<Image>> descargas = new Dictionary<string, Task<Image>>();
        private static readonly HttpClient cliente;

        static CargadorImagenes()
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            cliente = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
            //algunos sitios de imagenes rechazan peticiones sin navegador (User-Agent)
            cliente.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) CamionesWinForms/1.0");
        }


        //devuelve la imagen, o null si la URL no es valida, no responde o no es una imagen
        public static Task<Image> ObtenerAsync(string url)
        {
            if (string.IsNullOrWhiteSpace(url) || !Uri.IsWellFormedUriString(url.Trim(), UriKind.Absolute))
                return Task.FromResult<Image>(null);

            url = url.Trim();
            lock (descargas)
            {
                if (!descargas.TryGetValue(url, out Task<Image> tarea))
                {
                    tarea = Descargar(url);
                    descargas[url] = tarea;
                }
                return tarea;
            }
        }


        private static async Task<Image> Descargar(string url)
        {
            byte[] bytes;
            try
            {
                bytes = await cliente.GetByteArrayAsync(url).ConfigureAwait(false);
            }
            catch
            {
                return null;   //sin imagen: la tarjeta muestra el degradado de respaldo
            }

            return LeerConGdi(bytes) ?? LeerConWic(bytes);
        }


        //JPG, PNG, GIF y BMP: los lee GDI+ (System.Drawing) directamente
        private static Image LeerConGdi(byte[] bytes)
        {
            try
            {
                using (var memoria = new MemoryStream(bytes))
                using (Image original = Image.FromStream(memoria))
                {
                    //copia independiente del stream (Image.FromStream necesita el stream abierto)
                    return new Bitmap(original);
                }
            }
            catch
            {
                return null;
            }
        }


        //WebP (lo que entrega Bing) no lo entiende GDI+; se decodifica con los codecs de Windows (WIC)
        //a traves de las clases de imagen de WPF y se convierte a PNG para que WinForms lo pueda dibujar
        private static Image LeerConWic(byte[] bytes)
        {
            try
            {
                using (var entrada = new MemoryStream(bytes))
                using (var salida = new MemoryStream())
                {
                    BitmapDecoder decodificador = BitmapDecoder.Create(entrada, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                    var codificador = new PngBitmapEncoder();
                    codificador.Frames.Add(BitmapFrame.Create(decodificador.Frames[0]));
                    codificador.Save(salida);
                    return LeerConGdi(salida.ToArray());
                }
            }
            catch
            {
                return null;   //formato que Windows tampoco conoce
            }
        }
    }
}
