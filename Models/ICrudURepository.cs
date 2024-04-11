using System.Collections.Generic;

namespace IntexII.Models
{
    public interface ICrudURepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllUsers();
        TEntity GetUsersById(object id);
        void InsertUsers(TEntity entity);
        void UpdateUsers(TEntity entity);
        void DeleteUsers(object id); // Keep the parameter type as object id
    }
}