namespace Events.Handlers
{
    public interface IVictoryHandler: IGlobalSubscriber
    {
        public void HandleVictory(int score);
    }
}