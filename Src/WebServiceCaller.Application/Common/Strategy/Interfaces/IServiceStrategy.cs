using System.Threading;
using System.Threading.Tasks;
using WebServiceCaller.Application.Notification.Command.CreateNotification;
using WebServiceCaller.Application.Notification.Command.TestNotifier;

namespace WebServiceCaller.Application.Common.Strategy.Interfaces
{
    public interface IServiceStrategy
    {
        Task Execute(CreateNotificationCommand notification, CancellationToken cancellationToken);

        Task TestExecute(CreateTestNotifierCommand testNotifier, CancellationToken cancellationToken);
    }
}