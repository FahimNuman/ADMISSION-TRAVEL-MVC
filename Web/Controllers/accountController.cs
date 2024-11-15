using Services.ApplicationEntity.Account;
using Services.ApplicationServices.Account;
using Services.Domain;
using Services.Domain.Models.User.EditorModel;
using Services.Domain.SuperAdmin;
using Services.DomainServices;
using Services.DomainServices.Account;
using Services.EmailService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.AppCode;

namespace Web.Controllers
{
    public class accountController : BaseController
    {
        public readonly AccountDomainService _accountDomainService;

        public AccountAppEntity _accountAppEntity;
        public AccountApplicationService _accountApplicationService;
       
        // GET: account
        public accountController(AccountDomainService accountDomainService)
        {
            _accountDomainService = accountDomainService;
            _accountAppEntity = new AccountAppEntity();
            _accountAppEntity.AccountDomainService = accountDomainService;
            _accountApplicationService = new AccountApplicationService(_accountAppEntity);

        }

        public ActionResult Index(LoginModel model)
        {
            return View();
        }

        public ActionResult login(LoginModel model)
        {
            string serverMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.ServerMessage = serverMessage;
            return View();
        }

        public ActionResult registerresult()
        {
            return View();
        }

        public ActionResult register()
        {
            ViewBag.CountryList = new SelectList(_accountDomainService.GetAllCountryList(), "Name", "Name");
            return View();
        }

        [ActionName("forgot-password")]
        public ActionResult ForGotPassword()
        {
            string serverMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.ServerMessage = serverMessage;
            return View("~/Views/account/ForGotPassword.cshtml");
        }

        public ActionResult activation()
        {
            string serviceName = Request.Url.ToString();
            string key = serviceName.Split('/').Last();
            int minutes = 0;
            User userInfo = _accountDomainService.GetUserInfoByActivateKey(key);
            if (userInfo != null)
            {
                DateTime uTIme = userInfo.CreatedDate;
                DateTime cTime = DateTime.Now;

                TimeSpan duration = cTime - uTIme;
                minutes = Convert.ToInt32(duration.TotalMinutes);
            }

            ViewBag.ActiveKey = key;
            ViewBag.Duration = minutes;

            return View();
        }

        public ActionResult CheckUserEmailIsExist(string email)
        {
            int data = _accountDomainService.CheckUserEmailIsExist(email);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResendActivateKey(string key)
        {
            int data = 0;
            string newKey = GetUserActivitionKey();
            string email = _accountDomainService.ResendActivateKey(key, newKey);
           
            if (email != "") {
                string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
                string msg = "Click Following Link <br/>";
                msg += "<a href='" + baseUrl + "account/activation/" + newKey + "'> Active Account </a>";
                Emailer.SendMail(email, "New User Registration", msg);
                data = 1;
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessNewUser(UserOperationModel mObj)
        {
            bool isSuccess = false;
            long adminUserId = LoggedInUserInfoFromCookie.UserIdInCookie;
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
                        isSuccess = _accountDomainService.ProcessNewUser(mObj);
                    }

                },
                () =>
                {
                    if (isSuccess == true)
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

                        emailBodyContent= emailBodyContent.Replace("###UserName###", mObj.User.FirstName + " " + mObj.User.LastName);
                        emailBodyContent = emailBodyContent.Replace("###WebSiteURL###", baseUrl);
                        emailBodyContent = emailBodyContent.Replace("###WebSiteLogo###", baseUrl + "images/logo.png");
                        emailBodyContent = emailBodyContent.Replace("###PreHeader###", "New User Registration");
                        emailBodyContent = emailBodyContent.Replace("###LoginURL###", baseUrl + "account/login/" + activateKey);

                        emailBodyContent = emailBodyContent.Replace("###UserEmail###", mObj.User.EmailAddress);
                        emailBodyContent = emailBodyContent.Replace("###Password###", password);

                        Emailer.SendMail(mObj.User.EmailAddress, "New User Registration", emailBodyContent);
                        
                        MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Update success.");
                        return RedirectToAction("registerresult", "account");
                    }
                    else
                    {
                        MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                    }

                    return RedirectToAction("register", "account");
                },
                () => View());


        }



        public ActionResult Logout()
        {

            if (User.Identity.IsAuthenticated)
            {
                long userId = LoggedInUserInfoFromCookie.UserIdInCookie;
                //  _userDomainService.UpdateCurrentUserLoggedInInfo(userId);

                var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    authCookie.Expires = DateTime.Today.AddDays(-1);
                }

                Response.Cookies.Add(authCookie);

                FormsAuthentication.SignOut();

            }

            List<string> cookieKeyNameList = new List<string>();
            foreach (var key in HttpContext.Request.Cookies.Keys)
            {
                cookieKeyNameList.Add(key.ToString());
            }

            if (cookieKeyNameList != null && cookieKeyNameList.Count > 0)
            {
                foreach (string cookieKey in cookieKeyNameList)
                {
                    HttpCookie cookie = new HttpCookie(cookieKey);
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                }
            }
            return RedirectToAction("Index", "home");
        }




        [HttpPost]
        public ActionResult ProcessNewUserLogin(LoginModel loginModel)
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
                    loginResult = _accountApplicationService.ProcessNewUserLogin(loginModel.EmailAddress, loginModel.Password, loginModel.UserActivateKey, cookieUserId, loginModel.UserIP, IsFromSignIn);

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
                    LoggedInUserInfoFromCookie.UserAvatarUrlInCookie = loginResult.UserImagePath;
                    //LoggedInUserInfoFromCookie.UserLogoInCookie = loginResult.UserImagePath;
                    LoggedInUserInfoFromCookie.UserFirstNameInCookie = loginResult.FirstName;
                    LoggedInUserInfoFromCookie.UserLastNameInCookie = loginResult.LastName;
                    LoggedInUserInfoFromCookie.UserDisplayNameInCookie = loginResult.FirstName + " " + loginResult.LastName;
                    //LoggedInUserInfoFromCookie.UserCompanyIdInCookie = 0;
                    //LoggedInUserInfoFromCookie.PaidUserExpiredStatusInCookie = "NotExpired";
                    //LoggedInUserInfoFromCookie.TrialUserExpiredStatusInCookie = "NotExpired";
                    LoggedInUserInfoFromCookie.UserCreatedDateInCookie = loginResult.CreatedDate.ToString();
                    // LoggedInUserInfoFromCookie.UserRememberMeOptionsInCoockie = loginModel.RememberMe;
                    //LoggedInUserInfoFromCookie.SelectedAssesRiskIdInCookie = "";
                    //LoggedInUserInfoFromCookie.UserExpendIdInCookie = 0;

                    //if (loginResult.UserRoleId == 1)
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}


                    return RedirectToAction("Index", "home");


                },
                () => View(nameof(Index)));
        }


        #region Login Related Method


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
                    loginResult = _accountApplicationService.Login(loginModel.EmailAddress, loginModel.Password, cookieUserId, loginModel.UserIP, IsFromSignIn);

                },
                () =>
                {
                    //string userIdRole;



                    //if role is user or stuff

                    if (loginResult == null)
                    {
                        MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Invalid email or password");
                        return RedirectToAction("login", "account");
                    }


                    //userIdRole = loginResult.UserID + ":" + loginResult.UserRoleId;
                    //LoggedInUserInfoFromCookie.UserIdRoleEncryptedInCookie = userIdRole;
                    LoggedInUserInfoFromCookie.AppUserIdInCookie = loginResult.UserID;
                    LoggedInUserInfoFromCookie.UserEmailAddressInCookie = loginResult.EmailAddress;
                    //LoggedInUserInfoFromCookie.UserLogoInCookie = loginResult.UserImagePath;
                    LoggedInUserInfoFromCookie.UserAvatarUrlInCookie = loginResult.UserImagePath;
                    LoggedInUserInfoFromCookie.UserFirstNameInCookie = loginResult.FirstName;
                    LoggedInUserInfoFromCookie.UserLastNameInCookie = loginResult.LastName;
                    LoggedInUserInfoFromCookie.UserDisplayNameInCookie = loginResult.FirstName + " " + loginResult.LastName;
                    //LoggedInUserInfoFromCookie.UserCompanyIdInCookie = 0;
                    //LoggedInUserInfoFromCookie.PaidUserExpiredStatusInCookie = "NotExpired";
                    //LoggedInUserInfoFromCookie.TrialUserExpiredStatusInCookie = "NotExpired";
                    LoggedInUserInfoFromCookie.UserCreatedDateInCookie = loginResult.CreatedDate.ToString();
                    // LoggedInUserInfoFromCookie.UserRememberMeOptionsInCoockie = loginModel.RememberMe;
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
                () => View(nameof(Index)));
        }

        #endregion
    }
}