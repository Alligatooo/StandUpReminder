using System;
using System.Windows.Forms;

namespace StandUpReminder
{
    internal class TimerClass
    {
        public EventHandler<EventArgs> TimeEvent;

        public bool Running
        {
            get
            {
                lock (this)
                {
                    return _timer.Enabled;
                }
            }
        }

        private static TimerClass _instance;
        private readonly Timer _timer;

        public static TimerClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TimerClass();
                }
                return _instance;
            }
        }

        #region Constructor

        private TimerClass()
        {
            _timer = new Timer { Interval = 1000 };
            _timer.Tick += (sender, args) => TimeEvent.Invoke(this, args);
        }

        #endregion Constructor

        public void StartRunning()
        {
            lock (this)
            {
                if (Running) return;
                _timer.Start();
            }
        }

        public void StopRunning()
        {
            lock (this)
            {
                if (!Running) return;
                _timer.Stop();
            }
        }
    }
}