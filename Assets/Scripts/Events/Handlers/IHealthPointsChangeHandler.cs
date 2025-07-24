namespace Events.Handlers
{
    public interface IHealthPointsChangeHandler: IGlobalSubscriber
    {
        public void HandleHealthPointsChange(int healthPointsLeft, int healthPoints);
    }
}