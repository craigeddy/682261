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
        private const string AccountSecretKey = "afe84d07aaea4bd299282c3b13a653ee";

        public Form1()
        {
            InitializeComponent();
            var tfA = new TwoFactorAuthenticator();
            var setupCode = tfA.GenerateSetupCode(
                "FACTSInfo", 
                "eddycr@state.gov", 
                AccountSecretKey, // persistent, per-user cryptographic key (not shared w/ user)
                false);

            using (var ms = new MemoryStream(
                Convert.FromBase64String(setupCode.QrCodeSetupImageUrl.Replace("data:image/png;base64,", ""))))
            {
                pictureBox1.Image = Image.FromStream(ms);
            }

            // the QR Code and the setupCode.ManualEntryKey are shared with the user so that they can set up Authenticator
            txtSetupCode.Text = $@"Account: {setupCode.Account}{Environment.NewLine}Encoded Key: {setupCode.ManualEntryKey}";
        }

        private void ValidateClicked(object sender, EventArgs e)
        {
            var tfA = new TwoFactorAuthenticator();
            var result = tfA.ValidateTwoFactorPIN(AccountSecretKey, this.txtCode.Text);

            MessageBox.Show(result ? "Validated!" : "Incorrect", "Result");
        }
    }
}
