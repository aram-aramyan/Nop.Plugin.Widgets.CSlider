using System.Data.Entity;
using Nop.Core.Infrastructure;

namespace Nop.Plugin.Widgets.CSlider.Data
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
            Database.SetInitializer<CSliderObjectContext>(null);
        }

        public int Order => 0;
    }
}
