namespace ADayInTheOffice.Systems.Day
{
    public readonly struct EndOfDaySummaryData
    {
        public readonly int Day;
        public readonly int TasksCompleted;
        public readonly float Energy;
        public readonly float Stress;

        public EndOfDaySummaryData(int day, int tasksCompleted, float energy, float stress)
        {
            Day = day;
            TasksCompleted = tasksCompleted;
            Energy = energy;
            Stress = stress;
        }
    }

    public readonly struct ShowEndOfDaySummaryMsg
    {
        public readonly EndOfDaySummaryData Data;
        public ShowEndOfDaySummaryMsg(EndOfDaySummaryData data) => Data = data;
    }

    public readonly struct EndOfDaySummaryConfirmedMsg { }
}
