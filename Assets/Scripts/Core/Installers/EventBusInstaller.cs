using Core.Events;
using Zenject;

namespace Core.Installers
{
    public class EventBusInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EventBus>().AsSingle();
        }
    }
}