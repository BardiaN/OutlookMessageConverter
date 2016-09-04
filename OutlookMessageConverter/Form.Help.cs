using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutlookMessageConverter.InfraStructure;

namespace OutlookMessageConverter
{
    public partial class FormHelp : Form
    {
        public FormHelp()
        {
            InitializeComponent();
        }

        private void FormHelp_Load(object sender, EventArgs e)
        {
            string helpPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "help.txt");
            if (File.Exists(helpPath))
            {
                textBoxHint.Text = File.ReadAllText(helpPath);
                textBoxHint.Select(0, 0);
            }
            checkBoxShowInLoading.Checked = ConfigurationHelper.ShowHintOnLoad;
        }

        private void FormHelp_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigurationHelper.ShowHintOnLoad = checkBoxShowInLoading.Checked;
        }

        private void FormHelp_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                e.Handled = true;
                this.Close();
            }
        }
    }
}
