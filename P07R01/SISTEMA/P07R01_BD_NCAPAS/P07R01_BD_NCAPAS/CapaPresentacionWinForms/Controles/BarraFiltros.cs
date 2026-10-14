using System;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacionWinForms.Controles
{
    //barra de filtros y acciones (.filtros): pastilla de vidrio centrada que acomoda
    //sus controles en una fila, en el orden de su TabIndex, y ajusta su ancho a ellos
    public class BarraFiltros : PanelVidrio
    {
        private const int Separacion = 8;
        private const int MargenLados = 14;

        public BarraFiltros()
        {
            Radio = 999;   //border-radius: 999px (pastilla)
            Height = 70;
            Anchor = AnchorStyles.Top;
        }


        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            var controles = Controls.Cast<Control>().Where(c => c.Visible).OrderBy(c => c.TabIndex).ToList();
            var tarjeta = AreaTarjeta;
            int x = (int)tarjeta.X + MargenLados;

            foreach (Control c in controles)
            {
                c.Left = x;
                c.Top = (int)(tarjeta.Y + (tarjeta.Height - c.Height) / 2);
                x += c.Width + Separacion;
            }

            int ancho = x - Separacion + MargenLados + (int)tarjeta.X;
            if (controles.Count > 0 && Width != ancho)
                Width = ancho;

            Centrar();
        }


        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
                Parent.Resize += (s, a) => Centrar();
        }


        private void Centrar()
        {
            if (Parent != null)
                Left = Math.Max(0, (Parent.ClientSize.Width - Width) / 2);
        }
    }
}
