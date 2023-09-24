using ERPWebPortal.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERPWebPortal.Shared.Concrete.Json
{
    public class JsonRepository<T> : IRepository<T> where T : class
    {

        //json dosyasından verileri çekmek için generic yapıda method oluşturuldu.
        //prdorderve fault json dosyalarından veri okuyup veri türüne bağımsız olarak stediğimiz veri tipinde liste döndürülür.
        //ilerde farkli bir veri türünde json dosyası okuyup listesini elde etmek istenildiğinde bu method kullanilabilir
        //ilerde json dosyasından okuma vazgeçildiğinde xml'den okumaya karar verildiğinde XMLRepository oluşturulup IRepositoryden implement edilebilir
        public List<T> GetAll(string filePath)
        {

            string jsonString2 = File.ReadAllText(filePath);
            List<T> genericList = JsonSerializer.Deserialize<List<T>>(jsonString2)!;
            return genericList;
        }




    }
}
