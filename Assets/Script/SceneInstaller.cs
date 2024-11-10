using Zenject;

namespace Script
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<GameEvents.OnPlayerHit>();
            Container.DeclareSignal<GameEvents.OnShotFired>();
            Container.DeclareSignal<GameEvents.OnGunSwitch>();
            Container.DeclareSignal<GameEvents.OnPlayerHeal>();
            Container.DeclareSignal<GameEvents.OnPlayerDeath>();
            Container.DeclareSignal<GameEvents.OnEnemyDestroyed>();
        }
    }
}
