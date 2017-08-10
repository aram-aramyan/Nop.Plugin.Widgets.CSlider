using Autofac;
using Autofac.Core;
using Nop.Core.Caching;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.CSlider.Controllers;
using Nop.Plugin.Widgets.CSlider.Data;
using Nop.Plugin.Widgets.CSlider.Domain;
using Nop.Plugin.Widgets.CSlider.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.CSlider.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<CSliderService>().As<ICSliderService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<CSliderObjectContext>(builder, "nop_object_context_cslider");

            //override required repository with our custom context
            builder.RegisterType<EfRepository<CSliderSlide>>()
                .As<IRepository<CSliderSlide>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>("nop_object_context_cslider"))
                .InstancePerLifetimeScope();

            //we cache presentation models between requests
            builder.RegisterType<WidgetsCSliderController>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("nop_cache_static"));
        }

        /// <summary>
        /// Order of this dependency registrar implementation
        /// </summary>
        public int Order => 2;
    }
}
