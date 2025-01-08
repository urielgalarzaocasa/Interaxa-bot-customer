using ProyectoGenerico.Data;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Helper;
using System;

namespace ProyectoGenerico.BusinessRules
{
    public class TrackingPostalDNIBusinessRules

    {
        public PostalDNIResponse GetMessage(string DNI)
        {
            try { 
                PostalDNI[] trackingDNI = new PostalDNIData().Get(DNI);
                return new PostalDNIResponse(false,false, trackingDNI);
            }catch(Exception e)
            {
                LogHelper.GetInstance().PrintError(e.Message);
                return new PostalDNIResponse(true, true, null);
            }
        }
    }
}