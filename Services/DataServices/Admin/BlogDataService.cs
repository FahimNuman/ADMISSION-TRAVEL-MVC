using Core.DataService;
using Services.DataContext;
using Services.Domain.Admin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Admin
{
    public class BlogDataService : EntityContextDataService<BlogPosts>
    {
        public BlogDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }


        public IList<Admin_Get_PostList> GetAllBlogPostList()
        {
            List<Admin_Get_PostList> dataList = DbContext.Database.SqlQuery<Admin_Get_PostList>("Admin_Get_PostList").ToList();

            return dataList;
        }

        public int DeleteSelectedNews(int id)
        {

            int commentCount =  DbContext.Database.ExecuteSqlCommand("delete [BlogComments] where PostId=" + id);
            int result = DbContext.Database.ExecuteSqlCommand("delete [BlogPosts] where id=" + id);

            return result;
        }

        public IList<Get_Popular_PostList> GetPopularPostForUser()
        {
            List<Get_Popular_PostList> dataList = DbContext.Database.SqlQuery<Get_Popular_PostList>("Get_Popular_PostList").ToList();

            return dataList;
        }

        public Admin_Get_PostList GetBlogPostDetailsByPostId(int id)
        {
            SqlParameter prmId = new SqlParameter("@id", id);

            Admin_Get_PostList data = DbContext.Database.SqlQuery<Admin_Get_PostList>("[Admin_Get_PostDetailsByPostId] @id",prmId).FirstOrDefault();

            return data;
        }

        public BlogPosts GetNewsDetailsByNewsId(int id)
        {
            try
            {
                BlogPosts data = DbContext.Database.SqlQuery<BlogPosts>("select * from BlogPosts where Id = " + id).FirstOrDefault();

                return data;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public IList<Blog_Get_PostCommentsByPostId> GetBlogPostCommentsByPostId(int id)
        {
            SqlParameter prmId = new SqlParameter("@id", id);

            IList<Blog_Get_PostCommentsByPostId> data = DbContext.Database.SqlQuery<Blog_Get_PostCommentsByPostId>("[Blog_Get_PostCommentsByPostId] @id", prmId).ToList();

            return data;
        }

        public bool SaveNewPost(BlogOperationModel mObj, long adminUserId)
        {


            if (mObj != null && mObj.isAddOperation == true)
            {
                mObj.BlogPosts.CreatedDate = DateTime.Now;
                mObj.BlogPosts.CreatedId = adminUserId;
                DbContext.Set<BlogPosts>().Add(mObj.BlogPosts);
                DbContext.SaveChanges();

                return true;
            }

            if (mObj != null && mObj.isAddOperation == false)
            {
                BlogPosts oldItem = DbContext.Set<BlogPosts>().Where(x => x.Id == mObj.BlogPosts.Id).FirstOrDefault();

                oldItem.Title = mObj.BlogPosts.Title;
                oldItem.SortDetails = mObj.BlogPosts.SortDetails;
                oldItem.Descriptions = mObj.BlogPosts.Descriptions;
                if (mObj.BlogPosts.FeaturedImagePath != null)
                    oldItem.FeaturedImagePath = mObj.BlogPosts.FeaturedImagePath;
                oldItem.EditedDate = DateTime.Now;
                oldItem.EditedId = adminUserId;
                DbContext.SaveChanges();

                return true;
            }

            return false;

        }



        public bool SaveNewComments(BlogCommentsOperationModel mObj, long adminUserId)
        {


            if (mObj != null && mObj.isAddOperation == true)
            {
                mObj.BlogComments.DateTime = DateTime.Now;
                DbContext.Set<BlogComments>().Add(mObj.BlogComments);
                DbContext.SaveChanges();

                return true;
            }

            if (mObj != null && mObj.isAddOperation == false)
            {
                BlogComments oldItem = DbContext.Set<BlogComments>().Where(x => x.Id == mObj.BlogComments.Id).FirstOrDefault();

                oldItem.Coments = mObj.BlogComments.Coments;
                DbContext.SaveChanges();

                return true;
            }

            return false;

        }


    }
}
