using ProyectoGenerico.Data;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Helper;
using System;

namespace ProyectoGenerico.BusinessRules
{
    public class TrackingMeliBusinessRules

    {
        public TrackingMeliResponse GetTrackingNumber(string seguimiento)
        {
           TrackingMeliResponse response = new TrackingMeliResponse();
            response.error = false;
            
            TrackingMeliData data = new TrackingMeliData();

            try { 
                TrackingMeli trackingMeli = data.Get(seguimiento);
                response.trackingNumber = trackingMeli.TrackingNumber;
            }catch(Exception e)
            {
                LogHelper.GetInstance().PrintError(e.Message);
                response.error = true;
                response.trackingNumber = null;
            }


            return response;

        }
    }
}