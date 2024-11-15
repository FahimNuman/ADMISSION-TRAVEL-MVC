using Core.DataService;
using Services.AppServices;
using Services.DataContext;
using Services.Domain;
using Services.Domain.Admin;
using Services.Domain.SuperAdmin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.SuperAdmin
{
    public class SuperAdminDataService : EntityContextDataService<UserRoleMaps>
    {
        public SuperAdminDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }


        public Get_User_Permission_Status GetUserPermissionsStatus(long adminUserId)
        {
            SqlParameter prmUserId = new SqlParameter("@userId", adminUserId);
            Get_User_Permission_Status dataList = DbContext.Database.SqlQuery<Get_User_Permission_Status>("Get_User_Permission_Status @userId", prmUserId).FirstOrDefault();
            return dataList;
        }

        //public IList<Customer_Get_OrderList_By_CustomerId> GetCustomerOrdersHistoryByCustomerId(long id)
        //{
        //    SqlParameter prmUserId = new SqlParameter("@customerId", id);
        //    IList<Customer_Get_OrderList_By_CustomerId> dataList = DbContext.Database.SqlQuery<Customer_Get_OrderList_By_CustomerId>("Customer_Get_OrderList_By_CustomerId @customerId", prmUserId).ToList();
        //    return dataList;
        //}

        public CustomerOperationDataModel GetCustomerDetailsData(long id)
        {
            CustomerOperationDataModel returnData = new CustomerOperationDataModel();
            User user = DbContext.Set<User>().Where(x => x.UserID == id).FirstOrDefault();

            user.Password= SimpleCryptService.Factory().Decrypt(user.Password);
            user.ConfirmPassword = SimpleCryptService.Factory().Decrypt(user.ConfirmPassword);

            IList<UserRoleMaps> map = DbContext.Set<UserRoleMaps>().Where(x => x.UserId == id).ToList();

            if (map.Count > 0) {
                string uRole = "";
                foreach (UserRoleMaps i in map) {
                    if (uRole == "")
                        uRole = i.UserRoleId.ToString();
                    else
                        uRole = uRole + "," + i.UserRoleId.ToString();
                }

                returnData.UserRoleIdWithComma = uRole;
            }

            returnData.User = user;

            return returnData;

        }
        public User NewPasswordRequest(string email, string password)
        {
            User user = new User();
            try
            {
                
                if (email !="")
                {
                    user = DbContext.Set<User>().Where(x => x.EmailAddress == email.Trim()).FirstOrDefault();

                    if (user != null) {
                        user.Password = SimpleCryptService.Factory().Encrypt(password);
                        user.ConfirmPassword= SimpleCryptService.Factory().Encrypt(password);
                        DbContext.SaveChanges();

                        return user;
                    }
                }

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ProcessCustomer(CustomerOperationDataModel mObj, long adminUserId)
        {

            try
            {
                if (mObj != null && mObj.isAddOperation == true)
                {
                    string encryptNewPass = SimpleCryptService.Factory().Encrypt(mObj.User.Password);
                    mObj.User.Password = encryptNewPass;
                    mObj.User.ConfirmPassword = encryptNewPass;
                    mObj.User.CreatedDate = DateTime.Now;
                    mObj.User.CreatedBy = adminUserId;
                    DbContext.Set<User>().Add(mObj.User);
                    DbContext.SaveChanges();

                    if (mObj.UserRoleIdWithComma != null && mObj.UserRoleIdWithComma != "")
                    {
                        var splitIdArray = mObj.UserRoleIdWithComma.Split(',');

                        for (int item = 0; item < splitIdArray.Length; item++)
                        {
                            UserRoleMaps map = new UserRoleMaps();
                            map.UserRoleId = Convert.ToInt32(splitIdArray[item]);
                            map.UserId = mObj.User.UserID;
                            DbContext.Set<UserRoleMaps>().Add(map);
                            DbContext.SaveChanges();
                        }
                    }

                    return true;
                }
                if (mObj != null && mObj.isAddOperation == false)
                {
                    User user = DbContext.Set<User>().Where(x => x.UserID == mObj.User.UserID).FirstOrDefault();
                    
                    user.FirstName = mObj.User.FirstName;
                    user.LastName = mObj.User.LastName;
                    user.UserImagePath = mObj.User.UserImagePath;
                    user.AddressOne = mObj.User.AddressOne;
                    user.AddressTwo = mObj.User.AddressTwo;
                    user.City = mObj.User.City;
                    user.PhoneNumber = mObj.User.PhoneNumber;
                    user.PostCode = mObj.User.PostCode;
                    user.Country = mObj.User.Country;
                    user.EmailAddress = mObj.User.EmailAddress;
                    user.Password = SimpleCryptService.Factory().Encrypt(mObj.User.Password);
                    user.ConfirmPassword = SimpleCryptService.Factory().Encrypt(mObj.User.Password);
                    //user.CurrentPassword = SimpleCryptService.Factory().Encrypt(mObj.User.Password);
                    //user.RegistrationConfirmed = mObj.User.RegistrationConfirmed;
                    user.IsActivated = mObj.User.IsActivated;
                    //user.UserActivateKey = mObj.User.UserActivateKey;
                    //user.LastActiveTime = mObj.User.LastActiveTime;
                    user.EditedBy = adminUserId;
                    user.EditedDate = DateTime.Now;
                    user.EditedIP = mObj.User.EditedIP;
                    DbContext.SaveChanges();

                    

                    if (mObj.UserRoleIdWithComma != null && mObj.UserRoleIdWithComma != "")
                    {
                        int result = DbContext.Database.ExecuteSqlCommand("delete [UserRoleMaps] where UserId =" + mObj.User.UserID);

                        var splitIdArray = mObj.UserRoleIdWithComma.Split(',');

                        for (int item = 0; item < splitIdArray.Length; item++)
                        {
                            UserRoleMaps map = new UserRoleMaps();
                            map.UserRoleId = Convert.ToInt32(splitIdArray[item]);
                            map.UserId = mObj.User.UserID;
                            DbContext.Set<UserRoleMaps>().Add(map);
                            DbContext.SaveChanges();
                        }
                    }

                    return true;
                    
                }

                return false;
            }
            catch (Exception ex) {
                throw ex;
            }
        }




        //public bool ProcessProductCategory(ProductCategoryOperationDataModel mObj, long adminUserId)
        //{

        //    try
        //    {
        //        if (mObj != null && mObj.isAddOperation == true)
        //        {
        //            mObj.ProductCat.CreatedDate = DateTime.Now;
        //            mObj.ProductCat.CreatedId = adminUserId;
        //            DbContext.Set<PRODUCT_CATEGORIES>().Add(mObj.ProductCat);
        //            DbContext.SaveChanges();
                    
        //            return true;
        //        }
        //        if (mObj != null && mObj.isAddOperation == false)
        //        {
        //            PRODUCT_CATEGORIES oldData = DbContext.Set<PRODUCT_CATEGORIES>().Where(x => x.Id == mObj.ProductCat.Id).FirstOrDefault();

        //            oldData.Name = mObj.ProductCat.Name;
        //            oldData.Description = mObj.ProductCat.Description;
        //            oldData.ImageURL = mObj.ProductCat.ImageURL;
        //            oldData.ParentCatId = mObj.ProductCat.ParentCatId;
        //            oldData.ShowOnHomePage = mObj.ProductCat.ShowOnHomePage;
        //            oldData.ShowOnTopMenu = mObj.ProductCat.ShowOnTopMenu;
        //            oldData.Published = mObj.ProductCat.Published;
        //            oldData.DisplayOrder = mObj.ProductCat.DisplayOrder;
        //            oldData.SortName = mObj.ProductCat.SortName;
        //            oldData.IconClass = mObj.ProductCat.IconClass;
        //            oldData.EditedId = adminUserId;
        //            oldData.EditedDate = DateTime.Now;
        //            DbContext.SaveChanges();
                    
        //            return true;

        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public bool ProcessProductReview(ReviewProcessModel mObj, long adminUserId)
        //{

        //    try
        //    {
        //        if (mObj != null && mObj.isAddOperation == true)
        //        {

        //            if (adminUserId > 0) {
        //                User userData = DbContext.Set<User>().Where(x => x.UserID == adminUserId).FirstOrDefault();
        //                mObj.Review.CustomerId = userData.UserID;
        //                mObj.Review.CustomerEmail = userData.EmailAddress;
        //                mObj.Review.CustomerName = userData.FirstName + " " + userData.LastName;
        //            }
        //            mObj.Review.ReviewDate = DateTime.Now;
        //            DbContext.Set<PRODCUT_REVIEWS>().Add(mObj.Review);
        //            DbContext.SaveChanges();
                    
        //            return true;

        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public bool ProcessTripForAdmin(TimeSchedule mObj, bool isAddMode)
        {

            try
            {
                if (mObj != null && isAddMode == true)
                {
                    DbContext.Set<TimeSchedule>().Add(mObj);
                    DbContext.SaveChanges();

                    return true;

                }

                if (mObj != null && isAddMode == false)
                {
                    TimeSchedule oldData = DbContext.Set<TimeSchedule>().Where(x => x.Id == mObj.Id).FirstOrDefault();
                    oldData.FromId = mObj.FromId;
                    oldData.ToId = mObj.ToId;
                    oldData.RatePerSeat = mObj.RatePerSeat;
                    oldData.StartTime = mObj.StartTime;
                    oldData.ReachTime = mObj.ReachTime;
                    oldData.BusNumber = mObj.BusNumber;
                    oldData.ComanyName = mObj.ComanyName;
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

        public bool ProcessServiceProviderInDb(ServiceProviders mObj, bool isAddMode)
        {

            try
            {
                if (mObj != null && isAddMode == true)
                {
                    DbContext.Set<ServiceProviders>().Add(mObj);
                    DbContext.SaveChanges();

                    return true;

                }

                if (mObj != null && isAddMode == false)
                {
                    ServiceProviders oldData = DbContext.Set<ServiceProviders>().Where(x => x.Id == mObj.Id).FirstOrDefault();
                    oldData.ShowOnHomePage = mObj.ShowOnHomePage;
                    oldData.ShowOnContactPage = mObj.ShowOnContactPage;
                    oldData.Name = mObj.Name;
                    oldData.VisibleText = mObj.VisibleText;
                    oldData.ContactUsPageText = mObj.ContactUsPageText;
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

        public bool ProcessAdvantagesInDb(OURADVANTAGES mObj, bool isAddMode)
        {

            try
            {
                if (mObj != null && isAddMode == true)
                {
                    DbContext.Set<OURADVANTAGES>().Add(mObj);
                    DbContext.SaveChanges();

                    return true;

                }

                if (mObj != null && isAddMode == false)
                {
                    OURADVANTAGES oldData = DbContext.Set<OURADVANTAGES>().Where(x => x.Id == mObj.Id).FirstOrDefault();
                    oldData.ShowOnHomePage = mObj.ShowOnHomePage;
                    oldData.Name = mObj.Name;
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


        //public bool ProcessProduct(ProductOperationDataModel mObj, long adminUserId)
        //{

        //    try
        //    {
        //        if (mObj != null && mObj.isAddOperation == true)
        //        {
        //            mObj.PRODUCTSTable.CreatedDate = DateTime.Now;
        //            mObj.PRODUCTSTable.CreatedId = adminUserId;
        //            DbContext.Set<PRODUCTSTable>().Add(mObj.PRODUCTSTable);
        //            DbContext.SaveChanges();


        //            if (mObj.ProductCategoryList != null && mObj.ProductCategoryList != "")
        //            {
        //                var splitIdArray = mObj.ProductCategoryList.Split(',');

        //                for (int item = 0; item < splitIdArray.Length; item++)
        //                {
        //                    PRODUCT_CATEGPRY_MAPTable map = new PRODUCT_CATEGPRY_MAPTable();
        //                    map.CategoryId = Convert.ToInt32(splitIdArray[item]);
        //                    map.ProductId = mObj.PRODUCTSTable.Id;
        //                    DbContext.Set<PRODUCT_CATEGPRY_MAPTable>().Add(map);
        //                    DbContext.SaveChanges();
        //                }
        //            }


        //            if (mObj.ProductGalleryMapper != null && mObj.ProductGalleryMapper != "")
        //            {
        //                var splitIdArray = mObj.ProductGalleryMapper.Split(',');

        //                for (int item = 0; item < splitIdArray.Length; item++)
        //                {
        //                    PRODUCT_IMAGE_MAPTable map = new PRODUCT_IMAGE_MAPTable();
        //                    map.ImagePath = splitIdArray[item];
        //                    map.ProductId = mObj.PRODUCTSTable.Id;
        //                    DbContext.Set<PRODUCT_IMAGE_MAPTable>().Add(map);
        //                    DbContext.SaveChanges();
        //                }
        //            }

        //            return true;
        //        }
        //        if (mObj != null && mObj.isAddOperation == false)
        //        {
        //            PRODUCTSTable oldData = DbContext.Set<PRODUCTSTable>().Where(x => x.Id == mObj.PRODUCTSTable.Id).FirstOrDefault();

        //            oldData.ImageURL = mObj.PRODUCTSTable.ImageURL;
        //            oldData.Name = mObj.PRODUCTSTable.Name;
        //            oldData.SortDescription = mObj.PRODUCTSTable.SortDescription;
        //            oldData.Description = mObj.PRODUCTSTable.Description;
        //            oldData.SKU = mObj.PRODUCTSTable.SKU;
        //            oldData.Published = mObj.PRODUCTSTable.Published;
        //            oldData.ShowOnHomePage = mObj.PRODUCTSTable.ShowOnHomePage;
        //            oldData.AvailableSatartDate = mObj.PRODUCTSTable.AvailableSatartDate;
        //            oldData.AvailableEndDate = mObj.PRODUCTSTable.AvailableEndDate;
        //            oldData.Price = mObj.PRODUCTSTable.Price;
        //            oldData.OldPrice = mObj.PRODUCTSTable.OldPrice;
        //            oldData.ProductCost = mObj.PRODUCTSTable.ProductCost;
        //            oldData.AffeliateCommision = mObj.PRODUCTSTable.AffeliateCommision;
        //            oldData.DiscountId = mObj.PRODUCTSTable.DiscountId;
        //            oldData.TaxExempt = mObj.PRODUCTSTable.TaxExempt;
        //            oldData.TaxCategoryId = mObj.PRODUCTSTable.TaxCategoryId;
        //            oldData.InventoryMethodId = mObj.PRODUCTSTable.InventoryMethodId;
        //            oldData.ShippingEnabled = mObj.PRODUCTSTable.ShippingEnabled;
        //            oldData.ShipWeight = mObj.PRODUCTSTable.ShipWeight;
        //            oldData.ShipLength = mObj.PRODUCTSTable.ShipLength;
        //            oldData.ShipWidth = mObj.PRODUCTSTable.ShipWidth;
        //            oldData.ShipHeight = mObj.PRODUCTSTable.ShipHeight;
        //            oldData.IsFreeShipping = mObj.PRODUCTSTable.IsFreeShipping;
        //            oldData.ShipSeperetly = mObj.PRODUCTSTable.ShipSeperetly;
        //            oldData.VendorId = mObj.PRODUCTSTable.VendorId;
        //            oldData.EditedDate = mObj.PRODUCTSTable.EditedDate;
        //            oldData.EditedId = mObj.PRODUCTSTable.EditedId;

        //            DbContext.SaveChanges();



        //            if (mObj.ProductCategoryList != null && mObj.ProductCategoryList != "")
        //            {
        //                int result = DbContext.Database.ExecuteSqlCommand("delete PRODUCT_CATEGPRY_MAP where ProductId =" + mObj.PRODUCTSTable.Id);

        //                var splitIdArray = mObj.ProductCategoryList.Split(',');

        //                for (int item = 0; item < splitIdArray.Length; item++)
        //                {
        //                    PRODUCT_CATEGPRY_MAPTable map = new PRODUCT_CATEGPRY_MAPTable();
        //                    map.CategoryId = Convert.ToInt32(splitIdArray[item]);
        //                    map.ProductId = mObj.PRODUCTSTable.Id;
        //                    DbContext.Set<PRODUCT_CATEGPRY_MAPTable>().Add(map);
        //                    DbContext.SaveChanges();
        //                }
        //            }


        //            if (mObj.ProductGalleryMapper != null && mObj.ProductGalleryMapper != "")
        //            {
        //                int result = DbContext.Database.ExecuteSqlCommand("delete PRODUCT_IMAGE_MAP where ProductId =" + mObj.PRODUCTSTable.Id);

        //                var splitIdArray = mObj.ProductGalleryMapper.Split(',');

        //                for (int item = 0; item < splitIdArray.Length; item++)
        //                {
        //                    PRODUCT_IMAGE_MAPTable map = new PRODUCT_IMAGE_MAPTable();
        //                    map.ImagePath = splitIdArray[item];
        //                    map.ProductId = mObj.PRODUCTSTable.Id;
        //                    DbContext.Set<PRODUCT_IMAGE_MAPTable>().Add(map);
        //                    DbContext.SaveChanges();
        //                }
        //            }

        //            return true;

        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }

    
}
