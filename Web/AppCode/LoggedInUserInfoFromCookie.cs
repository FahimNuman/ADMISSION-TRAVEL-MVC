using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;
//using Microsoft.AspNetCore.Authorization;
using System.Web.Mvc;
using Services.Domain;
using Services.DomainServices;
using System.Security.Claims;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Http;

using System.Web;

namespace Web.AppCode
{
    public class LoggedInUserInfoFromCookie 
    {

        public static string LoggedInUserLogo;
        public static string LoggedInUserDisplayName;

        public static string UserIdRoleEncryptedInCookie
        {
            set
            {
                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                authCookie.Expires = DateTime.Now.AddMinutes(7200);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserRoleInCookie
        {

            get
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value))
                    return null;

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                string role = authTicket.Name.Split(':')[1];

                if (String.IsNullOrEmpty(role) == false)
                    return role;

                return null;
            }

        }

        public static long UserIdInCookie
        {
            get
            {
                HttpCookie authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null || string.IsNullOrWhiteSpace(authCookie.Value))
                    return -1;

                string loggedInUserID = "";

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                loggedInUserID = authTicket.Name.Split(':')[0];
                return long.Parse(loggedInUserID);
            }
          
        }


        public static long? AppUserIdInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserId.ToString()] != null)
                {
                    string userId = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserId.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(userId);
                    return Convert.ToInt64(authTicket.Name);
                }
                else {
                    return Convert.ToInt64(0);
                }
                
            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value.ToString(), true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserId.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserLogoInCookie
        {
        

            get
            {

                string loggedInUserLogo = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserOrCompanyLogo.ToString()].Value;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(loggedInUserLogo);
                return authTicket.Name;
            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserOrCompanyLogo.ToString(), encryptedTicket);
               // HttpCookie cookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserOrCompanyLogo.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        
        //User First Name
        public static string UserFirstNameInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserFirstName.ToString()] !=null) {
                    string firstName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserFirstName.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(firstName);
                    return authTicket.Name;
                }
                return "";

            }
            set
            {

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserFirstName.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }


        public static string UserAvatarUrlInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserAvatarURL.ToString()] != null)
                {
                    string firstName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserAvatarURL.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(firstName);
                    return authTicket.Name;
                }
                return "";

            }
            set
            {

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserAvatarURL.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserEmailAddressInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserEmailAddress.ToString()] != null)
                {
                    string firstName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserEmailAddress.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(firstName);
                    return authTicket.Name;
                }
                return "";

            }
            set
            {

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserEmailAddress.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserLastNameInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserLastName.ToString()] !=null)
                {
                    string lastName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserLastName.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(lastName);
                    return authTicket.Name;
                }
                return "";
            }
            set
            {
                //HttpCookie cookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserLastName.ToString());
                //cookie.Value = value.ToString();
                //cookie.Expires = DateTime.Now.AddDays(365);
                //HttpContext.Current.Response.Cookies.Add(cookie);

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserLastName.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }

        public static string UserDisplayNameInCookie
        {
            get
            {
                if (HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.DisplayName.ToString()]!=null) {
                    string displayName = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.DisplayName.ToString()].Value;
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(displayName);
                    return authTicket.Name;
                }

                return "";
                
            }
            set
            {
                //HttpCookie cookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.DisplayName.ToString());
                //cookie.Value = value.ToString();
                //cookie.Expires = DateTime.Now.AddDays(365);
                //HttpContext.Current.Response.Cookies.Add(cookie);

                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.DisplayName.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        
        public static bool UserRememberMeOptionsInCoockie
        {
          
           set
            {
                //HttpCookie cookieRemeberMe = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.RememberMePassword.ToString());
                //cookieRemeberMe.Expires = DateTime.Now.AddDays(365);
                //string rememberMe = "false";
                //if (value == true)
                //{
                //    rememberMe = "true";
                //}
                //cookieRemeberMe.Value = rememberMe;
                //HttpContext.Current.Response.Cookies.Add(cookieRemeberMe);

                string rememberMe = "false";
                if (value == true)
                {
                    rememberMe = "true";
                }
                var authTicket = new FormsAuthenticationTicket(rememberMe, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.RememberMePassword.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        

        public static string UserCreatedDateInCookie
        {
            get
            {
                string createdDate = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.UserCreatedDate.ToString()].Value;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(createdDate);
                return authTicket.Name;
            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.UserCreatedDate.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }


        public static string LegislationModalStatus
        {
            get
            {
                string status = HttpContext.Current.Request.Cookies[Services.Domain.Models.User.Enums.UserCookieData.LegislationModalStatus.ToString()].Value;
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(status);
                return authTicket.Name;
            }
            set
            {
                var authTicket = new FormsAuthenticationTicket(value, true, 7200);
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(Services.Domain.Models.User.Enums.UserCookieData.LegislationModalStatus.ToString(), encryptedTicket);
                authCookie.Expires = DateTime.Now.AddDays(365);
                HttpContext.Current.Response.Cookies.Add(authCookie);
            }
        }
        


    }//end class
}
