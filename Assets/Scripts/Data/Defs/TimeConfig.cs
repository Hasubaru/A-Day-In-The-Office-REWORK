using UnityEngine;

namespace ADayInTheOffice.Data.Defs
{
    [CreateAssetMenu(
        menuName = "A Day In The Office/Config/Time Config",
        fileName = "TimeConfig_Default"
    )]
    public sealed class TimeConfig : ScriptableObject
    {
        [Header("Work Day")]
        [Range(0, 23)] public int WorkStartHour = 9;
        [Range(0, 23)] public int WorkEndHour = 18;

        [Header("Speed")]
        [Tooltip("REAL seconds per 1 IN-GAME minute")]
        [Min(0.05f)] public float RealSecondsPerInGameMinute = 1f;

        public int WorkStartMinute => WorkStartHour * 60;
        public int WorkEndMinute => WorkEndHour * 60;
    }
}
