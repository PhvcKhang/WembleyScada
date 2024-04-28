namespace WembleyScada.Host.Application.Services;

public class OneSignalOptions
{
    public string BaseUrl { get; set; }
    public string ContentType { get; set; }
    public string Authorization { get; set; }

    public OneSignalOptions(string baseUrl, string contentType, string authorization)
    {
        BaseUrl = baseUrl;
        ContentType = contentType;
        Authorization = authorization;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public OneSignalOptions()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}
