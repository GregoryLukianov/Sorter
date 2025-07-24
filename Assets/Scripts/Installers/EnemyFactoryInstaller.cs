using Factory.Factories;
using Zenject;

namespace Installers
{
    public class EnemyFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemiesFactory>().AsSingle();
        }
    }
}