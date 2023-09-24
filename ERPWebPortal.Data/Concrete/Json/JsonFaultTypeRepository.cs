using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Entities.Dtos;
using ERPWebPortal.Shared.Concrete.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ERPWebPortal.Data.Concrete.Json
{
    public class JsonFaultTypeRepository : JsonRepository<FaultType>, IFaultTypeRepository
    {
        public List<FaultType> GetFaultType(string filePath)
        {
            //tüm listedeki duruşisimlerinin gruplanması
            List<FaultType> faultTypeList = new();
            string jsonString = File.ReadAllText(filePath);
            List<Fault> faultList = JsonSerializer.Deserialize<List<Fault>>(jsonString)!;
            var result = from p in faultList
                        group p by p.FaultType into g
                        orderby g.Key
                        select new
                        {
                            fName = g.Key,
                        
                        };

            foreach (var item in result)
            {
                FaultType faultType = new();
                faultType.FaultName = item.fName;
                faultTypeList.Add(faultType); ;

            }
            return faultTypeList;


        }
    }
}
