using ERPWebPortal.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Services.Abstract
{
    //duruş  için kullanılacak methodları ortak bir yerden yönetmek için bu interface tanımlanır
    public interface IFaultService
    {
        public List<Fault> GetDataFault(string filePath);
    }
}
