using System.Collections.Generic;
using System.Linq;
using IntexII.Data;

namespace IntexII.Models
{
    public class EFCrudURepository : ICrudURepository<Customers>
    {
        private readonly CrudUDBContext _context;


        public EFCrudURepository(CrudUDBContext context)
        {
            _context = context;
        }


        public IEnumerable<Customers> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }


        public Customers GetCustomersById(object id)
        {
            if (id is int Id)
            {
                return _context.Customers.Find(id);
            }
            return null;
        }


        public void InsertCustomers(Customers Customers)
        {
            _context.Customers.Add(Customers);
            _context.SaveChanges();
        }


        public void UpdateCustomers(Customers Customers)
        {
            _context.Customers.Update(Customers);
            _context.SaveChanges();
        }


        public void DeleteCustomers(object id)
        {
            if (id is int entityId)
            {
                var entity = _context.Customers.Find(entityId);
                if (entity != null)
                {
                    _context.Customers.Remove(entity);
                    _context.SaveChanges();
                }
            }
        }
    }
}
