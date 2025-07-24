namespace Events.Handlers
{
    public interface IEnemyDeathHandler: IGlobalSubscriber
    {
        public void HandleEnemyDeath();
    }
}