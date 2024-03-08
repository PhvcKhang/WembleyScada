﻿namespace WembleyScada.Api.Workers;

public class Buffer
{
    private readonly List<TagChangedNotification> tagChangedNotifications = new();
    private readonly ManagedMqttClient _mqttClient;

    public Buffer(ManagedMqttClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    public async Task Update(TagChangedNotification notification)
    {
        if(notification.TagId != "errorStatus" && notification.TagId != "endErrorStatus")
        {
            var existedNotification = tagChangedNotifications
                .FirstOrDefault(x => x.StationId == notification.StationId
                                      && x.TagId == notification.TagId);

            if(existedNotification is null)
            {
                tagChangedNotifications.Add(notification);
            }
            else
            {
                existedNotification.TagValue = notification.TagValue;
            }
        }

        else if(notification.TagId == "errorStatus")
        {
            if(!tagChangedNotifications
                .Any(x => x.StationId == notification.StationId
                       && x.TagId == "errorStatus"
                       && (string) x.TagValue == (string)notification.TagValue))
            {
                tagChangedNotifications.Add(notification);
            }
        }

        else if(notification.TagId == "machineStatus")
        {
            var status = Convert.ToInt16(notification.TagValue);

            if(status == 5)
            {
                var tagIds = tagChangedNotifications
                    .Where(x => x.StationId == notification.StationId)
                    .Select(x => x.TagId)
                    .ToList();

                foreach(var tagId in tagIds)
                {
                    await _mqttClient.Publish($"{notification.LineId}/{notification.StationId}/Metric/{tagId}", "", true);
                }

                ClearBuffer(notification.StationId);
            }
        }
    }
    public string GetAllTags() => System.Text.Json.JsonSerializer.Serialize(tagChangedNotifications);

    public List<TagChangedNotification> GetTagsByStationId(string stationId)
    {
        return tagChangedNotifications
            .Where(x => x.StationId == stationId)
            .ToList();
    }
    public void ClearBuffer(string stationId)
    {
        tagChangedNotifications
            .RemoveAll(x => x.StationId == stationId);
    }
}