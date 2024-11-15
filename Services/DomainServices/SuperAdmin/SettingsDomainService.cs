using Core.DomainService;
using Services.DataServices.SuperAdmin;
using Services.Domain.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.SuperAdmin
{
    public class SettingsDomainService : DomainService<SiteSettingsTable, long>
    {

        private readonly SettingsDataService _settingsDataService;
        public SettingsDomainService(SettingsDataService settingsDataService) : base(settingsDataService)
            {
            _settingsDataService = settingsDataService;
        }

        public SiteSettingsTable GetSettingsInformation()
        {
            return _settingsDataService.GetSettingsInformation();
        }

        public bool UpdateAboutUsPage(SettingsDataModel mObj)
        {
            return _settingsDataService.UpdateAboutUsPage(mObj);
        }

        public bool UpdateFAQPage(SettingsDataModel mObj)
        {
            return _settingsDataService.UpdateFAQPage(mObj);
        }
        public bool UpdatPrivacyPolicyPage(SettingsDataModel mObj)
        {
            return _settingsDataService.UpdatPrivacyPolicyPage(mObj);
        }

        public bool UpdatTermsAndConditionPage(SettingsDataModel mObj)
        {
            return _settingsDataService.UpdatTermsAndConditionPage(mObj);
        }

        public bool UpdateWebsiteInformation(SettingsDataModel mObj)
        {
            return _settingsDataService.UpdateWebsiteInformation(mObj);
        }
    }
}
