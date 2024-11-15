using Services.ApplicationEntity.Account;
using Services.AppServices;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.Account
{
    public class AccountApplicationService
    {
        protected AccountAppEntity _accountAppEntityInstance { get; set; }

        public AccountApplicationService(AccountAppEntity accountAppEntity)
        {
            _accountAppEntityInstance = accountAppEntity;
        }


        public User Login(string userName, string password, long cookieUserId, string userIp, bool isfromSingIn)
        {
            return _accountAppEntityInstance.AccountDomainService.Login(userName, password, cookieUserId, userIp, isfromSingIn);
        }

        public User ProcessNewUserLogin(string userName, string password, string key, long cookieUserId, string userIp, bool isfromSingIn)
        {
            return _accountAppEntityInstance.AccountDomainService.ProcessNewUserLogin(userName, password, key, cookieUserId, userIp, isfromSingIn);
        }

    }
}
