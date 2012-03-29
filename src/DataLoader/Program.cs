using System;
using System.Collections.Generic;
using System.Linq;
using Depot.Configuration;
using Depot.Extensions;

namespace Depot.DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Bootstrapper.Bootstrap();
            var dataLoaders = container.GetAllInstances<IDataLoader>();
            if (UnloadArgument(args))
                dataLoaders.Each(dataLoader => dataLoader.UnLoad());
            else
                dataLoaders.Each(dataLoader => dataLoader.Load());
        }

        private static bool UnloadArgument(IEnumerable<string> args)
        {
            return args != null && args.Any(arg => arg != null && (arg.Trim().ToLower().Contains("unload")));
        }
    }
}
