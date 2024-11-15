using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.DomainServices.Admin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers.Admin
{
    public class slidersController : BaseController
    {

        private readonly SliderDomainService _sliderDomainService;

        // GET: sliders

        public slidersController(SliderDomainService sliderDomainService)
        {
            
            _sliderDomainService = sliderDomainService;
        }
        public ActionResult Index()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {

                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;

                ViewBag.SliderList = _sliderDomainService.GetAllSliderList();
                return View();
            }
            else
                return RedirectToAction("unauthorized", "customer");
           
        }

        public ActionResult GetSliderTypeList()
        {
            IList<SliderTypeTable> dataList = _sliderDomainService.GetSliderTypeList();

            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        [ActionName("new-slider")]
        public ActionResult NewSlider()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                ViewBag.ProductCatList = new SelectList(_sliderDomainService.GetAllProductCategoryList(), "Id", "Name");
                return View("~/Views/sliders/NewSlider.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
            
        }
        
        public ActionResult edit(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {

                SliderOperationModel sliderOpModel = new SliderOperationModel();
                SlidersTable slider = new SlidersTable();
                ViewBag.ProductCatList = new SelectList(_sliderDomainService.GetAllProductCategoryList(), "Id", "Name");
                ViewBag.SliderTypeList = new SelectList(_sliderDomainService.GetSliderTypeList(), "Id", "Name");

                return DataActionViewService(
                   () =>
                   {
                       slider = _sliderDomainService.GetSliderDetailsById(id);
                       ViewBag.ImagePath = slider.ImagePath;
                       sliderOpModel.SlidersTable = slider;
                   },
                   () =>
                   {
                       if (sliderOpModel != null)
                       {
                           return View("~/Views/sliders/edit.cshtml", sliderOpModel);
                       }

                       return View("~/Views/sliders/edit.cshtml");
                   },
                   () => View("~/Views/sliders/edit.cshtml"));
            }
            else
                return RedirectToAction("unauthorized", "customer");

        }

        public ActionResult DeleteSlider(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                int result = _sliderDomainService.DeleteSlider(id);

                if (result == 1)
                {
                    MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your slider has been deleted");
                    return RedirectToAction("Index", "sliders");
                }
                else
                {
                    MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                }

                return RedirectToAction("Index", "sliders");
            }
            else
                return RedirectToAction("unauthorized", "customer");

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSliderEdit(SliderOperationModel mObj)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {

                bool isEditSuccess = false;
                long adminUserId = LoggedInUserInfoFromCookie.UserIdInCookie;

                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            if (mObj.SlideImage != null)
                                mObj.SlidersTable.ImagePath = GetUploadImagePath(mObj.SlideImage, "sliders");

                            mObj.isAddOperation = false;
                            isEditSuccess = _sliderDomainService.ProcessSlider(mObj, adminUserId);
                        }

                    },
                    () =>
                    {
                        if (isEditSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "The slider has been updated.");
                            return RedirectToAction("Index", "sliders");
                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return RedirectToAction("new-slider", "sliders");
                    },
                    () => View());
            }
            else
                return RedirectToAction("unauthorized", "customer");
            
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessSlider(SliderOperationModel mObj)
        {

            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {

                bool isAddSuccess = false;
                long adminUserId = LoggedInUserInfoFromCookie.UserIdInCookie;

                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            if (mObj.SlideImage != null)
                                mObj.SlidersTable.ImagePath = GetUploadImagePath(mObj.SlideImage, "sliders");
                            
                            mObj.isAddOperation = true;
                            isAddSuccess = _sliderDomainService.ProcessSlider(mObj, adminUserId);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "A new slider has been added.");
                            return RedirectToAction("Index", "sliders");
                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return RedirectToAction("new-slider", "sliders");
                    },
                    () => View());
            }
            else
                return RedirectToAction("unauthorized", "customer");
            
        }



    }
}