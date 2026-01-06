using UnityEngine;
using UnityEngine.SceneManagement;
using ADayInTheOffice.Characters.Player;
using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.Time;
using ADayInTheOffice.Systems.Day;

namespace ADayInTheOffice.Systems.SceneFlow
{
    public sealed class SceneFlowSystem
    {
        private readonly SignalBus _signals;
        private readonly TimeSystem _time;

        public string OfficeSceneName { get; private set; } = "Office";
        public string CitySceneName { get; private set; } = "City";
        public string HomeSceneName { get; private set; } = "Home";

        public SceneFlowSystem(SignalBus signals, TimeSystem time)
        {
            _signals = signals;
            _time = time;

            _signals.Subscribe<EndOfDaySummaryConfirmedMsg>(_ => GoToCity());
        }

        public void Configure(string office, string city, string home)
        {
            if (!string.IsNullOrWhiteSpace(office)) OfficeSceneName = office;
            if (!string.IsNullOrWhiteSpace(city)) CitySceneName = city;
            if (!string.IsNullOrWhiteSpace(home)) HomeSceneName = home;
        }

        public void GoToOffice()
        {
            SceneManager.sceneLoaded += OnSceneLoaded_MovePlayer;
            SceneManager.LoadScene(OfficeSceneName);
        }

        public void GoToCity()
        {
            SceneManager.sceneLoaded += OnSceneLoaded_MovePlayer;
            SceneManager.LoadScene(CitySceneName);
        }

        public void GoToHome()
        {
            SceneManager.sceneLoaded += OnSceneLoaded_MovePlayer;
            SceneManager.LoadScene(HomeSceneName);
        }

        private void OnSceneLoaded_MovePlayer(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded_MovePlayer;

            // tìm PlayerRoot (object có PlayerPersistence)
            var playerRoot = Object.FindFirstObjectByType<PlayerPersistence>();
            if (playerRoot == null) return;

            // tìm SpawnPoint trong scene mới
            var spawn = Object.FindFirstObjectByType<SceneSpawnPoint>();
            if (spawn == null) return;

            // tìm Rigidbody2D (nằm trong Player con)
            var rb = playerRoot.GetComponentInChildren<Rigidbody2D>();

            if (rb != null)
            {
                // XÓA TOÀN BỘ ĐÀ DI CHUYỂN
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;

                // ĐẶT PLAYER VÀO ĐÚNG SPAWN
                rb.position = spawn.transform.position;
                rb.rotation = spawn.transform.eulerAngles.z;
            }
            else
            {
                // fallback nếu không có Rigidbody
                playerRoot.transform.position = spawn.transform.position;
            }
        }


        public void SleepAndStartNewDay()
        {
            ServiceRegistry.Get<ADayInTheOffice.Systems.Save.SaveSystem>().SaveNow();
            var dayStats = ServiceRegistry.Get<ADayInTheOffice.Systems.Day.DayStatsModel>();
            dayStats.ResetForNewDay();

            _time.InitializeNewDay();
            GoToOffice();
        }
    }
}
