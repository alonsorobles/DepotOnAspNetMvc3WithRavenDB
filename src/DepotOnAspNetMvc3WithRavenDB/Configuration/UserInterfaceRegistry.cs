using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using StructureMap.Configuration.DSL;

namespace DepotOnAspNetMvc3WithRavenDB.Configuration
{
    public class UserInterfaceRegistry : Registry
    {
        public UserInterfaceRegistry()
        {
            Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.WithDefaultConventions();
            });

            For<IDocumentStore>()
                .Singleton()
                .Use(context =>
                {
                    var documentStore = new DocumentStore { ConnectionStringName = "RavenDB" };
                    documentStore.Initialize();
                    return documentStore;
                });

            For<IDocumentSession>()
                .HybridHttpOrThreadLocalScoped()
                .Use(context => context.GetInstance<IDocumentStore>().OpenSession());
        }
    }
}