using ProyectoGenerico.Data.Interface;
using ProyectoGenerico.Data.Context;
using System.Collections.Generic;
using ProyectoGenerico.Entities;
using System.Data.SqlClient;
using System.Linq;
using System;
using System.Data.Entity.Infrastructure;

namespace ProyectoGenerico.Data
{
    public class PostalDNIData
    {
        private readonly IProyectoGenericoContext context = new ProyectoGenericoContextPostal();

        public PostalDNIData()
        {
        }

        public PostalDNI[] Get(string DNI)
        {
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>();
                listParameters.Add(new SqlParameter("@DNI", DNI));


                //PostalDNI[] result = context.ExecuteStoredProcedure<PostalDNI>("ECO_TrackingPostalDNI ", listParameters.ToArray()).ToArray();
                
                //De momento no se puede usar el ExecuteStoredProcedure porque el usuario que tengo de base de datos no tiene permisos para crear procedimientos almacenados.
                string sqlQuery = $"select top 100 Pieza_Ocasa as 'NroSeguimiento', Pieza_Tipo as 'TipoTarjeta', Destinatario, Distribuidor as 'Remitente' from DW.Fact_Postmaster_Datos d where d.Dni=@DNI;";
                DbRawSqlQuery<PostalDNI> dbQuery = context.Database.SqlQuery<PostalDNI>(sqlQuery, new SqlParameter("@DNI", DNI));
                PostalDNI[] result  = dbQuery.ToArray();
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}