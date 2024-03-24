namespace WembleyScada.Api.Workers;

public class HubUserStorage
{
    public List<HubUser> Users { get; private set; } = new List<HubUser>();
    public void Add(HubUser user)
    {
        Users.Add(user);
    }
    public void Remove(string userId)
    {
        var user = Users.Find(x => x.UserId == userId);

        if (user is null)
        {
            return;
        }

        Users.Remove(user);
        user.Dispose();
    }
}
