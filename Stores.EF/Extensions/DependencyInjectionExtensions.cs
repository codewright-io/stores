using DevKnack.Stores;
using DevKnack.Stores.EF;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddStoresDbContext(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction)
            => services.AddDbContext<StoresDbContext>(optionsAction);

        public static IServiceCollection RegisterEntityFrameworkStore(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddStoresDbContext(optionsAction);

            services.AddScopedWithImplementation<IFileStore, EfFileStore>();

            FileStoreFactory.RegisterFileStore<EfFileStore>("ef", true);

            return services;
        }
    }
}