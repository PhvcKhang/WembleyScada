﻿namespace WembleyScada.Api.Application.Dtos;

public class TagChangedNotification
{
    public string StationId { get; set; }
    public string LineId { get; set; }
    public string TagId { get; set; }
    public object TagValue { get; set; }
    public DateTime TimeStamp { get; set; }

    public TagChangedNotification(string stationId, string lineId, string tagId, object tagValue, DateTime timeStamp)
    {
        StationId = stationId;
        LineId = lineId;
        TagId = tagId;
        TagValue = tagValue;
        TimeStamp = timeStamp;
    }
}
