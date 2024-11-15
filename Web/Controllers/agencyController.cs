using Services.Domain.Admin;
using Services.DomainServices.Account;
using Services.DomainServices.Admin;
using Services.DomainServices.Common;
using Services.EmailService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class agencyController : BaseController
    {
        // GET: agency
        private readonly CommonDomainService _commonDomainService;
        public readonly AdminDomainService _adminDomainService;
        public readonly AccountDomainService _accountDomainService;
        public agencyController(AdminDomainService adminDomainService, CommonDomainService commonDomainService, AccountDomainService accountDomainService)
        {
            _adminDomainService = adminDomainService;
            _commonDomainService = commonDomainService;
            _accountDomainService = accountDomainService;

        }


        [ActionName("about-us")]
        public ActionResult About()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return View("~/Views/home/About.cshtml");
        }

        [ActionName("clear-everything")]
        public ActionResult ClearEverithing()
        {
            StudentTicketBookingList.TicketEntityInSession = null;
            StudentTicketBookingList.CustomerEntityInSession = null;

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
            string urlLink = baseUrl + "agency/student-service";
            return Redirect(urlLink);
        }

        [ActionName("contact-us")]
        public ActionResult Contact()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            ViewBag.OurServiceProviders = _commonDomainService.GetContactPageServiceProviders();
            return View("~/Views/home/Contact.cshtml");
        }

        [ActionName("question-answer")]
        public ActionResult FAQ()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return View("~/Views/home/FAQ.cshtml");
        }

        [ActionName("terms-conditions")]
        public ActionResult TermsAndConditions()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return View("~/Views/home/TermsAndConditions.cshtml");
        }

        [ActionName("our-agents")]
        public ActionResult OurAgents()
        {
            ViewBag.OurAgentList= _adminDomainService.GetVendorListForAdmin();
            return View("~/Views/home/OurAgents.cshtml");
        }

        [ActionName("privacy-policy")]
        public ActionResult PrivacyPolicy()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return View("~/Views/home/PrivacyPolicy.cshtml");
        }

        [ActionName("feedback")]
        public ActionResult FeedBack()
        {
            
            return View("~/Views/home/FeedBack.cshtml");
        }

        [ActionName("customer-information")]
        public ActionResult CustomerInformation()
        {
            if (StudentTicketBookingList.TicketEntityInSession == null)
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
                string urlLink = baseUrl + "agency/student-service";
                return Redirect(urlLink);
            }
            else
            {
                string busNumber = "";
                int totalSeat = 0;
                int totalPrice = 0;
                string pricePerDet = "";
                string returnPrice = "";
                string eqPrice = "";

                foreach (SingleTicketPurchase loopItem in StudentTicketBookingList.TicketEntityInSession) {

                    if (busNumber == "")
                        busNumber = loopItem.BusNumber;
                    else
                        busNumber = busNumber + ", " + loopItem.BusNumber;

                    if (busNumber == "")
                        busNumber = loopItem.BusNumber;

                    totalSeat += loopItem.SeatCount;
                    totalPrice += loopItem.TotalSeatPrice;
                    if (pricePerDet == "")
                        pricePerDet = "(" + loopItem.PricePerSeat.ToString() + " * " + loopItem.SeatCount.ToString() + ")";
                    else
                        pricePerDet = pricePerDet + " (" + loopItem.PricePerSeat.ToString() + " * " + loopItem.SeatCount.ToString() + ")";

                    List<TimeSchedule> data = new List<TimeSchedule>();
                    if (loopItem.ReturnedDate != null && loopItem.ReturnedDate != "")
                    {
                        data = _accountDomainService.GetBusTimeScheduleListByFromAndToId(loopItem.toId, StudentTicketBookingList.TicketEntityInSession[0].fromId).ToList();


                        if (data != null && data.Count > 0)
                        {
                            int rP = (data[0].RatePerSeat * loopItem.SeatCount);
                            returnPrice = rP.ToString();
                            eqPrice = (loopItem.TotalSeatPrice + rP).ToString();
                        }
                        else {
                            int rP = (750 * loopItem.SeatCount);
                            returnPrice = rP.ToString();
                            eqPrice = (loopItem.TotalSeatPrice + rP).ToString();
                        }

                    }
                    
                }

                ViewBag.BusNumbers = busNumber;
                ViewBag.TotalSeat = totalSeat.ToString();
                if (returnPrice != "")
                    ViewBag.TotalPrice = totalPrice.ToString() + " + " + returnPrice + " = " + eqPrice;
                else
                    ViewBag.TotalPrice = totalPrice.ToString();

                if (returnPrice != "")
                    ViewBag.PricePerLoop = pricePerDet + " (return = " + returnPrice + ")";
                else
                    ViewBag.PricePerLoop = pricePerDet;

                return View("~/Views/home/CustomerInformation.cshtml");
            }
           
        }

        [ActionName("ticket-details")]
        public ActionResult TicketDetails()
        {
            if (StudentTicketBookingList.TicketEntityInSession == null || StudentTicketBookingList.CustomerEntityInSession == null)
            {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
                string urlLink = baseUrl + "agency/student-service";
                return Redirect(urlLink);
            }
            else
            {
                string busNumber = "";
                int totalSeat = 0;
                int totalPrice = 0;
                string pricePerDet = "";
                string source = "";
                string destination = "";
                string seatNumbers = "";
                string tourDates = "";
                string returnDate = "";
                string TravelTimes = "";
                string returnPrice = "";
                string eqPrice = "";
                foreach (SingleTicketPurchase loopItem in StudentTicketBookingList.TicketEntityInSession)
                {
                    if (busNumber == "")
                        busNumber = loopItem.BusNumber;
                    else
                        busNumber = busNumber + ", " + loopItem.BusNumber;

                    if (tourDates == "")
                        tourDates = loopItem.TourDate;
                    else
                        tourDates = tourDates + ", " + loopItem.TourDate;

                    if (TravelTimes == "")
                        TravelTimes = loopItem.startTime;
                    else
                        TravelTimes = TravelTimes + ", " + loopItem.startTime;
                    
                    if (source == "")
                        source = loopItem.TourRoot;
                    else
                        source = source + ", " + loopItem.TourRoot;

                    if (destination == "")
                        destination = loopItem.Destination;
                    else
                        destination = destination + ", " + loopItem.Destination;

                    if (seatNumbers == "")
                        seatNumbers = "(" + loopItem.SeatNumbers + ")";
                    else
                        seatNumbers = seatNumbers + " - " + "(" + loopItem.SeatNumbers + ")";


                    totalSeat += loopItem.SeatCount;
                    totalPrice += loopItem.TotalSeatPrice;
                    eqPrice = totalPrice.ToString();
                    if (pricePerDet == "")
                        pricePerDet = "(" + loopItem.PricePerSeat.ToString() + " * " + loopItem.SeatCount.ToString() + ")";
                    else
                        pricePerDet = pricePerDet + ", (" + loopItem.PricePerSeat.ToString() + " * " + loopItem.SeatCount.ToString() + ")";

                    if (loopItem.ReturnedDate != null && loopItem.ReturnedDate != "")
                    {
                        List<TimeSchedule> data = new List<TimeSchedule>();
                        if (returnDate == "")
                        {
                            returnDate = loopItem.ReturnedDate;
                            data = _accountDomainService.GetBusTimeScheduleListByFromAndToId(loopItem.toId, StudentTicketBookingList.TicketEntityInSession[0].fromId).ToList();


                            if (data != null && data.Count > 0)
                            {
                                int rP = (data[0].RatePerSeat * loopItem.SeatCount);
                                returnPrice = rP.ToString();
                                eqPrice = (loopItem.TotalSeatPrice + rP).ToString();
                            }
                            else
                            {
                                int rP = (750 * loopItem.SeatCount);
                                returnPrice = rP.ToString();
                                eqPrice = (loopItem.TotalSeatPrice + rP).ToString();
                            }
                        }

                        else
                            returnDate = returnDate + ", " + loopItem.ReturnedDate;
                    }
                }

                ViewBag.BusNumber = busNumber;
                ViewBag.Source = source;
                ViewBag.Destination = destination;
                ViewBag.SeatNumbers = seatNumbers;
                ViewBag.Totalrent = eqPrice;//totalPrice.ToString();
                ViewBag.JourneyDates = tourDates.ToString();
                ViewBag.TravelTimes = TravelTimes;
                ViewBag.ReturnDates = returnDate.ToString();
                ViewBag.NoOfTotalSeat = totalSeat.ToString();
                ViewBag.BookingDate = DateTime.Now.ToString("dd MMM yyyy HH:mm");
                ViewBag.StudentInformation = StudentTicketBookingList.CustomerEntityInSession;

                return View("~/Views/home/TicketDetails.cshtml");
            }
        }

        [ActionName("student-service")]
        public ActionResult StudentService()
        {

            if (Request.QueryString["mode"] == null || Request.QueryString["mode"] == "") {
                StudentTicketBookingList.TicketEntityInSession = null;
                StudentTicketBookingList.CustomerEntityInSession = null;
            }
               

            ViewBag.TotalTicket = "0";
            
            if (StudentTicketBookingList.TicketEntityInSession == null)
            {
                ViewBag.TotalTicket = "0";
                ViewBag.FromValue =1;
                ViewBag.ReadOnly = "false";
                ViewBag.MinDate = DateTime.Now;
            }
            else
            {
                ViewBag.TotalTicket = StudentTicketBookingList.TicketEntityInSession.Count;
                ViewBag.FromValue = StudentTicketBookingList.TicketEntityInSession[StudentTicketBookingList.TicketEntityInSession.Count-1].toId;
                ViewBag.ReadOnly = "true";
                ViewBag.MinDate = StudentTicketBookingList.TicketEntityInSession[StudentTicketBookingList.TicketEntityInSession.Count - 1].TourDate;
            }

            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return View("~/Views/home/StudentService.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendContactEmail(EmailRecoreds mObj)
        {
            bool isAddSuccess = true;
            string adminEmail = GetSiteSettingAdminEmal();

            return DataActionViewService(
                 () =>
                 {
                     if (mObj != null)
                     {
                         isAddSuccess = _commonDomainService.ProcessContactEmail(mObj);
                     }

                 },
                 () =>
                 {
                     string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                         Request.ApplicationPath.TrimEnd('/') + "/";
                     string url = baseUrl + "agency/contact-us?Success";


                     string emailBodyContent = "";
                     string fileName = Server.MapPath("~/email-template/admin-order.html");


                     if (System.IO.File.Exists(fileName) == true)
                     {
                         using (StreamReader textReader = new System.IO.StreamReader(fileName))
                         {
                             emailBodyContent = textReader.ReadToEnd();
                         }
                     }
                     
                     emailBodyContent = emailBodyContent.Replace("###UserName###", mObj.Name);
                     emailBodyContent = emailBodyContent.Replace("###MessageSubject###", mObj.Subject);
                     emailBodyContent = emailBodyContent.Replace("###Email###", mObj.EmailAddress);
                     emailBodyContent = emailBodyContent.Replace("###MobileNumber###", mObj.Mobile);
                     emailBodyContent = emailBodyContent.Replace("###MessageText###", mObj.Message);
                     emailBodyContent = emailBodyContent.Replace("###WebSiteURL###", baseUrl);
                     emailBodyContent = emailBodyContent.Replace("###WebSiteLogo###", baseUrl + "images/logo.png");
                     emailBodyContent = emailBodyContent.Replace("###PreHeader###", "New Contact Request");
                     
                     Emailer.SendMail(adminEmail, mObj.Subject, emailBodyContent);

                     return Redirect(url);
                 },
                 () => View());

        }

    }
}