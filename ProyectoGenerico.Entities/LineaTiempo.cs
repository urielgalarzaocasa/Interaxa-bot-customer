using System;

namespace ProyectoGenerico.Entities
{
    public class LineaTiempo
    {
        public string Paso { get; set; }// "1",
        public DateTime Fecha_Hora { get; set; }// "01/05/2023 10:00:00",
        public string Tipo_Tracking { get; set; }// "New",
        public string Descripcion { get; set; }// "",
        public string Estado { get; set; }// "PUS",
        public string Motivo { get; set; }// ""

    }
}