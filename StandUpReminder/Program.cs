using StandUpReminder.Properties;
using System;
using System.Drawing;
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
            private NotifyIcon notifyIcon = null;
            private ContextMenu defaultContexMenu;

            private bool notificationAlert = true;

            //Menus
            private MenuItem _settingsMenu;

            private MenuItem _notificationMenu;

            public StandUpReminderApplicationContext()
            {
                initMenus();
                StartTask();
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
                    new MenuItem("ShowRandomText", ShowRandomText),
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
            }

            private void RestartTimer(object sender, EventArgs e)
            {
                var dialogResult = MessageBox.Show("Do you really want to restart the timer?", "Confirm restart", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                }
            }

            private void ShowRandomText(object sender, EventArgs eventArgs)
            {
                int randomInt = (new Random().Next(1, 99));
                ShowText(randomInt + "", Color.White);
            }

            private void ShowText(string text, Color textColor)
            {
                ShowTextWithBorder(text, textColor, Color.LightGreen);
            }

            private void ShowTextWithBorder(string text, Color textColor, Color borderColor)
            {
                Bitmap bitmap = new Bitmap(16, 16);

                Brush brush = new SolidBrush(textColor);

                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.DrawString(text, new Font("Helvetica", 9), brush, 0, 0);

                Pen pen = new Pen(borderColor, width: 1);
                graphics.DrawRectangle(pen, 0, 0, 16, 16);

                IntPtr hIcon = bitmap.GetHicon();
                Icon icon = Icon.FromHandle(hIcon);
                notifyIcon.Icon.Dispose();
                notifyIcon.Icon = icon;
                bitmap.Dispose();
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