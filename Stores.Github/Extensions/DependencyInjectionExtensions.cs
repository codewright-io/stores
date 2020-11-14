using DevKnack.Stores;
using DevKnack.Stores.Github;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterGitHubStore(this IServiceCollection services)
        {
            services.AddScoped<RepositoryIdCache>();

            services.AddScopedWithImplementation<IFileStore, GithubFileStore>();
            services.AddScoped<RepositoryService>();
            services.AddScoped<IRepositoryQuery, RepositoryService>(p => p.GetRequiredService<RepositoryService>());
            services.AddScoped<IRepositoryService, RepositoryService>(p => p.GetRequiredService<RepositoryService>());
            services.AddScopedWithImplementation<ISearchRepositoryQuery, SearchRepositoryQuery>();
            services.AddScopedWithImplementation<IBranchService, BranchService>();
            services.AddScoped<IGitHubServices, GitHubServices>();

            FileStoreFactory.RegisterFileStore<GithubFileStore>("git", false);

            return services;
        }
    }
}