using Services.Domain.SuperAdmin;
using Services.DomainServices.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers.SuperAdmin
{
    public class SettingsController : BaseController
    {
        // GET: Settings
        private readonly SettingsDomainService _settingsDomainService;
        public SettingsController(SettingsDomainService settingsDomainService)
        {
            _settingsDomainService = settingsDomainService;
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAboutUsPage(SettingsDataModel mObj)
        {
            bool isAddSuccess = _settingsDomainService.UpdateAboutUsPage(mObj);
            if (isAddSuccess == true)
            {
                MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your website information has been updated");

            }
            else
            {
                MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
            }
            return RedirectToAction("settings", "admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFAQPage(SettingsDataModel mObj)
        {
            bool isAddSuccess = _settingsDomainService.UpdateFAQPage(mObj);
            if (isAddSuccess == true)
            {
                MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your website information has been updated");

            }
            else
            {
                MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
            }
            return RedirectToAction("settings", "admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatPrivacyPolicyPage(SettingsDataModel mObj)
        {
            bool isAddSuccess = _settingsDomainService.UpdatPrivacyPolicyPage(mObj);
            if (isAddSuccess == true)
            {
                MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your website information has been updated");

            }
            else
            {
                MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
            }
            return RedirectToAction("settings", "admin");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatTermsAndConditionPage(SettingsDataModel mObj)
        {
            bool isAddSuccess = _settingsDomainService.UpdatTermsAndConditionPage(mObj);
            if (isAddSuccess == true)
            {
                MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your website information has been updated");

            }
            else
            {
                MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
            }
            return RedirectToAction("settings", "admin");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateWebsiteInformation(SettingsDataModel mObj)
        {
            bool isAddSuccess = _settingsDomainService.UpdateWebsiteInformation(mObj);
            if (isAddSuccess == true)
            {
                MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your website information has been updated");

            }
            else
            {
                MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
            }
            return RedirectToAction("settings", "admin");
        }
    }
}