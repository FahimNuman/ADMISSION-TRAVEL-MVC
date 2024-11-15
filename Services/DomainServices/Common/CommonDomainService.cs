using Core.DomainService;
using Services.DataServices.Common;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Common
{
    public class CommonDomainService : DomainService<Categories, long>
    {

        private readonly CommonDataService _commonDataService;
        public CommonDomainService(CommonDataService commonDataService) : base(commonDataService)
        {
            _commonDataService = commonDataService;
        }

        public IList<OURADVANTAGES> GetHomePageAdvantageLists()
        {
            return _commonDataService.GetHomePageAdvantageLists();
        }

        public IList<ServiceProviders> GetHomePageServiceProviders()
        {
            return _commonDataService.GetHomePageServiceProviders();
        }

        public IList<ServiceProviders> GetContactPageServiceProviders()
        {
            return _commonDataService.GetContactPageServiceProviders();
        }
       
        public bool ProcessContactEmail(EmailRecoreds mObj)
        {
            return _commonDataService.ProcessContactEmail(mObj);
        }

        public IList<UserRoles> GetAllAdminRoles()
        {
            return _commonDataService.GetAllAdminRoles();
        }

        //public bool AddItemToMyCartList(long userId, string userCookie, int productId, int QTY)
        //{
        //    return _commonDataService.AddItemToMyCartList(userId, userCookie, productId, QTY);
        //}

        //public CustomerOrderEmailData ProcessCustomerOrder(OrderOperationDataModel mObj, long adminUserId)
        //{
        //    return _commonDataService.ProcessCustomerOrder(mObj, adminUserId);
        //}

        //public IList<Admin_Get_Parent_CategoryList> GetAllParentCategoryList()
        //{
        //    return _commonDataService.GetAllParentCategoryList();
        //}

        public IList<FONT_AWESOME_ICONS> GetAllFontAwesomeIconClass()
        {
            return _commonDataService.GetAllFontAwesomeIconClass();
        }

        //public IList<Get_Temp_CartList_By_Params> GetMyCartListItems(string userCookie, long userId)
        //{
        //    return _commonDataService.GetMyCartListItems(userCookie, userId);
        //}

        //public int ClearMyCartHistory(string userCookie, long userId)
        //{
        //    return _commonDataService.ClearMyCartHistory(userCookie, userId);
        //}

        //public int UpdateMyCartList(UpdateCartModel mObj, long userId)
        //{
        //    return _commonDataService.UpdateMyCartList(mObj, userId);
        //}

        //public int jQueryRemoveCurrentCartItem(string userCookie, long userId, int productId)
        //{
        //    return _commonDataService.jQueryRemoveCurrentCartItem(userCookie, userId, productId);
        //}
        public IList<Admin_Get_VendorList> GetAllVendorListForAdmin()
        {
            return _commonDataService.GetAllVendorListForAdmin();
        }
        //public IList<Get_Autocomplete_Product_List> GetAllAutocompleteProductList()
        //{
        //    return _commonDataService.GetAllAutocompleteProductList();
        //}
        //public IList<PRODUCT_DISCOUNTSTable> GetAllProductDiscountList()
        //{
        //    return _commonDataService.GetAllProductDiscountList();
        //}
        //public IList<ProductInventoryTypeTable> GetProductInventoryTypes()
        //{
        //    return _commonDataService.GetProductInventoryTypes();
        //}

        //public IList<MainMenuList> GetMainMenuList()
        //{
        //    return _commonDataService.GetMainMenuList();
        //}

        public SiteSettingsTable GetSiteSettingsData()
        {
            return _commonDataService.GetSiteSettingsData();
        }
    }
}
