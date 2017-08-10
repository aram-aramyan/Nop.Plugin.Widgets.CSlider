using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.CSlider.Models
{
    public class PublicInfoModel : BaseNopModel
    {
        public PublicInfoModel()
        {
            Slides = new List<SlideModel>();
        }

        public IList<SlideModel> Slides { get; set; }
    }
}