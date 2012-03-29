using StructureMap.Configuration.DSL;

namespace Depot.DataLoader.Configuration
{
    public class DataLoaderRegistry :Registry
    {
        public DataLoaderRegistry()
        {
            Scan(scanner =>
                     {
                         scanner.TheCallingAssembly();
                         scanner.WithDefaultConventions();
                         scanner.AddAllTypesOf<IDataLoader>();
                     });
        }
    }
}