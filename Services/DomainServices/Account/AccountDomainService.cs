using Core.DomainService;
using Services.AppServices;
using Services.DataServices.Account;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.Models.User.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Account
{
    public class AccountDomainService : DomainService<User, long>
    {
        private readonly AccountDataService _accountDataService;
        public static Dictionary<string, int> dLoginFailed = new Dictionary<string, int>();
        public AccountDomainService(AccountDataService accountDataService) : base(accountDataService)
            {
            _accountDataService = accountDataService;
        }

        public User Login(string userName, string password, long cookieUserId, string userIp, bool isfromSingIn)
        {
            string encryptPassword = SimpleCryptService.Factory().Encrypt(password);
            var loginResult = _accountDataService.Login(userName, encryptPassword, isfromSingIn);

            if (loginResult == null)
            {
                LoginFailureCountCheek(userIp);
                throw new InvalidUserSignupException(userName);
            }

            return loginResult;
        }


        public User ProcessNewUserLogin(string userName, string password, string key, long cookieUserId, string userIp, bool isfromSingIn)
        {
            string encryptPassword = SimpleCryptService.Factory().Encrypt(password);
            var loginResult = _accountDataService.ProcessNewUserLogin(userName, encryptPassword, key, isfromSingIn);

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

            var falideCount = AccountDomainService.dLoginFailed[userIp];

            if (falideCount >= 10)
            {
                string blockReason = "Login Failed 10 times";


            }

        }

        public IList<CountryListModel> GetAllCountryList()
        {
            return _accountDataService.GetAllCountryList();
        }
        public IList<DistricListInBD> GetAllDistrictListInsideBangladesh()
        {
            return _accountDataService.GetAllDistrictListInsideBangladesh();
        }

        public IList<Get_Travel_From_List> GetTravelFromSourceDataList()
        {
            return _accountDataService.GetTravelFromSourceDataList();
        }
        public IList<DistricListInBD> GetDistrictListByRemoveParamId(int id)
        {
            return _accountDataService.GetDistrictListByRemoveParamId(id);
        }
        public IList<Get_Travel_To_Data_List_By_FromId> GetTravelToDataRemoveFromId(int id)
        {
            return _accountDataService.GetTravelToDataRemoveFromId(id);
        }

        public IList<TimeSchedule> GetBusTimeScheduleListByFromAndToId(int fromId, int toId)
        {
            return _accountDataService.GetBusTimeScheduleListByFromAndToId(fromId, toId);
        }

        public IList<TicketBookingHistory> GetTicketBookingHistoryByParam(int fromId, int toId, string startTime, string travelDate)
        {
            return _accountDataService.GetTicketBookingHistoryByParam(fromId, toId, startTime, travelDate);
        }

        public bool BookSelectedSeat(TicketBookingHistoryUpdateModel model)
        {
            return _accountDataService.BookSelectedSeat(model);
        }

        public bool SaveBookingCustomerInformation(PassengerInformation model)
        {
            return _accountDataService.SaveBookingCustomerInformation(model);
        }

        public bool ProcessNewUser(UserOperationModel mObj)
        {
            return _accountDataService.ProcessNewUser(mObj);
        }
        public int CheckUserEmailIsExist(string email)
        {
            return _accountDataService.CheckUserEmailIsExist(email);
        }

        public string ResendActivateKey(string key,string newKey)
        {
            return _accountDataService.ResendActivateKey(key, newKey);
        }

        public User GetUserInfoByActivateKey(string key)
        {
            return _accountDataService.GetUserInfoByActivateKey(key);
        }
    }

}
