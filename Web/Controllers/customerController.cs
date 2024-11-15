using Services.Domain.SuperAdmin;
using Services.DomainServices;
using Services.DomainServices.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class customerController : BaseController
    {
        // GET: customer
        private readonly CustomerDomainService _customerDomainService;
        private readonly SuperAdminDomainService _superAdminDomainService;
        public customerController(CustomerDomainService customerDomainService, SuperAdminDomainService superAdminDomainService)
        {
            _superAdminDomainService = superAdminDomainService;
            _customerDomainService = customerDomainService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult _LoadCustomerProfileMenus()
        {
            return PartialView("~/Views/Partial/Menus/CustomerProfileMenu.cshtml");
        }

        public ActionResult dashboard()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(false, false, false, false);

            if (userStatus != null && userStatus.HasAgent > 0)
            {
                long adminUserId = 0;
                if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                    adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                CustomerOperationDataModel mObj = new CustomerOperationDataModel();
                return DataActionViewService(
                   () =>
                   {
                       mObj = _superAdminDomainService.GetCustomerDetailsData(adminUserId);
                       ViewBag.CustomerInformation = mObj;

                   },
                   () =>
                   {
                       if (mObj != null)
                       {
                           return View("~/Views/customer/dashboard.cshtml");
                       }

                       return RedirectToAction("unauthorized", "customer");
                   },
                   () => View("~/Views/customer/dashboard.cshtml"));
            }
            else
                return RedirectToAction("login", "account");

        }

        public ActionResult unauthorized()
        {
            
            return View("~/Views/customer/NoPermission.cshtml");
        }

        
        
    }
}