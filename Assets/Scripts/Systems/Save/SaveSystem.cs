using System;
using System.IO;
using UnityEngine;
using ADayInTheOffice.Systems.Day;
using ADayInTheOffice.Systems.StressEnergy;

namespace ADayInTheOffice.Systems.Save
{
    public sealed class SaveSystem
    {
        private const string FileName = "save.json";

        private readonly DayStatsModel _day;
        private readonly PlayerStatsModel _stats;

        private string FilePath => Path.Combine(Application.persistentDataPath, FileName);

        public SaveSystem(DayStatsModel day, PlayerStatsModel stats)
        {
            _day = day ?? throw new ArgumentNullException(nameof(day));
            _stats = stats ?? throw new ArgumentNullException(nameof(stats));
        }

        public bool HasSave() => File.Exists(FilePath);

        public void SaveNow()
        {
            var data = new SaveData
            {
                Version = 1,
                DayIndex = _day.DayIndex,
                Energy = _stats.Energy,
                Stress = _stats.Stress
            };

            var json = JsonUtility.ToJson(data, true);

            var dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(FilePath, json);
        }

        public void LoadOrCreate()
        {
            if (!File.Exists(FilePath))
            {
                SaveNow();
                return;
            }

            try
            {
                var json = File.ReadAllText(FilePath);
                var data = JsonUtility.FromJson<SaveData>(json);
                if (data == null)
                {
                    SaveNow();
                    return;
                }

                Apply(data);
            }
            catch (Exception e)
            {
                Debug.LogError($"SaveSystem load failed: {e.Message}");
                // Keep defaults if corrupted.
            }
        }

        private void Apply(SaveData data)
        {
            _day.SetDayIndex(data.DayIndex);
            _stats.SetEnergy(data.Energy);
            _stats.SetStress(data.Stress);
        }
    }
}
