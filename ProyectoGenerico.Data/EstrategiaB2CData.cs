using ProyectoGenerico.Data.Context;
using ProyectoGenerico.Data.Interface;
using ProyectoGenerico.Entities;
using System;
using System.Linq;
using System.Data.SqlClient;
using ProyectoGenerico.Helper;
using System.Collections.Generic;


namespace ProyectoGenerico.Data
{
    public class EstrategiaB2CData
    {
        private readonly IProyectoGenericoContext context = new ProyectoGenericoContext();
        public EstrategiaB2CData()
        {
        }
        public TrackCentro GetTrackCentro(string centroStock)
        {
            string procedimientoAlmacenado = "BotTrackingSeleccionarTrackCentro";
            LogHelper.GetInstance().PrintDebug($"EstrategiaB2CData GetTrackCentro() inicio | procedimientoAlmacenado: {procedimientoAlmacenado} {centroStock}");
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>();

                listParameters.Add(new SqlParameter("@IDCentro", centroStock));
                
                var response = context.ExecuteStoredProcedure<TrackCentro>(procedimientoAlmacenado, listParameters.ToArray()).FirstOrDefault();

                return response;
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("No se pudo obtener el centroStock: " + ex.Message);
                throw ex;
            }
        }
        public EstrategiaB2CResponse Get(EstrategiaB2C estrategia)
        {
            LogHelper.GetInstance().PrintDebug("EstrategiaB2C Get() inicio");
            try
            {
                List<SqlParameter> listParameters = new List<SqlParameter>();

                listParameters.Add(new SqlParameter("@solicitante"                  , estrategia.Solicitante ));
                listParameters.Add(new SqlParameter("@servicio"                     , estrategia.Servicio ));
	            listParameters.Add(new SqlParameter("@ap"                           , estrategia.Ap ));
                listParameters.Add(new SqlParameter("@estadoDeEnvio"                , estrategia.EstadoDeEnvio ));
	            listParameters.Add(new SqlParameter("@motivo"                       , estrategia.MotivoPOD ));
                listParameters.Add(new SqlParameter("@centroStock"                  , estrategia.CentroStock ));
	            listParameters.Add(new SqlParameter("@destino"                      , estrategia.Destino ));
                listParameters.Add(new SqlParameter("@visitas"                      , estrategia.Visitas ));

                var response = context.ExecuteStoredProcedure<EstrategiaB2CResponse>(GetStoredProcedure(), listParameters.ToArray()).FirstOrDefault();
               
                return response;
            }
            catch (Exception ex)
            {
                LogHelper.GetInstance().PrintError("No se pudo obtener la estrategia: " + ex.Message);
                throw ex;
            }
        }
        private string GetStoredProcedure()
        {
            return ConfigurationHelper.GetValue("appSettings", "StoredProcedure"); //BotTrackingSeleccionarEstrategia_ug BotTrackingSeleccionarEstrategia_ug_qa
        }
    }
}