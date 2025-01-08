using ProyectoGenerico.Data.Interface;
using ProyectoGenerico.Data.Context;
using System.Collections.Generic;
using ProyectoGenerico.Entities;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace ProyectoGenerico.Data
{
    public class PostalData
    {
        private readonly IProyectoGenericoContext context = new ProyectoGenericoContext();

        public PostalData()
        {
        }

        public TrackingPostal Get(string seguimiento)
        {
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>();
                listParameters.Add(new SqlParameter("@TrackingNumber", seguimiento));

                return context.ExecuteStoredProcedure<TrackingPostal>("PIMP_TrackingPostal", listParameters.ToArray()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}