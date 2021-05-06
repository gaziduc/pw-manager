using Matrixsoft.PwnedPasswords;
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
    /// Logique d'interaction pour AddPasswordWindow.xaml
    /// </summary>
    public partial class AddPasswordWindow : Window
    {
        private MainService.MainServiceClient _service;
        private long user_id;

        public AddPasswordWindow(MainService.MainServiceClient service, long user_id)
        {
            InitializeComponent();

            _service = service;
            this.user_id = user_id;
        }

        private async void add_button_Click(object sender, RoutedEventArgs e)
        {
            await _service.CreateServiceAsync(service_name.Text, url.Text, login.Text, password_textbox.Password, user_id, CategoryComboBox.Text);

            Close();
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(password_textbox.Password))
            {
                strenght_progress.Width = 0;
                hint_text.Text = "";
                return;
            }

            var result = Zxcvbn.Core.EvaluatePassword(password_textbox.Password);
            strenght_progress.Width = (result.Score * password_textbox.Width) / 4;

            if (result.Score <= 1)
                strenght_progress.Background = Brushes.Red;
            else if (result.Score == 2)
                strenght_progress.Background = Brushes.Orange;
            else if (result.Score == 3)
                strenght_progress.Background = Brushes.GreenYellow;
            else
                strenght_progress.Background = Brushes.Green;

            if (!String.IsNullOrEmpty(result.Feedback.Warning))
                hint_text.Text = "Warning: " + result.Feedback.Warning + "\n";
            else
                hint_text.Text = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var pwd = new PasswordGenerator.Password(includeLowercase: true, includeUppercase: true, includeNumeric: true, includeSpecial: true, passwordLength: 21);
            var password = pwd.Next();
            password_textbox.Password = password;
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var client = new PwnedPasswordsClient();
            var flag = await client.IsPasswordPwnedAsync(password_textbox.Password);
            if (flag)
            {
                MessageBox.Show("Be careful, this password has been compromised in a data breach.\nWe strongly advise you to change it",
                    "Password is compromised", MessageBoxButton.OK, MessageBoxImage.Warning);
            } else
            {
                MessageBox.Show("Your password is not in any of our data breach database",
                    "Password is OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
