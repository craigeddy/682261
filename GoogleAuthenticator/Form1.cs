using System;
using System.Drawing;
using System.IO;
using System.Net;
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
            var setupCode = tfA.GenerateSetupCode("FACTSInfo", "Craig Eddy", "afe84d07aaea4bd299282c3b13a653ee", 
                pictureBox1.Width, pictureBox1.Height);
            WebClient wc = new WebClient();
            MemoryStream ms = new MemoryStream(wc.DownloadData(setupCode.QrCodeSetupImageUrl));
            this.pictureBox1.Image = Image.FromStream(ms);

            this.txtSetupCode.Text = "Account: " + setupCode.Account + System.Environment.NewLine +
                                     "Secret Key: " + setupCode.AccountSecretKey + System.Environment.NewLine +
                                     "Encoded Key: " + setupCode.ManualEntryKey;

        }

        private void ValidateClicked(object sender, EventArgs e)
        {
            TwoFactorAuthenticator tfA = new TwoFactorAuthenticator();
            var result = tfA.ValidateTwoFactorPIN("afe84d07aaea4bd299282c3b13a653ee", this.txtCode.Text);

            MessageBox.Show(result ? "Validated!" : "Incorrect", "Result");
        }
    }
}
