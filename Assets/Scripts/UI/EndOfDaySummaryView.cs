using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.Day;

namespace ADayInTheOffice.UI
{
    public sealed class EndOfDaySummaryView : MonoBehaviour
    {
        [SerializeField] private GameObject _panelRoot;
        [SerializeField] private TMP_Text _dayText;
        [SerializeField] private TMP_Text _tasksText;
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private TMP_Text _stressText;

        [SerializeField] private Button _continueButton;

        private SignalBus _signals;
        private PauseService _pause;

        private void Awake()
        {
            _signals = ServiceRegistry.Get<SignalBus>();
            _pause = ServiceRegistry.Get<PauseService>();

            if (_panelRoot != null) _panelRoot.SetActive(false);

            if (_continueButton != null)
                _continueButton.onClick.AddListener(OnContinueClicked);
        }

        private void OnEnable()
        {
            _signals.Subscribe<ShowEndOfDaySummaryMsg>(OnShow);
        }

        private void OnDisable()
        {
            _signals.Unsubscribe<ShowEndOfDaySummaryMsg>(OnShow);
        }

        private void OnShow(ShowEndOfDaySummaryMsg msg)
        {
            var d = msg.Data;

            if (_dayText != null)
                _dayText.text = $"Day {d.Day}";

            if (_tasksText != null)
                _tasksText.text = $"Tasks: {d.TasksCompleted}";

            if (_energyText != null)
                _energyText.text = $"Energy: {d.Energy:0}";

            if (_stressText != null)
                _stressText.text = $"Stress: {d.Stress:0}";

            _panelRoot.SetActive(true);
            _pause.SetPaused(true);
        }


        private void OnContinueClicked()
        {
            if (_panelRoot != null) _panelRoot.SetActive(false);
            _pause.SetPaused(false);

            _signals.Publish(new EndOfDaySummaryConfirmedMsg());
        }
    }
}
