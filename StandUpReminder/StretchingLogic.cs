using StandUpReminder.Annotations;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace StandUpReminder
{
    public class StretchingLogic : INotifyPropertyChanged
    {
        public const int MaxWaitTime = 5;
        public const int MaxShowTime = 5;

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
                    Counter = MaxWaitTime;
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
            TimerClass.Instance.TimeEvent += OnTimeEvent;
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
                        Counter = MaxWaitTime;
                        _stretchingForm.Close();
                        currentState = State.WAITING;
                    }
                    else
                    {
                        Counter = MaxShowTime;
                        _stretchingForm = new StretchingForm(this, MaxShowTime);
                        currentState = State.SHOWN;
                        _stretchingForm.Show();
                        _stretchingForm.WindowState = FormWindowState.Normal;
                    }
                }

            }
        }

        public void ResetCounter()
        {
            Counter = currentState == State.WAITING ? MaxWaitTime : MaxShowTime;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}