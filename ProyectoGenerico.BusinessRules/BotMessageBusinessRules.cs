using ProyectoGenerico.Data;

using ProyectoGenerico.Entities;

using ProyectoGenerico.Helper;

using ProyectoGenerico.Services;

using System;


namespace ProyectoGenerico.BusinessRules
{
    public class BotMessageBusinessRules
    {
        private Tracking Tracking { get; set; }
        private string Seguimiento { get; set; }
        public BotMessageBusinessRules(string seguimiento) 
        {
            this.Tracking   = TrackingService.GetTracking(seguimiento);
            
            this.Seguimiento = seguimiento;
        }

        public BotMessageResponse GetMessage()
        {
            if (Tracking.Cabecera == null) 
                return Response(true, true, "Derivar a un asesor");
            
            try
            {
                LogHelper.GetInstance().PrintDebug("-- BotMessageBusinessRules inicio --");

                EstrategiaB2C estrategiaQuery = new EstrategiaB2C
                {
                    Solicitante = this.GetSolicitante(),

                    Servicio = this.GetServicio(),

                    EstadoDeEnvio = this.GetEstadoDeEnvio(),

                    MotivoPOD = this.GetMotivoPOD(),

                    CentroStock = this.GetCentroStock(),

                    Destino = this.GetDestino(),

                    Visitas = this.GetVisitas(),

                    Ap = this.GetAP()
                };

                DebugLog(estrategiaQuery);

                EstrategiaB2CData   estrategiaB2CData   = new EstrategiaB2CData();
                
                EstrategiaB2CResponse       estrategiaResponse  = estrategiaB2CData.Get(estrategiaQuery);

                ValidarRespuesta(estrategiaResponse);

                bool existeDemora   = ExisteDemora(estrategiaResponse.UltimaFechaDeActualizacionEnSap);
                
                string message      = existeDemora is false ? estrategiaResponse.RespuestaSinDemora : estrategiaResponse.RespuestaConDemora;
                
                bool derivaAsesor   = existeDemora;

                message = ReplaceMessageDynamicValues(message);

                BotMessageResponse botMessageResponse = new BotMessageResponse
                {
                    message = message,

                    cliente = estrategiaResponse.Cliente,

                    servicio = estrategiaResponse.Servicio,

                    estadoDeEnvio = estrategiaResponse.EstadoDeEnvio,

                    derivaAsesor = derivaAsesor,

                    error = false
                };

                return botMessageResponse;

            } catch (Exception ex){

                if (!this.Tracking.Cabecera.NroSeguimiento.IsNullOrEmpty())
                { 
                    LogHelper.GetInstance().PrintError("BotMessageBusinessRules: " + this.Tracking.Cabecera.NroSeguimiento + ex.Message); 
                }
                if (!this.Tracking.Detalle[0].Descripcion.IsNullOrEmpty() ) return Response(false, false, this.Tracking.Detalle[0].Descripcion);

                return Response(true, true, "Derivar a un asesor");
            }
        }

        private void ValidarRespuesta(EstrategiaB2CResponse estrategiaResponse)
        {
            if (estrategiaResponse == null) throw new Exception("La consulta no devolvió resultados.");
            
            if (estrategiaResponse.UltimaFechaDeActualizacionEnSap == null) { throw new Exception("La consulta no devolvió el campo Fecha de Ultima Actualización."); }
            
            if (estrategiaResponse.RespuestaConDemora == null) { throw new Exception("La consulta no devolvió el campo Respuesta Con Demora."); }
            
            if (estrategiaResponse.RespuestaSinDemora == null) { throw new Exception("La consulta no devolvió el campo Respuesta Sin Demora."); }
        }

        private void DebugLog(EstrategiaB2C estrategiaQuery)
        {
            LogHelper.GetInstance().PrintDebug($"Seguimiento: {this.Seguimiento}, Solicitante: {estrategiaQuery.Solicitante}, AP: {estrategiaQuery.Ap}, Servicio: {estrategiaQuery.Servicio}, EstadoDeEnvio: {estrategiaQuery.EstadoDeEnvio}, MotivoPOD: {estrategiaQuery.MotivoPOD}, CentroStock: {estrategiaQuery.CentroStock}, Destino: {estrategiaQuery.Destino}, Visitas: {estrategiaQuery.Visitas}");
        }

        private string ReplaceMessageDynamicValues(string message)
        {
            DateTime fechaParseada = ParseFecha(Tracking.Cabecera.EstadoFecha);
            
            string string1 = message.Replace("(fecha)", fechaParseada.ToString());
            
            string string2 = string1.Replace("(nombre)", Tracking.Cabecera.Receptor);

            message = string2;

            return message;
        }

        private DateTime ParseFecha(string myDate)
        {
            DateTime fechaParseada = DateTime.ParseExact(myDate, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            return fechaParseada;
        }

        private bool ExisteDemora(string horasDeDemora)
        {
            if (horasDeDemora == "N/A") return false;

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
                    return "ULTIMAMILLA";
            }

            return "PREULTIMAMILLA";
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
    }
}