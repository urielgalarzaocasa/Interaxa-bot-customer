using ProyectoGenerico.Data.Interface;
using ProyectoGenerico.Data.Context;
using System.Collections.Generic;
using ProyectoGenerico.Entities;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace ProyectoGenerico.Data
{
    public class TrackingMeliData
    {
        private readonly IProyectoGenericoContext context = new ProyectoGenericoContextTrackingMeli();

        public TrackingMeliData()
        {
        }

        public TrackingMeli Get(string seguimiento)
        {
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>
                {
                    new SqlParameter("@seguimiento", seguimiento)
                };

                return context.ExecuteStoredProcedure<TrackingMeli>("ConsultarTrackingNumber", listParameters.ToArray()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}