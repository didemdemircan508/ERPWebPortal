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

        public double failureInterval { get; set; }

        public double breakInterval { get; set; }

        public double setupInterval { get; set; }

        public double rdInterval{ get; set; }

        public double totalInterval { get; set; }
    }
}
