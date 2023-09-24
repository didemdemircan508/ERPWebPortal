using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Entities.Concrete
{
    public class PrdOrder
    {
        public string PrdOrderNum { get; set; } 

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}
