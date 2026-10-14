using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//CargadorImagenes usa las clases de imagen de WPF para abrir fotos WebP; sin este atributo, WPF
//cambiaria la escala (DPI) de toda la aplicacion la primera vez que se usa y el texto se veria mas grande
[assembly: System.Windows.Media.DisableDpiAwareness]

namespace CapaPresentacionWinForms
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
        }
    }
}
