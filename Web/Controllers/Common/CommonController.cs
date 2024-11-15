using Kendo.Mvc.UI;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.DomainServices.Account;
using Services.DomainServices.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers.Common
{
    public class CommonController : Controller
    {
        private readonly CommonDomainService _commonDomainService;
        public readonly AccountDomainService _accountDomainService;
        // GET: Common

        public CommonController(CommonDomainService commonDomainService, AccountDomainService accountDomainService)
        {
            _commonDomainService = commonDomainService;
            _accountDomainService = accountDomainService;
        }

        public ActionResult GetHomePageAdvantageLists()
        {
            IList<OURADVANTAGES> rolesList = _commonDomainService.GetHomePageAdvantageLists();

            return Json(rolesList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllAdminRoles()
        {
            IList<UserRoles> rolesList = _commonDomainService.GetAllAdminRoles();

            return Json(rolesList, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult AddItemToMyCartList(string userCookie, int productId, int QTY)
        //{
        //    long userId = 0;
        //    if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
        //        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
        //    bool isSuccess= _commonDomainService.AddItemToMyCartList(userId, userCookie, productId, QTY);

        //    return Json(isSuccess, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetAllParentCategoryList()
        //{
        //    IList<Admin_Get_Parent_CategoryList> dataList = _commonDomainService.GetAllParentCategoryList();

        //    return Json(dataList, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetAllFontAwesomeIconClass()
        {
            IList<FONT_AWESOME_ICONS> dataList = _commonDomainService.GetAllFontAwesomeIconClass();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }


        //public ActionResult GetMyCartListItems(string userCookie)
        //{
        //    long userId = 0;
        //    if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
        //        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
        //    IList<Get_Temp_CartList_By_Params> dataList = _commonDomainService.GetMyCartListItems(userCookie, userId);

        //    return Json(dataList, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult ClearMyCartHistory(string userCookie)
        //{
        //    long userId = 0;
        //    if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
        //        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
        //    int rowCount = _commonDomainService.ClearMyCartHistory(userCookie, userId);
        //    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
        //               Request.ApplicationPath.TrimEnd('/') + "/";

        //    string url = baseUrl + "product/";
        //    return Json(url, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult UpdateMyCartList(UpdateCartModel mObj)
        //{
        //    long userId = 0;
        //    string userCookie = mObj.BrowserCookieId;

        //    if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
        //        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
        //    int rowCount = _commonDomainService.UpdateMyCartList(mObj, userId);


        //    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
        //               Request.ApplicationPath.TrimEnd('/') + "/";

        //    string url = baseUrl + "shop/my-cart/" + userCookie;
        //    return Redirect(url);
        //}

        //public ActionResult jQueryRemoveCurrentCartItem(string userCookie, int productId)
        //{
        //    long userId = 0;
        //    if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
        //        userId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
        //    int rowCount = _commonDomainService.jQueryRemoveCurrentCartItem(userCookie, userId, productId);

        //    string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
        //               Request.ApplicationPath.TrimEnd('/') + "/";

        //    string url = baseUrl + "shop/my-cart/" + userCookie;

        //    return Json(url, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult GetAllCountryList()
        {
            IList<CountryListModel> data = _accountDomainService.GetAllCountryList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllDistrictListInsideBangladesh()
        {
            IList<DistricListInBD> data = _accountDomainService.GetAllDistrictListInsideBangladesh();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTravelFromSourceDataList()
        {
            IList<Get_Travel_From_List> data = _accountDomainService.GetTravelFromSourceDataList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTravelToDataRemoveFromId(int id)
        {
            IList<Get_Travel_To_Data_List_By_FromId> data = _accountDomainService.GetTravelToDataRemoveFromId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictListByRemoveParamId(int id)
        {
            IList<DistricListInBD> data = _accountDomainService.GetDistrictListByRemoveParamId(id);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetBusTimeScheduleListByFromAndToId(int fromId, int toId)
        {
            IList<TimeSchedule> data = _accountDomainService.GetBusTimeScheduleListByFromAndToId(fromId, toId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTicketBookingHistoryByParam(int fromId, int toId, string startTime, string travelDate)
        {
            IList<TicketBookingHistory> data = _accountDomainService.GetTicketBookingHistoryByParam(fromId, toId,startTime, travelDate);
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveBookingInformation(PassengerInformation model)
        {

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";


            if (StudentTicketBookingList.TicketEntityInSession == null)
            {
                string urlLink = baseUrl + "agency/student-service";
                return Redirect(urlLink);
            }
            else
            {
                string busNumber = "";
                int totalSeat = 0;
                int totalPrice = 0;
                string pricePerDet = "";
                Random rnd = new Random();
                Random rnd1 = new Random();
                string pnrNumber = rnd.Next(50000).ToString() + rnd1.Next(548775).ToString();

                bool isSuccess = false;

                StudentTicketBookingList.CustomerEntityInSession = new CustomerInformationInSession();
                StudentTicketBookingList.CustomerEntityInSession.CustomerName = model.C_Name;
                StudentTicketBookingList.CustomerEntityInSession.TicketNumber = pnrNumber;
                StudentTicketBookingList.CustomerEntityInSession.CustomerPhoneNumber = model.C_Phone;
                StudentTicketBookingList.CustomerEntityInSession.CustomerPayNumber = model.C_PaymentNumber;
                StudentTicketBookingList.CustomerEntityInSession.CustomerTrNumber = model.C_TransectionNumber;


                foreach (SingleTicketPurchase loopItem in StudentTicketBookingList.TicketEntityInSession)
                {
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
                        pricePerDet = pricePerDet + ", (" + loopItem.PricePerSeat.ToString() + " * " + loopItem.SeatCount.ToString() + ")";


                    TicketBookingHistoryUpdateModel tableModel = new TicketBookingHistoryUpdateModel();
                    tableModel.fromId = loopItem.fromId;
                    tableModel.toId = loopItem.toId;
                    tableModel.seatList = loopItem.SeatNumbers;
                    tableModel.startTime = loopItem.startTime;
                    tableModel.DepartDate= loopItem.TourDate;
                    isSuccess = _accountDomainService.BookSelectedSeat(tableModel);

                    PassengerInformation passenger = new PassengerInformation();
                    passenger = model;
                    passenger.C_TotalSeat = loopItem.SeatCount.ToString();
                    passenger.C_From = loopItem.TourRoot.ToString();
                    passenger.C_To = loopItem.Destination.ToString();
                    passenger.C_Date = loopItem.TourDate.ToString();
                    passenger.C_TIme = loopItem.startTime.ToString();
                    passenger.C_Amount = loopItem.TotalSeatPrice.ToString();
                    passenger.C_PaymentNumber = model.C_PaymentNumber;
                    passenger.C_TransectionNumber = model.C_TransectionNumber;
                    passenger.C_Status = "Booked";
                    passenger.PNRNumber = pnrNumber;
                    passenger.C_AgentId = LoggedInUserInfoFromCookie.UserDisplayNameInCookie;

                    
                    List<TimeSchedule> data = new List<TimeSchedule>();
                    if (loopItem.ReturnedDate != null && loopItem.ReturnedDate != "")
                    {
                        passenger.C_IsReturn = "true";
                        passenger.C_returnDate = loopItem.ReturnedDate;
                        data = _accountDomainService.GetBusTimeScheduleListByFromAndToId(loopItem.toId, StudentTicketBookingList.TicketEntityInSession[0].fromId).ToList();

                        int rP = 0;
                        if (data != null && data.Count > 0)
                        {
                            rP = (data[0].RatePerSeat * loopItem.SeatCount);
                        }
                        else
                        {
                            rP = (750 * loopItem.SeatCount);
                        }
                        
                        passenger.C_Amount = rP.ToString();

                    } 
                    else {
                        passenger.C_IsReturn = "false";
                        passenger.C_returnDate = "";
                    }

                    

                    bool passengerSave = _accountDomainService.SaveBookingCustomerInformation(passenger);

                }

                string urlLink = baseUrl + "agency/ticket-details";
                return Redirect(urlLink);


            }
        }


        [HttpPost]
        public ActionResult BookSelectedSeat(TicketBookingHistoryUpdateModel model)
        {
            string[] str = model.seatList.Trim().Split(',');
            int len = str.Length;

            SingleTicketPurchase purchaseInfo = new SingleTicketPurchase();
            purchaseInfo.fromId = model.fromId;
            purchaseInfo.toId = model.toId;
            purchaseInfo.startTime = model.startTime.Trim();
            purchaseInfo.SeatNumbers = model.seatList.Trim();
            purchaseInfo.SeatNumberArray = str;
            purchaseInfo.SeatCount = len;
            purchaseInfo.PricePerSeat = model.TicketPrice.Trim();
            purchaseInfo.TotalSeatPrice = (len * int.Parse(model.TicketPrice.Trim()));
            purchaseInfo.ReturnedDate = model.ReturnDate != null ? model.ReturnDate.Trim() : "";
            purchaseInfo.BusNumber = model.BusNumber;
            purchaseInfo.TourRoot = model.FromText;
            purchaseInfo.Destination = model.DistenceText;
            purchaseInfo.TourDate = model.DepartDate;
            purchaseInfo.TravelTime = model.startTime;
            

            if (StudentTicketBookingList.TicketEntityInSession == null)
            {
                StudentTicketBookingList.TicketEntityInSession = new List<SingleTicketPurchase>();
                StudentTicketBookingList.TicketEntityInSession.Add(purchaseInfo);

            }
            else
            {
                StudentTicketBookingList.TicketEntityInSession.Add(purchaseInfo);
            }

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
            string urlLink = baseUrl + "agency/student-service?mode=more-trip";
            if (model.btnMode == "more")
                return Redirect(urlLink);
            else
            {

                string detailsURL = baseUrl + "agency/customer-information";
                return Redirect(detailsURL);

                //bool isSuccess = false;
                //foreach (SingleTicketPurchase loopItem in StudentTicketBookingList.TicketEntityInSession) {
                //    TicketBookingHistoryUpdateModel tableModel = new TicketBookingHistoryUpdateModel();
                //    tableModel.fromId = loopItem.fromId;
                //    tableModel.toId = loopItem.toId;
                //    tableModel.seatList = loopItem.SeatNumbers;
                //    tableModel.startTime = loopItem.startTime;

                //    isSuccess = _accountDomainService.BookSelectedSeat(tableModel);

                //}


                //if (isSuccess == true)
                //{
                //    string detailsURL = baseUrl + "agency/ticket-details";
                //    Redirect(detailsURL);
                //}

                //return Redirect(urlLink);
            }
               
        }

        public ActionResult GetAllVendorListForAdmin()
        {
            IList<Admin_Get_VendorList> data = _commonDomainService.GetAllVendorListForAdmin();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetAllAutocompleteProductList()
        //{
        //    IList<Get_Autocomplete_Product_List> data = _commonDomainService.GetAllAutocompleteProductList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetAllProductDiscountList()
        //{
        //    IList<PRODUCT_DISCOUNTSTable> data = _commonDomainService.GetAllProductDiscountList();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetProductInventoryTypes()
        //{
        //    IList<ProductInventoryTypeTable> data = _commonDomainService.GetProductInventoryTypes();
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public string GetSavingFileName(HttpPostedFileBase file, string savingPath) {
            Guid fileNameInGuid = Guid.NewGuid();
            
            if (!Directory.Exists(savingPath))
            {
                Directory.CreateDirectory(savingPath);
            }

            var fileName = Path.GetFileName(file.FileName);
            var saVingFileName = fileNameInGuid.ToString() + "-" + fileName;
            string uploadFilePathAndName = Path.Combine(savingPath, saVingFileName);
            file.SaveAs(uploadFilePathAndName);

            return saVingFileName;
        }


        public ActionResult UploadUserImage(List<HttpPostedFileBase> uPhotoUploder)
        {
            var file = uPhotoUploder[0];
            string savingPath = Server.MapPath("~/uploads/user");
            string saVingFileName = GetSavingFileName(file, savingPath);
            var result = new DataSourceResult()
            {
                Data = "uploads/user/" + saVingFileName
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadCategoryImage(List<HttpPostedFileBase> uPhotoUploder)
        {
            var file = uPhotoUploder[0];
            string savingPath = Server.MapPath("~/uploads/product-cat");
            string saVingFileName = GetSavingFileName(file, savingPath);
            var result = new DataSourceResult()
            {
                Data = "uploads/product-cat/" + saVingFileName
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadProductsImage(List<HttpPostedFileBase> uPhotoUploder)
        {
            var file = uPhotoUploder[0];
            string savingPath = Server.MapPath("~/uploads/products");
            string saVingFileName = GetSavingFileName(file, savingPath);
            var result = new DataSourceResult()
            {
                Data = "uploads/products/" + saVingFileName
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadProductGallery(List<HttpPostedFileBase> mltUploader)
        {
            var file = mltUploader[0];
            string savingPath = Server.MapPath("~/uploads/products/gallery");
            string saVingFileName = GetSavingFileName(file, savingPath);
            var result = new DataSourceResult()
            {
                Data = "uploads/products/gallery/" + saVingFileName
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}