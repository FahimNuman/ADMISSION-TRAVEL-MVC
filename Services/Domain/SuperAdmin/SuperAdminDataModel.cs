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

    public class Get_User_Permission_Status {
        public int HasAdministrators { get; set; }
        public int HasAgent { get; set; }
    }
    public class CustomerOperationDataModel {
        public User User { get; set; }
        public string UserRoleIdWithComma { get; set; }
        public bool isAddOperation { get; set; }
        public HttpPostedFileBase ThumbImage { get; set; }
        public string ViewModeValue { get; set; }
    }


    [Table("UserRoleMaps")]
    public class UserRoleMaps {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public long UserId { get; set; }
        public int UserRoleId { get; set; }
    }


    [Table("UserRoles")]
    public class UserRoles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Role { get; set; }
    }


    public class Admin_Get_Vendor_List
    {
        public long UserID { get; set; }
        public string UserFullName { get; set; }
        public string UserImagePath { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string City { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserRole { get; set; }
        public bool IsActivated { get; set; }
    }


    public class Admin_Get_User_List {
        public long UserID { get; set; }
        public string UserFullName { get; set; }
        public string UserImagePath { get; set; }
        public string EmailAddress { get; set; }
        public bool RegistrationConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActiveTime { get; set; }
        public string UserRole { get; set; }
        public bool IsActivated { get; set; }
    }

    public class Admin_Get_Dashboard_Chart_Data {
        public int TicketSale { get; set; }
        public string AmountDate { get; set; }
        public string SaleDate { get; set; }
        public int C_Amount { get; set; }
    }
    public class Admin_Get_Dashboard_Counter_Box_Data {
        public int Agents { get; set; }
        public int BusRoute { get; set; }
        public int ServiceProvider { get; set; }
        public int Advantage { get; set; }

    }

    public class Admin_Get_TimeList {
        public int Id { get; set; }
        public int FromID { get; set; }
        public string FromName { get; set; }
        public string ToName { get; set; }
        public int RatePerSeat { get; set; }
        public string StartTime { get; set; }
        public string ReachTime { get; set; }
        public string BusNumber { get; set; }
        public string ComanyName { get; set; }
    }

}
