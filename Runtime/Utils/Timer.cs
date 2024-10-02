using System;

namespace FiveBabbittGames
{ 
    public abstract class Timer 
    {
        protected float initialTime;
        protected float Time { get; set; }
        public bool IsRunning { get; protected set; }
        public float InitialTime => initialTime;
    
        public float Progress => Time / initialTime;
    
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };
        public Action<float> OnTimerAdjusted = delegate { };

        public float GetTime() => Time;

        protected Timer(float value) 
        {
            initialTime = value;
            IsRunning = false;
        }

        /// <summary>
        /// Start the <see cref="Timer">, set the current time to the initial time and invoke <see cref="OnTimerStart"/>
        /// </summary>
        public void Start()
        {
            Time = initialTime;
            if (!IsRunning) 
            {
                IsRunning = true;
                OnTimerStart.Invoke();
            }
        }

        /// <summary>
        /// Stop the timer and invoke <see cref="OnTimerStop"/>
        /// </summary>
        public void Stop() 
        {
            if (IsRunning) 
            {
                IsRunning = false;
                OnTimerStop.Invoke();
            }
        }

        /// <summary>
        /// Add/Subtract time to <see cref="Time"/> while the timer is running and Invoke <see cref="OnTimerAdjusted"/>
        /// </summary>
        /// <param name="time">Time to be added or subtracted</param>
        public void AdjustTime(float time)
        {
            if (IsRunning)
            {
                Time += time;
                OnTimerAdjusted.Invoke(time);
            }
        }
    
        public void Resume() => IsRunning = true;
        public void Pause() => IsRunning = false;
    
        public abstract void Tick(float deltaTime);
    }

    public class CountdownTimer : Timer 
    {
        public CountdownTimer(float value) : base(value) { }

        public override void Tick(float deltaTime) 
        {
            if (IsRunning && Time > 0) 
            {
                Time -= deltaTime;
            }
        
            if (IsRunning && Time <= 0) 
            {
                Stop();
            }
        }
    
        public bool IsFinished => Time <= 0;
    
        public void Reset() => Time = initialTime;

        /// <summary>
        /// Overload of <see cref="Reset"/> where initialTime is set to newTime and <see cref="Time"/> is set to initialTime
        /// </summary>
        /// <param name="newTime"></param>
        public void Reset(float newTime) 
        {
            initialTime = newTime;
            Reset();
        }
    }

    public class StopwatchTimer : Timer 
    {
        public StopwatchTimer() : base(0) { }

        public override void Tick(float deltaTime) 
        {
            if (IsRunning) {
                Time += deltaTime;
            }
        }
    
        public void Reset() => Time = initialTime;
    }
}
