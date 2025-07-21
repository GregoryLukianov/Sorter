namespace Core.Events.Handlers
{
    public interface IScoreHandler: IGlobalSubscriber
    {
        public void AddScore();
    }
}