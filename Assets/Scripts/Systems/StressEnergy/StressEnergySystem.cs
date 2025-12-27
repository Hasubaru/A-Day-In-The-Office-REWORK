using ADayInTheOffice.Core;

namespace ADayInTheOffice.Systems.StressEnergy
{
    public sealed class StressEnergySystem
    {
        private readonly SignalBus _signals;
        private readonly PlayerStatsModel _stats;

        public StressEnergySystem(SignalBus signals, PlayerStatsModel stats)
        {
            _signals = signals;
            _stats = stats;
        }

        public void ApplyStress(float delta)
        {
            _stats.AddStress(delta);
            _signals.Publish(new PlayerStatsChangedMsg(_stats.Energy, _stats.Stress));
        }

        public void ApplyEnergy(float delta)
        {
            _stats.AddEnergy(delta);
            _signals.Publish(new PlayerStatsChangedMsg(_stats.Energy, _stats.Stress));
        }
    }

    public readonly struct PlayerStatsChangedMsg
    {
        public readonly float Energy;
        public readonly float Stress;

        public PlayerStatsChangedMsg(float energy, float stress)
        {
            Energy = energy;
            Stress = stress;
        }
    }
}
