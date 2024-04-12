using System.Collections.Generic;


namespace IntexII.Models
{
    public interface ICrudURepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAllCustomers();
        IEnumerable<TEntity> GetMostRecent50Customers();


        TEntity GetCustomersById(object id);
        void InsertCustomers(TEntity entity);
        void UpdateCustomers(TEntity entity);
        void DeleteCustomers(object id); // Keep the parameter type as object id
    }
}