using Core.DataService;
using Services.DataContext;
using Services.Domain.SuperAdmin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.SuperAdmin
{
    public class SettingsDataService : EntityContextDataService<SiteSettingsTable>
    {
        public SettingsDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public SiteSettingsTable GetSettingsInformation()
        {
            SiteSettingsTable dataList = DbContext.Database.SqlQuery<SiteSettingsTable>("SELECT * FROM SiteSettings").FirstOrDefault();
            return dataList;
        }

        public bool UpdateAboutUsPage(SettingsDataModel mObj)
        {
            try
            {
                if (mObj != null && mObj.Setting != null)
                {
                    SiteSettingsTable oldData = DbContext.Set<SiteSettingsTable>().Where(x => x.Id == mObj.Setting.Id).FirstOrDefault();
                    if (mObj.Setting.AboutUsPageDetails != null)
                        oldData.AboutUsPageDetails = mObj.Setting.AboutUsPageDetails;
                    else
                        oldData.AboutUsPageDetails = "";
                    DbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public bool UpdateFAQPage(SettingsDataModel mObj)
        {
            try
            {
                if (mObj != null && mObj.Setting != null)
                {
                    SiteSettingsTable oldData = DbContext.Set<SiteSettingsTable>().Where(x => x.Id == mObj.Setting.Id).FirstOrDefault();
                    if (mObj.Setting.FAQPageDetails != null)
                        oldData.FAQPageDetails = mObj.Setting.FAQPageDetails;
                    else
                        oldData.FAQPageDetails = "";
                    DbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatPrivacyPolicyPage(SettingsDataModel mObj)
        {
            try
            {
                if (mObj != null && mObj.Setting != null)
                {
                    SiteSettingsTable oldData = DbContext.Set<SiteSettingsTable>().Where(x => x.Id == mObj.Setting.Id).FirstOrDefault();
                    if (mObj.Setting.PrivacyPolicyPage != null)
                        oldData.PrivacyPolicyPage = mObj.Setting.PrivacyPolicyPage;
                    else
                        oldData.PrivacyPolicyPage = "";
                    DbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatTermsAndConditionPage(SettingsDataModel mObj)
        {
            try
            {
                if (mObj != null && mObj.Setting != null)
                {
                    SiteSettingsTable oldData = DbContext.Set<SiteSettingsTable>().Where(x => x.Id == mObj.Setting.Id).FirstOrDefault();
                    if (mObj.Setting.TermsAndConditionPage != null)
                        oldData.TermsAndConditionPage = mObj.Setting.TermsAndConditionPage;
                    else
                        oldData.TermsAndConditionPage = "";
                    DbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool UpdateWebsiteInformation(SettingsDataModel mObj)
        {
            try
            {
                if (mObj != null && mObj.Setting != null)
                {
                    SiteSettingsTable oldData = DbContext.Set<SiteSettingsTable>().Where(x => x.Id == mObj.Setting.Id).FirstOrDefault();


                    if (mObj.Setting.LogoURL != null && mObj.Setting.LogoURL !="")
                        oldData.LogoURL = mObj.Setting.LogoURL;
                    else
                        oldData.LogoURL = "images/logo.png";

                    if (mObj.Setting.HeaderContactInfo != null && mObj.Setting.HeaderContactInfo != "")
                        oldData.HeaderContactInfo = mObj.Setting.HeaderContactInfo;
                    else
                        oldData.HeaderContactInfo = "KabirStor@gmail.com";

                    if (mObj.Setting.AdminEmailAddress != null && mObj.Setting.AdminEmailAddress != "")
                        oldData.AdminEmailAddress = mObj.Setting.AdminEmailAddress;
                    else
                        oldData.AdminEmailAddress = "KabirStor@gmail.com";

                    if (mObj.Setting.ContactLocation != null && mObj.Setting.ContactLocation != "")
                        oldData.ContactLocation = mObj.Setting.ContactLocation;
                    else
                        oldData.ContactLocation = "Mymensingh, Charpara, NOyapara - 0033 ";

                    if (mObj.Setting.ContactEmailAddress != null && mObj.Setting.ContactEmailAddress != "")
                        oldData.ContactEmailAddress = mObj.Setting.ContactEmailAddress;
                    else
                        oldData.ContactEmailAddress = "KabirStor@gmail.com";

                    if (mObj.Setting.ContactMobile != null && mObj.Setting.ContactMobile != "")
                        oldData.ContactMobile = mObj.Setting.ContactMobile;
                    else
                        oldData.ContactMobile = "+880 1974920082 ";

                    if (mObj.Setting.CustomerCareNumber1 != null && mObj.Setting.CustomerCareNumber1 != "")
                        oldData.CustomerCareNumber1 = mObj.Setting.CustomerCareNumber1;
                    else
                        oldData.CustomerCareNumber1 = "+880 1974920082 ";

                    if (mObj.Setting.CustomerCareNumber2 != null && mObj.Setting.CustomerCareNumber2 != "")
                        oldData.CustomerCareNumber2 = mObj.Setting.CustomerCareNumber2;
                    else
                        oldData.CustomerCareNumber2 = "+880 1974920082 ";

                    DbContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
