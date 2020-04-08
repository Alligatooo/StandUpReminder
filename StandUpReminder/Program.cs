using StandUpReminder.Properties;
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace StandUpReminder
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StandUpReminderApplicationContext());
        }

        private class StandUpReminderApplicationContext : ApplicationContext
        {
            private NotifyIcon notifyIcon;
            private ContextMenu defaultContexMenu;

            private bool notificationAlert = true;
            private TimerClass _timerClass;

            //Menus
            private MenuItem _settingsMenu;

            private MenuItem _notificationMenu;

            private int maxTimerActivity = 3600; //60 min
            private int maxTimerPause = 300; //5 min
            private int _currentCount = 20;
            private bool pause = false;

            public StandUpReminderApplicationContext()
            {
                initMenus();
                _timerClass = TimerClass.Instance;
                _timerClass.TimeEvent += OnTimeEvent;
                StartTask();
            }

            public void OnTimeEvent(object sender, EventArgs e)
            {
                lock (this)
                {
                    _currentCount--;
                    if (_currentCount == 0)
                    {
                        if (!pause)
                        {
                            sendAlert();
                            _currentCount = maxTimerPause;
                        }
                        else
                        {
                            _currentCount = maxTimerActivity;
                        }
                        pause = !pause;
                        return;
                    }

                    //While pause show "P"
                    if (pause)
                    {
                        notifyIcon.Icon =
                            TrayIconLogic.ShowTextWithBorder("P", TrayIconLogic.DefaultTextColor, Color.DeepSkyBlue);
                        return;
                    }

                    //While Activity show time left
                    if (_currentCount > 60)
                    {
                        int count = _currentCount / 60;
                        notifyIcon.Icon = TrayIconLogic.ShowText(count.ToString());
                    }
                    else if (_currentCount <= 60 && _currentCount != 0)
                    {
                        notifyIcon.Icon = TrayIconLogic.ShowTextWithBorder(_currentCount.ToString(), TrayIconLogic.DefaultTextColor, TrayIconLogic.WarningBorderColor);
                    }
                }
            }

            private void sendAlert()
            {
                bool pause = true;
                if (!notificationAlert) return;
                SystemSounds.Beep.Play();


                
            }
            private void initMenus()
            {
                _notificationMenu = new MenuItem("Notification", ChangeNotification);
                _notificationMenu.Checked = true;

                _settingsMenu = new MenuItem("Settings", new MenuItem[]
                {
                    _notificationMenu
                });

                defaultContexMenu = new ContextMenu(new MenuItem[]
                {
                    new MenuItem("Exit", Exit),
                    new MenuItem("RestartTimer", RestartTimer),
                    _settingsMenu
                });

                notifyIcon = new NotifyIcon()
                {
                    Icon = Resources.AppIcon,
                    ContextMenu = defaultContexMenu,
                    Visible = true
                };
            }

            private void ChangeNotification(object sender, EventArgs e)
            {
                if (_notificationMenu.Checked)
                {
                    _notificationMenu.Checked = false;
                }
                else
                {
                    _notificationMenu.Checked = true;
                }

                notificationAlert = _notificationMenu.Checked;
            }

            private void StartTask()
            {
                _timerClass.StartRunning();
            }

            private void RestartTimer(object sender, EventArgs e)
            {
                var dialogResult = MessageBox.Show("Do you really want to restart the timer?", "Confirm restart", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _currentCount = maxTimerActivity;
                }
            }

            private void Exit(object sender, EventArgs eventArgs)
            {
                var dialogResult = MessageBox.Show("Do you really want to exit this application?", "Confirm exit",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    notifyIcon.Visible = false;
                    Application.Exit();
                }
            }
        }
    }
}