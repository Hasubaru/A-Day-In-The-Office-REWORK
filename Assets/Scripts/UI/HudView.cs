using TMPro;
using UnityEngine;
using ADayInTheOffice.Core;
using ADayInTheOffice.Systems.Time;
using ADayInTheOffice.Systems.Tasks;
using ADayInTheOffice.Systems.StressEnergy;

namespace ADayInTheOffice.UI
{
    public sealed class HudView : MonoBehaviour
    {
        [Header("Assign TMP texts")]
        [SerializeField] private TMP_Text _timeText;
        [SerializeField] private TMP_Text _taskText;
        [SerializeField] private TMP_Text _statsText;

        private SignalBus _signals;

        private void Awake()
        {
            _signals = ServiceRegistry.Get<SignalBus>();
        }

        private void OnEnable()
        {
            _signals.Subscribe<TimeChangedMsg>(OnTime);
            _signals.Subscribe<TaskStartedMsg>(OnTaskStarted);
            _signals.Subscribe<TaskProgressMsg>(OnTaskProgress);
            _signals.Subscribe<TaskCompletedMsg>(OnTaskCompleted);
            _signals.Subscribe<PlayerStatsChangedMsg>(OnStats);
        }

        private void OnDisable()
        {
            _signals.Unsubscribe<TimeChangedMsg>(OnTime);
            _signals.Unsubscribe<TaskStartedMsg>(OnTaskStarted);
            _signals.Unsubscribe<TaskProgressMsg>(OnTaskProgress);
            _signals.Unsubscribe<TaskCompletedMsg>(OnTaskCompleted);
            _signals.Unsubscribe<PlayerStatsChangedMsg>(OnStats);
        }

        private void OnTime(TimeChangedMsg msg)
        {
            if (_timeText != null) _timeText.text = TimeSystem.FormatHHMM(msg.MinuteOfDay);
        }

        private void OnTaskStarted(TaskStartedMsg msg)
        {
            if (_taskText != null) _taskText.text = $"{msg.Def.DisplayName}: 0%";
        }

        private void OnTaskProgress(TaskProgressMsg msg)
        {
            if (_taskText == null) return;
            int pct = Mathf.RoundToInt(msg.Progress01 * 100f);
            _taskText.text = $"{msg.Def.DisplayName}: {pct}%";
        }

        private void OnTaskCompleted(TaskCompletedMsg msg)
        {
            if (_taskText != null) _taskText.text = $"{msg.Def.DisplayName}: DONE";
        }

        private void OnStats(PlayerStatsChangedMsg msg)
        {
            if (_statsText != null) _statsText.text = $"Energy {msg.Energy:0} | Stress {msg.Stress:0}";
        }
    }
}
