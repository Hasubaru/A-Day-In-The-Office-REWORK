using ADayInTheOffice.Core;
using ADayInTheOffice.Data.Defs;
using ADayInTheOffice.Systems.Time;
using ADayInTheOffice.Systems.Tasks;
using ADayInTheOffice.Systems.StressEnergy;
using ADayInTheOffice.Systems.SceneFlow;

namespace ADayInTheOffice.Game
{
    public sealed class GameContext
    {
        public readonly SignalBus Signals = new();

        public readonly TimeSystem Time;
        public readonly PlayerStatsModel PlayerStats;
        public readonly StressEnergySystem StressEnergy;
        public readonly TaskSystem Tasks;
        public readonly SceneFlowSystem SceneFlow;

        public GameContext(TimeConfig timeConfig)
        {
            PlayerStats = new PlayerStatsModel();

            Time = new TimeSystem(Signals, timeConfig);
            StressEnergy = new StressEnergySystem(Signals, PlayerStats);
            Tasks = new TaskSystem(Signals, Time, StressEnergy);
            SceneFlow = new SceneFlowSystem(Signals, Time);
        }

        public void Initialize()
        {
            ServiceRegistry.Register(this);
            ServiceRegistry.Register(Signals);
            ServiceRegistry.Register(Time);
            ServiceRegistry.Register(PlayerStats);
            ServiceRegistry.Register(StressEnergy);
            ServiceRegistry.Register(Tasks);
            ServiceRegistry.Register(SceneFlow);

            Time.InitializeNewDay();
        }

        public void Tick(float deltaTime)
        {
            Time.Tick(deltaTime);
            Tasks.Tick(deltaTime);
        }
    }
}
