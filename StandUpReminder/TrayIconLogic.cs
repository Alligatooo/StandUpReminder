using System;
using System.Drawing;

namespace StandUpReminder
{
    internal class TrayIconLogic
    {
        public static Color DefaultBorderColor = Color.LawnGreen;

        public static Color DefaultTextColor = Color.White;
        public static Color WarningBorderColor = Color.Red;

        public static Icon ShowRandomText()
        {
            int randomInt = (new Random().Next(1, 99));
            return ShowText(randomInt + "", DefaultTextColor);
        }

        public static Icon ShowText(string text)
        {
            return ShowText(text, DefaultTextColor);
        }

        public static Icon ShowText(string text, Color textColor)
        {
            return ShowTextWithBorder(text, textColor, DefaultBorderColor);
        }

        public static Icon ShowTextWithBorder(string text, Color textColor, Color borderColor)
        {
            if (text.Length == 1)
            {
                text = " " + text;
            }
            Bitmap bitmap = new Bitmap(16, 16);

            Brush brush = new SolidBrush(textColor);

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawString(text, new Font("Helvetica", 9), brush, 0, 0);

            Pen pen = new Pen(borderColor, width: 1);
            graphics.DrawRectangle(pen, 0, 0, 16, 16);

            IntPtr hIcon = bitmap.GetHicon();
            return Icon.FromHandle(hIcon);
        }
    }
}