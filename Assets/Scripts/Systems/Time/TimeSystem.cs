using System;
using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;

namespace ADayInTheOffice.Systems.Time
{
    public sealed class TimeSystem
    {
        private readonly SignalBus _signals;
        private readonly TimeConfig _cfg;

        private float _accum;

        public int CurrentMinuteOfDay { get; private set; }
        public float RealSecondsPerInGameMinute => _cfg.RealSecondsPerInGameMinute;

        public TimeSystem(SignalBus signals, TimeConfig cfg)
        {
            _signals = signals;
            _cfg = cfg;
        }

        public void InitializeNewDay()
        {
            CurrentMinuteOfDay = _cfg.WorkStartMinute;
            _accum = 0f;
            _signals.Publish(new TimeChangedMsg(CurrentMinuteOfDay));
        }

        public void Tick(float dt)
        {
            if (CurrentMinuteOfDay >= _cfg.WorkEndMinute) return;

            _accum += dt;
            while (_accum >= _cfg.RealSecondsPerInGameMinute)
            {
                _accum -= _cfg.RealSecondsPerInGameMinute;
                AdvanceOneMinute();
            }
        }

        private void AdvanceOneMinute()
        {
            CurrentMinuteOfDay++;
            _signals.Publish(new TimeChangedMsg(CurrentMinuteOfDay));

            if (CurrentMinuteOfDay >= _cfg.WorkEndMinute)
            {
                CurrentMinuteOfDay = _cfg.WorkEndMinute;
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
        public TimeChangedMsg(int minute) => MinuteOfDay = minute;
    }

    public readonly struct WorkdayEndedMsg { }
}
