using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Domain.Admin
{

    [Table("EmailRecoreds")]
    public class EmailRecoreds
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Mobile { get; set; }
        public string EmailAddress { get; set; }
        public string Message { get; set; }
        public DateTime SendingTime { get; set; }
    }


    [Table("Categories")]
    public class Categories
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table("OUR_ADVANTAGES")]
    public class OURADVANTAGES
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool ShowOnHomePage { get; set; }
    }

    [Table("SERVICE_PROVIDERS")]
    public class ServiceProviders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string VisibleText { get; set; }
        public string ContactUsPageText { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool ShowOnContactPage { get; set; }
    }


    [Table("FONT_AWESOME_ICONS")]
    public class FONT_AWESOME_ICONS
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string IconClass { get; set; }
    }

    [Table("ExamRoutines")]
    public class ExamRoutines
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SortDetails { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class CountryListModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    [Table("TimeSchedule")]
    public class TimeSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public int RatePerSeat { get; set; }
        public string StartTime { get; set; }
        public string ReachTime { get; set; }
        public string BusNumber { get; set; }
        public string ComanyName { get; set; }

    }

    [Table("DistricListInBD")]
    public class DistricListInBD
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public string NameBd { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }
        public string WebsiteLink { get; set; }
    }

    public class Get_Travel_From_List {
        public int FromID { get; set; }
        public string FromName { get; set; }
    }

    public class Get_Travel_To_Data_List_By_FromId
    {
        public int ToId { get; set; }
        public string ToName { get; set; }
        public int RatePerSeat { get; set; }
        public string ReachTime { get; set; }
        public string BusNumber { get; set; }
    }

    public class Admin_Get_VendorList
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    //public class Get_Autocomplete_Product_List
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; } 
    //    public string ImageURL { get; set; }
    //}
    //public class MainMenuList
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string IconClass { get; set; }
    //    public bool HasChild { get; set; }
    //    public bool HasSubMenu { get; set; }
    //    public IList<SubMenuListItem> SubMenuItem { get; set; }
    //}

    //public class SubMenuListItem
    //{
    //    public int Id { get; set; }
    //    public int ParentId { get; set; }
    //    public string IconClass { get; set; }
    //    public string Name { get; set; }
    //    public IList<SubMenuListItem> ChildMenuItem { get; set; }
    //}

    public class TicketBookingHistoryUpdateModel
    {
        public int fromId { get; set; }
        public int toId { get; set; }
        public string startTime { get; set; }
        public string seatList { get; set; }
        public string FromText { get; set; }
        public string DistenceText { get; set; }
        public string DepartDate { get; set; }
        public string TicketPrice { get; set; }
        public string BusNumber { get; set; }
        public string IsReturn { get; set; }
        public string ReturnDate { get; set; }
        public string btnMode { get; set; }
    }


    public class SingleTicketPurchaseDomain
    {
        public int fromId { get; set; }
        public int toId { get; set; }
        public string startTime { get; set; }
        public string[] SeatNumberArray { get; set; }
        public int SeatCount { get; set; }
        public int TotalSeatPrice { get; set; }
        public string PricePerSeat { get; set; }
        public string BusNumber { get; set; }
        public string TourDate { get; set; }
        public string SeatNumbers { get; set; }
        public string TourRoot { get; set; }
        public string Destination { get; set; }
        public string TravelTime { get; set; }
        public string ReturnedDate { get; set; }
        public Boolean IsReturnDate { get; set; }
    }


    [Table("TicketBookingHistory")]
    public class TicketBookingHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public string StartTime { get; set; }
        public string StationName { get; set; }
        public string TravelDate { get; set; }
        public bool S1 { get; set; }
        public bool S2 { get; set; }
        public bool S3 { get; set; }
        public bool S4 { get; set; }
        public bool S5 { get; set; }
        public bool S6 { get; set; }
        public bool S7 { get; set; }
        public bool S8 { get; set; }
        public bool S9 { get; set; }
        public bool S10 { get; set; }
        public bool S11 { get; set; }
        public bool S12 { get; set; }
        public bool S13 { get; set; }
        public bool S14 { get; set; }
        public bool S15 { get; set; }
        public bool S16 { get; set; }
        public bool S17 { get; set; }
        public bool S18 { get; set; }
        public bool S19 { get; set; }
        public bool S20 { get; set; }
        public bool S21 { get; set; }
        public bool S22 { get; set; }
        public bool S23 { get; set; }
        public bool S24 { get; set; }
        public bool S25 { get; set; }
        public bool S26 { get; set; }
        public bool S27 { get; set; }
        public bool S28 { get; set; }
        public bool S29 { get; set; }
        public bool S30 { get; set; }
        public bool S31 { get; set; }
        public bool S32 { get; set; }
        public bool S33 { get; set; }
        public bool S34 { get; set; }
        public bool S35 { get; set; }
        public bool S36 { get; set; }
        public bool S37 { get; set; }
        public bool S38 { get; set; }
        public bool S39 { get; set; }
        public bool S40 { get; set; }
        public bool S41 { get; set; }
        public bool S42 { get; set; }
        public bool S43 { get; set; }
        public bool S44 { get; set; }
        public bool S45 { get; set; }

    }

    [Table("PassengerInformation")]
    public class PassengerInformation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PNRNumber { get; set; }
        public string C_Name { get; set; }
        public string C_Phone { get; set; }
        public string C_From { get; set; }
        public string C_To { get; set; }
        public string C_Date { get; set; }
        public string C_TIme { get; set; }
        public string C_TotalSeat { get; set; }
        public string C_IsReturn { get; set; }
        public string C_returnDate { get; set; }
        public string C_Amount { get; set; }
        public string C_AgentId { get; set; }
        public string C_Status { get; set; }
        public string C_PaymentNumber { get; set; }
        public string C_TransectionNumber { get; set; }

    }

}
