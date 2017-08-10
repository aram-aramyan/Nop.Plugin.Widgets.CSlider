using System.Collections.Generic;
using Nop.Plugin.Widgets.CSlider.Domain;

namespace Nop.Plugin.Widgets.CSlider.Services
{
    public interface ICSliderService
    {
        void DeleteSlide(CSliderSlide slide);

        IList<CSliderSlide> GetAll();

        CSliderSlide GetById(int slideId);

        void InsertSlide(CSliderSlide slide);

        void InsertSlides(IEnumerable<CSliderSlide> slides);

        void UpdateSlide(CSliderSlide slide);
    }
}