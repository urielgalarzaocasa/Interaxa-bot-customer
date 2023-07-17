using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGenerico.Data.Interface
{
    public interface IProyectoGenericoContext
    {
        Database Database { get; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        IEnumerable<TEntity> ExecuteStoredProcedure<TEntity>(string storedProcedure, params object[] parameters) where TEntity : class;
        void Dispose();
    }
}
