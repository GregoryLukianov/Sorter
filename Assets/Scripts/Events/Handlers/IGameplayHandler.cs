namespace Events.Handlers
{
    public interface IGameplayHandler: IGlobalSubscriber
    {
        public void HandleGameplayStart();
        public void HandleGameOver();
        public void HandleWin(int score);
    }
}