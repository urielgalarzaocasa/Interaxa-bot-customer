namespace ProyectoGenerico.Entities
{
    public class TrackingPostal
    {
        public CabeceraPostal Cabecera { get; set; }
        public DetallePostal[] Detalle { get; set; }
        public LineaTiempoPostal[] LineaTiempo { get; set; }
    }
}