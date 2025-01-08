using System.Data.Entity.Infrastructure;
using ProyectoGenerico.Data.Interface;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System;

namespace ProyectoGenerico.Data.Context
{
    public partial class ProyectoGenericoContextPostal : DbContext, IProyectoGenericoContext
    {
        public int _contador = 0;
        public IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storedProcedure, params object[] parameters) where TEntity : class
        {
            bool first = true;
            var sqlQueryBuilder = new StringBuilder();
            sqlQueryBuilder.AppendFormat("EXEC {0}", storedProcedure);

            if (parameters != null && parameters.Any())
            {
                foreach (var parameter in parameters.OfType<SqlParameter>())
                {
                    if (first)
                    {
                        if (Convert.ToString(parameter.Value) == "")
                        {
                            sqlQueryBuilder.AppendFormat(" {0} = {1} ", parameter.ParameterName, "''");
                            first = false;
                        }
                        else
                        {
                            sqlQueryBuilder.AppendFormat(" {0} = {1} ", parameter.ParameterName, "'" + parameter.Value + "'");
                            first = false;
                        }
                    }
                    else
                    {
                        if (parameter.Value == null || Convert.ToString(parameter.Value) == "")
                        {
                            sqlQueryBuilder.AppendFormat(", {0} = {1} ", parameter.ParameterName, "''");
                            first = false;
                        }
                        else
                        {
                            decimal value;
                            if (Decimal.TryParse(parameter.Value.ToString(), out value))
                            {
                                sqlQueryBuilder.AppendFormat(", {0} = {1} ", parameter.ParameterName, "'" + parameter.Value.ToString().Replace(".", "").Replace(",", ".") + "'");
                            }
                            else
                            {
                                sqlQueryBuilder.AppendFormat(", {0} = {1} ", parameter.ParameterName, "'" + parameter.Value + "'");
                            }
                        }
                    }
                }
            }
            return Database.SqlQuery<TEntity>(sqlQueryBuilder.ToString(), parameters);
        }

        IDbSet<TEntity> IProyectoGenericoContext.Set<TEntity>()
        {
            return base.Set<TEntity>();
        }

        public ProyectoGenericoContextPostal() : base("name=dbConnPostal")
        {
            // Get the ObjectContext related to this DbContext
            var objectContext = (this as IObjectContextAdapter).ObjectContext;

            // Sets the command timeout for all the commands
            objectContext.CommandTimeout = 240;
            Debug.WriteLine(_contador + "+ + Stack " + Environment.StackTrace.Substring(0, 600));
            _contador++;

            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public ProyectoGenericoContextPostal(bool lazyload) : base("name=dbConnPostal")
        {
            Debug.WriteLine(_contador + "o o Stack " + Environment.StackTrace.Substring(0, 600));
            _contador++;

            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        protected override void Dispose(bool lazyLoad)
        {
            _contador--;
            Debug.WriteLine(_contador + "- - Stack " + Environment.StackTrace.Substring(0, 600));
            Configuration.LazyLoadingEnabled = false;
            base.Dispose(lazyLoad);
        }
    }
}