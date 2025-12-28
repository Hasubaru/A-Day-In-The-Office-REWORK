using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.Tasks;

namespace ADayInTheOffice.Systems.Day
{
    public sealed class DayStatsSystem
    {
        private readonly SignalBus _signals;
        private readonly DayStatsModel _model;

        public DayStatsSystem(SignalBus signals, DayStatsModel model)
        {
            _signals = signals;
            _model = model;

            _signals.Subscribe<TaskCompletedMsg>(_ => _model.AddTaskCompleted());
        }
    }
}
