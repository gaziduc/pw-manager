using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PWManager
{
    /// <summary>
    /// Logique d'interaction pour SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private MainService.MainServiceClient _service;

        public SignUpWindow(MainService.MainServiceClient service)
        {
            InitializeComponent();

            _service = service;
        }

        private async void sign_up_Click(object sender, RoutedEventArgs e)
        {
            if (login_textbox.Text.Length == 0)
            {
                MessageBox.Show("Login field cannot be empty.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var strenght = PWManagerWCF.PasswordStrenght.IsValidMasterPassword(password_textbox.Password);

            if (strenght.isTooShort)
                MessageBox.Show("Your password must have more than 12 characters.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (strenght.isMissingUppercaseLetter)
                MessageBox.Show("Your password must have at least one uppercase character.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (strenght.isMissingLowercaseLetter)
                MessageBox.Show("Your password must have at least one lowercase character.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (strenght.isMissingNumber)
                MessageBox.Show("Your password must have at least one digit.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (strenght.isMissingSpecialCharacter)
                MessageBox.Show("Your password must have at least one special character.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                if (await _service.LoginExistsAsync(login_textbox.Text))
                {
                    MessageBox.Show("This login is already used, please choose another one.", "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var res = await _service.CreateUserAsync(login_textbox.Text, password_textbox.Password);
                if (res.Item1 == -1)
                {
                    MessageBox.Show(res.Item2, "PW Manager sign up error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MainWindow mainWindow = new MainWindow(_service, res.Item1);
                mainWindow.Show();

                this.Close();
            }
            
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(password_textbox.Password))
            {
                strenght_progress.Value = 0.0;
                hint_text.Text = "";
                return;
            }

            var result = Zxcvbn.Core.EvaluatePassword(password_textbox.Password);
            strenght_progress.Value = result.Score * 25;

            if (!String.IsNullOrEmpty(result.Feedback.Warning))
                hint_text.Text = "Warning: " + result.Feedback.Warning + "\n";
            else
                hint_text.Text = "";
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow(_service);

            login.Show();

            Close();
        }
    }
}
