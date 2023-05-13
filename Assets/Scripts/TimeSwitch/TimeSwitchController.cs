using System;

public enum TimePeriod{Past, Future}

public static class TimeSwitchController
{
    public static TimePeriod TimePeriod { get; set; }

    public static event Action OnPeriodSwitch;

    public static void ToggleTimePeriod()
    {
        TimePeriod = TimePeriod == TimePeriod.Past ? TimePeriod.Future : TimePeriod.Past;
        OnPeriodSwitch?.Invoke();
    }
    
    public static void ChangeTimePeriodToFuture()
    {
        TimePeriod = TimePeriod.Future;
        OnPeriodSwitch?.Invoke();
    }

    public static void ChangeTimePeriodToPast()
    {
        TimePeriod = TimePeriod.Past;
        OnPeriodSwitch?.Invoke();
    }
}
