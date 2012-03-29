using Depot.Configuration;
using StructureMap.Configuration.DSL;

namespace Depot.Web.Configuration
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(scanner =>
                     {
                         scanner.TheCallingAssembly();
                         scanner.Assembly("Depot.Core");
                         scanner.WithDefaultConventions();
                         scanner.AddAllTypesOf<IStartupTask>();
                     });
        }
    }
}