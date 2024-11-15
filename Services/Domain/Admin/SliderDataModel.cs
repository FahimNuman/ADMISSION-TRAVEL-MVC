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

    public class SliderOperationModel
    {
        public SlidersTable SlidersTable { get; set; }
        public bool isAddOperation { get; set; }
        public HttpPostedFileBase SlideImage { get; set; }
        public HttpPostedFileBase ThumbImage { get; set; }
    }

    [Table("Sliders")]
    public class SlidersTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public Boolean SliderIsActive { get; set; }
        public string ImagePath { get; set; }
        public long CreatedId { get; set; }
        public DateTime CreatedDateTime { get; set; }
       
    }


    public class Admin_Get_SliderList {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string ImagePath { get; set; }
        public string CreatedDate { get; set; }
    }


    [Table("SliderType")]
    public class SliderTypeTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

    }
}
