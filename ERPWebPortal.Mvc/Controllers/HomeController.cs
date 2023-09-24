
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using System.Text;
using ERPWebPortal.Services.Abstract;
using ERPWebPortal.Entities.Dtos;
using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Entities.Concrete;

namespace ERPWebPortal.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //kullanıcalak olan rapor için reportservice interface constructorda tanımlanır,
        //arayüz sınıfında service katmanı kullanılmalıdır(best practice)
        private readonly IFaultTypeRepository _faultTypeRepository;
        private readonly IReportService _reportService;

        public HomeController(ILogger<HomeController> logger, IReportService reportService,IFaultTypeRepository faultTypeRepository)
        {
            _logger = logger;
            _reportService = reportService;
            _faultTypeRepository = faultTypeRepository;
          

        }
        //gelen isteğe bağlı olarak reporun oluşmasını sağlayan method yazılır,report serviste yazılan methodları isteğe bağlı burda çağrılır
        public IActionResult Index()
        {
            GeneralPrdOrderListDto generalPrdOrderListDtos = new();
            List<PrdOrderReportDto> prdOrderReportDtos = new();
            List<FaultType>  faultTypes = new();

            prdOrderReportDtos = _reportService.Report();
            //faultTypes = _faultTypeRepository.GetAll("faultType.json");
            faultTypes = _faultTypeRepository.GetFaultType("faultData.json");
            generalPrdOrderListDtos.prdOrderReportDtos = prdOrderReportDtos;
            generalPrdOrderListDtos.faultTypes = faultTypes;
            return View(generalPrdOrderListDtos);
          

        }


    }
}