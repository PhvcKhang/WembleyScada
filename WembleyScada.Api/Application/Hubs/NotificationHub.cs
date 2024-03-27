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
        var userId = Context.ConnectionId;
        var variablesToGet = Context.Items;
        var user = new HubUser(userId, new List<string>());
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Context.ConnectionId;

        _hubUserStorage.Remove(userId);

        await base.OnDisconnectedAsync(exception);
    }
    public async Task SendMetricsToUser()
    {
        var userId = Context.ConnectionId;
        var user = _hubUserStorage.Users.Find(x => x.UserId == userId);
        var json = _buffer.GetAllTags();

        if (user is null)
        {
            return;
        }

        await Clients.User(user.UserId).SendAsync("GetMetrics", json);
    }
    public async Task<string> SendAll() => await Task.FromResult(_buffer.GetAllTags());

    public async Task SendAllTags()
    {
        var tags = _buffer.GetAllTags();
        await Clients.All.SendAsync("GetAll", tags);
    }

    //Client gửi ConnectionId với ds biến => lưu lại => change => gửi lên
    //Memory leak khi có nhiều người subcrise => deconstructor
    //Làm sao để biết khi user ngắt kết nối thì có thể dispose dữ liệu về user đó
}
