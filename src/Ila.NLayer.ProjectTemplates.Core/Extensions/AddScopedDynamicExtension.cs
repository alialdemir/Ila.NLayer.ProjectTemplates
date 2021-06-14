using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ila.NLayer.ProjectTemplates.Core.Extensions
{
    public static class AddScopedDynamicExtension
    {
        /// <summary>
        /// Adds repository and services as AddScoped with reflection method
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="tint">Generic type</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddScopedDynamic(this IServiceCollection services, Type tint)
        {
            TypeInfo genericType = tint.GetTypeInfo();

            return services.AddScopedDynamic(tint, genericType);
        }

        /// <summary>
        /// Adds repository and services as AddScoped with reflection method
        /// </summary>
        /// <param name="services">Service collection</param>
        /// <param name="tint">Generic type</param>
        /// <param name="source">Source type</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddScopedDynamic(this IServiceCollection services, Type tint, Type source)
        {
            TypeInfo genericType = tint.GetTypeInfo();

            IEnumerable<Type> implementationTypes = source
                                                        .Assembly
                                                        .GetTypes()
                                                        .Where(x => !string.IsNullOrEmpty(x.Namespace))
                                                        .Where(x => x.IsClass)
                                                        .Where(x => x.GetInterface(genericType.Name) != null);

            foreach (Type implementationType in implementationTypes)
            {
                var serviceType = implementationType.GetInterfaces().FirstOrDefault(x => x.GetInterface(genericType.Name) != null && x.Name != genericType.Name);
                if (serviceType != null)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }

            return services;
        }
    }
}