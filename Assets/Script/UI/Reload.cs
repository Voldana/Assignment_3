using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Script.UI
{
    public class Reload: MonoBehaviour
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private TMP_Text gunName;
        [SerializeField] private Image image;
        
        private bool isFilling;
        
        private float currentTime;
        private float fillTime = 2;

        private void Start()
        {
            SubscribeSignals();
        }

        private void SubscribeSignals()
        {
            signalBus.Subscribe<GameEvents.OnShotFired>(ShotFired);
            signalBus.Subscribe<GameEvents.OnGunSwitch>(GunSwitched);
        }

        private void GunSwitched(GameEvents.OnGunSwitch signal)
        {
            fillTime = signal.cooldown;
            gunName.text = signal.name;
            image.fillAmount = 1;
            isFilling = false;
        }
        
        private void ShotFired(GameEvents.OnShotFired signal)
        {
            fillTime = signal.cooldown;
            image.fillAmount = 0;
            currentTime = 0f;
            isFilling = true;
        }

        private void Update()
        {
            if (isFilling)
                FillCooldown();
        }
        
        private void FillCooldown()
        {
            if (currentTime < fillTime)
            {
                currentTime += Time.deltaTime; 
                image.fillAmount = Mathf.Clamp01(currentTime / fillTime);
            }
            else
                isFilling = false; 
        }
    }
}