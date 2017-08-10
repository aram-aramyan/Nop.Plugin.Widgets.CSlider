using Nop.Core;

namespace Nop.Plugin.Widgets.CSlider.Domain
{
    public class CSliderSlide : BaseEntity
    {
        public int PictureId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public string ButtonText { get; set; }
        public int Order { get; set; }
    }
}