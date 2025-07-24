namespace Events.Handlers
{
    public interface IGameplayHandler: IGlobalSubscriber
    {
        public void HandleGameplayStart();
    }
}