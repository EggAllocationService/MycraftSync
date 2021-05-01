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
    public partial class UpdatingWindow : Form
    {
        public Utils.Source pack;
        public static UpdatingWindow instance;
        public UpdatingWindow(Utils.Source pack)
        {
            InitializeComponent();
            this.pack = pack;
            instance = this;
        }

        private async void UpdatingWindow_Shown(object sender, EventArgs e)
        {
            await Utils.Updater.Update(pack);
            MessageBox.Show("Update complete!", "Message",  MessageBoxButtons.OK);
            instance = null;
            Close();

        }
    }
}
