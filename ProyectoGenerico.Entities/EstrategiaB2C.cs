namespace ProyectoGenerico.Entities
{
    public class EstrategiaB2C
    {
        private int Id { get; set; }
        public string Cliente { get; set; }
        public string Solicitante { get; set; }
        public string Servicio { get; set; }
        public string Ap { get; set; }
        public string EstadoDeEnvio { get; set; }
        public string MotivoPOD { get; set; }
        public string UltimaFechaDeActualizacionEnSap { get; set; }
        public string CentroStock { get; set; }
        public string Destino { get; set; }
        public string Visitas { get; set; }
        public string RespuestaSinDemora { get; set; }
        public string RespuestaConDemora { get; set; }
        public string TextosUXHC { get; set; }

    }
}