using ProyectoGenerico.Data.Context;
using ProyectoGenerico.Data.Interface;
using ProyectoGenerico.Entities;
using ProyectoGenerico.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ProyectoGenerico.Data
{
    public class TestData
    {
        private readonly IProyectoGenericoContext context = new ProyectoGenericoContext();

        public TestData()
        {
        }

        public TestEntity Add(TestAdd testEntity)
        {
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>();
                listParameters.Add(new SqlParameter("@Nombre", testEntity.Nombre));
                listParameters.Add(new SqlParameter("@Apellido", testEntity.Apellido));

                return context.ExecuteStoredProcedure<TestEntity>("Test_Nombre_Add", listParameters.ToArray()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}