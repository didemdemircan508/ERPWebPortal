﻿using ERPWebPortal.Data.Abstract;
using ERPWebPortal.Data.Concrete;
using ERPWebPortal.Entities.Concrete;
using ERPWebPortal.Entities.Dtos;
using ERPWebPortal.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPWebPortal.Services.Concrete
{
    public class ReportManager : IReportService
    {
        private readonly IFaultRepository _faultRepository;
        private readonly IPrdOrderRepository _prdOrderRepository;
        //repositorydelerden istediğimiz methodları çekmek için repository ler inetrface bazında constructora verilir
        public ReportManager(IFaultRepository faultRepository, IPrdOrderRepository prdOrderRepository)
        {
            _faultRepository = faultRepository;
            _prdOrderRepository = prdOrderRepository;

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
                double failureTime=0, breakTime=0, setupTime = 0,rdTime = 0;
                DateTime realStart= DateTime.MinValue, realEnd = DateTime.MinValue;
                Boolean isConditon;

                foreach (var fault in faultList)
                {
                    isConditon = false;
                    //duruş bitiş ve başlangıç tarihi iş emri başlangıç ve bitiş tarihi arasındaysa
                    if (prdorder.StartDate < fault.FaultStartDate && prdorder.EndDate > fault.FaultEndDate)
                    {
                        //duruş başlangıç gerçek duruş başlangıç alınır
                        realStart = fault.FaultStartDate;
                        //duruş bitiş gerçek bitiş alınır
                        realEnd = fault.FaultEndDate;
                        isConditon = true;

                    }
                    //duruş başlangıç tarihi iş emri baş-bitş arasında fakat ,duruş bitiş tarihi ,iş emri bitiş tarihi dışındaysa

                    if (prdorder.StartDate < fault.FaultStartDate && prdorder.EndDate < fault.FaultEndDate && fault.FaultStartDate < prdorder.EndDate)
                    {
                        //duruş başlangıç gerçek duruş alınır
                        realStart = fault.FaultStartDate;
                        //duruş bitiş iş emri bitişi alınır
                        realEnd = prdorder.EndDate;
                        isConditon = true;

                    }
                    //duruş bitiş tarihi iş emri baş-bitiş arasında fakat duruş başlangıç tarihi iş emri başlangıç tarihinden küçükse
                    if (prdorder.StartDate > fault.FaultStartDate && prdorder.EndDate > fault.FaultEndDate && prdorder.StartDate < fault.FaultEndDate)
                    {

                        //duruş başlangıç iş emri başlangıç alınır
                        realStart = prdorder.StartDate;
                        //duruş bitiş gerçek duruş bitiş alınır
                        realEnd = fault.FaultEndDate;
                        isConditon=true;


                    }
                    //duruş başlangıç ve bitiş tarihi ,iş emri baş-bitiş tarihinin arsaında değilse ,
                    if (prdorder.StartDate > fault.FaultStartDate && prdorder.EndDate < fault.FaultEndDate && prdorder.StartDate < fault.FaultEndDate)
                    {

                        //duruş başlangıç iş emri başlangıç alınır
                        realStart = prdorder.StartDate;
                        //duruş bitiş  iş emri bitiş alınır
                        realEnd = prdorder.EndDate;
                        isConditon=true;

                    }
                    //arıza tipleri göre hesaplanılan aralıkların toplamı alınır
                    if (fault.FaultType == FaultTypes.Ariza.ToString()&& isConditon)
                    {
                        failureTime += (realEnd - realStart).TotalMinutes;

                    }
                    if (fault.FaultType == FaultTypes.Mola.ToString() && isConditon)
                    {

                        breakTime += (realEnd - realStart).TotalMinutes;

                    }

                    if (fault.FaultType == FaultTypes.Setup.ToString() && isConditon)
                    {
                        setupTime += (realEnd - realStart).TotalMinutes;
                    }

                    if (fault.FaultType == FaultTypes.Arge.ToString() && isConditon)
                    {
                        rdTime += (realEnd - realStart).TotalMinutes;
                    }



                }

                //raporda kullanılan liste doldurulur
                prdOrderReportDto.PrdOrderNum = prdorder.PrdOrderNum;
                prdOrderReportDto.failureInterval = failureTime;
                prdOrderReportDto.breakInterval = breakTime;
                prdOrderReportDto.setupInterval = setupTime;
                prdOrderReportDto.rdInterval = rdTime;
                prdOrderReportDto.totalInterval = failureTime + breakTime + setupTime + rdTime;

                reportList.Add(prdOrderReportDto);

            }

            return reportList;



        }


    }
}
