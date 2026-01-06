using System;

namespace ADayInTheOffice.Systems.Save
{
    [Serializable]
    public sealed class SaveData
    {
        public int Version = 1;

        public int DayIndex = 1;
        public float Energy = 100f;
        public float Stress = 0f;
    }
}
