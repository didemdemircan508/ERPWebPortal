
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using System.IO;
using System.Text;
using ERPWebPortal.Services.Abstract;



namespace ERPWebPortal.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //kullanıcalak olan rapor için reportservice interface constructorda tanımlanır,
        //arayüz sınıfında service katmanı kullanılmalıdır(best practice)

        private readonly IReportService _reportService;

        public HomeController(ILogger<HomeController> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
          

        }
        //gelen isteğe bağlı olarak reporun oluşmasını sağlayan method yazılır,report serviste yazılan methodları isteğe bağlı burda çağrılır
        public IActionResult Index()
        {
           var  prdOrderReportDtos = _reportService.Report();
           return View(prdOrderReportDtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

      

    }
}