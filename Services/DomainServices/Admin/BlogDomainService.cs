using Core.DomainService;
using Services.DataServices.Admin;
using Services.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Admin
{
    public class BlogDomainService : DomainService<BlogPosts, long>
    {
        private readonly BlogDataService _blogDataService;
        public BlogDomainService(BlogDataService blogDataService) : base(blogDataService)
            {
            _blogDataService = blogDataService;
        }

        public bool SaveNewPost(BlogOperationModel mObj, long adminUserId)
        {
            return _blogDataService.SaveNewPost(mObj, adminUserId);
        }

        public bool SaveNewComments(BlogCommentsOperationModel mObj, long adminUserId)
        {
            return _blogDataService.SaveNewComments(mObj, adminUserId);
        }
        public int DeleteSelectedNews(int id)
        {
            return _blogDataService.DeleteSelectedNews(id);
        }
        public IList<Admin_Get_PostList> GetAllBlogPostList()
        {
            return _blogDataService.GetAllBlogPostList();
        }
        public IList<Get_Popular_PostList> GetPopularPostForUser()
        {
            return _blogDataService.GetPopularPostForUser();
        }
        public Admin_Get_PostList GetBlogPostDetailsByPostId(int id)
        {
            return _blogDataService.GetBlogPostDetailsByPostId(id);
        }
        public BlogPosts GetNewsDetailsByNewsId(int id)
        {
            return _blogDataService.GetNewsDetailsByNewsId(id);
        }
        public IList<Blog_Get_PostCommentsByPostId> GetBlogPostCommentsByPostId(int id)
        {
            return _blogDataService.GetBlogPostCommentsByPostId(id);
        }
    }
}
