using System.Diagnostics;
using Depot.Configuration;
using Depot.Extensions;
using StructureMap;

namespace Depot.Web.Configuration
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
                ExecuteStartupTasks();
                Debug.Assert(Container != null, "Container != null");
                Debug.WriteLine(Container.WhatDoIHave());
            }
        }

        private static void ExecuteStartupTasks()
        {
            var startupTasks = Container.GetAllInstances<IStartupTask>();
            startupTasks.Each(task => task.Execute());
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