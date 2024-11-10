using System;
using DG.Tweening;

namespace Project.Scripts.UI.HUD
{
    internal class Timer
    {
        private readonly int startTime;
        private Action<int> onTick;
        private Action onComplete;

        private bool timerStarted;
        private bool stopTimer;
        private int timer;
        private Tween tween;

        public Timer(int startTime, Action<int> onTick)
        {
            this.startTime = startTime;
            this.onTick = onTick;
        }

        public void Start()
        {
            if (timerStarted)
                return;
            tween?.Kill();

            timerStarted = true;
            stopTimer = false;
            timer = startTime;
            PassTime();
        }

        public void Reset()
        {
            timerStarted = false;
            Start();
        }

        private void PassTime()
        {
            tween = DOVirtual.DelayedCall(1, () =>
            {
                timer++;
                if (stopTimer) return;
                onTick?.Invoke(timer);
                if (timer <= 0)
                {
                    onComplete?.Invoke();
                    timerStarted = false;
                    return;
                }

                PassTime();
            });
        }

        public bool IsStarted()
        {
            return timerStarted;
        }

        public void IncreaseTime(int amount)
        {
            timer += amount;
        }

        public int GetRemainingTime()
        {
            return timer;
        }

        public void Stop()
        {
            timerStarted = false;
            stopTimer = true;
            tween?.Kill();
        }
    }
}