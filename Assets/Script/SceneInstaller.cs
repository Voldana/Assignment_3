using Script.UI;
using UnityEngine;
using Zenject;

namespace Script
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private LoseMenu loseMenu;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindFactory<LossDetails, LoseMenu, LoseMenu.Factory>().FromComponentInNewPrefab(loseMenu);

            Container.DeclareSignal<GameEvents.OnPlayerHit>();
            Container.DeclareSignal<GameEvents.OnShotFired>();
            Container.DeclareSignal<GameEvents.OnGunSwitch>();
            Container.DeclareSignal<GameEvents.OnPlayerHeal>();
            Container.DeclareSignal<GameEvents.OnPlayerDeath>();
            Container.DeclareSignal<GameEvents.OnEnemyDestroyed>();
        }
    }
}