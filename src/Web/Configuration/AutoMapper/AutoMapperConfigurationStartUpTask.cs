using AutoMapper;
using Depot.Configuration;

namespace Depot.Web.Configuration.Automapper
{
    public class AutoMapperConfigurationStartUpTask : IStartupTask
    {
        public void Execute()
        {
            Mapper.Initialize(configuration => configuration.AddProfile<ProductProfile>());
        }
    }
}