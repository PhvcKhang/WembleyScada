using WembleyScada.Api.Application.Dtos;

namespace WembleyScada.Api.Application.Hubs;

public class NotificationHub : Hub
{
    private readonly Buffer _buffer;
    private readonly HubUserStorage _hubUserStorage;

    public NotificationHub(Buffer buffer, HubUserStorage hubUserStorage)
    {
        _buffer = buffer;
        _hubUserStorage = hubUserStorage;
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            var user = new HubUser(Context.ConnectionId);
            _hubUserStorage.Add(user);
        }
        catch (Exception ex)
        {
             await Clients.Caller.SendAsync("OnError", ex.Message);
        }
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            var userConnectionId = Context.ConnectionId;
            _hubUserStorage.Remove(userConnectionId);
            await Clients.Caller.SendAsync("OnError", "Disconnected");
        }
        catch(Exception ex)
        {
            await Clients.Caller.SendAsync("OnError", "Disconnect Error: " + ex.Message);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task UpdateTopics(List<string> topics)
    {
        try
        {
            var connectionId = Context.ConnectionId;

            var user = _hubUserStorage.Users.Find(x => x.ConnectionId == connectionId); ;

            if (topics is null)
            {
                return;
            }

            await Clients.Caller.SendAsync("Check", System.Text.Json.JsonSerializer.Serialize(user));

            if (user is null)
            {
                return;
            }

            user.UpdateTopics(topics);
            await Clients.Caller.SendAsync("Check", System.Text.Json.JsonSerializer.Serialize(user));
            await Clients.Caller.SendAsync("Check", "Updated Successfully");

        }
        catch(Exception ex)
        {
            await Clients.Caller.SendAsync("OnError", ex.Message);
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
