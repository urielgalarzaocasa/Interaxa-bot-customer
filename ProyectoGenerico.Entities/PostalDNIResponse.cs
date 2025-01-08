using System;

namespace ProyectoGenerico.Entities
{
    public class PostalDNIResponse
    {
        public bool derivaAsesor { get; set; }
        public bool error { get; set; }
        public Array message { get; set; }

        public PostalDNIResponse(bool derivaAsesor, bool error, Array message)
        {
            this.derivaAsesor = derivaAsesor;
            this.error = error;
            this.message = message;
        }
    }
}