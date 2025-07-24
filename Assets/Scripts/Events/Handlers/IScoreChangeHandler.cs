namespace Events.Handlers
{
    public interface IScoreChangeHandler: IGlobalSubscriber
    {
        public void HandleScoreChange(int score);
    }
}