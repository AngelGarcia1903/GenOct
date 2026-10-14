using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace CapaPresentacionWinForms.Controles
{
    //acomoda las tarjetas en columnas que se reparten el ancho disponible, igual que
    //grid-template-columns: repeat(auto-fill, minmax(320px, 1fr)) de .gridview-container-menu
    public class ListaTarjetas : FlowLayoutPanel
    {
        private const int AnchoMinimo = 300;
        private const int Separacion = 14;

        public ListaTarjetas()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.SupportsTransparentBackColor, true);
            BackColor = System.Drawing.Color.Transparent;
            AutoScroll = true;
            WrapContents = true;
        }

        public int Cantidad => Controls.Count;

        public IEnumerable<TarjetaItem> Tarjetas => Controls.OfType<TarjetaItem>();


        //reemplaza las tarjetas actuales por las nuevas y conecta los botones Editar / Eliminar
        public void Mostrar(IEnumerable<TarjetaItem> tarjetas, EventHandler editar, EventHandler eliminar)
        {
            SuspendLayout();

            foreach (Control anterior in Controls.Cast<Control>().ToList())
                anterior.Dispose();
            Controls.Clear();

            foreach (TarjetaItem tarjeta in tarjetas)
            {
                tarjeta.Margin = new Padding(0, 0, Separacion, Separacion);
                tarjeta.EditarClick += editar;
                tarjeta.EliminarClick += eliminar;
                Controls.Add(tarjeta);
            }

            AjustarAnchos();
            ResumeLayout(true);
            AutoScrollPosition = new System.Drawing.Point(0, 0);
        }


        private void AjustarAnchos()
        {
            int disponible = ClientSize.Width - Padding.Horizontal - 2;
            if (disponible <= 0) return;

            int columnas = Math.Max(1, (disponible + Separacion) / (AnchoMinimo + Separacion));
            int ancho = (disponible - Separacion * columnas) / columnas;

            foreach (Control c in Controls)
                if (c.Width != ancho) c.Width = ancho;
        }


        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            AjustarAnchos();
        }


        //el fondo es transparente: al desplazarse hay que repintar todo para que no queden rastros
        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            Invalidate(true);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            Invalidate(true);
        }
    }
}
