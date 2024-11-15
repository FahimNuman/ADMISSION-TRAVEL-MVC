using Core.DomainService;
using Services.AppServices;
using Services.DataServices.Admin;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.Models.User.Exceptions;
using Services.Domain.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Admin
{
    public class AdminDomainService : DomainService<User, long>
    {
        private readonly AdminDataService _adminDataService;
        public static Dictionary<string, int> dLoginFailed = new Dictionary<string, int>();
        public AdminDomainService(AdminDataService adminDataService) : base(adminDataService)
            {
            _adminDataService = adminDataService;
        }

        public IList<Admin_Get_User_List> GetCustomerListForAdmin()
        {
            return _adminDataService.GetCustomerListForAdmin();
        }
        public Admin_Get_Dashboard_Counter_Box_Data GetDashboardCounterboxData()
        {
            return _adminDataService.GetDashboardCounterboxData();
        }
        public IList<EmailRecoreds> GetFeedbackandEmails()
        {
            return _adminDataService.GetFeedbackandEmails();
        }
        public IList<Admin_Get_Dashboard_Chart_Data> GetDashboardChartData()
        {
            return _adminDataService.GetDashboardChartData();
        }

        public IList<Admin_Get_TimeList> GetTravelTimeList()
        {
            return _adminDataService.GetTravelTimeList();
        }
        public IList<OURADVANTAGES> GetAllAdvantageList()
        {
            return _adminDataService.GetAllAdvantageList();
        }
        public IList<ServiceProviders> GetAllServiceProviders()
        {
            return _adminDataService.GetAllServiceProviders();
        }
        public IList<ExamRoutines> GetAllExamRoutineList()
        {
            return _adminDataService.GetAllExamRoutineList();
        }
        public int DeleteServiceProvider(int id)
        {
            return _adminDataService.DeleteServiceProvider(id);
        }

        public int DeleteSelectedTrip(int id)
        {
            return _adminDataService.DeleteSelectedTrip(id);
        }

        public int DeleteSelectedAdvantage(int id)
        {
            return _adminDataService.DeleteSelectedAdvantage(id);
        }

        public OURADVANTAGES GetAdvantageDetailsById(int id)
        {
            return _adminDataService.GetAdvantageDetailsById(id);
        }
        public ServiceProviders GetProviderDetailsById(int id)
        {
            return _adminDataService.GetProviderDetailsById(id);
        }
        public TimeSchedule GetTripDetailsByTripId(int id)
        {
            return _adminDataService.GetTripDetailsByTripId(id);
        }

        public IList<Admin_Get_Vendor_List> GetVendorListForAdmin()
        {
            return _adminDataService.GetVendorListForAdmin();
        }


        public User Login(string userName, string password, long cookieUserId, string userIp, bool isfromSingIn)
        {
            string encryptPassword = SimpleCryptService.Factory().Encrypt(password);
            var loginResult = _adminDataService.Login(userName, encryptPassword, isfromSingIn);

            if (loginResult == null)
            {
                LoginFailureCountCheek(userIp);
                throw new InvalidUserSignupException(userName);
            }

            return loginResult;
        }

        public void LoginFailureCountCheek(string userIp)
        {
            // repeat offense? add to the number of attempts
            if (dLoginFailed.ContainsKey(userIp))
            {
                // increment number of failed attempts
                ++dLoginFailed[userIp];
            }
            else // first failed login attempt for given username
            {
                // first failed attempt, so initialize it
                dLoginFailed[userIp] = 1;
            }

            var falideCount = AdminDomainService.dLoginFailed[userIp];

            if (falideCount >= 10)
            {
                string blockReason = "Login Failed 10 times";


            }

        }
    }
}
