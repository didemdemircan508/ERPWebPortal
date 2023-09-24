using ERPWebPortal.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Entities.Dtos
{
    public class GeneralPrdOrderListDto
    {
        public List<PrdOrderReportDto> prdOrderReportDtos { get; set; }
        public List<FaultType>    faultTypes { get; set; }
    }
}
