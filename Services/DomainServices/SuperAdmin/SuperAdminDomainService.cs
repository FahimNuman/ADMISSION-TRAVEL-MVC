using Core.DomainService;
using Services.DataServices.SuperAdmin;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.SuperAdmin
{
    public class SuperAdminDomainService : DomainService<UserRoleMaps, long>
    {

        private readonly SuperAdminDataService _superAdminDataService;
        public SuperAdminDomainService(SuperAdminDataService superAdminDataService) : base(superAdminDataService)
            {
            _superAdminDataService = superAdminDataService;
        }

        public Get_User_Permission_Status GetUserPermissionsStatus(long adminUserId)
        {
            return _superAdminDataService.GetUserPermissionsStatus(adminUserId);
        }

        public bool ProcessCustomer(CustomerOperationDataModel mObj, long adminUserId)
        {
            return _superAdminDataService.ProcessCustomer(mObj, adminUserId);
        }
        //public bool ProcessProductCategory(ProductCategoryOperationDataModel mObj, long adminUserId)
        //{
        //    return _superAdminDataService.ProcessProductCategory(mObj, adminUserId);
        //}

        //public bool ProcessProduct(ProductOperationDataModel mObj, long adminUserId)
        //{
        //    return _superAdminDataService.ProcessProduct(mObj, adminUserId);
        //}

        //public bool ProcessProductReview(ReviewProcessModel mObj, long adminUserId)
        //{
        //    return _superAdminDataService.ProcessProductReview(mObj, adminUserId);
        //}

        public bool ProcessTripForAdmin(TimeSchedule mObj, bool isAddMode)
        {
            return _superAdminDataService.ProcessTripForAdmin(mObj, isAddMode);
        }
        public bool ProcessAdvantagesInDb(OURADVANTAGES mObj, bool isAddMode)
        {
            return _superAdminDataService.ProcessAdvantagesInDb(mObj, isAddMode);
        }
        public bool ProcessServiceProviderInDb(ServiceProviders mObj, bool isAddMode)
        {
            return _superAdminDataService.ProcessServiceProviderInDb(mObj, isAddMode);
        }
        public CustomerOperationDataModel GetCustomerDetailsData(long id)
        {
            return _superAdminDataService.GetCustomerDetailsData(id);
        }
        //public IList<Customer_Get_OrderList_By_CustomerId> GetCustomerOrdersHistoryByCustomerId(long id)
        //{
        //    return _superAdminDataService.GetCustomerOrdersHistoryByCustomerId(id);
        //}
        public User NewPasswordRequest(string email, string password)
        {
            return _superAdminDataService.NewPasswordRequest(email,password);
        }

    }
}
