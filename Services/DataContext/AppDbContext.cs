using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Services.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRoleMaps> UserRoleMaps { get; set; }

        public virtual DbSet<OURADVANTAGES> OURADVANTAGES { get; set; }

        public virtual DbSet<PassengerInformation> PassengerInformation { get; set; }
        public virtual DbSet<ServiceProviders> ServiceProviders { get; set; }
        public virtual DbSet<ExamRoutines> ExamRoutines { get; set; }
        public virtual DbSet<EmailRecoreds> EmailRecoreds { get; set; }
        public virtual DbSet<DistricListInBD> DistricListInBD { get; set; }
        public virtual DbSet<TicketBookingHistory> TicketBookingHistory { get; set; }

        public virtual DbSet<TimeSchedule> TimeSchedule { get; set; }

        public virtual DbSet<SiteSettingsTable> SiteSettingsTable { get; set; }
        public virtual DbSet<SlidersTable> SlidersTable { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<BlogPosts> BlogPosts { get; set; }
        public virtual DbSet<BlogComments> BlogComments { get; set; }
        //public virtual DbSet<PRODUCT_ORDERS> CustomerOrderTable { get; set; }
        //public virtual DbSet<PRODUCTSTable> PRODUCTSTable { get; set; }
        //public virtual DbSet<OrderStatus> OrderStatus { get; set; }
        //public virtual DbSet<PRODUCT_ORDER_MAP> CustomerOrderMap { get; set; }
        //public virtual DbSet<PRODUCT_CATEGORIES> ProductCategories { get; set; }
        //public virtual DbSet<PRODUCT_DISCOUNTSTable> PRODUCT_DISCOUNTSTable { get; set; }
        //public virtual DbSet<TempCartItems> TempCartItems { get; set; }
        //public virtual DbSet<ProductInventoryTypeTable> ProductInventoryTypeTable { get; set; }
        //public virtual DbSet<PRODUCT_IMAGE_MAPTable> PRODUCT_IMAGE_MAPTable { get; set; }
        //public virtual DbSet<PRODCUT_REVIEWS> PRODCUT_REVIEWS { get; set; }
        //public virtual DbSet<PRODUCT_CATEGPRY_MAPTable> PRODUCT_CATEGPRY_MAPTable { get; set; }
        public virtual DbSet<FONT_AWESOME_ICONS> FontAwesomeIcons { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}