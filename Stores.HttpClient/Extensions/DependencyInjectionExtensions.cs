using DevKnack.Stores;
using DevKnack.Stores.HttpClient;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterHttpFileStore(this IServiceCollection services)
        {
            services.AddScoped<HttpFileStore>();
            services.ReplaceWithScoped<IFileStore, HttpFileStore>();

            services.AddScoped<HttpRepositoryService>();
            services.AddScoped<IRepositoryQuery, HttpRepositoryService>(p => p.GetRequiredService<HttpRepositoryService>());
            services.AddScoped<IRepositoryService, HttpRepositoryService>(p => p.GetRequiredService<HttpRepositoryService>());

            services.AddScopedWithImplementation<ISearchRepositoryQuery, HttpSearchRepositoryQuery>();
            services.AddScopedWithImplementation<IBranchService, HttpBranchService>();

            services.AddScoped<IGitHubServices, HttpGitHubServices>();

            return services;
        }
    }
}