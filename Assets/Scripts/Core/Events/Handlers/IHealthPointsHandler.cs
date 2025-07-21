namespace Core.Events.Handlers
{
    public interface IHealthPointsHandler: IGlobalSubscriber
    {
        public void GetDamage();
    }
}