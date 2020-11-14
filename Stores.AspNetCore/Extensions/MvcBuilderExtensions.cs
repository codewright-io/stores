using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Stores.AspNetCore.Extensions
{
    public static class MvcBuilderExtensions
    {
        public static IMvcBuilder AddStoreControllers(this IMvcBuilder builder)
        {
            var assembly = typeof(DevKnack.Stores.AspNetCore.Controllers.StoreController).Assembly;

            builder.PartManager.ApplicationParts.Add(new AssemblyPart(assembly));

            return builder;
        }
    }
}