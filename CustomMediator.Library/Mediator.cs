using CustomMediator.Library.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CustomMediator.Library
{
    public class Mediator : IMediator
    {
        ///request = GetByIdProductQueryRequest
        ///response = GetByIdProductQueryResponse
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var reqType = request.GetType();

            var reqTypeInterface = reqType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
                .FirstOrDefault();

            var responseType = reqTypeInterface.GetGenericArguments()[0];


            var genericType = typeof(IRequestHandler<,>).MakeGenericType(reqType, responseType);

            var handler = ServiceProvider.ServiceProvicer.GetService(genericType);

            return (Task<TResponse>)genericType.GetMethod("Handle").Invoke(handler, new object[] { request });
        }
     

        public async Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
        {
            var notifType = notification.GetType();

            var notifTypeInterface = notifType.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(INotification))
                .FirstOrDefault();

            var eventType = notifTypeInterface.GetGenericArguments()[0];


            var handler = ServiceProvider.ServiceProvicer.GetService(typeof(INotificationHandler<>));

            eventType.GetMethod("Handle").Invoke(handler, new object[] { notification, cancellationToken });
        }
    }
}
