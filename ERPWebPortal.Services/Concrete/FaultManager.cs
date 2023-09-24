using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERPWebPortal.Services.Concrete
{
    public class FaultManager : IFaultService
    {
        //arayüz katmanından yada diğer katmanlardan ulaşabilmek için fault service, somut faultmanager classına implement edilir
        private readonly IFaultRepository _faultRepository;

        //hangi repository 'i kullanıcaksak dependendy injection için constructor oluşturuldu
        public FaultManager(IFaultRepository faultRepository)
        {
            _faultRepository = faultRepository;
            
        }
        //istediğimiz methodlar repositorya bağlı olarak buradan ulaşılabilir,iterfacede oluşturduğumuz methodun içi doldurulur
        public List<Fault> GetDataFault(string FilePath)
        {

           return _faultRepository.GetAll(FilePath);
        }
    }
}
