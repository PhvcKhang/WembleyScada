namespace WembleyScada.Api.Application.Hubs;

public class NotificationHub : Hub
{
    private readonly Buffer _buffer;

    public NotificationHub(Buffer buffer)
    {
        _buffer = buffer;
    }

    public string GetAllTags() => _buffer.GetAllTags();

    public async Task SendAllTags()
    {
        var tags = _buffer.GetAllTags();
        await Clients.All.SendAsync("GetAll", tags);
    } 
}
