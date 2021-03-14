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
        /// <typeparam name="TInterface">Repository</typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddScopedDynamic(this IServiceCollection services, Type tint)
        {
            TypeInfo genericType = tint.GetTypeInfo();

            IEnumerable<Type> implementationTypes = genericType
                                                        .Assembly
                                                        .GetTypes()
                                                        .Where(x => !string.IsNullOrEmpty(x.Namespace))
                                                        .Where(x => x.IsClass)
                                                        .Where(x => x.GetInterface(genericType.Name) != null);

            foreach (Type implementationType in implementationTypes)
            {
                var serviceType = implementationType.GetInterfaces().FirstOrDefault(x => x.GetInterface(genericType.Name) != null);
                if (serviceType != null)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }

            return services;
        }
    }
}