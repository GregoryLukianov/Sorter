using Shapes.Factories.Base.Factories;
using Zenject;

namespace Core.Installers
{
    public class EnemyFactoryInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemiesFactory>().AsSingle();
        }
    }
}