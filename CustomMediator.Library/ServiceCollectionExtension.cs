using CustomMediator.Library.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace CustomMediator.Library
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddCustomMediator(this IServiceCollection services, Assembly[] assemblies)
        {

            AddRequestHandlers(services, assemblies);
            AddNotificationHandlers(services, assemblies);
            services.AddSingleton<IMediator, Mediator>();

            return services;
        }


        private static void AddRequestHandlers(IServiceCollection services, Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(i => i.GetTypes()).Where(i => !i.IsInterface);

            var requestHandlers = types
                .Where(i => IsAssignableToGenericType(i, typeof(IRequestHandler<,>)))
                .ToList();

            foreach (var handler in requestHandlers)
            {
                var handlerInterface = handler.GetInterfaces().FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
                var requestType = handlerInterface.GetGenericArguments()[0];
                var responseType = handlerInterface.GetGenericArguments()[1];

                var genericType = typeof(IRequestHandler<,>).MakeGenericType(requestType, responseType);

                services.AddTransient(genericType, handler);

                //services.AddTransient(IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>, GetByIdProductQueryHandler);
            }
        }

        private static void AddNotificationHandlers(IServiceCollection services, Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(i => i.GetTypes()).Where(i => !i.IsInterface);

            var notificationHandlers = types
                .Where(i => IsAssignableToGenericType(i, typeof(INotificationHandler<>)))
                .ToList();

            foreach (var handler in notificationHandlers)
            {
                var handlerInterface = handler.GetInterfaces().FirstOrDefault(i => i.GetGenericTypeDefinition() == typeof(INotificationHandler<>));
                var domainEvent = handlerInterface.GetGenericArguments()[0];
     
                services.AddTransient(domainEvent, handler);

                //services.AddTransient(INotificationHandler<OrderStartedDomainEvent>, OrderStartedDomainEventHandler);
            }
        }

        public static IServiceProvider UseCustomMediator(this IServiceProvider serviceProvider)
        {
            ServiceProvider.SetInstance(serviceProvider);
            return serviceProvider;
        }

        

        private static bool IsAssignableToGenericType(Type givenType, Type targetType)
        {         
            var interfaceTypes = givenType.GetInterfaces();

            foreach (var it in interfaceTypes)
            {
                if (IsGeneric(it, targetType))
                    return true;
            }

            if (IsGeneric(givenType, targetType))
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            bool IsGeneric(Type _interfaceType, Type _targetType)
            {
                return _interfaceType.IsGenericType && _interfaceType.GetGenericTypeDefinition() == _targetType;
            }

            return IsAssignableToGenericType(baseType, targetType);
        }
    }
}
