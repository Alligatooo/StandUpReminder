using System.Drawing;
using System.Windows.Forms;
using StandUpReminder.Properties;

namespace StandUpReminder
{
    class StretchingForm : Form
    {
        public StretchingForm()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.BackColor = Color.Transparent;
            this.TransparencyKey = Color.Transparent;
            this.Icon = Resources.Stretching;
            

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StretchingForm
            // 
            this.Name = "StretchingForm";
            this.ResumeLayout(false);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.DrawImage(Resources.MeditationLotus, (this.Width/2-Resources.MeditationLotus.Width/2), (this.Height/2-Resources.MeditationLotus.Height/2));
        }
    }
}
