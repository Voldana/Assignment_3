using System;
using Project.Scripts.UI.HUD;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class HUD: MonoBehaviour
    {
        [SerializeField] private TMP_Text clock;

        private Timer timer;

        private void Start()
        {
            timer = new Timer(0, UpdateTime);
            timer.Start();
        }

        private void UpdateTime(int newTime)
        {
            clock.text = $"{newTime / 60}:{newTime % 60:D2}";
        }
    }
}