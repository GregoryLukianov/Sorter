using Events.Handlers;

namespace UI
{
    public class StartMenu : MenuPanel, IGameplayHandler
    {
        protected override void Awake()
        {
            _eventBus.Subscribe(this);
            TurnOn();
        }

        public void StartGame()
        {
            _eventBus.RaiseEvent<IGameplayHandler>(h => h.HandleGameplayStart());
        }

        public void HandleGameplayStart()
        {
            TurnOff();
        }
    }
}