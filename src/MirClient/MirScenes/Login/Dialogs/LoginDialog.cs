using MirClient.MirControls;
using MirClient.MirGraphics;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MirClient.MirScenes.Login
{
    public sealed class LoginDialog : MirImageControl
    {
        public MirImageControl TitleLabel, AccountIDLabel, PassLabel;
        public MirButton AccountButton, CloseButton, OKButton, PassButton, ViewKeyButton;
        public MirTextBox AccountIDTextBox, PasswordTextBox;
        private bool _accountIDValid, _passwordValid;

        public LoginDialog()
        {
            Index = 60;
            Library = Libraries.Prguse;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            PixelDetect = false;
            Size = new Size(328, 220);

            TitleLabel = new MirImageControl
            {
                Index = 30,
                Library = Libraries.Title,
                Parent = this,
            };
            TitleLabel.Location = new Point((Size.Width - TitleLabel.Size.Width) / 2, 12);

            AccountIDLabel = new MirImageControl
            {
                Index = 31,
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(52, 83),
            };

            PassLabel = new MirImageControl
            {
                Index = 32,
                Library = Libraries.Title,
                Parent = this,
                Location = new Point(43, 105)
            };

            OKButton = new MirButton
            {
                Enabled = false,
                Size = new Size(42, 42),
                HoverIndex = 321,
                Index = 62,
                Library = Libraries.Prguse,
                Location = new Point(227, 81),
                Parent = this,
                PressedIndex = 322
            };
            OKButton.Click += (o, e) => Login();

            AccountButton = new MirButton
            {
                HoverIndex = 324,
                Index = 61,
                Library = Libraries.Prguse,
                Location = new Point(60, 163),
                Parent = this,
                PressedIndex = 325,
            };

            PassButton = new MirButton
            {
                HoverIndex = 327,
                Index = 326,
                Library = Libraries.Title,
                Location = new Point(166, 163),
                Parent = this,
                PressedIndex = 328,
            };

            ViewKeyButton = new MirButton
            {
                HoverIndex = 333,
                Index = 332,
                Library = Libraries.Title,
                Location = new Point(60, 189),
                Parent = this,
                PressedIndex = 334,
            };

            CloseButton = new MirButton
            {
                HoverIndex = 330,
                Index = 64,
                Library = Libraries.Prguse,
                Location = new Point(28, 252),
                Parent = this,
                PressedIndex = 331
            };
            CloseButton.Click += (o, e) => Program.Form.Close();

            PasswordTextBox = new MirTextBox
            {
                Location = new Point(97, 115),
                Parent = this,
                Password = true,
                Size = new Size(136, 15),
                MaxLength = Globals.MaxPasswordLength
            };

            PasswordTextBox.TextBox.TextChanged += PasswordTextBox_TextChanged;
            PasswordTextBox.TextBox.KeyPress += TextBox_KeyPress;
            PasswordTextBox.Text = Settings.Password;

            AccountIDTextBox = new MirTextBox
            {
                Location = new Point(97, 86),
                Parent = this,
                Size = new Size(136, 15),
                MaxLength = Globals.MaxAccountIDLength
            };

            AccountIDTextBox.TextBox.TextChanged += AccountIDTextBox_TextChanged;
            AccountIDTextBox.TextBox.KeyPress += TextBox_KeyPress;
            AccountIDTextBox.Text = Settings.AccountID;
        }

        private void AccountIDTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^[A-Za-z0-9]{" + Globals.MinAccountIDLength + "," + Globals.MaxAccountIDLength + "}$");

            if (string.IsNullOrEmpty(AccountIDTextBox.Text) || !reg.IsMatch(AccountIDTextBox.TextBox.Text))
            {
                _accountIDValid = false;
                AccountIDTextBox.Border = !string.IsNullOrEmpty(AccountIDTextBox.Text);
                AccountIDTextBox.BorderColour = Color.Red;
            }
            else
            {
                _accountIDValid = true;
                AccountIDTextBox.Border = true;
                AccountIDTextBox.BorderColour = Color.Green;
            }
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            Regex reg = new Regex(@"^[A-Za-z0-9]{" + Globals.MinPasswordLength + "," + Globals.MaxPasswordLength + "}$");

            if (string.IsNullOrEmpty(PasswordTextBox.TextBox.Text) || !reg.IsMatch(PasswordTextBox.TextBox.Text))
            {
                _passwordValid = false;
                PasswordTextBox.Border = !string.IsNullOrEmpty(PasswordTextBox.TextBox.Text);
                PasswordTextBox.BorderColour = Color.Red;
            }
            else
            {
                _passwordValid = true;
                PasswordTextBox.Border = true;
                PasswordTextBox.BorderColour = Color.Green;
            }

            RefreshLoginButton();
        }

        public void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender == null || e.KeyChar != (char)Keys.Enter) return;

            e.Handled = true;

            if (!_accountIDValid)
            {
                AccountIDTextBox.SetFocus();
                return;
            }
            if (!_passwordValid)
            {
                PasswordTextBox.SetFocus();
                return;
            }

            if (OKButton.Enabled)
                OKButton.InvokeMouseClick(null);
        }

        private void RefreshLoginButton()
        {
            OKButton.Enabled = _accountIDValid && _passwordValid;
        }

        private void Login()
        {
            OKButton.Enabled = false;
        }

        public override void Show()
        {
            if (Visible) return;
            Visible = true;
            AccountIDTextBox.SetFocus();

            if (Settings.Password != string.Empty && Settings.AccountID != string.Empty)
            {
                Login();
            }
        }

        public void Clear()
        {
            AccountIDTextBox.Text = string.Empty;
            PasswordTextBox.Text = string.Empty;
        }

        #region Disposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                TitleLabel = null;
                AccountIDLabel = null;
                PassLabel = null;
                AccountButton = null;
                CloseButton = null;
                OKButton = null;
                PassButton = null;
                AccountIDTextBox = null;
                PasswordTextBox = null;

            }

            base.Dispose(disposing);
        }

        #endregion
    }
}