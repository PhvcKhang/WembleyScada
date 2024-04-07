namespace WembleyScada.Api.Application.Hubs;

public class NotificationHub : Hub
{
    private readonly Buffer _buffer;
    private readonly ClientStorage _clientStorage;

    public NotificationHub(Buffer buffer, ClientStorage clientStorage)
    {
        _buffer = buffer;
        _clientStorage = clientStorage;
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var connectionId = Context.ConnectionId;
            await Clients.Caller.SendAsync("LogInfoMessage", "Client " + connectionId + " has subcribed to the hub");
        }
        catch (Exception ex)
        {
            await Clients.Caller.SendAsync("LogInfoMessage", ex.Message);
        }
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var connectionId = Context.ConnectionId;
            _clientStorage.RemoveClient(connectionId);

            await Clients.Caller.SendAsync("LogInfoMessage", "Disconnected");
        }
        catch(Exception ex)
        {
            await Clients.Caller.SendAsync("LogInfoMessage", ex.Message);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task UpdateTopics(List<string> topics)
    {
        try
        {
            var connectionId = Context.ConnectionId;
            var client = new Client(connectionId);
            _clientStorage.AddClient(client);

            if (topics is null)
            {
                await Clients.Caller.SendAsync("LogInfoMessage", "Topics can not be null");
                return;
            }

            client.UpdateTopics(topics);
            await Clients.Caller.SendAsync("LogInfoMessage", "Updated Successfully");

        }
        catch(Exception ex)
        {
            await Clients.Caller.SendAsync("LogInfoMessage", ex.Message);
        }
    }

    public async Task<string> SendAll()
    {
        var tags = _buffer.GetAllTags();
        return await Task.FromResult(tags);
    }
        
    public async Task SendAllTags()
    {
        var tags = _buffer.GetAllTags();
        await Clients.All.SendAsync("GetAll", tags);
    }
}
