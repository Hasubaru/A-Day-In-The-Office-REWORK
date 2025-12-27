using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;
using ADayInTheOffice.Systems.Time;
using ADayInTheOffice.Systems.StressEnergy;

namespace ADayInTheOffice.Systems.Tasks
{
    public sealed class TaskSystem
    {
        private readonly SignalBus _signals;
        private readonly TimeSystem _time;
        private readonly StressEnergySystem _stressEnergy;

        private ActiveTask _active;

        public TaskSystem(SignalBus signals, TimeSystem time, StressEnergySystem stressEnergy)
        {
            _signals = signals;
            _time = time;
            _stressEnergy = stressEnergy;

            _signals.Subscribe<WorkdayEndedMsg>(_ => CancelActiveTask());
        }

        public bool HasActiveTask => _active.Def != null;

        public void StartTask(TaskDef def)
        {
            if (def == null) return;
            if (HasActiveTask) return;

            _active = new ActiveTask(def, def.DurationMinutes);
            _signals.Publish(new TaskStartedMsg(def));
        }

        public void CancelActiveTask()
        {
            if (!HasActiveTask) return;
            var def = _active.Def;
            _active = default;
            _signals.Publish(new TaskCanceledMsg(def));
        }

        public void Tick(float dt)
        {
            if (!HasActiveTask) return;

            float minutesPerSecond = 1f / _time.RealSecondsPerInGameMinute;
            _active.RemainingMinutes -= dt * minutesPerSecond;

            float progress01 = 1f - (_active.RemainingMinutes / _active.TotalMinutes);
            if (progress01 < 0f) progress01 = 0f;
            if (progress01 > 1f) progress01 = 1f;

            _signals.Publish(new TaskProgressMsg(_active.Def, progress01));

            if (_active.RemainingMinutes <= 0f)
                CompleteTask();
        }

        private void CompleteTask()
        {
            var def = _active.Def;
            _active = default;

            _stressEnergy.ApplyStress(def.StressDeltaOnComplete);
            _stressEnergy.ApplyEnergy(def.EnergyDeltaOnComplete);

            _signals.Publish(new TaskCompletedMsg(def));
        }

        private struct ActiveTask
        {
            public TaskDef Def;
            public float RemainingMinutes;
            public float TotalMinutes;

            public ActiveTask(TaskDef def, int totalMinutes)
            {
                Def = def;
                TotalMinutes = totalMinutes;
                RemainingMinutes = totalMinutes;
            }
        }
    }

    public readonly struct TaskStartedMsg { public readonly TaskDef Def; public TaskStartedMsg(TaskDef def) => Def = def; }
    public readonly struct TaskProgressMsg { public readonly TaskDef Def; public readonly float Progress01; public TaskProgressMsg(TaskDef def, float p){Def=def;Progress01=p;} }
    public readonly struct TaskCompletedMsg { public readonly TaskDef Def; public TaskCompletedMsg(TaskDef def) => Def = def; }
    public readonly struct TaskCanceledMsg { public readonly TaskDef Def; public TaskCanceledMsg(TaskDef def) => Def = def; }
}
