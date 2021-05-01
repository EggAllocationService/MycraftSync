using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MycraftSync
{
    
    public partial class MainWindow : Form
    {
        public static MainWindow instance;
        public MainWindow()
        {
            instance = this;
            InitializeComponent();
            Config.RefreshBox();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            new AddSourceWindow().ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {

        }

        private async void packBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.uninstallButton.Visible = false;
            Utils.Source s = Config.GetSource((string)packBox.SelectedItem);
            if (s == null) return;
            this.actionButton.Enabled = true;
            previewWindow.Url = new Uri(s.baseURL + "info.html");
            if (await Config.UpdateNeeded(s.name))
            {
                // there is an update needed
                this.actionButton.Text = "Update";
                
            } else
            {
                if (Config.IsInstalled(s.name))
                {
                    this.actionButton.Enabled = false;
                    this.actionButton.Text = "Installed";
                    this.uninstallButton.Visible = true;
                } else
                {
                    // not selected
                    this.actionButton.Text = "Install";
                    this.uninstallButton.Visible = true;
                }
            }
        }

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            Config.Save();
            Application.Exit();
        }

        private void actionButton_Click(object sender, EventArgs e)
        {
            if (actionButton.Text == "Update")
            {
                // update time!
                Utils.Source s = Config.GetSource((string)packBox.SelectedItem);
                if (s == null) return;
                UpdatingWindow w = new UpdatingWindow(s);
                w.ShowDialog();
                Config.RefreshBox();
                // done updaterning
            } else if (actionButton.Text == "Install")
            {
                var name = (string)packBox.SelectedItem;
                Utils.LauncherProfile p = new Utils.LauncherProfile();
                p.icon = "Emerald_Block";
                p.gameDir = System.IO.Path.Combine(Config.rootDir, name);
                p.lastUsed = JsonConvert.SerializeObject(DateTime.UtcNow);
                p.created = p.lastUsed;
                p.name = name;
                p.lastVersionId = "1.16.5";
                p.type = "custom";
                Utils.MinecraftLauncher.cached.profiles.Remove(name);
                Utils.MinecraftLauncher.cached.profiles.Add(name, p);
                Utils.MinecraftLauncher.Save();
                actionButton.Text = "Installed";
                actionButton.Enabled = false;
            } else if (actionButton.Text == "Placeholder")
            {
                MessageBox.Show("You're a fast fucker arent you?", "Bruh", MessageBoxButtons.OK);
            }

           
        }

        private void uninstallButton_Click(object sender, EventArgs e)
        {

           

            var s = (string)packBox.SelectedItem;
            if (s == null) return;
            if (MessageBox.Show("Are you sure you want to delete " + s + "?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }
            
            Utils.MinecraftLauncher.cached.profiles.Remove(s);
            System.IO.Directory.Delete(System.IO.Path.Combine(Config.rootDir, s), true);
            Config.RefreshBox();

        }
    }
}
