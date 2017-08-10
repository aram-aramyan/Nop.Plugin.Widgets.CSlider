using System.Collections.Generic;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.CSlider.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public ConfigurationModel()
        {
            Slides = new List<SlideConfigModel>();
        }

        public int ActiveStoreScopeConfiguration { get; set; }

        public IList<SlideConfigModel> Slides { get; set; }
    }
}