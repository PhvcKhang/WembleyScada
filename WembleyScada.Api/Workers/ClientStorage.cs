namespace WembleyScada.Api.Workers;

public class ClientStorage
{
    private readonly List<Client> Clients  = new();

    public ClientStorage() { }

    public void AddClient(Client client)
    {
        Clients.Add(client);
    }
    public async Task<List<Client>> GetSubcribedClientsByTopic(string topic)
    {
        var subcribedClients = new List<Client>();

        foreach (var client in Clients)
        {
            var isContained = client.RegisteredTopics.Any(clientTopic => topic.Contains(clientTopic));

            if (isContained)
            {
                subcribedClients.Add(client);
            }
        }
        return await Task.FromResult(subcribedClients);
    }
    public void RemoveClient(string connectionId)
    {
        var client = Clients.Find(x => x.ConnectionId == connectionId);

        if (client is null)
        {
            return;
        }

        Clients.Remove(client);
        client.Dispose();
    }
}
