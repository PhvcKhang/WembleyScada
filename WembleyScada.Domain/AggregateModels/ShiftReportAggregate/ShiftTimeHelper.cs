namespace WembleyScada.Domain.AggregateModels.ShiftReportAggregate;
public static class ShiftTimeHelper
{
    public static readonly TimeSpan Shift1StartTime = new(6, 0, 0);
    public static readonly TimeSpan Shift2StartTime = new(18, 0, 0);

    public static int GetShiftNumber(DateTime timestamp)
    {
        var shiftTime = timestamp.TimeOfDay;
        if (shiftTime > Shift1StartTime && shiftTime < Shift2StartTime)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public static DateTime GetShiftDate(DateTime timestamp)
    {
        var shiftNumber = GetShiftNumber(timestamp);
        if (shiftNumber == 2 && timestamp.TimeOfDay < Shift1StartTime)
        {
            return timestamp.Date.AddDays(-1);
        }
        else
        {
            return timestamp.Date;
        }
    }

    public static TimeSpan GetShiftElapsedTime(DateTime date, int shiftNumber)
    {
        return GetVietnamTime() - GetShiftStartTime(date, shiftNumber);
    }

    public static DateTime GetVietnamTime()
    {
        return DateTime.UtcNow.AddHours(7);
    }

    private static DateTime GetShiftStartTime(DateTime date, int shiftNumber)
    {
        if (shiftNumber == 1)
        {
            return date.Add(Shift1StartTime);
        }
        else
        {
            return date.Add(Shift2StartTime);
        }
    }
}
