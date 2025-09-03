using System;
using ProyectoGenerico.Data;
using ProyectoGenerico.Helper;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Services;
using Microsoft.SqlServer.Server;

namespace ProyectoGenerico.BusinessRules
{
    public class BotMessageBusinessRules
    {
        private Tracking Tracking { get; set; }
        private string Seguimiento { get; set; }
        private string Trackingtype {  get; set; }
        public BotMessageBusinessRules(string seguimiento,string trackingtype) 
        {
            this.Tracking = TrackingService.GetTracking(seguimiento, trackingtype);
            this.Seguimiento = seguimiento;
            this.Trackingtype = trackingtype;
        }

        public BotMessageResponse GetMessage()
        {
            if (Tracking.Cabecera == null) 
                return Response(true, true, "Derivar a un asesor");
            
            try
            {
                LogHelper.GetInstance().PrintDebug("-- BotMessageBusinessRules inicio --");

                bool Influencer = Tracking.Cabecera.Influencer;

                EstrategiaB2C estrategiaQuery = new EstrategiaB2C
                {
                    Solicitante = this.GetSolicitante(),
                    Servicio = this.GetServicio(),
                    EstadoDeEnvio = this.GetEstadoDeEnvio(),
                    MotivoPOD = this.GetMotivoPOD(),
                    CentroStock = this.GetCentroStock(),
                    Destino = this.GetDestino(),
                    Visitas = this.GetVisitas(),
                    Ap = this.GetAP(),
                    Centro = this.GetCentro()
                };

                DebugLog(estrategiaQuery);

                EstrategiaB2CData estrategiaB2CData = new EstrategiaB2CData();
                EstrategiaB2CResponse estrategiaResponse = estrategiaB2CData.Get(estrategiaQuery);

                ValidarRespuesta(estrategiaResponse);

                string message = estrategiaResponse.Texto;
                bool derivaAsesor = ExisteDemora(estrategiaResponse.HoraHabilesFin);
                
                TrackCentro trackCentro = estrategiaB2CData.GetTrackCentro(Tracking.Cabecera.CentroStock);

                if(!derivaAsesor)
                    message = ReplaceMessageDynamicValues(estrategiaResponse.NombreCliente, message, trackCentro);

                BotMessageResponse botMessageResponse = new BotMessageResponse
                {
                    message = message,
                    cliente = estrategiaResponse.Cliente,
                    servicio = estrategiaQuery.Servicio,
                    estadoDeEnvio = estrategiaQuery.EstadoDeEnvio,
                    derivaAsesor = derivaAsesor,
                    error = false,
                    influencer = Influencer
                };

                return botMessageResponse;

            } catch (Exception ex){

                if (!this.Tracking.Cabecera.NroSeguimiento.IsNullOrEmpty())
                    LogHelper.GetInstance().PrintError("BotMessageBusinessRules: " + this.Tracking.Cabecera.NroSeguimiento + ex.Message); 
                
                if (!this.Tracking.Detalle[0].Descripcion.IsNullOrEmpty() ) 
                    return Response(false, false, this.Tracking.Detalle[0].Descripcion);

                return Response(true, true, "Derivar a un asesor");
            }
        }

        private void ValidarRespuesta(EstrategiaB2CResponse estrategiaResponse)
        {
            if (estrategiaResponse == null) throw new Exception("La consulta no devolvió resultados.");
            if (estrategiaResponse.Fecha == null) { throw new Exception("La consulta no devolvió el campo Fecha de Ultima Actualización."); }
        }

        private void DebugLog(EstrategiaB2C estrategiaQuery)
        {
            LogHelper.GetInstance().PrintDebug($"Seguimiento: {this.Seguimiento}, Solicitante: {estrategiaQuery.Solicitante}, AP: {estrategiaQuery.Ap}, Servicio: {estrategiaQuery.Servicio}, EstadoDeEnvio: {estrategiaQuery.EstadoDeEnvio}, MotivoPOD: {estrategiaQuery.MotivoPOD}, CentroStock: {estrategiaQuery.CentroStock}, Destino: {estrategiaQuery.Destino}, Visitas: {estrategiaQuery.Visitas}");
        }

        private string ReplaceMessageDynamicValues(string nombreCliente, string message, TrackCentro trackCentro)
        {
            string messageResponse = message;
            messageResponse = message.Replace("[REMITENTE]", nombreCliente);
            messageResponse = messageResponse.Replace("(Fecha del Z4)", this.GetUltimaFechaDelDetalle().ToString("dd/MM/yyyy HH:mm:ss"));
            messageResponse = messageResponse.Replace("(Nombre del receptor)", this.GetReceptor());
            //messageResponse = messageResponse.Replace("(Entrecalles - Fachada - Observacion particular de la vivienda - Nombre del Barrio Cerrado)", GetDomicilioDestino());
            messageResponse = messageResponse.Replace("[nombre de destinatario]", GetDestinatario());
            messageResponse = messageResponse.Replace("[Domicilio]", GetDomicilioDestino());
            messageResponse = messageResponse.Replace("[FECHA EVENTO BOVEDA]", this.GetUltimaFechaDelDetalle().ToString("dd/MM/yyyy HH:mm:ss"));
            messageResponse = messageResponse.Replace("[CIUDAD, DOMICILIO, HORARIO]", this.GetDomicilioHorarioSucursal(trackCentro));
            return messageResponse;
        }

        private string GetDomicilioHorarioSucursal(TrackCentro trackCentro)
        { 
            return string.Concat(trackCentro.Direccion, " - ", trackCentro.Horario);
        }

        private DateTime ParseFecha(string myDate)
        {
            DateTime fechaParseada = DateTime.ParseExact(myDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            return fechaParseada;
        }

        private bool ExisteDemora(string horasDeDemora)
        {
            if (horasDeDemora == "N/A") return false;
            if (horasDeDemora == "+N/A") return false;
            if (horasDeDemora == "-N/A") return false;
            int horasPermitidas = ParseUltimaFechaDeActualizacion(horasDeDemora);
            int horasReales = int.Parse(this.GetUltimaFechaDeActualizacion());
            if(horasReales > horasPermitidas) return true;

            return false;
        }

        private int ParseUltimaFechaDeActualizacion(string horasDeDemora)
        {
            string fechaTrimed = horasDeDemora.Replace(" ", "");
            string decena = fechaTrimed[1].ToString();
            string unidad = fechaTrimed[2].ToString();
            string horasString = string.Concat(decena, unidad);
            int horas = int.Parse(horasString);

            return horas;
        }
       
        private BotMessageResponse Response(bool derivaAsesor, bool error, string message)
        {
            BotMessageResponse botMessageResponse = new BotMessageResponse
            {
                derivaAsesor = derivaAsesor,
                error = error,
                message = message
            };

            return botMessageResponse;
        }
        private string GetDestinatario()
        {
            return this.Tracking.Cabecera.Destinatario;
        }
        private string GetDomicilioDestino()
        {
            return this.Tracking.Cabecera.Domicilio_Destino;
        }
        private string GetReceptor()
        {
            return this.Tracking.Cabecera.Receptor;
        }

        /*
         * Si el código de seguimiento empieza por "ECO" significa que entró por ecommerce, de lo contrario entró por paquetería.
         */
        private string GetServicio()
        {
            string nroSeguimientoTrimed = Tracking.Cabecera.NroSeguimiento.Replace(" ", "");
            char char1 = nroSeguimientoTrimed[0];
            char char2 = nroSeguimientoTrimed[1];
            char char3 = nroSeguimientoTrimed[2];
            string eco = string.Concat(char1, char2, char3);

            if (eco.ToUpper() == "PST") 
                return "";

            if (!int.TryParse(eco, out int result)) 
                return "PAQUETERIA";

            return "ECOMMERCE";
        }

        private string GetSolicitante()
        {
            return this.Tracking.Cabecera.Solicitante;
        }

        private string GetVisitas()
        {
            int visitas = 0;
            foreach (var detalle in Tracking.Detalle)
            {
                if (detalle.Estado == "POD" || detalle.Estado == "10") visitas++;
            }
            return visitas.ToString();
        }

        private string GetDestino() 
        {
            return "";
        }

        private string GetCentroStock() 
        {
            //Si el texto contiene el string o parte del string: "El pedido llegó al centro de distribución OCASA [sucursal], responsable de la entrega a destino" y un estado HFD es ULTIMAMILLA
            //De lo contrario es PREULTIMAMILLA
            //EL ENVÍO HA LLEGADO AL CENTRO DE DISTRIBUCIÓN OCASA [SUCURSAL]. PREULTIMAMILLA

            string ultimamilla = "El pedido llegó al centro de distribución OCASA";
            
            foreach (var detalle in Tracking.Detalle)
            {
                if (detalle.Estado == "HFD" && detalle.Descripcion.ToLower().Trim().Contains(ultimamilla.ToLower().Trim())) 
                    return "ULTIMA MILLA";
            }

            return "PRE ULTIMA MILLA";
        }

        private DateTime GetUltimaFechaDelDetalle()
        {
            return Tracking.Detalle[0].fecha_hora;
        }

        private string GetUltimaFechaDeActualizacion()
        {
            DateTime fechaDelTracking = GetUltimaFechaDelDetalle();
            DateTime fechaActual = DateTime.Now;
            TimeSpan diferenciaDeFechas = fechaActual - fechaDelTracking;
            int diferenciaEnDias = (int)diferenciaDeFechas.TotalHours;

            return diferenciaEnDias.ToString();
        }

        private string GetMotivoPOD()
        {
            return Tracking.Cabecera.Motivo == "0" ? "" : Tracking.Cabecera.Motivo;
        }

        private string GetEstadoDeEnvio()
        {
            return this.Tracking.Cabecera.Estado; 
            //if(!this.estados.ContainsKey(estadoId)) { throw new Exception("No existe el estado: " + estadoId); }
            //return this.estados[estadoId].ToUpper();
        }

        private string GetAP()
        {
            return Tracking.Cabecera.Ap;
        }

        private string GetCentro()
        {
            return "";
        }
    }
}