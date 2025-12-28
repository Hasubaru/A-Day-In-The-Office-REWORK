namespace ADayInTheOffice.Systems.Day
{
    public sealed class DayStatsModel
    {
        public int DayIndex { get; private set; } = 1;
        public int TasksCompletedToday { get; private set; }

        public void ResetForNewDay()
        {
            DayIndex++;
            TasksCompletedToday = 0;
        }

        public void AddTaskCompleted() => TasksCompletedToday++;
    }
}
