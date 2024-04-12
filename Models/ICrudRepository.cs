using System.Collections.Generic;


namespace IntexII.Models
{
    public interface ICrudRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entity);
        void Delete(object id); // Keep the parameter type as object id
    }
}