using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Shared.Concrete.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Data.Concrete.Json
{
    public class JsonFaultTypeRepository : JsonRepository<FaultType>, IFaultTypeRepository
    {
    }
}
