using System;
using ADayInTheOffice.Core;

namespace ADayInTheOffice.Systems.Time
{
    /// <summary>
    /// In-game clock for a workday.
    /// Defaults: 09:00 to 18:00, 1 real second = 1 in-game minute (tunable).
    /// </summary>
    public sealed class TimeSystem
    {
        private readonly SignalBus _signals;

        public float RealSecondsPerInGameMinute { get; private set; } = 1f;

        public int WorkStartMinute { get; private set; } = 9 * 60;
        public int WorkEndMinute { get; private set; } = 18 * 60;

        public int CurrentMinuteOfDay { get; private set; }

        private float _accum;

        public TimeSystem(SignalBus signals) => _signals = signals;

        public void InitializeDefaultWorkday()
        {
            CurrentMinuteOfDay = WorkStartMinute;
            _accum = 0f;
            _signals.Publish(new TimeChangedMsg(CurrentMinuteOfDay));
        }

        public void Tick(float dt)
        {
            if (CurrentMinuteOfDay >= WorkEndMinute) return;

            _accum += dt;
            while (_accum >= RealSecondsPerInGameMinute)
            {
                _accum -= RealSecondsPerInGameMinute;
                AdvanceOneMinute();
            }
        }

        private void AdvanceOneMinute()
        {
            CurrentMinuteOfDay++;
            _signals.Publish(new TimeChangedMsg(CurrentMinuteOfDay));

            if (CurrentMinuteOfDay >= WorkEndMinute)
            {
                CurrentMinuteOfDay = WorkEndMinute;
                _signals.Publish(new WorkdayEndedMsg());
            }
        }

        public static string FormatHHMM(int minuteOfDay)
        {
            minuteOfDay = Math.Clamp(minuteOfDay, 0, 24 * 60);
            int h = minuteOfDay / 60;
            int m = minuteOfDay % 60;
            return $"{h:00}:{m:00}";
        }
    }

    public readonly struct TimeChangedMsg
    {
        public readonly int MinuteOfDay;
        public TimeChangedMsg(int minuteOfDay) => MinuteOfDay = minuteOfDay;
    }

    public readonly struct WorkdayEndedMsg { }
}
