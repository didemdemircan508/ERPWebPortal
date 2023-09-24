using ERPWebPortal.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Entities.Dtos
{
    //raporda kullanılacak olan verilerin sınıfı oluşturuldu
    public class PrdOrderReportDto
    {

        public string PrdOrderNum { get; set; }

        public List<SubPrdOrderReportDto> sub { get; set; }

        public List<FaultType> FaultTypes { get; set; }


      

    }
}
