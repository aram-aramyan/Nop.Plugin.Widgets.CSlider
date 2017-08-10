using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Widgets.CSlider.Domain;
using Nop.Plugin.Widgets.CSlider.Infrastructure.Cache;
using Nop.Plugin.Widgets.CSlider.Models;
using Nop.Plugin.Widgets.CSlider.Services;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using Nop.Web.Framework.Security;

namespace Nop.Plugin.Widgets.CSlider.Controllers
{
    public class WidgetsCSliderController : BasePluginController
    {
        private readonly ICacheManager _cacheManager;
        private readonly ILocalizationService _localizationService;
        private readonly IPictureService _pictureService;
        private readonly ISettingService _settingService;
        private readonly ICSliderService _sliderService;
        private readonly IStoreContext _storeContext;
        private readonly IStoreService _storeService;
        private readonly IWorkContext _workContext;

        public WidgetsCSliderController(IWorkContext workContext,
            IStoreContext storeContext,
            IStoreService storeService,
            IPictureService pictureService,
            ISettingService settingService,
            ICacheManager cacheManager,
            ILocalizationService localizationService,
            ICSliderService sliderService)
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _storeService = storeService;
            _pictureService = pictureService;
            _settingService = settingService;
            _cacheManager = cacheManager;
            _localizationService = localizationService;
            _sliderService = sliderService;
        }

        protected string GetPictureUrl(int pictureId)
        {
            var cacheKey = string.Format(ModelCacheEventConsumer.PICTURE_URL_MODEL_KEY, pictureId);
            return _cacheManager.Get(cacheKey, () =>
            {
                var url = _pictureService.GetPictureUrl(pictureId, showDefaultPicture: false, targetSize: 256);
                //little hack here. nulls aren't cacheable so set it to ""
                if (url == null)
                    url = "";

                return url;
            });
        }

        [ChildActionOnly]
        public ActionResult PublicInfo(string widgetZone, object additionalData = null)
        {
            // var settings = _settingService.LoadSetting<CSliderSettings>(_storeContext.CurrentStore.Id);
            var slides = _sliderService.GetAll();

            if (slides.Count == 0)
                return Content("");

            var model = new PublicInfoModel
            {
                Slides = slides.Select(slide => new SlideModel
                {
                    PictureUrl = GetPictureUrl(slide.PictureId),
                    Title = slide.Title,
                    Content = slide.Content,
                    Link = slide.Link,
                    ButtonText = slide.ButtonText,
                    Order = slide.Order
                }).ToList()
            };



            return View("~/Plugins/Widgets.CSlider/Views/PublicInfo.cshtml", model);
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            return View("~/Plugins/Widgets.CSlider/Views/Configure.cshtml");
        }

        [AdminAuthorize]
        [HttpPost]
        [AdminAntiForgery]
        public ActionResult List(DataSourceRequest command)
        {
            var model = _sliderService
                .GetAll()
                .OrderBy(s => s.Order)
                .Select(slide => new SlideConfigModel
                {
                    Id = slide.Id,
                    PictureId = slide.PictureId,
                    Title = slide.Title,
                    Content = slide.Content,
                    Link = slide.Link,
                    ButtonText = slide.ButtonText,
                    Order = slide.Order
                })
            .ToList();

            return Json(new DataSourceResult
            {
                Data = model,
                Total = model.Count
            });
        }

        [AdminAuthorize]
        public ActionResult Create()
        {
            var model = new SlideConfigModel();
            return View("~/Plugins/Widgets.CSlider/Views/Create.cshtml", model);
        }

        [AdminAuthorize]
        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Create(string btnId, string formId, SlideConfigModel model)
        {
            var slide = new CSliderSlide
            {
                Id = 0,
                PictureId = model.PictureId,
                Title = model.Title,
                Content = model.Content,
                Link = model.Link,
                ButtonText = model.ButtonText,
                Order = model.Order
            };

            _sliderService.InsertSlide(slide);

            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;

            return View("~/Plugins/Widgets.CSlider/Views/Create.cshtml", model);
        }

        [AdminAuthorize]
        public ActionResult Edit(int id)
        {

            var slide = _sliderService.GetById(id);
            if (slide == null)
                return RedirectToAction("Configure");

            var model = new SlideConfigModel
            {
                Id = slide.Id,
                PictureId = slide.PictureId,
                Title = slide.Title,
                Content = slide.Content,
                Link = slide.Link,
                ButtonText = slide.ButtonText,
                Order = slide.Order
            };

            return View("~/Plugins/Widgets.CSlider/Views/Edit.cshtml", model);
        }

        [AdminAuthorize]
        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Edit(string btnId, string formId, SlideConfigModel model)
        {

            var slide = _sliderService.GetById(model.Id);
            if (slide == null)
                return RedirectToAction("Configure");

            //delete old picture
            if (slide.PictureId > 0 && slide.PictureId != model.PictureId)
            {
                var previousPicture = _pictureService.GetPictureById(slide.PictureId);
                if (previousPicture != null)
                    _pictureService.DeletePicture(previousPicture);
            }

            slide.PictureId = model.PictureId;
            slide.Title = model.Title;
            slide.Content = model.Content;
            slide.Link = model.Link;
            slide.ButtonText = model.ButtonText;
            slide.Order = model.Order;

            _sliderService.UpdateSlide(slide);

            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;

            return View("~/Plugins/Widgets.CSlider/Views/Edit.cshtml", model);
        }

        [AdminAuthorize]
        [HttpPost]
        [AdminAntiForgery]
        public ActionResult Delete(int id)
        {
            var slide = _sliderService.GetById(id);
            if (slide == null)
                return RedirectToAction("Configure");

            //delete picture
            if (slide.PictureId > 0)
            {
                var previousPicture = _pictureService.GetPictureById(slide.PictureId);
                if (previousPicture != null)
                    _pictureService.DeletePicture(previousPicture);
            }
            _sliderService.DeleteSlide(slide);

            return new NullJsonResult();
        }
    }
}