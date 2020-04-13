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
            private ContextMenu _defaultContextMenu;

            private readonly TimerClass _timerClass;

            //Menus

            private MenuItem _statusMenu;
            private MenuItem _timeLeftMenu;

            private MenuItem _pauseMenu;

            private MenuItem _symbolMenu;
            private MenuItem _notificationMenu;
            private MenuItem _settingsMenu;

            private int _currentCount = 20;
            private bool _pause;

            public StandUpReminderApplicationContext()
            {
                InitMenus();

                //ExitHandler
                Application.ApplicationExit += (s, e) =>
                {
                    this._notifyIcon.Visible = false;
                    this._notifyIcon.Dispose();
                };
                _timerClass = TimerClass.Instance;
                _timerClass.TimeEvent += OnTimeEvent;
                StretchingLogic stretchingLogic = new StretchingLogic();
                StartTask();
            }

            private void InitMenus()
            {
                //Statusmenuitem "Status: Pause"
                _statusMenu = new MenuItem(Resources.StatusLabel)
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

                _symbolMenu = new MenuItem("Show time", OnSymbolChange) { Checked = true };

                //SettingsMenu containing notificationMenu
                _settingsMenu = new MenuItem("Settings", new[]
                {
                    _notificationMenu,
                    _symbolMenu
                });

                _defaultContextMenu = new ContextMenu(new[]
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

                _defaultContextMenu.Popup += (sender, args) =>
                {
                    _statusMenu.Text = Resources.StatusLabel + (_pause ? Resources.PauseLabel : "Running");
                };

                _notifyIcon = new NotifyIcon()
                {
                    Icon = Resources.AppIcon,
                    ContextMenu = _defaultContextMenu,
                    Visible = true
                };
            }

            private void OnSymbolChange(object sender, EventArgs e)
            {
                _symbolMenu.Text = _symbolMenu.Checked ? "Show icon" : "Show time";
                _symbolMenu.Checked = !_symbolMenu.Checked;
                Settings.Default.ShowTimerTray = _symbolMenu.Checked;
                Settings.Default.Save();
            }

            private void OnTimeEvent(object sender, EventArgs e)
            {
                lock (this)
                {
                    _currentCount--;
                    _timeLeftMenu.Text = $@"{(_currentCount / 60),0:00}:{(_currentCount % 60),0:00}";

                    if (_currentCount == 0)
                    {
                        if (!_pause)
                        {
                            SendAlert(Resources.ActivityEnd);
                            _currentCount = Settings.Default.PauseDuration;
                        }
                        else
                        {
                            SendAlert(Resources.ActivityStart);
                            _currentCount = Settings.Default.StandupIdleTime;
                        }
                        _pause = !_pause;
                        return;
                    }

                    if (Settings.Default.ShowTimerTray)
                    {
                        //While _pause show "P"
                        if (_pause)
                        {
                            _notifyIcon.Icon =
                                TrayIconLogic.ShowTextWithBorder("P", TrayIconLogic.DefaultTextColor,
                                    Color.DeepSkyBlue);
                            return;
                        }

                        //While Activity show time left
                        if (_currentCount > 60)
                        {
                            int count = _currentCount / 60;
                            _notifyIcon.Icon = TrayIconLogic.ShowText(count.ToString());
                        }
                        else
                            _notifyIcon.Icon = TrayIconLogic.ShowTextWithBorder(_currentCount.ToString(),
                                TrayIconLogic.DefaultTextColor, TrayIconLogic.WarningBorderColor);
                    }
                    else
                    {
                        _notifyIcon.Icon = Resources.Stretching;
                    }
                }
            }

            private void SendAlert(Stream soundStream)
            {
                if (!Settings.Default.NotifactionAlertEnabled) return;
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

                Settings.Default.NotifactionAlertEnabled = _notificationMenu.Checked;
                Settings.Default.Save();
            }

            private void StartTask()
            {
                _timerClass.StartRunning();
            }

            private void OnRestartClicked(object sender, EventArgs e)
            {
                var dialogResult = MessageBox.Show(Resources.RestartQuestionMessageBox, Resources.RestartCaptionMessageBox, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _currentCount = _pause ? Settings.Default.PauseDuration : Settings.Default.StandupIdleTime;
                }
            }

            private void OnExitPressed(object sender, EventArgs eventArgs)
            {
                var dialogResult = MessageBox.Show(Resources.ExitQuestionMessageBox, Resources.ExitCaptionMessageBox,
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