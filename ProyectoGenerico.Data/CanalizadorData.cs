using ProyectoGenerico.Data.Context;
using ProyectoGenerico.Data.Interface;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using ProyectoGenerico.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ProyectoGenerico.Data
{
    public class CanalizadorData
    {
        private readonly IProyectoGenericoContext context = new ProyectoGenericoContext();

        public CanalizadorData()
        {
        }

        public Canalizador Get(string CodPostal, string CentroStock)
        {
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>();
                listParameters.Add(new SqlParameter("@CodigoPostal" , CodPostal ));
                listParameters.Add(new SqlParameter("@Centro"       , CentroStock ));

                Canalizador canalizador = context.ExecuteStoredProcedure<Canalizador>("BotConsultaCentroStok", listParameters.ToArray()).FirstOrDefault();

                return canalizador;
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("Error al buscar centro de stock: " + ex.Message);
                throw ex;
            }
        }
    }
}