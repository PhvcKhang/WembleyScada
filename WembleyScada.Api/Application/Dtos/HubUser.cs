namespace WembleyScada.Api.Application.Dtos;

public class HubUser : IDisposable
{
    private bool isDisposed;

    public string ConnectionId { get; private set; }
    public List<string> Topics { get; private set; } = new List<string>();

    public HubUser(string connectionId)
    {
        ConnectionId = connectionId;
    }
    public void UpdateTopics(List<string> topics)
    {
        Topics.Clear();
        Topics.AddRange(topics);
    }
    protected virtual void Dispose(bool isDisposing)
    {
        if (!isDisposed)
        {
            if (isDisposing)
            {
                Dispose();
            }
            isDisposed = true;
        }
    }
    ~HubUser()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(isDisposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(isDisposing: true);
        GC.SuppressFinalize(this);
    }
}
