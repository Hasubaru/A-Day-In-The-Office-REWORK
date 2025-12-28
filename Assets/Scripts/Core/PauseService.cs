namespace ADayInTheOffice.Core
{
    public sealed class PauseService
    {
        public bool IsPaused { get; private set; }
        public void SetPaused(bool paused) => IsPaused = paused;
    }
}
