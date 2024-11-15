using Core.DataService;
using Services.DataContext;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Admin
{
    public class AdminDataService : EntityContextDataService<User>
    {
        public AdminDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public Admin_Get_Dashboard_Counter_Box_Data GetDashboardCounterboxData()
        {

            Admin_Get_Dashboard_Counter_Box_Data dataList = DbContext.Database.SqlQuery<Admin_Get_Dashboard_Counter_Box_Data>("Admin_Get_Dashboard_Counter_Box_Data").FirstOrDefault();

            return dataList;
        }
        public IList<EmailRecoreds> GetFeedbackandEmails()
        {

            IList<EmailRecoreds> dataList = DbContext.Database.SqlQuery<EmailRecoreds>("select * from EmailRecoreds").ToList();

            return dataList;
        }

        public IList<Admin_Get_Dashboard_Chart_Data> GetDashboardChartData()
        {

            List<Admin_Get_Dashboard_Chart_Data> dataList = DbContext.Database.SqlQuery<Admin_Get_Dashboard_Chart_Data>("Admin_Get_Dashboard_Chart_Data").ToList();

            return dataList;
        }

        public IList<Admin_Get_User_List> GetCustomerListForAdmin()
        {

            List<Admin_Get_User_List> dataList = DbContext.Database.SqlQuery<Admin_Get_User_List>("Admin_Get_User_List").ToList();

            return dataList;
        }

        public TimeSchedule GetTripDetailsByTripId(int id)
        {
            TimeSchedule dataList = DbContext.Database.SqlQuery<TimeSchedule>("select * from TimeSchedule where id="+ id).FirstOrDefault();

            return dataList;
        }

        public IList<OURADVANTAGES> GetAllAdvantageList()
        {
            IList<OURADVANTAGES> dataList = DbContext.Database.SqlQuery<OURADVANTAGES>("select * from OUR_ADVANTAGES" ).ToList();

            return dataList;
        }

        public IList<ServiceProviders> GetAllServiceProviders()
        {
            IList<ServiceProviders> dataList = DbContext.Database.SqlQuery<ServiceProviders>("select * from SERVICE_PROVIDERS").ToList();

            return dataList;
        }
        public IList<ExamRoutines> GetAllExamRoutineList()
        {
            IList<ExamRoutines> dataList = DbContext.Database.SqlQuery<ExamRoutines>("select * from ExamRoutines").ToList();

            return dataList;
        }

        public int DeleteServiceProvider(int id)
        {

            int result = DbContext.Database.ExecuteSqlCommand("delete [SERVICE_PROVIDERS] where id=" + id);

            return result;
        }

        public int DeleteSelectedTrip(int id)
        {

            int result = DbContext.Database.ExecuteSqlCommand("delete [TimeSchedule] where id=" + id);

            return result;
        }

        public int DeleteSelectedAdvantage(int id)
        {

            int result = DbContext.Database.ExecuteSqlCommand("delete [OUR_ADVANTAGES] where id=" + id);

            return result;
        }

        public ServiceProviders GetProviderDetailsById(int id)
        {
            ServiceProviders dataList = DbContext.Database.SqlQuery<ServiceProviders>("select * from SERVICE_PROVIDERS where id = " + id).FirstOrDefault();

            return dataList;
        }
        public OURADVANTAGES GetAdvantageDetailsById(int id)
        {
            OURADVANTAGES dataList = DbContext.Database.SqlQuery<OURADVANTAGES>("select * from OUR_ADVANTAGES where id = "+ id).FirstOrDefault();

            return dataList;
        }

        public IList<Admin_Get_TimeList> GetTravelTimeList()
        {

            List<Admin_Get_TimeList> dataList = DbContext.Database.SqlQuery<Admin_Get_TimeList>("Admin_Get_TimeList").ToList();

            return dataList;
        }

        public IList<Admin_Get_Vendor_List> GetVendorListForAdmin()
        {

            List<Admin_Get_Vendor_List> dataList = DbContext.Database.SqlQuery<Admin_Get_Vendor_List>("Admin_Get_Vendor_List").ToList();

            return dataList;
        }


        public User Login(string userName, string encryptPassword, bool isfromSingIn)
        {
            try
            {
                if (isfromSingIn == true)
                {
                    return DbContext.Set<User>().FirstOrDefault(x =>
                   (x.EmailAddress == userName) && x.Password == encryptPassword &&
                   x.IsActivated == true);
                }
                else
                {
                    return DbContext.Set<User>().FirstOrDefault(x =>
                   (x.EmailAddress == userName) && x.Password == encryptPassword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
