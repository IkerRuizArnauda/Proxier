using System;
using System.Windows.Forms;

using Proxier.Core;

namespace Proxier.Configuration
{
    public partial class ConfigurationForm : Form
    {
        public string IPAddress { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ConfigurationForm(ProxyServer current)
        {
            InitializeComponent();

            this.TxtIpAddress.Text = current.IPAddress;
            this.TxtPort.Text = current.Port.ToString();
            this.TxtUsername.Text = current.Username.ToString();
            this.TxtPassword.Text = current.Password.ToString();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress = TxtIpAddress.Text;
                Username = TxtUsername.Text;
                Password = TxtPassword.Text;
                Port = Convert.ToInt32(TxtPort.Text);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                DialogResult = DialogResult.None;
            }
        }
    }
}
