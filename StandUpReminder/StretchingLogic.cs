using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StandUpReminder
{
    class StretchingLogic
    {
        public const int MaxTime = 900;

        private bool _enabled;
        private int _counter;

        public bool Enabled
        {
            get { return _enabled;}
            set {
                if (value)
                {
                    _counter = MaxTime;
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
            _counter = 10;
            TimerClass.Instance.TimeEvent += OnTimeEvent;
        }

        private void OnTimeEvent(object sender, EventArgs e)
        {
            lock (this)
            {
                _counter--;
                if (_counter == 0)
                {
                    StretchingForm stretchingForm = new StretchingForm();
                    stretchingForm.Show();
                    stretchingForm.WindowState = FormWindowState.Normal;
                }
            }    
        }
    }
}
