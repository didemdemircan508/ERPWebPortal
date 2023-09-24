using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Entities.Concrete
{
    public class Fault
    {

        public string FaultType { get; set; }

        public DateTime FaultStartDate { get; set; }

        public DateTime FaultEndDate { get; set; }
    }
}
