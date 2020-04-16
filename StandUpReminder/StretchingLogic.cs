using StandUpReminder.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using StandUpReminder.Properties;

namespace StandUpReminder
{
    public class StretchingLogic : INotifyPropertyChanged
    {

        private bool _enabled;

        private int _counter;

        public int Counter
        {
            get { return _counter; }
            set
            {
                _counter = value;
                OnPropertyChanged();
            }
        }

        private State currentState = State.WAITING;
        private StretchingForm _stretchingForm;

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (value)
                {
                    Counter = Settings.Default.StretchingIdeDuration;
                    TimerClass.Instance.TimeEvent += OnTimeEvent;
                }
                else
                {
                    TimerClass.Instance.TimeEvent -= OnTimeEvent;
                }

                _enabled = value;
            }
        }

        public StretchingLogic()
        {
            Counter = 5;
        }

        private void OnTimeEvent(object sender, EventArgs e)
        {
            lock (this)
            {
                Counter--;
                if (Counter == 0)
                {
                    if (currentState == State.SHOWN)
                    {
                        _stretchingForm.Close();
                        currentState = State.WAITING;
                        Counter = Settings.Default.StretchingIdeDuration;
                    }
                    else
                    {
                        Counter = Settings.Default.StretchingShowDuration;
                        _stretchingForm = new StretchingForm(this);
                        currentState = State.SHOWN;
                        _stretchingForm.Show();
                        _stretchingForm.WindowState = FormWindowState.Normal;
                    }
                }

            }
        }

        public void ResetCounter()
        {
            Counter = currentState == State.WAITING ? Settings.Default.StretchingIdeDuration : Settings.Default.StretchingShowDuration;
        }

        private enum State
        {
            WAITING,
            SHOWN
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (propertyName == "Counter" && currentState == State.WAITING)
                return;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}