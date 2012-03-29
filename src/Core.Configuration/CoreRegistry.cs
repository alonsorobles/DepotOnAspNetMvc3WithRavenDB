using StructureMap.Configuration.DSL;

namespace Depot.Configuration
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(scanner =>
                     {
                         scanner.TheCallingAssembly();
                         scanner.AssembliesFromApplicationBaseDirectory();
                         scanner.WithDefaultConventions();
                         scanner.AddAllTypesOf<IStartupTask>();
                     });
        }
    }
}