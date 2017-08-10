using System.Web.Mvc;
using System.Web.Routing;
using Nop.Web.Framework.Mvc.Routes;

namespace Nop.Plugin.Widgets.CSlider
{
    public partial class RouteProvider : IRouteProvider
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Widgets.CSlider.Create",
                 "Plugins/WidgetsCSlider/Create",
                 new { controller = "WidgetsCSlider", action = "Create", },
                 new[] { "Nop.Plugin.Widgets.CSlider.Controllers" }
            );

            routes.MapRoute("Plugin.Widgets.CSlider.Edit",
                 "Plugins/WidgetsCSlider/Edit",
                 new { controller = "WidgetsCSlider", action = "Edit" },
                 new[] { "Nop.Plugin.Widgets.CSlider.Controllers" }
            );
        }
        public int Priority => 0;
    }
}