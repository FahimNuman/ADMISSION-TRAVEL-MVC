using PagedList;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.DomainServices.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.AppCode;

namespace Web.Controllers.Admin
{
    public class blogController : BaseController
    {
        private readonly BlogDomainService _blogDomainService;
        // GET: blog
        public blogController(BlogDomainService blogDomainService)
        {

            _blogDomainService = blogDomainService;
        }

        public ActionResult Index(int? page)
        {
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = 5;
            ViewBag.AllPostList = _blogDomainService.GetAllBlogPostList().ToPagedList(pageIndex, defaSize); ;
            return View("~/Views/blog/Index.cshtml");
        }


        public ActionResult DeleteSelectedNews(int id)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                int result = _blogDomainService.DeleteSelectedNews(id);

                if (result == 1)
                {
                    MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your news has been deleted.");
                    return RedirectToAction("PostList", "blog");
                }
                else
                {
                    MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                }

                return RedirectToAction("PostList", "blog");
            }
            else
                return RedirectToAction("unauthorized", "customer");


        }

        public ActionResult PostList()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.SuccessMessage = successMessage;
                string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
                ViewBag.ErrorMessage = errorMessage;

                ViewBag.AllPostList = _blogDomainService.GetAllBlogPostList();
                return View("~/Views/blog/PostList.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");

            
        }

        public PartialViewResult _LoadPopularPost()
        {
            ViewBag.PopularPostList = _blogDomainService.GetPopularPostForUser();
            return PartialView("~/Views/Partial/Blog/PopularPostList.cshtml");
        }

        [ActionName("new-post")]
        public ActionResult NewPost()
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {
                return View("~/Views/blog/NewPost.cshtml");
            }
            else
                return RedirectToAction("unauthorized", "customer");
            
        }


        [ActionName("edit-news")]
        public ActionResult EditPost(int id)
        {
            try
            {
                Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

                if (userStatus != null && userStatus.HasAdministrators > 0)
                {
                    BlogPosts info = _blogDomainService.GetNewsDetailsByNewsId(id);
                    info.Descriptions= HttpUtility.HtmlDecode(info.Descriptions);
                    BlogOperationModel mObj = new BlogOperationModel();
                    mObj.BlogPosts = info;
                    ViewBag.FeaturedImage = info.FeaturedImagePath;
                    return View("~/Views/blog/EditPost.cshtml", mObj);
                }
                else
                    return RedirectToAction("unauthorized", "customer");
            }
            catch (Exception ex) {
                throw ex;
            }

        }

        public ActionResult details(int id)
        {

            string successMessage = MessageDisplayHelper.InformationMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.SuccessMessage = successMessage;
            string errorMessage = MessageDisplayHelper.ErrorMessageSetOrDisplay(this, false, string.Empty);
            ViewBag.ErrorMessage = errorMessage;


            ViewBag.PostDetailsData = _blogDomainService.GetBlogPostDetailsByPostId(id);
            ViewBag.CommentsList = _blogDomainService.GetBlogPostCommentsByPostId(id);
            return View("~/Views/blog/BlogDetails.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveExistingPost(BlogOperationModel mObj)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {

                bool isAddSuccess = false;
                long adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            if (mObj.FeaturedImage != null)
                                mObj.BlogPosts.FeaturedImagePath = GetUploadImagePath(mObj.FeaturedImage, "posts");
                            mObj.isAddOperation = false;
                            isAddSuccess = _blogDomainService.SaveNewPost(mObj, adminUserId);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "The news has been updated.");
                            return RedirectToAction("PostList", "blog");
                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return RedirectToAction("new-post", "blog");
                    },
                    () => View());
            }
            else
                return RedirectToAction("unauthorized", "customer");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewPost(BlogOperationModel mObj)
        {
            Get_User_Permission_Status userStatus = GetUserAllPermissions(true, false, false, true);

            if (userStatus != null && userStatus.HasAdministrators > 0)
            {

                bool isAddSuccess = false;
                long adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

                return DataActionViewService(
                    () =>
                    {
                        if (mObj != null)
                        {
                            if (mObj.FeaturedImage != null)
                                mObj.BlogPosts.FeaturedImagePath = GetUploadImagePath(mObj.FeaturedImage, "posts");
                            mObj.isAddOperation = true;
                            isAddSuccess = _blogDomainService.SaveNewPost(mObj, adminUserId);
                        }

                    },
                    () =>
                    {
                        if (isAddSuccess == true)
                        {
                            MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "A news has been added.");
                            return RedirectToAction("PostList", "blog");
                        }
                        else
                        {
                            MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                        }

                        return RedirectToAction("new-post", "blog");
                    },
                    () => View());
            }
            else
                return RedirectToAction("unauthorized", "customer");
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNewComments(BlogCommentsOperationModel mObj)
        {
            bool isAddSuccess = false;
            long adminUserId =0;
            if (LoggedInUserInfoFromCookie.AppUserIdInCookie != null)
                adminUserId = LoggedInUserInfoFromCookie.AppUserIdInCookie.Value;

            string baseUrl = Request.Url.Scheme + "://" + Request.Url.Authority +
                       Request.ApplicationPath.TrimEnd('/') + "/";
            string urlLink = baseUrl + "blog/details/"+mObj.BlogComments.PostId;

            return DataActionViewService(
                () =>
                {
                    if (mObj != null)
                    {
                        mObj.isAddOperation = true;
                        isAddSuccess = _blogDomainService.SaveNewComments(mObj, adminUserId);
                    }

                },
                () =>
                {
                    if (isAddSuccess == true)
                    {
                        MessageDisplayHelper.InformationMessageSetOrDisplay(this, true, "Your comments has been publised.");
                        
                    }
                    else
                    {
                        MessageDisplayHelper.ErrorMessageSetOrDisplay(this, true, "Something wrong");
                    }
                    return Redirect(urlLink);

                },
                () => View());


        }

    }
}