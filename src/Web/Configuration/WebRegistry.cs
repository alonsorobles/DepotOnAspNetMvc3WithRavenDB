using Raven.Client;
using Raven.Client.Document;
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