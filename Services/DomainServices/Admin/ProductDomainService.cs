using Core.DomainService;
using Services.DataServices.Admin;
using Services.Domain.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DomainServices.Admin
{
    public class ProductDomainService : DomainService<PRODUCT_CATEGORIES, long>
    {

        private readonly ProductDataService _productDataService;
        public ProductDomainService(ProductDataService productDataService) : base(productDataService)
            {
            _productDataService = productDataService;
        }

        public IList<Admin_Get_CategoryList> GetAllProductCatList()
        {
            return _productDataService.GetAllProductCatList();
        }

        public IList<Admin_Get_AllProductList> GetAllProductListForAdmin()
        {
            return _productDataService.GetAllProductListForAdmin();
        }

        public IList<Get_Product_List_By_Param> GetProductListForUserByParam(int catId, int orderBy, int min, int max) 
        {
            return _productDataService.GetProductListForUserByParam(catId, orderBy, min, max);
        }

        public IList<Get_Product_List_By_Search_Value> GetProductListForUserBySearchText(string searchText, int orderBy, int min, int max)
        {
            return _productDataService.GetProductListForUserBySearchText(searchText, orderBy, min, max);
        }


        public IList<Get_Featured_Product_For_HomePage> GetFeaturedProductListForHomePage()
        {
            return _productDataService.GetFeaturedProductListForHomePage();
        }

        public IList<Get_Top_Ten_Best_Selling_Product> GetTopTenBestSellerProduct()
        {
            return _productDataService.GetTopTenBestSellerProduct();
        }

        public HomePageCategoryProductLisst GetHomePageCategoryProductList()
        {
            return _productDataService.GetHomePageCategoryProductList();
        }

        public IList<Get_Category_Parent_Items> GetCategoryParentItems(int catId)
        {
            return _productDataService.GetCategoryParentItems(catId);
        }

        public Get_Product_Details_By_ProductId GetProductDetailsByForUserProductId(int id)
        {
            return _productDataService.GetProductDetailsByForUserProductId(id);
        }

        public PRODUCT_CATEGORIES GetCategoryDetailsByCatId(int id)
        {
            return _productDataService.GetCategoryDetailsByCatId(id);
        }

        public ProductOperationDataModel GetProductDetailsByProductId(int id)
        {
            return _productDataService.GetProductDetailsByProductId(id);
        }
    }
}
