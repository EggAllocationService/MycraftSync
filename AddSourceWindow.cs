using System;
using System.Windows.Forms;

namespace MycraftSync
{
    public partial class AddSourceWindow : Form
    {
        public AddSourceWindow()
        {
            InitializeComponent();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                return;
            }
            Config.AddSource(textBox1.Text);
            
        }
    }
}
