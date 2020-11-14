using System;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Registers both the impementation & the interface so that you can request either and get the same copy for your scope
        /// </summary>
        public static IServiceCollection AddScopedWithImplementation<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            services.AddScoped<TImplementation>();

            // Make sure that when the interface service is requested it receives the same copy as the implementation
            services.AddScoped<TService, TImplementation>(p => p.GetRequiredService<TImplementation>());

            return services;
        }

        /// <summary>
        /// Replace services with a singleton implementation
        /// </summary>
        public static IServiceCollection ReplaceWithSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
            => ReplaceWith<TService, TImplementation>(services, s => s.AddSingleton<TService, TImplementation>());

        public static IServiceCollection ReplaceWithSingleton<TService, TImplementation>(this IServiceCollection services, TImplementation instance)
            where TService : class
            where TImplementation : class, TService
            => ReplaceWith<TService, TImplementation>(services, s => s.AddSingleton<TService, TImplementation>(_ => instance));

        /// <summary>
        /// Replace services with a scoped implementation
        /// </summary>
        public static IServiceCollection ReplaceWithScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
            => ReplaceWith<TService, TImplementation>(services, AddScopedWithImplementation<TService, TImplementation>);

        public static IServiceCollection ReplaceWithScoped<TService, TImplementation>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory)
            where TService : class
            where TImplementation : class, TService
            => ReplaceWith<TService, TImplementation>(services, s => s.AddScoped(implementationFactory));

        private static IServiceCollection ReplaceWith<TService, TImplementation>(this IServiceCollection services, Func<IServiceCollection, IServiceCollection> replaceAction)
            where TService : class
            where TImplementation : class, TService
        {
            var currentServices = services.Where(descriptor => descriptor.ServiceType == typeof(TService)).ToList();
            foreach (var service in currentServices)
            {
                services.Remove(service);
            }

            return replaceAction.Invoke(services);
        }
    }
}