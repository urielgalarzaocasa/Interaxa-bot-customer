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
        public bool influencer { get; set; }

        public BotMessageResponse ErrorResponse()
        {
            BotMessageResponse botMessageResponse = new BotMessageResponse
            {
                derivaAsesor = false, //true, -> original
                error = true,
                message = "Derivar al asesor."
            };

            return botMessageResponse;
        }

        public BotMessageResponse CustomErrorResponse(bool derivaAsesor, bool error, string message)
        {
            BotMessageResponse botMessageResponse = new BotMessageResponse
            {
                derivaAsesor = false, //derivaAsesor, -> original
                error = error,
                message = message
            };

            return botMessageResponse;
        }
    }
}