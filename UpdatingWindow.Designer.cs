
namespace MycraftSync
{
    partial class UpdatingWindow
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.statusText = new System.Windows.Forms.Label();
            this.smallBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 50);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(570, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // statusText
            // 
            this.statusText.AutoSize = true;
            this.statusText.Location = new System.Drawing.Point(9, 105);
            this.statusText.Name = "statusText";
            this.statusText.Size = new System.Drawing.Size(78, 13);
            this.statusText.TabIndex = 1;
            this.statusText.Text = "Downloading...";
            // 
            // smallBar
            // 
            this.smallBar.Location = new System.Drawing.Point(12, 79);
            this.smallBar.Name = "smallBar";
            this.smallBar.Size = new System.Drawing.Size(570, 12);
            this.smallBar.TabIndex = 2;
            // 
            // UpdatingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 185);
            this.ControlBox = false;
            this.Controls.Add(this.smallBar);
            this.Controls.Add(this.statusText);
            this.Controls.Add(this.progressBar1);
            this.Name = "UpdatingWindow";
            this.Text = "UpdatingWindow";
            this.Shown += new System.EventHandler(this.UpdatingWindow_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Label statusText;
        public System.Windows.Forms.ProgressBar smallBar;
    }
}