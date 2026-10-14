using System;
using System.Collections.Generic;
using System.Linq;
using CapaEntidades;
using CapaNegocios;
using CapaPresentacionWinForms.Controles;
using CapaPresentacionWinForms.Utilidades;

namespace CapaPresentacionWinForms
{
    //choferes en tarjetas con filtro y botones para agregar, editar y eliminar (igual que ChoferesWeb.aspx)
    public partial class FrmChoferes : FormularioSeccion
    {
        private N_Chofer objNegocio = new N_Chofer();
        private bool cargando = true;   //evita el mensaje de "filtro aplicado" al abrir

        public FrmChoferes()
        {
            InitializeComponent();
        }


        private void FrmChoferes_Load(object sender, EventArgs e)
        {
            cboFiltro.SelectedIndex = 0;   //"Todos"
            CargarChoferes();
            cargando = false;
        }


        //devuelve cuantos choferes se mostraron (-1 si hubo error) para que los botones den un mensaje
        private int CargarChoferes()
        {
            try
            {
                bool? disponibilidad = null;
                if (cboFiltro.SelectedIndex == 1)
                    disponibilidad = true;
                else if (cboFiltro.SelectedIndex == 2)
                    disponibilidad = false;

                List<E_Chofer> lista = objNegocio.ListarChoferes(disponibilidad);
                lstChoferes.Mostrar(lista.Select(CrearTarjeta), Tarjeta_Editar, Tarjeta_Eliminar);

                if (lista.Count == 0)
                    alerta.Mostrar("No se encontraron choferes con el filtro seleccionado", TipoAlerta.Info);
                else
                    alerta.Ocultar();

                return lista.Count;
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al cargar choferes: " + ex.Message, TipoAlerta.Error);
                return -1;
            }
        }


        //una tarjeta por chofer, con los mismos datos que la tarjeta de la web
        private static TarjetaItem CrearTarjeta(E_Chofer c)
        {
            return new TarjetaItem
            {
                UrlImagen = c.UrlFoto,
                Insignia = c.Disponibilidad ? "Disponible" : "No disponible",
                InsigniaPositiva = c.Disponibilidad,
                Categoria = "Licencia " + c.Licencia,
                Titulo = c.NombreCompleto,
                Descripcion = $"Teléfono: {c.Telefono}\nNacimiento: {c.FechaNacimiento:dd/MM/yyyy}",
                Numero = $"#{c.IdChofer}",
                Unidad = "ID",
                Dato = c
            };
        }


        //tambien se ejecuta al cambiar el filtro (como el AutoPostBack del DropDownList)
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            int total = CargarChoferes();
            if (total > 0)
                alerta.Mostrar($"Filtro aplicado: {total} chofer(es) — {cboFiltro.Text}", TipoAlerta.Info);
        }

        private void cboFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cargando)
                btnFiltrar_Click(sender, e);
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            cargando = true;
            cboFiltro.SelectedIndex = 0;
            cargando = false;

            int total = CargarChoferes();
            if (total > 0)
                alerta.Mostrar($"Lista actualizada: {total} chofer(es) a las {DateTime.Now:HH:mm:ss}", TipoAlerta.Exito);
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            using (var frm = new FrmChoferEditar())
            {
                if (Dialogos.MostrarModal(frm, this) == System.Windows.Forms.DialogResult.OK)
                {
                    CargarChoferes();
                    alerta.Mostrar("Chofer agregado correctamente", TipoAlerta.Exito);
                }
            }
        }


        private void Tarjeta_Editar(object sender, EventArgs e)
        {
            var seleccionado = (E_Chofer)((TarjetaItem)sender).Dato;

            try
            {
                //se vuelve a pedir a la BD para editar los datos actuales
                E_Chofer chofer = objNegocio.ObtenerChoferPorID(seleccionado.IdChofer);

                using (var frm = new FrmChoferEditar(chofer))
                {
                    if (Dialogos.MostrarModal(frm, this) == System.Windows.Forms.DialogResult.OK)
                    {
                        CargarChoferes();
                        alerta.Mostrar("Datos del chofer actualizados correctamente", TipoAlerta.Exito);
                    }
                }
            }
            catch (Exception ex)
            {
                alerta.Mostrar("Error al editar el chofer: " + ex.Message, TipoAlerta.Error);
            }
        }


        private void Tarjeta_Eliminar(object sender, EventArgs e)
        {
            var chofer = (E_Chofer)((TarjetaItem)sender).Dato;

            if (!Mensajes.ConfirmarEliminar("Eliminar chofer", $"¿Seguro que desea eliminar al chofer {chofer.NombreCompleto}?", this))
                return;

            string resultado = objNegocio.EliminarChofer(chofer.IdChofer);
            CargarChoferes();

            if (resultado.ToUpper() == "OK")
                alerta.Mostrar("Chofer eliminado correctamente", TipoAlerta.Exito);
            else
                alerta.Mostrar(Mensajes.Traducir(resultado), TipoAlerta.Advertencia);
        }

    }//fin de clase
}//fin de namespace
