namespace CamionesAPI.DTOs
{
    // Respuesta de los GET de lista: los registros de la pagina pedida + datos para navegar.
    // <T> = el tipo de registro (Camiones, Choferes, Rutas): una sola clase sirve para todos.
    public class ResultadoPaginado<T>
    {
        public List<T> Datos { get; set; } = new();
        public int PaginaActual { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public bool TieneAnterior => PaginaActual > 1;
        public bool TieneSiguiente => PaginaActual < TotalPaginas;
    }
}
