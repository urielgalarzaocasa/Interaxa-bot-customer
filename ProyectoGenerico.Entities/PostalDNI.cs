using System;

namespace ProyectoGenerico.Entities
{
    public class PostalDNI
    {
        public string NroSeguimiento { get; set; }
        public string TipoTarjeta { get; set; }
        public string Destinatario { get; set; }
        public string Remitente { get; set; }

    }
}

//Procedimiento con el viejo postal
//PIMP_TrackingPostalDNI 11529190
//NroSeguimiento TipoTarjeta	Destinatario	Remitente
//PST0065232037	TC CABAL	BERMUDEZ SUSANA	BANCO DE LA CIUDAD DE BUENOS AIRES
//PST0060459390	TC CABAL	BERMUDEZ SUSANA	BANCO DE LA CIUDAD DE BUENOS AIRES
//PST0059819082	VISA / TARJETAS CREDITO	SCIAINI/VICTOR HUGO	BANCO DE LA CIUDAD DE BUENOS AIRES
//PST0059819083	VISA / TARJETAS CREDITO	BERMUDEZ/SUSANA	BANCO DE LA CIUDAD DE BUENOS AIRES

//Procedimiento que me pasó fede bal por slack despues del "Evento"
//ECO_TrackingPostalDNI 41080772
//select top 100 Pieza_Ocasa as 'NroSeguimiento', Pieza_Tipo as 'TipoTarjeta', Destinatario, Distribuidor as 'Remitente' from DW.Fact_Postmaster_Datos d where d.Dni=@DNI;
//NroSeguimiento   TipoTarjeta	        Destinatario	      Remitente
//PST83095542	   TARJETA DE CREDITO	PERALTA MICAELA AIL	  OCASA DISTRIBUCION POSTAL
//PST83069338	   TARJETA DE CREDITO	PERALTA MICAELA AIL	  OCASA DISTRIBUCION POSTAL
//PST83069334	   TARJETA DE CREDITO	PERALTA MICAELA AIL	  OCASA DISTRIBUCION POSTAL
//PST83123169	   TARJETA DE CREDITO	PERALTA/MICAELA A	  OCASA DISTRIBUCION POSTAL