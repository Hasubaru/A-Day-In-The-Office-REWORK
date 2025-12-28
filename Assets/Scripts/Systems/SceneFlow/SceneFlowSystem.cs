using UnityEngine.SceneManagement;
using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.Time;

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

            _signals.Subscribe<WorkdayEndedMsg>(_ => GoToCity());
        }

        public void Configure(string office, string city, string home)
        {
            if (!string.IsNullOrWhiteSpace(office)) OfficeSceneName = office;
            if (!string.IsNullOrWhiteSpace(city)) CitySceneName = city;
            if (!string.IsNullOrWhiteSpace(home)) HomeSceneName = home;
        }

        public void GoToOffice()
        {
            SceneManager.LoadScene(OfficeSceneName);
        }

        public void GoToCity()
        {
            SceneManager.LoadScene(CitySceneName);
        }

        public void GoToHome()
        {
            SceneManager.LoadScene(HomeSceneName);
        }

        public void SleepAndStartNewDay()
        {
            _time.InitializeNewDay();
            GoToOffice();
        }
    }
}
