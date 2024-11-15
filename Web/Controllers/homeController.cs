using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.DomainServices.Admin;
using Services.DomainServices.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers
{
    public class homeController : BaseController
    {

        private readonly SliderDomainService _sliderDomainService;
        private readonly BlogDomainService _blogDomainService;
        private readonly CommonDomainService _commonDomainService;
        //private readonly ProductDomainService _productDomainService;
        public homeController(SliderDomainService sliderDomainService, BlogDomainService blogDomainService, CommonDomainService commonDomainService)
        {

            _sliderDomainService = sliderDomainService;
            _blogDomainService = blogDomainService;
            _commonDomainService = commonDomainService;
            //_productDomainService = productDomainService;

        }
        public ActionResult Index()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            ViewBag.AdvantageList = _commonDomainService.GetHomePageAdvantageLists();
            ViewBag.OurServiceProviders = _commonDomainService.GetHomePageServiceProviders();
            return View();
        }
     
        public ActionResult InternalServerError()
        {
            return View("~/Views/ErrorPage/InternalServerError.cshtml");
        }
        
        public PartialViewResult _LoadMainSlider(SiteSettingsTable siteSettings)
        {
            
            ViewBag.MainSlider = _sliderDomainService.GetMainSlider();
            return PartialView("~/Views/Partial/home/HomeMainSlider.cshtml");
        }

        public PartialViewResult _LoadHomePageBlog()
        {
            ViewBag.AllPostList = _blogDomainService.GetAllBlogPostList();
            return PartialView("~/Views/Partial/home/HomeTwoPost.cshtml");
        }
        
        public PartialViewResult _LoadMainMenu()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return PartialView("~/Views/Partial/Menus/TopMainMenu.cshtml");
        }

        public PartialViewResult _LoadWebSiteFooter()
        {
            ViewBag.SiteSetting = _commonDomainService.GetSiteSettingsData();
            return PartialView("~/Views/Partial/home/WebSiteFooter.cshtml");
        }

    }
}