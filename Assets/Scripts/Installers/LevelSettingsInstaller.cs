using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(menuName = "ScriptableObjectsInstallers/LevelSettings")]
    public class LevelSettingsInstaller: ScriptableObjectInstaller<LevelSettingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<LevelSettings>().FromScriptableObjectResource("ScriptableObjects/LevelSettings").AsSingle();
        }
    }
}