
namespace MycraftSync
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.packBox = new System.Windows.Forms.ListBox();
            this.addButton = new System.Windows.Forms.Button();
            this.previewWindow = new System.Windows.Forms.WebBrowser();
            this.actionButton = new System.Windows.Forms.Button();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.configBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.configBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // packBox
            // 
            this.packBox.FormattingEnabled = true;
            this.packBox.Location = new System.Drawing.Point(10, 41);
            this.packBox.Name = "packBox";
            this.packBox.Size = new System.Drawing.Size(199, 277);
            this.packBox.TabIndex = 0;
            this.packBox.SelectedIndexChanged += new System.EventHandler(this.packBox_SelectedIndexChanged);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(10, 322);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(18, 20);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "+";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // previewWindow
            // 
            this.previewWindow.AllowWebBrowserDrop = false;
            this.previewWindow.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.previewWindow.Location = new System.Drawing.Point(240, 41);
            this.previewWindow.MinimumSize = new System.Drawing.Size(20, 20);
            this.previewWindow.Name = "previewWindow";
            this.previewWindow.Size = new System.Drawing.Size(435, 277);
            this.previewWindow.TabIndex = 2;
            // 
            // actionButton
            // 
            this.actionButton.Enabled = false;
            this.actionButton.Location = new System.Drawing.Point(599, 355);
            this.actionButton.Name = "actionButton";
            this.actionButton.Size = new System.Drawing.Size(75, 23);
            this.actionButton.TabIndex = 3;
            this.actionButton.Text = "Placeholder";
            this.actionButton.UseVisualStyleBackColor = true;
            this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.Location = new System.Drawing.Point(518, 355);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(75, 23);
            this.uninstallButton.TabIndex = 4;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Visible = false;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.actionButton);
            this.Controls.Add(this.previewWindow);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.packBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Mycraft Updater";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.configBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button addButton;
        public System.Windows.Forms.ListBox packBox;
        private System.Windows.Forms.BindingSource configBindingSource;
        public System.Windows.Forms.Button actionButton;
        public System.Windows.Forms.Button uninstallButton;
        public System.Windows.Forms.WebBrowser previewWindow;
    }
}