
namespace WembleyScada.Host.Application.Commands.ExecutionTime;

public class ExecutionTimeNotificationHandler : INotificationHandler<ExecutionTimeNotification>
{
    private readonly ExecutionTimeBuffers _executionTimeBuffers;

    public ExecutionTimeNotificationHandler(ExecutionTimeBuffers executionTimeBuffers)
    {
        _executionTimeBuffers = executionTimeBuffers;
    }

    public Task Handle(ExecutionTimeNotification notification, CancellationToken cancellationToken)
    {
        _executionTimeBuffers.Update(notification.StationId, notification.ExecutionTime);
        return Task.CompletedTask;
    }
}
