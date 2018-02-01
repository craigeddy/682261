using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Authenticator;

namespace GoogleAuthenticator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TwoFactorAuthenticator tfA = new TwoFactorAuthenticator();
            var setupCode = tfA.GenerateSetupCode("FACTSInfo", "Craig Eddy", Guid.NewGuid().ToString("N"), 
                pictureBox1.Width, pictureBox1.Height);
            WebClient wc = new WebClient();
            MemoryStream ms = new MemoryStream(wc.DownloadData(setupCode.QrCodeSetupImageUrl));
            this.pictureBox1.Image = Image.FromStream(ms);

            this.txtSetupCode.Text = "Account: " + setupCode.Account + System.Environment.NewLine +
                                     "Secret Key: " + setupCode.AccountSecretKey + System.Environment.NewLine +
                                     "Encoded Key: " + setupCode.ManualEntryKey;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TwoFactorAuthenticator tfA = new TwoFactorAuthenticator();
            var result = tfA.ValidateTwoFactorPIN("f68f1fe894d548a1bbc66165c46e61eb", this.txtCode.Text);

            MessageBox.Show(result ? "Validated!" : "Incorrect", "Result");
        }
    }
}
