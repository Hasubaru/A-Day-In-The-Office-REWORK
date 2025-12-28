using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.StressEnergy;
using ADayInTheOffice.Systems.Time;

namespace ADayInTheOffice.Systems.Day
{
    public sealed class EndOfDaySummarySystem
    {
        private readonly SignalBus _signals;
        private readonly DayStatsModel _dayStats;
        private readonly PlayerStatsModel _playerStats;

        public EndOfDaySummarySystem(
            SignalBus signals,
            DayStatsModel dayStats,
            PlayerStatsModel playerStats)
        {
            _signals = signals;
            _dayStats = dayStats;
            _playerStats = playerStats;

            _signals.Subscribe<WorkdayEndedMsg>(_ => OnWorkdayEnded());
        }

        private void OnWorkdayEnded()
        {
            var data = new EndOfDaySummaryData(
                _dayStats.DayIndex,
                _dayStats.TasksCompletedToday,
                _playerStats.Energy,
                _playerStats.Stress
            );

            _signals.Publish(new ShowEndOfDaySummaryMsg(data));
        }
    }
}
