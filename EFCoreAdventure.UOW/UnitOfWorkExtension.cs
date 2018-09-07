using Adventurers.Design.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EFCoreAdventure.UOW
{
    public static class UnitOfWorkExtension
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services, 
            string connection, Type assemblyType) where TContext : DbContext
        {
            services.AddDbContext<TContext>(options =>
            options.UseSqlServer(connection, b => b.MigrationsAssembly(assemblyType.Assembly.GetName().Name)));
            services.AddScoped<DbContext, TContext>();
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            return services;
        }
    }
}
