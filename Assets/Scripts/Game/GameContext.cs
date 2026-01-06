using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;
using ADayInTheOffice.Systems.Time;
using ADayInTheOffice.Systems.Tasks;
using ADayInTheOffice.Systems.StressEnergy;
using ADayInTheOffice.Systems.SceneFlow;
using ADayInTheOffice.Systems.Day;
using ADayInTheOffice.Systems.Save;

namespace ADayInTheOffice.Game
{
    public sealed class GameContext
    {
        public readonly SignalBus Signals;

        public readonly SaveSystem Save;
        public readonly TimeSystem Time;
        public readonly PlayerStatsModel PlayerStats;
        public readonly StressEnergySystem StressEnergy;
        public readonly TaskSystem Tasks;
        public readonly SceneFlowSystem SceneFlow;

        public readonly PauseService Pause;
        public readonly DayStatsModel DayStats;

        public GameContext(TimeConfig timeConfig)
        {
            Signals = new SignalBus();

            Pause = new PauseService();

            PlayerStats = new PlayerStatsModel();
            DayStats = new DayStatsModel();

            Save = new SaveSystem(DayStats, PlayerStats);

            StressEnergy = new StressEnergySystem(Signals, PlayerStats);
            DayStatsSystem _ = new DayStatsSystem(Signals, DayStats);

            Time = new TimeSystem(Signals, timeConfig);
            Tasks = new TaskSystem(Signals, Time, StressEnergy);

            EndOfDaySummarySystem __ = new EndOfDaySummarySystem(Signals, DayStats, PlayerStats);

            SceneFlow = new SceneFlowSystem(Signals, Time);
        }

        public void Initialize()
        {
            ServiceRegistry.Register(this);
            ServiceRegistry.Register(Signals);
            ServiceRegistry.Register(Pause);

            ServiceRegistry.Register(DayStats);
            ServiceRegistry.Register(PlayerStats);
            ServiceRegistry.Register(StressEnergy);
            ServiceRegistry.Register(Time);
            ServiceRegistry.Register(Tasks);
            ServiceRegistry.Register(SceneFlow);
            ServiceRegistry.Register(Save);

            Save.LoadOrCreate();
            Time.InitializeNewDay();
        }

        public void Tick(float dt)
        {
            if (Pause.IsPaused) return;

            Time.Tick(dt);
            Tasks.Tick(dt);
        }
    }
}
