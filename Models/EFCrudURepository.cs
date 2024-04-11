using System.Collections.Generic;
using System.Linq;
using IntexII.Data;


namespace IntexII.Models
{
    public class EFCrudURepository : ICrudURepository<AspNetUsers>
    {
        private readonly CrudUDBContext _context;


        public EFCrudURepository(CrudUDBContext context)
        {
            _context = context;
        }


        public IEnumerable<AspNetUsers> GetAllUsers()
        {
            return _context.Users.ToList();
        }


        public AspNetUsers GetUsersById(object id)
        {
            if (id is int Id)
            {
                return _context.Users.Find(Id);
            }
            return null;
        }


        public void InsertUsers(AspNetUsers users)
        {
            _context.Users.Add(users);
            _context.SaveChanges();
        }


        public void UpdateUsers(AspNetUsers users)
        {
            _context.Users.Update(users);
            _context.SaveChanges();
        }


        public void DeleteUsers(object id)
        {
            if (id is int entityId)
            {
                var entity = _context.Users.Find(entityId);
                if (entity != null)
                {
                    _context.Users.Remove(entity);
                    _context.SaveChanges();
                }
            }
        }
    }
}