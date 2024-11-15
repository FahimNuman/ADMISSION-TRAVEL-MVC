using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Services.Domain
{

    public class UserOperationModel
    {
        public User User { get; set; }
        public bool isAddOperation { get; set; }
    }

    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserID { get; set; }

        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "User Image Path")]
        public string UserImagePath { get; set; }

        [StringLength(200)]
        [Display(Name = "Address One")]
        public string AddressOne { get; set; }

        [StringLength(200)]
        [Display(Name = "Address Two")]
        public string AddressTwo { get; set; }
        [StringLength(100)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(50)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [StringLength(50)]
        [Display(Name = "PostCode")]
        public string PostCode { get; set; }

        [StringLength(100)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [StringLength(200)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [StringLength(100)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again!")]
        public string ConfirmPassword { get; set; }

        public bool IsActivated { get; set; }

        [Display(Name = "Created By")]
        public long? CreatedBy { get; set; }

        [Display(Name = "Created Date")]
        [Column(TypeName = "DateTime")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Edited By")]
        public long? EditedBy { get; set; }

        [Display(Name = "Edited Date")]
        [Column(TypeName = "DateTime")]
        public DateTime? EditedDate { get; set; }

        public string CreatedIP { get; set; }

        public string EditedIP { get; set; }

    }
}
