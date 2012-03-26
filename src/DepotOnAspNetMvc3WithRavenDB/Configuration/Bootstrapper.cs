using System.Diagnostics;
using StructureMap;

namespace DepotOnAspNetMvc3WithRavenDB.Configuration
{
    public class Bootstrapper
    {
        private static readonly object SyncRoot = new object();

        public static IContainer Container { get; private set; }

        public static IContainer Bootstrap()
        {
            if (Container == null)
            {
                InitializeContainer();
            }
            return Container;
        }

        private static void InitializeContainer()
        {
            lock (SyncRoot)
            {
                if (Container != null)
                    return;
                InitializeStructureMap();
                Debug.Assert(Container != null, "Container != null");
                Debug.WriteLine(Container.WhatDoIHave());
            }
        }

        private static void InitializeStructureMap()
        {
            Container = new Container(configuration => configuration.Scan(scanner =>
            {
                scanner.TheCallingAssembly();
                scanner.LookForRegistries();
            }));
        }
    }
}