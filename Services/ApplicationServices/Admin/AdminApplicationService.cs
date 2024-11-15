using Services.ApplicationEntity.Admin;
using Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ApplicationServices.Admin
{
    public class AdminApplicationService
    {
        protected AdminAppEntity _adminAppEntityInstance { get; set; }

        public AdminApplicationService(AdminAppEntity adminAppEntity)
        {
            _adminAppEntityInstance = adminAppEntity;
        }

        public User Login(string userName, string password, long cookieUserId, string userIp, bool isfromSingIn)
        {
            return _adminAppEntityInstance.AdminDomainService.Login(userName, password, cookieUserId, userIp, isfromSingIn);
        }
    }
}
