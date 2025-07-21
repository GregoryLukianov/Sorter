namespace Core.Events.Handlers
{
    public interface IUpdateScoreUIHandler: IGlobalSubscriber
    {
        public void UpdateScoreUI(int score);
    }
}