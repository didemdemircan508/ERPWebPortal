using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Services.Abstract
{
    //iş emri  için kullanılacak methodları ortak bir yerden yönetmek için bu interface tanımlanır
    public interface IPrdOrderService
    {
        public List<PrdOrder> GetDataPrdOrder(string filePath);


    }
}
