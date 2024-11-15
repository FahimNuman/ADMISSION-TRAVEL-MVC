using Core.DataService;
using Services.DataContext;
using Services.Domain.Admin;
using Services.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DataServices.Admin
{
    public class SliderDataService : EntityContextDataService<SlidersTable>
    {

        public SliderDataService(AppDbContext dbContext) : base(dbContext)
        {
            DbContext.Database.CommandTimeout = SiteSettings.DbTimeOut;
        }

        public IList<Admin_Get_SliderList> GetAllSliderList()
        {
            
            List<Admin_Get_SliderList> dataList = DbContext.Database.SqlQuery<Admin_Get_SliderList>("Admin_Get_SliderList").ToList();

            return dataList;
        }
        public IList<Categories> GetAllProductCategoryList()
        {

            IList<Categories> dataList = DbContext.Database.SqlQuery<Categories>("SELECT * FROM Categories").ToList();

            return dataList;
        }

        public IList<SliderTypeTable> GetSliderTypeList()
        {

            IList<SliderTypeTable> dataList = DbContext.Database.SqlQuery<SliderTypeTable>("SELECT * FROM SliderType").ToList();

            return dataList;
        }

        public IList<SlidersTable> GetMainSlider()
        {

            IList<SlidersTable> dataList = DbContext.Database.SqlQuery<SlidersTable>("select * from Sliders where SliderIsActive=1").ToList();

            return dataList;
        }

        public IList<SlidersTable> GetAdvertismentSlider()
        {

            IList<SlidersTable> dataList = DbContext.Database.SqlQuery<SlidersTable>("select * from Sliders where SliderIsActive=1").ToList();

            return dataList;
        }
        public IList<SlidersTable> GetFeedbackSlider()
        {

            IList<SlidersTable> dataList = DbContext.Database.SqlQuery<SlidersTable>("select * from Sliders where SliderIsActive=1").ToList();

            return dataList;
        }

        public SlidersTable GetSliderDetailsById(int Id)
        {
            SlidersTable data = new SlidersTable();
            data = DbContext.Set<SlidersTable>().Where(x => x.Id == Id).FirstOrDefault();
            return data;
        }

        public int DeleteSlider(int id)
        {

            int result = DbContext.Database.ExecuteSqlCommand("delete [Sliders] where id=" + id);

            return result;
        }


        public bool ProcessSlider(SliderOperationModel mObj, long adminUserId)
        {


            if (mObj != null && mObj.isAddOperation == true)
            {
                mObj.SlidersTable.CreatedDateTime = DateTime.Now;
                mObj.SlidersTable.CreatedId = adminUserId;
                DbContext.Set<SlidersTable>().Add(mObj.SlidersTable);
                DbContext.SaveChanges();

                return true;
            }

            if (mObj != null && mObj.isAddOperation == false)
            {
                SlidersTable oldItem = DbContext.Set<SlidersTable>().Where(x => x.Id == mObj.SlidersTable.Id).FirstOrDefault();

                oldItem.Name = mObj.SlidersTable.Name;
                oldItem.Caption = mObj.SlidersTable.Caption;
                oldItem.SliderIsActive = mObj.SlidersTable.SliderIsActive;
                if (mObj.SlidersTable.ImagePath != null)
                    oldItem.ImagePath = mObj.SlidersTable.ImagePath;
                DbContext.SaveChanges();

                return true;
            }

            return false;

        }

    }
}
