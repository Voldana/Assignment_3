using Zenject;

namespace Script
{
    public class SceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<GameEvents.OnPlayerHit>();
        }
    }
}
