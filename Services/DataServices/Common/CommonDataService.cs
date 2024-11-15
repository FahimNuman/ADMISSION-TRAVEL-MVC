using Core.DataService;
using Services.DataContext;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Common
{
    public class CommonDataService : EntityContextDataService<Categories>
    {
        public CommonDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }


        //public bool AddItemToMyCartList(long userId, string userCookie, int productId, int QTY)
        //{

        //    try
        //    {
        //        if (userId > 0)
        //        {
        //            TempCartItems dbValue = new TempCartItems();
        //            dbValue.UserId = userId;
        //            dbValue.ProductId = productId;
        //            dbValue.QTY = QTY;
        //            dbValue.CartDateTime = DateTime.Now;
        //            DbContext.Set<TempCartItems>().Add(dbValue);
        //            DbContext.SaveChanges();
        //            return true;

                    
        //        }
        //        else
        //        {
        //            TempCartItems dbValue = new TempCartItems();
        //            dbValue.CookieId = userCookie;
        //            dbValue.ProductId = productId;
        //            dbValue.QTY = QTY;
        //            dbValue.CartDateTime = DateTime.Now;
        //            DbContext.Set<TempCartItems>().Add(dbValue);
        //            DbContext.SaveChanges();
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //public CustomerOrderEmailData ProcessCustomerOrder(OrderOperationDataModel mObj, long adminUserId)
        //{

        //    try
        //    {
        //        DateTime currentDateTime = DateTime.Now;
        //        CustomerOrderEmailData emailData = new CustomerOrderEmailData();

        //        if (mObj != null && mObj.OrderTable != null) {

        //            if (adminUserId > 0)
        //                mObj.OrderTable.CustomerId = adminUserId;

        //            mObj.OrderTable.OrderStatusId = 1;
        //            mObj.OrderTable.OrderDateTime = currentDateTime;
        //            DbContext.Set<PRODUCT_ORDERS>().Add(mObj.OrderTable);
        //            DbContext.SaveChanges();

        //            string cookieId = "";
        //            if (mObj.OrderTable.TempCookie != null)
        //                cookieId = mObj.OrderTable.TempCookie;

        //            SqlParameter prmCookieId = new SqlParameter("@cookieId", cookieId);
        //            SqlParameter prmUserId = new SqlParameter("@uId", adminUserId);
        //            IList<Get_Temp_CartList_By_Params> dataList = DbContext.Database.SqlQuery<Get_Temp_CartList_By_Params>("Get_Temp_CartList_By_Params @cookieId,@uId", prmCookieId, prmUserId).ToList();

        //            if (dataList != null && dataList.Count > 0) {

        //                for (int mp = 0; mp < dataList.Count; mp++) {
        //                    PRODUCT_ORDER_MAP mapTable = new PRODUCT_ORDER_MAP();
        //                    mapTable.ProductId = dataList[mp].Id;
        //                    mapTable.OrderId = mObj.OrderTable.Id;
        //                    mapTable.ProductName = dataList[mp].Name;
        //                    mapTable.QTY = dataList[mp].TotalQTY;
        //                    mapTable.Price = dataList[mp].Price;
        //                    mapTable.TotalPrice = dataList[mp].TotalPrice;
        //                    mapTable.OrderDate = currentDateTime;
        //                    DbContext.Set<PRODUCT_ORDER_MAP>().Add(mapTable);
        //                    DbContext.SaveChanges();

        //                    if (adminUserId > 0)
        //                       DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where ProductId= "+ dataList[mp].Id + " and UserId=" + adminUserId);
        //                    else
        //                        DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where ProductId= " + dataList[mp].Id + " and CookieId='" + mObj.OrderTable.TempCookie+"'");
                            
        //                }
        //            }

        //            emailData.OrderItemList = dataList;
        //            emailData.OrderTable = mObj.OrderTable;
        //        }

        //        return emailData;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public bool ProcessContactEmail(EmailRecoreds mObj)
        {

            try
            {
                if (mObj != null)
                {
                    mObj.SendingTime = DateTime.Now;
                    DbContext.Set<EmailRecoreds>().Add(mObj);
                    DbContext.SaveChanges();

                    return true;

                }

                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<OURADVANTAGES> GetHomePageAdvantageLists()
        {
            IList<OURADVANTAGES> dataList = DbContext.Database.SqlQuery<OURADVANTAGES>("SELECT * FROM OUR_ADVANTAGES where ShowOnHomePage =1").ToList();
            return dataList;
        }

        public IList<ServiceProviders> GetHomePageServiceProviders()
        {
            IList<ServiceProviders> dataList = DbContext.Database.SqlQuery<ServiceProviders>("SELECT * FROM SERVICE_PROVIDERS where ShowOnHomePage =1").ToList();
            return dataList;
        }

        public IList<ServiceProviders> GetContactPageServiceProviders()
        {
            IList<ServiceProviders> dataList = DbContext.Database.SqlQuery<ServiceProviders>("SELECT * FROM SERVICE_PROVIDERS where ShowOnContactPage =1").ToList();
            return dataList;
        }

        public IList<UserRoles> GetAllAdminRoles()
        {
            IList<UserRoles> dataList = DbContext.Database.SqlQuery<UserRoles>("SELECT * FROM UserRoles").ToList();
            return dataList;
        }

        public IList<FONT_AWESOME_ICONS> GetAllFontAwesomeIconClass()
        {
            IList<FONT_AWESOME_ICONS> dataList = DbContext.Database.SqlQuery<FONT_AWESOME_ICONS>("SELECT * FROM FONT_AWESOME_ICONS").ToList();
            return dataList;
        }

        //public IList<Admin_Get_Parent_CategoryList> GetAllParentCategoryList()
        //{
        //    IList<Admin_Get_Parent_CategoryList> dataList = DbContext.Database.SqlQuery<Admin_Get_Parent_CategoryList>("Admin_Get_Parent_CategoryList").ToList();
        //    return dataList;
        //}
         
        //public IList<Get_Temp_CartList_By_Params> GetMyCartListItems(string userCookie, long userId)
        //{
        //    SqlParameter prmCookieId = new SqlParameter("@cookieId", userCookie);
        //    SqlParameter prmUserId = new SqlParameter("@uId", userId);
        //    IList<Get_Temp_CartList_By_Params> dataList = DbContext.Database.SqlQuery<Get_Temp_CartList_By_Params>("Get_Temp_CartList_By_Params @cookieId,@uId", prmCookieId, prmUserId).ToList();
        //    return dataList;
        //}

        //public int ClearMyCartHistory(string userCookie, long userId)
        //{
        //    int result = 0;
        //    if (userId > 0)
        //        result = DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where UserId=" + userId);
        //    else
        //        result = DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where CookieId=" + userCookie);
        //    return result;
        //}

        //public int UpdateMyCartList(UpdateCartModel mObj, long userId)
        //{
        //    int result = 0;

        //    if (mObj != null && mObj.productItems != null && mObj.productItems.Count > 0)
        //    {
        //        for (int i = 0; i < mObj.productItems.Count; i++) {

        //            if (userId > 0)
        //                result+= DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where UserId=" + userId + " and ProductId=" + mObj.productItems[i].ProductId);
        //            else
        //                result += DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where CookieId=" + mObj.BrowserCookieId + " and ProductId=" + mObj.productItems[i].ProductId);

        //            TempCartItems dbValue = new TempCartItems();
        //            dbValue.CookieId = mObj.BrowserCookieId;
        //            dbValue.ProductId = mObj.productItems[i].ProductId;
        //            dbValue.QTY = mObj.productItems[i].QTY;
        //            dbValue.CartDateTime = DateTime.Now;
        //            DbContext.Set<TempCartItems>().Add(dbValue);
        //            DbContext.SaveChanges();
        //        }
        //    }
            
        //    return result;
        //}

        public int jQueryRemoveCurrentCartItem(string userCookie, long userId, int productId)
        {
            int result = 0;
            if (userId > 0)
                result = DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where UserId=" + userId + " and ProductId="+ productId);
            else
                result = DbContext.Database.ExecuteSqlCommand("Delete from TempCartItems where CookieId=" + userCookie + " and ProductId=" + productId);
            return result;
        }

        public IList<Admin_Get_VendorList> GetAllVendorListForAdmin()
        {
            List<Admin_Get_VendorList> dataList = DbContext.Database.SqlQuery<Admin_Get_VendorList>("Admin_Get_VendorList").ToList();
            return dataList;
        }

        //public IList<Get_Autocomplete_Product_List> GetAllAutocompleteProductList()
        //{
        //    List<Get_Autocomplete_Product_List> dataList = DbContext.Database.SqlQuery<Get_Autocomplete_Product_List>("Get_Autocomplete_Product_List").ToList();
        //    return dataList;
        //}

        //public IList<PRODUCT_DISCOUNTSTable> GetAllProductDiscountList()
        //{
        //    IList<PRODUCT_DISCOUNTSTable> dataList = DbContext.Database.SqlQuery<PRODUCT_DISCOUNTSTable>("select * from [dbo].[PRODUCT_DISCOUNTS]").ToList();
        //    return dataList;
        //}

        //public IList<ProductInventoryTypeTable> GetProductInventoryTypes()
        //{
        //    IList<ProductInventoryTypeTable> dataList = DbContext.Database.SqlQuery<ProductInventoryTypeTable>("select * from [dbo].[PRODUCT_INVENTORY_TYPE]").ToList();
        //    return dataList;
        //}

        public SiteSettingsTable GetSiteSettingsData()
        {
            try
            {
                SiteSettingsTable data = DbContext.Database.SqlQuery<SiteSettingsTable>("select * from SiteSettings").FirstOrDefault();
                return data;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        //public IList<MainMenuList> GetMainMenuList()
        //{
        //    IList<MainMenuList> menuList = DbContext.Database.SqlQuery<MainMenuList>("select Id, Name, IconClass from PRODUCT_CATEGORIES where Published=1 and ShowOnTopMenu = 1 and ParentCatId is null").ToList();
        //    if (menuList.Count > 0) {
        //        for (int menu = 0; menu < menuList.Count; menu++) {
        //            IList<SubMenuListItem> subMenuList= DbContext.Database.SqlQuery<SubMenuListItem>("select Id, Name, IconClass from PRODUCT_CATEGORIES where Published=1 and ShowOnTopMenu = 1 and ParentCatId =" + menuList[menu].Id).ToList();
        //            menuList[menu].SubMenuItem = subMenuList;

        //            if (subMenuList.Count > 0) {
        //                menuList[menu].HasSubMenu = true;

        //                for (int sub = 0; sub < subMenuList.Count; sub++) {
        //                    IList<SubMenuListItem> childMenu = DbContext.Database.SqlQuery<SubMenuListItem>("select Id, Name, IconClass from PRODUCT_CATEGORIES where Published=1 and ShowOnTopMenu = 1 and ParentCatId =" + menuList[menu].SubMenuItem[sub].Id).ToList();
        //                    menuList[menu].SubMenuItem[sub].ChildMenuItem = childMenu;
        //                    if (childMenu.Count > 0) 
        //                        menuList[menu].HasChild = true;
        //                }
        //            }
        //        }
        //    }

        //    return menuList;
        //}

    }
}
