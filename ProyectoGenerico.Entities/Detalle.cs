using System;

namespace ProyectoGenerico.Entities
{
    public class Detalle
    {
        public DateTime fecha_hora { get; set; } //"10/05/2023 11:44:31",
        public string Descripcion { get; set; } //"Hubo un inconveniente en el retiro del envío. Por favor comunicate al 0810-888-6227 para recibir las indicaciones de los pasos a seguir.",
        public string Descripcion_Corta { get; set; } //"Hubo un inconveniente en el retiro del envío. Por favor comunicate al 0810-888-6227 para recibir las indicaciones de los pasos a seguir.",
        public string Estado { get; set; } //"PUP",
        public string Motivo { get; set; } //"Y1"

    }
}