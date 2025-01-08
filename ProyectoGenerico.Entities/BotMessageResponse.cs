using System;

namespace ProyectoGenerico.Entities
{
    public class BotMessageResponse
    {
        public bool derivaAsesor { get; set; }
        public bool error { get; set; }
        public string message { get; set; }
        public string cliente { get; set; }
        public string servicio { get; set; }
        public string estadoDeEnvio { get; set; }
    }
}