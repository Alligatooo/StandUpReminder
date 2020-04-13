using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using StandUpReminder.Properties;

namespace StandUpReminder
{
    public partial class StretchingForm : Form
    {
        private StretchingLogic _strechtingLogic;

        public StretchingForm()
        {
            InitializeComponent();
            //BackgroundColor

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.TransparencyKey = Color.Transparent;


            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            pictureBox.Region = new Region(gp);
            pictureBox.BackColor = Color.DarkGray;
            this.Icon = Resources.Stretching;



            this.CenterToScreen();
            //this.Controls.Add(pictureBox1);
        }

        public StretchingForm(StretchingLogic stretchingLogic)
        {
            stretchingLogic.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName.Equals("Counter"))
                {
                    int counter = ((StretchingLogic) sender).Counter;

                    Debug.WriteLine(progressBar.Maximum - counter);
                    progressBar.Value = progressBar.Maximum - counter;
                }
            };
            InitializeComponent();
            //BackgroundColor

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.TransparencyKey = Color.Transparent;


            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            pictureBox.Region = new Region(gp);
            pictureBox.BackColor = Color.DarkGray;
            this.Icon = Resources.Stretching;

            this._strechtingLogic = stretchingLogic;
            progressBar.Maximum = Settings.Default.StretchingShowDuration;


            this.CenterToScreen();
        }


        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _strechtingLogic.ResetCounter();
            progressBar.Value = 0;
        }
    }
}
