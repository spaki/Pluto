using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Pluto.Repository.Config
{
    internal static class ModelBuilderExtensions
    {
        public static void AddAssemblyConfiguration<T>(this ModelBuilder modelBuilder) =>
            typeof(T)
                .Assembly
                .GetTypes()
                .Where(x =>
                {
                    var typeInfo = x.GetTypeInfo();
                    var implementedInterfaces = typeInfo.GetInterfaces();
                    return !typeInfo.IsAbstract
                                && implementedInterfaces.Any(e => e == typeof(T))
                                && implementedInterfaces.Any(e => e.GetTypeInfo().IsGenericType && e.GetGenericTypeDefinition() == typeof(IDbEntityConfiguration<>));
                })
                .Select(Activator.CreateInstance)
                .Cast<IDbEntityConfiguration>()
                .ToList()
                .ForEach(e => e.Configure(modelBuilder));
    }
}
