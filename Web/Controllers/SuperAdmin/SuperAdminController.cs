using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.DomainServices.SuperAdmin;
using Services.EmailService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers.SuperAdmin
{
    public class SuperAdminController : BaseController
    {
        public static bool UserHasPermission;
        private readonly SuperAdminDomainService _superAdminDomainService;

        // GET: SuperAdmin
        public SuperAdminController(SuperAdminDomainService superAdminDomainService)
        {

            _superAdminDomainService = superAdminDomainService;
        }


        

        public PartialViewResult _CheckSuperAdminPermission()
        {
            long adminUserId = 0;
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;
            if (adminUserId > 0) {
                Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
                if (userStatus != null && userStatus.HasAdministrators > 0)
                    return PartialView("~/Views/Partial/Permissions/UserAllowed.cshtml");
            }

            return PartialView("~/Views/Partial/Permissions/UserNotAllowed.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProveidersInDb(ServiceProviders mObj)
        {
            bool isAddSuccess = false;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "admin/service-providers";

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            isAddSuccess = _superAdminDomainService.ProcessServiceProviderInDb(mObj, false);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "The provider has been updated.");

                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return Redirect(url);
                    },
                    () => View());

            }

            return Redirect(url);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewProveidersInDb(ServiceProviders mObj)
        {
            bool isAddSuccess = false;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "admin/service-providers";

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            isAddSuccess = _superAdminDomainService.ProcessServiceProviderInDb(mObj, true);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "A new service provider has been added.");

                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return Redirect(url);
                    },
                    () => View());

            }

            return Redirect(url);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExistingAdvantagesInDb(OURADVANTAGES mObj)
        {
            bool isAddSuccess = false;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "admin/AllAdvantages";

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            isAddSuccess = _superAdminDomainService.ProcessAdvantagesInDb(mObj, false);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "The advantage has been updated.");

                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return Redirect(url);
                    },
                    () => View());

            }

            return Redirect(url);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewAdvantagesInDb(OURADVANTAGES mObj)
        {
            bool isAddSuccess = false;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "admin/AllAdvantages";

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            isAddSuccess = _superAdminDomainService.ProcessAdvantagesInDb(mObj, true);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "A new advantage has been added.");

                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return Redirect(url);
                    },
                    () => View());

            }

            return Redirect(url);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNewTripInDb(TimeSchedule mObj)
        {
            bool isAddSuccess = false;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "admin/AllTrips";

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            isAddSuccess = _superAdminDomainService.ProcessTripForAdmin(mObj, true);
                        }

                    },
                    () =>
                    {

                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "A new trip has been added.");
                          
                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return Redirect(url);
                    },
                    () => View());
                
            }

            return Redirect(url);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditExistingTripInDb(TimeSchedule mObj)
        {
            bool isAddSuccess = false;
            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                            Request.ApplicationPath.TrimEnd('/') + "/";
            string url = baseUrl + "admin/AllTrips";

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, false);
            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            isAddSuccess = _superAdminDomainService.ProcessTripForAdmin(mObj, false);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "The trip has been updated.");

                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return Redirect(url);
                    },
                    () => View());

            }

            return Redirect(url);
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessEditCustomer(CustomerOperationDataModel mObj)
        {
            bool isAddSuccess = false;
            long adminUserId = 0;
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

            string activateKey = GetUserActivitionKey();
            string password = mObj.User.Password;
            return DataActionViewService(
                () =>
                {
                    if (mObj != null)
                    {
                        mObj.User.EditedIP = GetIPAddress();
                        mObj.isAddOperation = false;
                        isAddSuccess = _superAdminDomainService.ProcessCustomer(mObj, adminUserId);
                    }

                },
                () =>
                {
                    if (isAddSuccess == true)
                    {
                        string emailBodyContent = "";
                        string fileName = Server.MapPath("~/email-template/signup.html");


                        if (System.IO.File.Exists(fileName) == true)
                        {
                            using (StreamReader textReader = new System.IO.StreamReader(fileName))
                            {
                                emailBodyContent = textReader.ReadToEnd();
                            }
                        }

                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                        Request.ApplicationPath.TrimEnd('/') + "/";

                        emailBodyContent = emailBodyContent.Replace("###UserName###", mObj.User.FirstName + " " + mObj.User.LastName);
                        emailBodyContent = emailBodyContent.Replace("###WebSiteURL###", baseUrl);
                        emailBodyContent = emailBodyContent.Replace("###WebSiteLogo###", baseUrl + "images/logo.png");
                        emailBodyContent = emailBodyContent.Replace("###PreHeader###", "Update Information");
                        emailBodyContent = emailBodyContent.Replace("###LoginURL###", baseUrl + "account/login/" + activateKey);

                        emailBodyContent = emailBodyContent.Replace("###UserEmail###", mObj.User.EmailAddress);
                        emailBodyContent = emailBodyContent.Replace("###Password###", password);

                        Emailer.SendMail(mObj.User.EmailAddress, "Update Information", emailBodyContent);
                        MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Update success.");

                        if (Request.QueryString["mode"] != null && Request.QueryString["mode"] != "" && Request.QueryString["mode"] == "admin")
                        {
                            return RedirectToAction("userlist", "admin");
                        }
                        else
                        {
                            return RedirectToAction("agentlist", "admin");
                        }
                    }
                    else
                    {
                        MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                    }

                    return RedirectToAction("agentlist", "admin");
                },
                () => View());


        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessNewCustomer(CustomerOperationDataModel mObj)
        {
            bool isAddSuccess = false;
            long adminUserId = 0;
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

            string activateKey = GetUserActivitionKey();
            string password = mObj.User.Password;
            return DataActionViewService(
                () =>
                {
                    if (mObj != null)
                    {
                        mObj.User.CreatedIP = GetIPAddress();
                        //mObj.User.UserActivateKey = activateKey;
                        mObj.isAddOperation = true;
                        isAddSuccess = _superAdminDomainService.ProcessCustomer(mObj, adminUserId);
                    }

                },
                () =>
                {
                    if (isAddSuccess == true)
                    {
                        string emailBodyContent = "";
                        string fileName = Server.MapPath("~/email-template/signup.html");


                        if (System.IO.File.Exists(fileName) == true)
                        {
                            using (StreamReader textReader = new System.IO.StreamReader(fileName))
                            {
                                emailBodyContent = textReader.ReadToEnd();
                            }
                        }

                        string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                        Request.ApplicationPath.TrimEnd('/') + "/";

                        emailBodyContent = emailBodyContent.Replace("###UserName###", mObj.User.FirstName + " " + mObj.User.LastName);
                        emailBodyContent = emailBodyContent.Replace("###WebSiteURL###", baseUrl);
                        emailBodyContent = emailBodyContent.Replace("###WebSiteLogo###", baseUrl + "images/logo.png");
                        emailBodyContent = emailBodyContent.Replace("###PreHeader###", "New Login Permission");
                        emailBodyContent = emailBodyContent.Replace("###LoginURL###", baseUrl + "account/login/" + activateKey);

                        emailBodyContent = emailBodyContent.Replace("###UserEmail###", mObj.User.EmailAddress);
                        emailBodyContent = emailBodyContent.Replace("###Password###", password);

                        Emailer.SendMail(mObj.User.EmailAddress, "New Login Permission", emailBodyContent);
                        MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Update success.");

                        if (Request.QueryString["mode"] != null && Request.QueryString["mode"] != "" && Request.QueryString["mode"] == "admin")
                        {
                            return RedirectToAction("userlist", "admin");
                        }
                        else {
                            return RedirectToAction("agentlist", "admin");
                        }
                            
                    }
                    else
                    {
                        MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                    }

                    return RedirectToAction("agentlist", "admin");
                },
                () => View());


        }


        public string GenerateRandomPassword()
        {
            
            string numbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_!@#$1234567890";

            string characters = numbers;
            int length = 10;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }

            return otp;
        }

        [HttpPost]
        public ActionResult NewPasswordRequest(string email)
        {
            string password = GenerateRandomPassword();
            string activateKey = GetUserActivitionKey();
            User user = _superAdminDomainService.NewPasswordRequest(email, password);

            if (user != null) {

                string emailBodyContent = "";
                string fileName = Server.MapPath("~/email-template/forgot-password.html");


                if (System.IO.File.Exists(fileName) == true)
                {
                    using (StreamReader textReader = new System.IO.StreamReader(fileName))
                    {
                        emailBodyContent = textReader.ReadToEnd();
                    }
                }

                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                Request.ApplicationPath.TrimEnd('/') + "/";

                emailBodyContent = emailBodyContent.Replace("###UserName###", user.FirstName + " " + user.LastName);
                emailBodyContent = emailBodyContent.Replace("###WebSiteURL###", baseUrl);
                emailBodyContent = emailBodyContent.Replace("###WebSiteLogo###", baseUrl + "images/logo.png");
                emailBodyContent = emailBodyContent.Replace("###PreHeader###", "Update Password");
                emailBodyContent = emailBodyContent.Replace("###LoginURL###", baseUrl + "account/login/" + activateKey);

                emailBodyContent = emailBodyContent.Replace("###UserEmail###", user.EmailAddress);
                emailBodyContent = emailBodyContent.Replace("###Password###", password);

                Emailer.SendMail(user.EmailAddress, "Update Password", emailBodyContent);
            }

            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Your email address not collected in our list.");
            return RedirectToAction("forgot-password", "account");

        }

        public ActionResult Index()
        {
            return View();
        }

    }
}