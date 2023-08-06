using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserService
    {
        List<User> Get();
        User Get(string id);
        List<User> GetByFilter(string? userName);
        User Add(User user);
        void Update(string id, User user);
        void Delete(string id);
    }
}
