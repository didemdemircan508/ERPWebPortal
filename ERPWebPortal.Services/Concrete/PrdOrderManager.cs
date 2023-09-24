using ERPWebPortal.Data.Abstract;


using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Entities.Dtos;
using ERPWebPortal.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERPWebPortal.Services.Concrete
{

    //arayüz katmanından yada diğer katmanlardan ulaşabilmek için PrdOrder service, somut PrdOrderManager classına implement edilir
    public class PrdOrderManager : IPrdOrderService
    {
        private readonly IPrdOrderRepository _prdOrderRepository;
        //hangi repository 'i kullanıcaksak dependendy injection için constructor oluşturuldu
        public PrdOrderManager(IPrdOrderRepository  prdOrderRepository)
        {
            _prdOrderRepository = prdOrderRepository;
        }
        //istediğimiz methodlar repositorya bağlı olarak buradan ulaşılabilir,iterfacede oluşturduğumuz methodun içi doldurulur
        public List<PrdOrder> GetDataPrdOrder(string filePath)
        {

            return _prdOrderRepository.GetAll(filePath);
        }
    }
}
