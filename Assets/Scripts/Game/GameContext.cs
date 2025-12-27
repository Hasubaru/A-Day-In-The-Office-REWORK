using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.Time;
using ADayInTheOffice.Systems.Tasks;
using ADayInTheOffice.Systems.StressEnergy;

namespace ADayInTheOffice.Game
{
    /// <summary>
    /// Owns runtime systems/models. Pure C#.
    /// </summary>
    public sealed class GameContext
    {
        public readonly SignalBus Signals = new();

        public readonly TimeSystem Time;
        public readonly PlayerStatsModel PlayerStats;
        public readonly StressEnergySystem StressEnergy;
        public readonly TaskSystem Tasks;

        public GameContext()
        {
            PlayerStats = new PlayerStatsModel();
            Time = new TimeSystem(Signals);
            StressEnergy = new StressEnergySystem(Signals, PlayerStats);
            Tasks = new TaskSystem(Signals, Time, StressEnergy);
        }

        public void Initialize()
        {
            ServiceRegistry.Register(this);
            ServiceRegistry.Register(Signals);
            ServiceRegistry.Register(Time);
            ServiceRegistry.Register(PlayerStats);
            ServiceRegistry.Register(StressEnergy);
            ServiceRegistry.Register(Tasks);

            Time.InitializeDefaultWorkday();
        }

        public void Tick(float deltaTime)
        {
            Time.Tick(deltaTime);
            Tasks.Tick(deltaTime);
        }
    }
}
