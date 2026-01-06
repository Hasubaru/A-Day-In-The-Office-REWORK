namespace ADayInTheOffice.Systems.StressEnergy
{
    public sealed class PlayerStatsModel
    {
        public float Energy { get; private set; } = 100f;
        public float Stress { get; private set; } = 0f;

        public void AddEnergy(float delta) => Energy = Clamp01_100(Energy + delta);
        public void AddStress(float delta) => Stress = Clamp01_100(Stress + delta);

        public void SetEnergy(float value) => Energy = Clamp01_100(value);
        public void SetStress(float value) => Stress = Clamp01_100(value);
        private static float Clamp01_100(float v)
        {
            if (v < 0f) return 0f;
            if (v > 100f) return 100f;
            return v;
        }
    }
}
