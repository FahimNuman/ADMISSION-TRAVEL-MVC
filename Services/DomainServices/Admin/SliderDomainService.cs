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
    public class SliderDomainService : DomainService<SlidersTable, long>
    {

        private readonly SliderDataService _sliderDataService;
        public SliderDomainService(SliderDataService sliderDataService) : base(sliderDataService)
            {
            _sliderDataService = sliderDataService;
        }


        public IList<Admin_Get_SliderList> GetAllSliderList()
        {
            return _sliderDataService.GetAllSliderList();
        }

        public IList<Categories> GetAllProductCategoryList()
        {
            return _sliderDataService.GetAllProductCategoryList();
        }

        public IList<SliderTypeTable> GetSliderTypeList()
        {
            return _sliderDataService.GetSliderTypeList();
        }

        public bool ProcessSlider(SliderOperationModel mObj, long adminUserId)
        {
            return _sliderDataService.ProcessSlider(mObj, adminUserId);
        }

        public int DeleteSlider(int id)
        {
            return _sliderDataService.DeleteSlider(id);
        }

        public SlidersTable GetSliderDetailsById(int Id)
        {
            return _sliderDataService.GetSliderDetailsById(Id);
        }

        public IList<SlidersTable> GetMainSlider()
        {
            return _sliderDataService.GetMainSlider();
        }

        public IList<SlidersTable> GetAdvertismentSlider()
        {
            return _sliderDataService.GetAdvertismentSlider();
        }

        public IList<SlidersTable> GetFeedbackSlider()
        {
            return _sliderDataService.GetFeedbackSlider();
        }

    }
}
