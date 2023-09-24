using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Entities.Dtos;
using ERPWebPortal.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Services.Concrete
{
    public class ReportManager:IReportService
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IPrdOrderRepository _prdOrderRepository;
        private readonly IFaultTypeRepository _faultTypeRepository;

        //repositorydelerden istediğimiz methodları çekmek için repository ler inetrface bazında constructora verilir
        public ReportManager(IFaultRepository faultRepository, IPrdOrderRepository prdOrderRepository,IFaultTypeRepository faultTypeRepository)
        {
            _faultRepository = faultRepository;
            _prdOrderRepository = prdOrderRepository;
            _faultTypeRepository = faultTypeRepository;


        }
        public List<PrdOrderReportDto> Report()
        {    //iş emri listesini json dosyasından okuma işlemi yapılır
            List<PrdOrder> prdOrderList = _prdOrderRepository.GetAll("prdOrderData.json");
            //duurş listesini json dosyasından okuma işlemi yapılır

            List<Fault> faultList = _faultRepository.GetAll("faultData.json");

             List<PrdOrderReportDto> reportList = new();
            //her iş emri için  ve duruş listesi içerisinde tek tek gezilip,iş emrinın başlama ve bitiş tarihi aralığında süreler hesaplanır ,duruş tipine göre toplamı yapılır
            foreach (var prdorder in prdOrderList)
            {
                PrdOrderReportDto prdOrderReportDto = new();
                prdOrderReportDto.sub = new();
                DateTime realStart = DateTime.MinValue, realEnd = DateTime.MinValue;
                Boolean isConditon;
                prdOrderReportDto.PrdOrderNum = prdorder.PrdOrderNum;

                foreach (var fault in faultList)
                {
                    isConditon = false;
                    //duruş bitiş ve başlangıç tarihi iş emri başlangıç ve bitiş tarihi arasındaysa,yada eşitse
                    //data deseni:08-11 ,09-10 ;08-11,08-10;09-11
                    if (prdorder.StartDate <= fault.FaultStartDate && prdorder.EndDate >= fault.FaultEndDate)
                    {
                        //duruş başlangıç gerçek duruş başlangıç alınır
                        realStart = fault.FaultStartDate;
                        //duruş bitiş gerçek bitiş alınır
                        realEnd = fault.FaultEndDate;
                        isConditon = true;
                       


                    }
                    //duruş başlangıç tarihi iş emri baş-bitş arasında yada duruş başlangıç iş emri başlangıç tarihine eşitse fakat ,duruş bitiş tarihi ,iş emri bitiş tarihi dışındaysa
                    //data deseni:08-11 ,09-12;08-11,08-12
                    if (prdorder.StartDate <= fault.FaultStartDate && prdorder.EndDate < fault.FaultEndDate && fault.FaultStartDate < prdorder.EndDate)
                    {
                        //duruş başlangıç gerçek duruş alınır
                        realStart = fault.FaultStartDate;
                        //duruş bitiş iş emri bitişi alınır
                        realEnd = prdorder.EndDate;
                        isConditon = true;
                       

                    }
                    //duruş bitiş tarihi iş emri baş-bitiş arasında yada duruş bitiş iş emri tarihine eşitse fakat duruş başlangıç tarihi iş emri başlangıç tarihinden küçükse
                    //data deseni 08-11,07-09;08-11,07-11
                    if (prdorder.StartDate > fault.FaultStartDate && prdorder.EndDate >= fault.FaultEndDate && prdorder.StartDate < fault.FaultEndDate)
                    {

                        //duruş başlangıç iş emri başlangıç alınır
                        realStart = prdorder.StartDate;
                        //duruş bitiş gerçek duruş bitiş alınır
                        realEnd = fault.FaultEndDate;
                        isConditon = true;
                       

                    }
                    //duruş başlangıç ve bitiş tarihi ,iş emri baş-bitiş tarihinin arasında değilse ,
                    //08-11,07-12

                    if (prdorder.StartDate > fault.FaultStartDate && prdorder.EndDate < fault.FaultEndDate && prdorder.StartDate < fault.FaultEndDate)
                    {

                        //duruş başlangıç iş emri başlangıç alınır
                        realStart = prdorder.StartDate;
                        //duruş bitiş  iş emri bitiş alınır
                        realEnd = prdorder.EndDate;
                        isConditon = true;
                       


                    }

                    if (isConditon)
                    {
                        SubPrdOrderReportDto item = new();
                        item.FaultName = fault.FaultType.ToString();
                        item.FaultInterval = (realEnd - realStart).TotalMinutes;
                        prdOrderReportDto.sub.Add(item);
                    }


                }


                //duruş tipleri aynı olan dataların toplanması 
                var result = from p in prdOrderReportDto.sub
                            group p by p.FaultName into g
                            orderby g.Key
                            select new
                            {
                                fName = g.Key,
                                interval = g.Sum(prd => prd.FaultInterval)

                            };


                prdOrderReportDto.sub = new();

                foreach (var item in result)
                {
                    SubPrdOrderReportDto item2 = new();
                    
                    item2.FaultName = item.fName;
                    item2.FaultInterval = item.interval;
                    prdOrderReportDto.sub.Add(item2);


                }


                reportList.Add(prdOrderReportDto);

            }
           
            return reportList;

        }
    }
}
