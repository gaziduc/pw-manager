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
    /// Logique d'interaction pour ModifyServiceWindow.xaml
    /// </summary>
    public partial class ModifyServiceWindow : Window
    {
        private MainService.MainServiceClient _service;
        private Models.DataGridView _serv;

        public ModifyServiceWindow(MainService.MainServiceClient service, Models.DataGridView serv)
        {
            InitializeComponent();

            _service = service;

            service_name.Text = serv.name;
            url.Text = serv.url;
            login.Text = serv.login;
            password_textbox.Password = serv.password;

            CategoryComboBox.Text = serv.category_name;

            _serv = serv;
        }


        private async void GetCategoryName(short id) 
        {
            string category = await _service.GetCategoryFromIdAsync(id);
            CategoryComboBox.Text = category;
        }

        private async void modify_button_Click(object sender, RoutedEventArgs e)
        {
            await _service.UpdateServiceAsync(_serv.id, service_name.Text, url.Text, login.Text, password_textbox.Password, CategoryComboBox.Text, _serv.user_id);
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
            }
            else
            {
                MessageBox.Show("Your password is not in any of our data breach database",
                    "Password is OK", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
