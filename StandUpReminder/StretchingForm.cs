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
            this.BackColor = Color.Transparent;
            this.BackgroundImage = global::StandUpReminder.Properties.Resources.MeditationLotus;
            this.TransparencyKey = Color.Transparent;
            this.Icon = Resources.Stretching;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // StretchingForm
            // 
            this.BackgroundImage = global::StandUpReminder.Properties.Resources.MeditationLotus;
            this.ClientSize = new System.Drawing.Size(1572, 920);
            this.Name = "StretchingForm";
            this.ResumeLayout(false);
            //e.Graphics.DrawImage(Resources.MeditationLotus, 0, 0);

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
    }
}
