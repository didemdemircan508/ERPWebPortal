using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Shared.Abstract
{
    public interface IRepository<T> where T : class
    {
        //dataları çekmek için ortak bir interface oluşturulup bu  interface somut sınıflara implement edildi
        
        List<T> GetAll(string filePath);
       

    }
}
