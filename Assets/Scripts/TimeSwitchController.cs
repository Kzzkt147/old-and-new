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
}
