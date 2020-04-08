using StandUpReminder.Properties;
using System;
using System.Drawing;
using System.IO;
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
            private NotifyIcon _notifyIcon;
            private ContextMenu _defaultContexMenu;

            private bool _notificationAlert = true;
            private TimerClass _timerClass;

            //Menus
            private MenuItem _settingsMenu;

            private MenuItem _statusMenu;
            private MenuItem _pauseMenu;

            private MenuItem _notificationMenu;

            private int _maxTimerActivity = 3600; //60 min
            private int _maxTimerPause = 300; //5 min
            private int _currentCount = 20;
            private bool _pause = false;
            private MenuItem _timeLeftMenu;

            public StandUpReminderApplicationContext()
            {
                InitMenus();
                _timerClass = TimerClass.Instance;
                _timerClass.TimeEvent += OnTimeEvent;
                StartTask();
            }

            private void InitMenus()
            {
                //Statusmenuitem "Status: Pause"
                _statusMenu = new MenuItem("Status: ")
                {
                    Break = true,
                    DefaultItem = true
                };
                
                //TimeLeftMenu "00:00"
                _timeLeftMenu = new MenuItem();

                //PauseMenu (un-)pauses the timer
                _pauseMenu = new MenuItem(Resources.PauseLabel, OnPausePressed);
                
                //Turn notification on/off
                _notificationMenu = new MenuItem("Notification", OnChangeNotificationClicked) { Checked = true };

                //SettingsMenu containing notificationMenu
                _settingsMenu = new MenuItem("Settings", new MenuItem[]
                {
                    _notificationMenu
                });

                _defaultContexMenu = new ContextMenu(new MenuItem[]
                {
                    _statusMenu,
                    _timeLeftMenu,
                    new MenuItem("-"),
                    new MenuItem("RestartTimer", OnRestartClicked),
                    _pauseMenu,
                    new MenuItem("-"),
                    _settingsMenu,
                    new MenuItem("-"),
                    new MenuItem("Exit", OnExitPressed)
                });

                _defaultContexMenu.Popup += (sender, args) =>
                {
                    _statusMenu.Text = "Status: " + (_pause ? Resources.PauseLabel : "Running");
                };

                _notifyIcon = new NotifyIcon()
                {
                    Icon = Resources.AppIcon,
                    ContextMenu = _defaultContexMenu,
                    Visible = true
                };
            }

            private void OnTimeEvent(object sender, EventArgs e)
            {

                lock (this)
                {
                    _currentCount--;
                    _timeLeftMenu.Text = $@"{(_currentCount/60),0:00}:{(_currentCount % 60),0:00}";

                    if (_currentCount == 0)
                    {
                        if (!_pause)
                        {
                            SendAlert(Resources.ActivityEnd);
                            _currentCount = _maxTimerPause;
                        }
                        else
                        {
                            SendAlert(Resources.ActivityStart);
                            _currentCount = _maxTimerActivity;
                        }
                        _pause = !_pause;
                        return;
                    }

                    //While _pause show "P"
                    if (_pause)
                    {
                        _notifyIcon.Icon =
                            TrayIconLogic.ShowTextWithBorder("P", TrayIconLogic.DefaultTextColor, Color.DeepSkyBlue);
                        return;
                    }

                    //While Activity show time left
                    if (_currentCount > 60)
                    {
                        int count = _currentCount / 60;
                        _notifyIcon.Icon = TrayIconLogic.ShowText(count.ToString());
                    }
                    else
                        _notifyIcon.Icon = TrayIconLogic.ShowTextWithBorder(_currentCount.ToString(), TrayIconLogic.DefaultTextColor, TrayIconLogic.WarningBorderColor);
                }
            }

            private void SendAlert(Stream soundStream)
            {
                if (!_notificationAlert) return;
                new SoundPlayer(soundStream).Play();
            }

            private void OnPausePressed(object sender, EventArgs e)
            {
                if (_pauseMenu.Text.Equals(Resources.PauseLabel))
                {
                    _timerClass.TimeEvent -= OnTimeEvent;
                    _pauseMenu.Text = Resources.UnpauseLabel;
                }
                else
                {
                    _timerClass.TimeEvent += OnTimeEvent;
                    _pauseMenu.Text = Resources.PauseLabel;
                }
            }

            private void OnChangeNotificationClicked(object sender, EventArgs e)
            {
                if (_notificationMenu.Checked)
                {
                    _notificationMenu.Checked = false;
                }
                else
                {
                    _notificationMenu.Checked = true;
                }

                _notificationAlert = _notificationMenu.Checked;
            }

            private void StartTask()
            {
                _timerClass.StartRunning();
            }

            private void OnRestartClicked(object sender, EventArgs e)
            {
                var dialogResult = MessageBox.Show("Do you really want to restart the timer?", "Confirm restart", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _currentCount = _pause ? _maxTimerPause : _maxTimerActivity;
                }
            }

            private void OnExitPressed(object sender, EventArgs eventArgs)
            {
                var dialogResult = MessageBox.Show("Do you really want to exit this application?", "Confirm exit",
                    MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    _notifyIcon.Visible = false;
                    this.Dispose();
                    Application.Exit();
                }
            }
        }
    }
}