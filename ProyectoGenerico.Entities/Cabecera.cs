using System;

namespace ProyectoGenerico.Entities
{
    public class Cabecera
    {
        public string NroSeguimiento { get; set; }//= "ECO0058857614";
        public string Remitente { get; set; }//= "5941 - FAMILY CARS";
        public string Destinatario { get; set; }//= "VIAMO";
        public string Domicilio_Destino { get; set; }//= "OTTO KRAUSE 4760, AREA DE PROMOCIÓN EL TRIÁNGULO, BUENOS AIRES, ARGENTINA - 1667 - AREA DE PROMOCIÓN EL TRIÁNGULO - ARGENTINA";
        public string Domicilio_GPS_Destino { get; set; }//= "OTTO KRAUSE 4760, AREA DE PROMOCIÓN EL TRIÁNGULO, BUENOS AIRES, ARGENTINA , AREA DE PROMOCIÓN EL TRIÁNGULO , BUENOS AIRES, ARGENTINA";
        public string Domicilio_Gps_Origen { get; set; }//= "PARDO 2357, BELLA VISTA, PROVINCIA DE BU , BELLA VISTA , BUENOS AIRES, ARGENTINA";
        public string Patente { get; set; }//= "";
        public string Estado { get; set; }//= "PUP";
        public string Motivo { get; set; }//= "Y1";
        public bool Ayuda { get; set; }//= false;
        public string Solicitante { get; set; }//= "0102003190";
        public string TipoTracking { get; set; }//= "ZS01";
        public int PasoActualLineaTiempo { get; set; }//= 0;
        public int CantidadPasosLineaTiempo { get; set; }//= 4;
        public string EstadoDescripcion { get; set; }//= "Finalizado";
        public string EstadoFecha { get; set; }//= "10/05/2023 14:10:34";
        public string DomicilioDestinoLatitud { get; set; }//= "0.0";
        public string DomicilioDestinoLongitud { get; set; }//= "0.0";
        public string Ap { get; set; }//= "40036830"; //TODO revisar con Fede cuando agregue el dato al api de tracking
        public string ApPos { get; set; } // "000010"
        public string CodigoPostalOrigen { get; set; } // "0.0"
        public string CodigoPostalDestino { get; set; }  // "0.0"
        public string CentroStock { get; set; } //A018
        public int PasoActual { get; set; } //0
        public int CantidadPasos { get; set; } //0
        public string Receptor { get; set; } //Luis
        public bool Influencer { get; set; } //false

    }
}