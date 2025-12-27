using UnityEngine;

namespace ADayInTheOffice.Data.Defs
{
    [CreateAssetMenu(menuName = "A Day In The Office/Defs/TaskDef", fileName = "TaskDef_")]
    public sealed class TaskDef : ScriptableObject
    {
        [Header("Identity")]
        public string Id = "task_id";
        public string DisplayName = "Task";

        [Header("Rules")]
        public WorkstationType RequiredWorkstation = WorkstationType.DevDesk;
        [Min(1)] public int DurationMinutes = 30;

        [Header("Effects")]
        public float StressDeltaOnComplete = 5f;
        public float EnergyDeltaOnComplete = -10f;
    }
}
