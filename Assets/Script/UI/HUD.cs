using System;
using Project.Scripts.UI.HUD;
using TMPro;
using UnityEngine;
using Zenject;

namespace Script.UI
{
    public class HUD: MonoBehaviour
    {
        [Inject] private SignalBus signalBus;
        
        [SerializeField] private TMP_Text clock;
        
        private Timer timer;

        private void Start()
        {
            SubscribeSignals();
            timer = new Timer(0, UpdateTime);
            timer.Start();
        }

        private void SubscribeSignals()
        {
        }

        private void UpdateTime(int newTime)
        {
            clock.text = $"{newTime / 60}:{newTime % 60:D2}";
        }
    }
}