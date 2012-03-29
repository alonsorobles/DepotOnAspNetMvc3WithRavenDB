using StructureMap.Configuration.DSL;

namespace Depot.Web.Configuration
{
    public class WebRegistry : Registry
    {
        public WebRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            });
        }
    }
}