using Core.Gameplay;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(menuName = "ScriptableObjectsInstallers/EnemyCatalog")]
    public class EnemyCatalogInstaller: ScriptableObjectInstaller<EnemyCatalogInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyCatalogSO>().FromScriptableObjectResource("ScriptableObjects/Enemies/EnemyCatalog").AsSingle();
        }
    }
}