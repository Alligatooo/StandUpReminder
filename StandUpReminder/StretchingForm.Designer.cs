namespace StandUpReminder
{
    partial class StretchingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.flowPlanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flowPanelControls = new System.Windows.Forms.FlowLayoutPanel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.flowPanelButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.ResetTimeButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.stretchingLogicBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.stretchingLogicBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.flowPlanelMain.SuspendLayout();
            this.flowPanelControls.SuspendLayout();
            this.flowPanelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stretchingLogicBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stretchingLogicBindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox.BackColor = System.Drawing.Color.DimGray;
            this.pictureBox.Image = global::StandUpReminder.Properties.Resources.MeditationLotus;
            this.pictureBox.Location = new System.Drawing.Point(3, 3);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(912, 888);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // flowPlanelMain
            // 
            this.flowPlanelMain.AutoSize = true;
            this.flowPlanelMain.BackColor = System.Drawing.Color.Transparent;
            this.flowPlanelMain.Controls.Add(this.pictureBox);
            this.flowPlanelMain.Controls.Add(this.flowPanelControls);
            this.flowPlanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowPlanelMain.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPlanelMain.Location = new System.Drawing.Point(5, 5);
            this.flowPlanelMain.Margin = new System.Windows.Forms.Padding(10);
            this.flowPlanelMain.Name = "flowPlanelMain";
            this.flowPlanelMain.Size = new System.Drawing.Size(920, 975);
            this.flowPlanelMain.TabIndex = 1;
            // 
            // flowPanelControls
            // 
            this.flowPanelControls.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.flowPanelControls.BackColor = System.Drawing.Color.DarkGray;
            this.flowPanelControls.Controls.Add(this.progressBar);
            this.flowPanelControls.Controls.Add(this.flowPanelButtons);
            this.flowPanelControls.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowPanelControls.Location = new System.Drawing.Point(100, 899);
            this.flowPanelControls.Margin = new System.Windows.Forms.Padding(5);
            this.flowPanelControls.Name = "flowPanelControls";
            this.flowPanelControls.Padding = new System.Windows.Forms.Padding(10);
            this.flowPanelControls.Size = new System.Drawing.Size(718, 71);
            this.flowPanelControls.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.progressBar.Location = new System.Drawing.Point(13, 13);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(695, 15);
            this.progressBar.TabIndex = 1;
            // 
            // flowPanelButtons
            // 
            this.flowPanelButtons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowPanelButtons.BackColor = System.Drawing.Color.Transparent;
            this.flowPanelButtons.Controls.Add(this.ResetTimeButton);
            this.flowPanelButtons.Controls.Add(this.closeButton);
            this.flowPanelButtons.Location = new System.Drawing.Point(537, 31);
            this.flowPanelButtons.Margin = new System.Windows.Forms.Padding(0);
            this.flowPanelButtons.Name = "flowPanelButtons";
            this.flowPanelButtons.Size = new System.Drawing.Size(174, 30);
            this.flowPanelButtons.TabIndex = 3;
            // 
            // ResetTimeButton
            // 
            this.ResetTimeButton.Location = new System.Drawing.Point(3, 3);
            this.ResetTimeButton.Name = "ResetTimeButton";
            this.ResetTimeButton.Size = new System.Drawing.Size(80, 25);
            this.ResetTimeButton.TabIndex = 0;
            this.ResetTimeButton.Text = "Reset Time";
            this.ResetTimeButton.UseVisualStyleBackColor = true;
            this.ResetTimeButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(89, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(80, 25);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // stretchingLogicBindingSource
            // 
            this.stretchingLogicBindingSource.DataSource = typeof(StandUpReminder.StretchingLogic);
            // 
            // stretchingLogicBindingSource1
            // 
            this.stretchingLogicBindingSource1.DataSource = typeof(StandUpReminder.StretchingLogic);
            // 
            // StretchingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(930, 985);
            this.Controls.Add(this.flowPlanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StretchingForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "StretchingForm2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.flowPlanelMain.ResumeLayout(false);
            this.flowPlanelMain.PerformLayout();
            this.flowPanelControls.ResumeLayout(false);
            this.flowPanelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stretchingLogicBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stretchingLogicBindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.FlowLayoutPanel flowPlanelMain;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.FlowLayoutPanel flowPanelControls;
        private System.Windows.Forms.FlowLayoutPanel flowPanelButtons;
        private System.Windows.Forms.Button ResetTimeButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.BindingSource stretchingLogicBindingSource;
        private System.Windows.Forms.BindingSource stretchingLogicBindingSource1;
    }
}