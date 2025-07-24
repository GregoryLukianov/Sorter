namespace Events.Handlers
{
    public interface IAddScoreHandler: IGlobalSubscriber
    {
        public void HandleAddScore();
    }
}