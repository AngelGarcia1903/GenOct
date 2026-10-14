using System.Drawing;
using System.Windows.Forms;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms.Controles
{
    //ventana de mensaje y de confirmacion con el mismo estilo que los modales de la web
    //(por ejemplo "Eliminar camion" / "¿Seguro que desea eliminar este camion?")
    public class FrmMensaje : DialogoBase
    {
        private readonly Label lblTexto = new Label();
        private readonly BotonRedondo btnAceptar = new BotonRedondo();
        private readonly BotonRedondo btnCancelar = new BotonRedondo();

        private FrmMensaje(string titulo, string mensaje, Color franja, string textoAceptar, EstiloBoton estiloAceptar, bool conCancelar)
        {
            Titulo = titulo;
            ClientSize = new Size(460, 230);

            lblTexto.Text = mensaje;
            lblTexto.Font = Tema.Fuente(10f);
            lblTexto.ForeColor = Tema.Tinta;
            lblTexto.BackColor = Color.Transparent;
            lblTexto.Location = new Point(26, AltoEncabezado + 22);
            lblTexto.Size = new Size(ClientSize.Width - 52, ClientSize.Height - AltoEncabezado - AltoPie - 30);
            Controls.Add(lblTexto);

            btnAceptar.Text = textoAceptar;
            btnAceptar.Estilo = estiloAceptar;
            btnAceptar.Pastilla = false;
            btnAceptar.Size = new Size(116, 46);
            btnAceptar.Location = new Point(ClientSize.Width - 24 - btnAceptar.Width, ClientSize.Height - AltoPie + 14);
            btnAceptar.DialogResult = DialogResult.OK;
            Controls.Add(btnAceptar);
            AcceptButton = btnAceptar;

            if (conCancelar)
            {
                btnCancelar.Text = "Cancelar";
                btnCancelar.Estilo = EstiloBoton.Secundario;
                btnCancelar.Pastilla = false;
                btnCancelar.Size = new Size(116, 46);
                btnCancelar.Location = new Point(btnAceptar.Left - 8 - btnCancelar.Width, btnAceptar.Top);
                btnCancelar.DialogResult = DialogResult.Cancel;
                Controls.Add(btnCancelar);
                CancelButton = btnCancelar;
            }

            Franja = franja;
        }

        private Color Franja { get; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var b = new SolidBrush(Franja))
                e.Graphics.FillRectangle(b, 0, AltoEncabezado + 1, 5, ClientSize.Height - AltoEncabezado - AltoPie - 1);
        }


        public static void Mostrar(string titulo, string mensaje, Color color, IWin32Window ventana = null)
        {
            using (var frm = new FrmMensaje(titulo, mensaje, color, "Aceptar", EstiloBoton.Primario, false))
                Dialogos.MostrarModal(frm, ventana as Control);
        }

        public static bool Confirmar(string titulo, string mensaje, string textoAceptar, EstiloBoton estilo, Color color, IWin32Window ventana = null)
        {
            using (var frm = new FrmMensaje(titulo, mensaje, color, textoAceptar, estilo, true))
                return Dialogos.MostrarModal(frm, ventana as Control) == DialogResult.OK;
        }
    }
}
