namespace WembleyScada.Api.Application.Dtos;

public class HubUser : IDisposable
{
    private bool IsDisposed = false;

    public string UserId { get; private set; }
    public List<string> Metrics { get; private set; }

    public HubUser(string userId, List<string> metrics)
    {
        UserId = userId;
        Metrics = metrics;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    ~HubUser()
    {
        Dispose(false);
    }
    protected virtual void Dispose(bool disposing)
    {
        if(!IsDisposed)
        {
            if(disposing)
            {
                Dispose();
            }

            Dispose();
            IsDisposed = true;
        }
    }
}
