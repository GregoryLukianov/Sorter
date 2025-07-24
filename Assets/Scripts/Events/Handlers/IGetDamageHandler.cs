namespace Events.Handlers
{
    public interface IGetDamageHandler: IGlobalSubscriber
    {
        public void HandleGetDamage();
    }
}