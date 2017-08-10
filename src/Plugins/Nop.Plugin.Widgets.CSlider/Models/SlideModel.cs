using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.CSlider.Models
{
    public class SlideModel : BaseNopModel
    {
        public string PictureUrl { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Link { get; set; }

        public string ButtonText { get; set; }

        public int Order { get; set; }
    }
}