using Services.ApplicationEntity.Admin;
using Services.ApplicationServices.Admin;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.Models.User.EditorModel;
using Services.Domain.SuperAdmin;
using Services.DomainServices.Admin;
using Services.DomainServices.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;
using Web.Controllers.SuperAdmin;

namespace Web.Controllers.Admin
{
    public class adminController : BaseController
    {
        
        public readonly AdminDomainService _adminDomainService;
        private readonly SuperAdminDomainService _superAdminDomainService;
        private readonly SettingsDomainService _settingsDomainService;
        // GET: AdminDashboard
        public adminController(AdminDomainService adminDomainService, SuperAdminDomainService superAdminDomainService, SettingsDomainService settingsDomainService)
        {
            _adminDomainService = adminDomainService;
            _superAdminDomainService = superAdminDomainService;
            _settingsDomainService = settingsDomainService;
        }


        public ActionResult login()
        {
            return View("~/Views/Admin/AdminDashboard/Login.cshtml");
        }

        public ActionResult dashboard()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false,true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                ViewBag.DashbaordCounter = _adminDomainService.GetDashboardCounterboxData();
                return View("~/Views/Admin/AdminDashboard/Dashboard.cshtml");
            }
            else if (userStatus != null && userStatus.HasAgent > 0) {
                return RedirectToAction("","customer");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }


        public ActionResult feedbacks()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                ViewBag.FeedbackList = _adminDomainService.GetFeedbackandEmails();
                return View("~/Views/Admin/AdminDashboard/Feedbacks.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }

        public ActionResult GetDashboardChartData()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                IList<Admin_Get_Dashboard_Chart_Data> dataList = _adminDomainService.GetDashboardChartData();

                return Json(dataList, JsonRequestBehavior.AllowGet);
            }
            else
                return RedirectToAction("unauthorized", "customer");

            
        }


        public ActionResult DeleteServiceProvider(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                int result = _adminDomainService.DeleteServiceProvider(id);

                if (result == 1)
                {
                    MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your service provider has been deleted.");
                    return RedirectToAction("service-providers", "admin");
                }
                else
                {
                    MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                }

                return RedirectToAction("service-providers", "admin");
            }
            else
                return RedirectToAction("unauthorized", "customer");


        }


        public ActionResult DeleteSelectedTrip(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                int result = _adminDomainService.DeleteSelectedTrip(id);

                if (result == 1)
                {
                    MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your rout has been deleted.");
                    return RedirectToAction("AllTrips", "admin");
                }
                else
                {
                    MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                }

                return RedirectToAction("AllTrips", "admin");
            }
            else
                return RedirectToAction("unauthorized", "customer");


        }


        public ActionResult DeleteSelectedAdvantage(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                int result = _adminDomainService.DeleteSelectedAdvantage(id);

                if (result == 1)
                {
                    MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your advantage has been deleted.");
                    return RedirectToAction("AllTrips", "admin");
                }
                else
                {
                    MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                }

                return RedirectToAction("AllTrips", "admin");
            }
            else
                return RedirectToAction("unauthorized", "customer");


        }


        [ActionName("service-providers")]
        public ActionResult AllServiceProviders()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;

                ViewBag.ServiceProviders = _adminDomainService.GetAllServiceProviders();
                return View("~/Views/Admin/AdminDashboard/AllServiceProviders.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }


        
        public ActionResult ExamRoutine()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;

                ViewBag.AllExamRoutines = _adminDomainService.GetAllExamRoutineList();
                return View("~/Views/Admin/AdminDashboard/ExamRoutine.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }

        //[ActionName("new-service-provider")]
        //public ActionResult ExamRoutine()
        //{
        //    Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

        //    if (userStatus != null && userStatus.HasAdministrators > 0)
        //    {
        //        string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
        //        ViewBag.SuccessMessage = successMessage;
        //        string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
        //        ViewBag.ErrorMessage = errorMessage;

        //        ViewBag.AllExamRoutines = _adminDomainService.GetAllExamRoutineList();
        //        return View("~/Views/Admin/AdminDashboard/ExamRoutine.cshtml");
        //    }
        //    else
        //        return RedirectToAction("unauthorized", "customer");
        //}

        [ActionName("new-service-provider")]
        public ActionResult NewServiceProviders()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                
                return View("~/Views/Admin/AdminDashboard/NewServiceProviders.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }

        [ActionName("edit-service-provider")]
        public ActionResult EditServiceProviders(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                ServiceProviders details = _adminDomainService.GetProviderDetailsById(id);
                return View("~/Views/Admin/AdminDashboard/EditServiceProviders.cshtml", details);
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }


        public ActionResult AllAdvantages()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;


                ViewBag.TimeListData = _adminDomainService.GetAllAdvantageList();
                return View("~/Views/Admin/AdminDashboard/AllAdvantages.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }

        [ActionName("new-advantage")]
        public ActionResult AddNewAdvantage()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
               
                return View("~/Views/Admin/AdminDashboard/AddNewAdvantage.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }

        [ActionName("edit-advantage")]
        public ActionResult EditExistingAdvantage(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                OURADVANTAGES details = _adminDomainService.GetAdvantageDetailsById(id);
                return View("~/Views/Admin/AdminDashboard/EditExistingAdvantage.cshtml", details);
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }



        public ActionResult AllTrips()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0) {

                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;

                ViewBag.TimeListData = _adminDomainService.GetTravelTimeList();
                return View("~/Views/Admin/AdminDashboard/AllTrips.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }


        [ActionName("new-trip")]
        public ActionResult NewTrip()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
               
                return View("~/Views/Admin/AdminDashboard/NewTrip.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }


        [ActionName("edit-trip")]
        public ActionResult EditTrip(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                TimeSchedule tripInfo = _adminDomainService.GetTripDetailsByTripId(id);
                return View("~/Views/Admin/AdminDashboard/EditTrip.cshtml", tripInfo);
            }
            else
                return RedirectToAction("unauthorized", "customer");
        }

        public ActionResult settings()
        {

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0) {
                SettingsDataModel Settings = new SettingsDataModel();
                Settings.Setting = _settingsDomainService.GetSettingsInformation();
                Settings.Setting.AboutUsPageDetails = HttpUtility.HtmlDecode(Settings.Setting.AboutUsPageDetails);
                Settings.Setting.FAQPageDetails = HttpUtility.HtmlDecode(Settings.Setting.FAQPageDetails);
                Settings.Setting.TermsAndConditionPage = HttpUtility.HtmlDecode(Settings.Setting.TermsAndConditionPage);
                Settings.Setting.PrivacyPolicyPage = HttpUtility.HtmlDecode(Settings.Setting.PrivacyPolicyPage);
                Settings.Setting.HeaderContactInfo = HttpUtility.HtmlDecode(Settings.Setting.HeaderContactInfo);

                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;


                return View("~/Views/Admin/AdminDashboard/Settings.cshtml", Settings);
            } 
            else
                return RedirectToAction("unauthorized", "customer");


            
        }

        public ActionResult agentlist()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0) {

                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;

                ViewBag.CustomerList = _adminDomainService.GetVendorListForAdmin();
                return View("~/Views/Admin/Customer/CustomerList.cshtml");
            } 
            else
                return RedirectToAction("unauthorized", "customer");
          
        }

        public ActionResult userlist()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;


                ViewBag.VendorList = _adminDomainService.GetCustomerListForAdmin();
                return View("~/Views/Admin/Customer/VendorList.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
           
        }


        [ActionName("new-agent")]
        public ActionResult NewCustomer()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return View("~/Views/Admin/Customer/NewCustomer.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
           
        }
        

        public ActionResult EditCustomer(long id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                CustomerOperationDataModel mObj = new CustomerOperationDataModel();
                return DataActionViewService(
                   () =>
                   {
                       mObj = _superAdminDomainService.GetCustomerDetailsData(id);
                       ViewBag.OldEmailAddress = mObj.User.EmailAddress;
                       ViewBag.ViewModeValue = "";

                       if (Request.QueryString["view"] != null && Request.QueryString["view"] != "")
                       {
                           ViewBag.ViewModeValue = Request.QueryString["view"];
                       }

                   },
                   () =>
                   {
                       if (mObj != null)
                       {
                           return View("~/Views/Admin/Customer/EditCustomer.cshtml", mObj);
                       }

                       return View("~/Views/Admin/Customer/EditCustomer.cshtml");
                   },
                   () => View("~/Views/Admin/Customer/EditCustomer.cshtml"));
            }
            else
                return RedirectToAction("unauthorized", "customer");

            
        }



        [HttpPost]
        public ActionResult Processlogin(LoginModel loginModel)
        {

            User loginResult = null;
            long cookieUserId = LoggedInUserInfoFromCookie.UserIdInCookie;
            bool IsFromSignIn = true;

            if (loginModel.IsFromSignIn != null)
            {
                IsFromSignIn = loginModel.IsFromSignIn.Value;
            }
            return DataActionViewService(
                () =>
                {
                    loginResult = _adminDomainService.Login(loginModel.EmailAddress, loginModel.Password, cookieUserId, loginModel.UserIP, IsFromSignIn);

                },
                () =>
                {
                    //string userIdRole;



                    //if role is user or stuff

                    if (loginResult == null)
                    {
                        MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Error to login");
                        return RedirectToAction("login", "account");
                    }

                    //userIdRole = loginResult.UserID + ":" + loginResult.UserRoleId;
                    //LoggedInUserInfoFromCookie.UserIdRoleEncryptedInCookie = userIdRole;
                    LoggedInUserInfoFromCookie.AppUserIdInCookie = loginResult.UserID;
                    LoggedInUserInfoFromCookie.UserAvatarUrlInCookie = loginResult.UserImagePath;
                    LoggedInUserInfoFromCookie.UserFirstNameInCookie = loginResult.FirstName;
                    LoggedInUserInfoFromCookie.UserLastNameInCookie = loginResult.LastName;
                    LoggedInUserInfoFromCookie.UserDisplayNameInCookie = loginResult.FirstName + " " + loginResult.LastName;
                    //LoggedInUserInfoFromCookie.UserCompanyIdInCookie = 0;
                    //LoggedInUserInfoFromCookie.PaidUserExpiredStatusInCookie = "NotExpired";
                    //LoggedInUserInfoFromCookie.TrialUserExpiredStatusInCookie = "NotExpired";
                    LoggedInUserInfoFromCookie.UserCreatedDateInCookie = loginResult.CreatedDate.ToString();
                    LoggedInUserInfoFromCookie.UserRememberMeOptionsInCoockie = loginModel.RememberMe;
                    //LoggedInUserInfoFromCookie.SelectedAssesRiskIdInCookie = "";
                    //LoggedInUserInfoFromCookie.UserExpendIdInCookie = 0;

                    //if (loginResult.UserRoleId == 1)
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}
                    Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);

                    if (userStatus != null && userStatus.HasAdministrators > 0)
                        return RedirectToAction("dashboard", "admin");
                    else
                        return RedirectToAction("dashboard", "customer");


                },
                () => View(nameof(login)));
        }
    }
}