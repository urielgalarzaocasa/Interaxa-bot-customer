namespace ProyectoGenerico.Entities
{
    public class Tracking
    {
        public Cabecera Cabecera { get; set; }
        public Detalle[] Detalle { get; set; }
        public LineaTiempo[] LineaTiempo { get; set; }
    }
}