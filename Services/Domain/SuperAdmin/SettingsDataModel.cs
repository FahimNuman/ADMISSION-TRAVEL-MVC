using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Domain.SuperAdmin
{
    public class SettingsDataModel
    {
        public SiteSettingsTable Setting { get; set; }
        public HttpPostedFileBase H_BannerURL_Left { get; set; }
        public HttpPostedFileBase H_BannerURL_CenterOne { get; set; }
        public HttpPostedFileBase H_BannerURL_CenterTwo { get; set; }
    }

    [Table("SiteSettings")]
    public class SiteSettingsTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string LogoURL { get; set; }
        public string FaviconURL { get; set; }
        public string HeaderContactInfo { get; set; }
        public string AboutUsPageDetails { get; set; }
        public string FAQPageDetails { get; set; }
        public string AdminEmailAddress { get; set; }
        public string PrivacyPolicyPage { get; set; }
        public string TermsAndConditionPage { get; set; }
        public string ContactLocation { get; set; }
        public string ContactMobile { get; set; }
        public string ContactEmailAddress { get; set; }
        public string CustomerCareNumber1 { get; set; }
        public string CustomerCareNumber2 { get; set; }
        public string OfficeHours { get; set; }
    }
}
