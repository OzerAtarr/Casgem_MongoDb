using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEstateService
    {
        List<Estate> Get();
        Estate Get(string id);
        List<Estate> GetByFilter(string? city, string? type, int? room, string? title, int? price, string? buildYear);
        Estate Add(Estate estate);
        void Update(string id, Estate estate);
        void Delete(string id);
    }
}
