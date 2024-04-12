using System.Collections.Generic;
using System.Linq;
using IntexII.Data;




namespace IntexII.Models
{
    public class EFCrudRepository : ICrudRepository<Product>
    {
        private readonly CrudDBContext _context;




        public EFCrudRepository(CrudDBContext context)
        {
            _context = context;
        }




        public IEnumerable<Product> GetAll()
        {
            return _context.Products.ToList();
        }




        public Product GetById(object id)
        {
            if (id is int productId)
            {
                return _context.Products.Find(productId);
            }
            return null;
        }




        public void Insert(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }




        public void Update(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }




        public void Delete(object id)
        {
            if (id is int entityId)
            {
                var entity = _context.Products.Find(entityId);
                if (entity != null)
                {
                    _context.Products.Remove(entity);
                    _context.SaveChanges();
                }
            }
        }
    }
}