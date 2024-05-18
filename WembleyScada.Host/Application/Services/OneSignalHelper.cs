namespace WembleyScada.Host.Application.Services;

public class OneSignalHelper
{
    private OneSignalOptions Options;

    public OneSignalHelper(IOptions<OneSignalOptions> options)
    {
        Options = options.Value;
    }

    public async Task SendErrorStatusAsync(string lineId, string stationId, string metricName, object value, DateTime timestamp)
    {
        var options = new RestClientOptions(Options.BaseUrl);
        var client = new RestClient(options);

        var request = new RestRequest("");

        request.AddHeader("Content-Type", Options.ContentType);
        request.AddHeader("Authorization", Options.Authorization);

        var metric = new MetricMessage(metricName, value, timestamp);

        var includedSegments = "\"included_segments\": [\"Total Subscriptions\"],";
        var contents = "\"contents\":{\"en\":\"errorStatus\",\"vi\":\" " + value + " \"},";
        var appId = "\"app_id\":\"96b6cb31-9a02-4505-9afd-c89262e08349\",";
        var android_channel = "\"android_channel_id\":\"06860f70-789e-4331-ac69-daa579e89795\",";

        var dataContent = JsonConvert.SerializeObject(new List<MetricMessage>() { metric }).ToString();

        //Delete brackets
        dataContent = dataContent.Remove(0, 2);
        dataContent = dataContent.Remove(dataContent.Length - 2, 2);

        var data = "\"data\":{"+ dataContent +"}";

        var json = "{"+ includedSegments + contents + appId + android_channel + data + "}";

        request.AddJsonBody(json, false);

        await client.PostAsync(request);

    }
}
