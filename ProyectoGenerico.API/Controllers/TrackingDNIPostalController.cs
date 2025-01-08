using ProyectoGenerico.BusinessRules;
using ProyectoGenerico.Entities;
using System;
using System.Web.Http;
using ProyectoGenerico.Helper;

namespace ProyectoGenerico.API.Controllers
{
    public class TrackingDNIPostalController : ApiController
    {
        public PostalDNIResponse Get(string DNI)
        {
            try
            {
                LogHelper.GetInstance().PrintDebug("Api inicio: ");

                TrackingPostalDNIBusinessRules trackingPostalDNIBusinessRules = new TrackingPostalDNIBusinessRules();

               return trackingPostalDNIBusinessRules.GetMessage(DNI);
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("Api error: " + ex.Message);
                
                return new PostalDNIResponse(true, true, null); 
            }
            finally
            {
                LogHelper.GetInstance().PrintDebug("Api fin");
            }
        }
    }
}