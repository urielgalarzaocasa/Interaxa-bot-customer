using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using System;
using System.Web.Http;
using ProyectoGenerico.Helper;

namespace ProyectoGenerico.API.Controllers
{
    public class TrackingMeliController : ApiController
    {
        public TrackingMeliResponse Get(string Seguimiento)
        {
            TrackingMeliResponse trackingMeliResponse = new TrackingMeliResponse();
            try
            {
                LogHelper.GetInstance().PrintDebug("TrackingMeli inicio: ");

                TrackingMeliBusinessRules trackingMeliBusinessRules = new TrackingMeliBusinessRules();

                trackingMeliResponse = trackingMeliBusinessRules.GetTrackingNumber(Seguimiento);

                return trackingMeliResponse;
            }
            catch (Exception ex)
            {
                //excepciones sin controlar, enviar por email!

                LogHelper.GetInstance().PrintError("TrackingMeli error: " + ex.Message);
                
                trackingMeliResponse.error = true;
                trackingMeliResponse.trackingNumber = null;
                
                return trackingMeliResponse;
            }
            finally
            {
                LogHelper.GetInstance().PrintDebug("TrackingMeli fin");
            }
        }
    }
}