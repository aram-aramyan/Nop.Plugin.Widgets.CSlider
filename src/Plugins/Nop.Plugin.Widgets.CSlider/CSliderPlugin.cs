using System.Collections.Generic;
using System.IO;
using System.Web.Routing;
using Nop.Core;
using Nop.Core.Plugins;
using Nop.Plugin.Widgets.CSlider.Data;
using Nop.Plugin.Widgets.CSlider.Domain;
using Nop.Plugin.Widgets.CSlider.Services;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.CSlider
{
    public class CSliderPlugin : BasePlugin, IWidgetPlugin
    {
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly IWebHelper _webHelper;
        private readonly CSliderObjectContext _objectContext;
        private readonly ICSliderService _sliderService;

        public CSliderPlugin(IPictureService pictureService,
            ISettingService settingService, IWebHelper webHelper, CSliderObjectContext objectContext, ICSliderService sliderService)
        {
            _pictureService = pictureService;
            _settingService = settingService;
            _webHelper = webHelper;
            _objectContext = objectContext;
            _sliderService = sliderService;
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string> { "home_page_top" };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName,
            out RouteValueDictionary routeValues)
        {
            actionName = "PublicInfo";
            controllerName = "WidgetsCSlider";
            routeValues = new RouteValueDictionary
            {
                {"Namespaces", "Nop.Plugin.Widgets.CSlider.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "WidgetsCSlider";
            routeValues = new RouteValueDictionary
            {
                { "Namespaces", "Nop.Plugin.Widgets.CSlider.Controllers" },
                { "area", null }
            };
        }

        public override void Install()
        {
            //settings
            var settings = new CSliderSettings();
            _settingService.SaveSetting(settings);

            //data
            _objectContext.Install();

            //sample data
            //pictures
            var sampleImagesPath = CommonHelper.MapPath("~/Plugins/Widgets.CSlider/Content/cslider/images/");
            var sampleText = @"
Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. 
Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            var slides = new List<CSliderSlide>()
            {
                new CSliderSlide
                {
                    PictureId =
                        _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "1.png"), MimeTypes.ImagePng,
                            "slide_1").Id,
                    Title = "Slide 1",
                    Content = sampleText,
                    Link = _webHelper.GetStoreLocation(false),
                      ButtonText = "&raquo;",
                  Order = 1
                },
                new CSliderSlide
                {
                    PictureId =
                        _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "2.png"), MimeTypes.ImagePng,
                            "slide_2").Id,
                    Title = "Slide 2",
                    Content = sampleText,
                    Link = _webHelper.GetStoreLocation(false),
                     ButtonText = "&raquo;",
                   Order = 2
                },
                new CSliderSlide
                {
                    PictureId =
                        _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "3.png"), MimeTypes.ImagePng,
                            "slide_3").Id,
                    Title = "Slide 3",
                    Content = sampleText,
                    Link = _webHelper.GetStoreLocation(false),
                    ButtonText = "&raquo;",
                    Order = 3
                },
                new CSliderSlide
                {
                    PictureId =
                        _pictureService.InsertPicture(File.ReadAllBytes(sampleImagesPath + "4.png"), MimeTypes.ImagePng,
                            "slide_4").Id,
                    Title = "Slide 4",
                    Content = sampleText,
                    Link = _webHelper.GetStoreLocation(false),
                    ButtonText = "&raquo;",
                    Order = 4
                }
            };

            _sliderService.InsertSlides(slides);


            //locales
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Id", "ID");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Picture", "Picture");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Title", "Title");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Content", "Content");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Link", "Link");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.ButtonText", "ButtonText");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Order", "Order");

            // locales ru-RU
            var ruLocale = "ru-RU";
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Id", "ID", ruLocale);
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Picture", "Изображение", ruLocale);
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Title", "Заголовок", ruLocale);
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Content", "Текст", ruLocale);
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Link", "Ссылка", ruLocale);
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.ButtonText", "Тект кнопки", ruLocale);
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Order", "Порядок", ruLocale);

            base.Install();
        }

        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<CSliderSettings>();

            //data
            _objectContext.Uninstall();

            //locales
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Id");
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Picture");
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Title");
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Content");
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Link");
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.ButtonText");
            this.DeletePluginLocaleResource("Plugins.Widgets.CSlider.Fields.Order");

            base.Uninstall();
        }
    }
}
