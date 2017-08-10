using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.CSlider.Models
{
    public class SlideConfigModel : BaseNopModel
    {
        public int Id { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.CSlider.Fields.Picture")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.CSlider.Fields.Title")]
        [AllowHtml]
        public string Title { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.CSlider.Fields.Content")]
        [AllowHtml]
        public string Content { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.CSlider.Fields.Link")]
        [AllowHtml]
        public string Link { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.CSlider.Fields.ButtonText")]
        [AllowHtml]
        public string ButtonText { get; set; }

        [NopResourceDisplayName("Plugins.Widgets.CSlider.Fields.Order")]
        public int Order { get; set; }
    }
}