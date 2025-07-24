namespace Events.Handlers
{
    public interface IGameOverHandler: IGlobalSubscriber
    {
        public void HandleGameOver();
    }
}