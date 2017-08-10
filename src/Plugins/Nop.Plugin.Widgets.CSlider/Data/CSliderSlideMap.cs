using Nop.Data.Mapping;
using Nop.Plugin.Widgets.CSlider.Domain;

namespace Nop.Plugin.Widgets.CSlider.Data
{
    public partial class CSliderSlideMap : NopEntityTypeConfiguration<CSliderSlide>
    {
        public CSliderSlideMap()
        {
            this.ToTable("CSliderSlide");
            this.HasKey(x => x.Id);
        }
    }
}