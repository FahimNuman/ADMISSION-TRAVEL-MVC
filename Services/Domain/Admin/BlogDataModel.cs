using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Services.Domain.Admin
{

    public class BlogOperationModel
    {
        public BlogPosts BlogPosts { get; set; }
        public bool isAddOperation { get; set; }
        public HttpPostedFileBase FeaturedImage { get; set; }
    }

    public class BlogCommentsOperationModel
    {
        public BlogComments BlogComments { get; set; }
        public bool isAddOperation { get; set; }
    }

    [Table("BlogPosts")]
    public class BlogPosts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string SortDetails { get; set; }
        public string Descriptions { get; set; }
        public string FeaturedImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedId { get; set; }
        public DateTime? EditedDate { get; set; }
        public long? EditedId { get; set; }
    }

    [Table("BlogComments")]
    public class BlogComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PostId { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Emial { get; set; }
        public string Coments { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class Get_Popular_PostList
    {
        public int PostId { get; set; }
        public int Total { get; set; }
        public string Title { get; set; }
        public string FeaturedImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class Admin_Get_PostList {
        public int Id { get; set; }
        public string Title { get; set; }
        public string SortDetails { get; set; }
        public string Descriptions { get; set; }
        public string FeaturedImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AdminName { get; set; }
        public int TotalsComments { get; set; }
    }

    public class Blog_Get_PostCommentsByPostId {
        public int Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Emial { get; set; }
        public string Coments { get; set; }
        public DateTime DateTime { get; set; }
        public string AdminName { get; set; }
    }



}
