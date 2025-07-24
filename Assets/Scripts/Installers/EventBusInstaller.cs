using Events;
using Zenject;

namespace Installers
{
    public class EventBusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle();
        }
    }
}