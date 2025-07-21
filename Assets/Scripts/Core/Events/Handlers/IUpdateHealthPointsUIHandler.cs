namespace Core.Events.Handlers
{
    public interface IUpdateHealthPointsUIHandler: IGlobalSubscriber
    {
        public void UpdateHealthPoints(int healthPointsLeft);
    }
}