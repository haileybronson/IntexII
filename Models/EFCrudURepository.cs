using System;
using System.Collections.Generic;
using System.Linq;
using IntexII.Data;
using Microsoft.EntityFrameworkCore;


namespace IntexII.Models
{
    public class EFCrudURepository : ICrudURepository<Customers>
    {
        private readonly CrudUDBContext _context;


        public EFCrudURepository(CrudUDBContext context)
        {
            _context = context;
        }
      
        public Customers GetCustomersById(object id)
        {
            if (id is int customer_Id)
            {
                return _context.Customers.Find(customer_Id);
            }
            return null;
        }
      
        public IEnumerable<Customers> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }
      
        public IEnumerable<Customers> GetMostRecent50Customers()
        {
            return _context.Customers
                .OrderByDescending(c => c.customer_Id)
                .Take(50)
                .ToList();
        }


        public void InsertCustomers(Customers customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }


        public void UpdateCustomers(Customers customer)
        {
            _context.Customers.Update(customer);
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