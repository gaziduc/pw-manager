using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private MainService.MainServiceClient _service;

        public LoginWindow()
        {
            InitializeComponent();

            _service = new MainService.MainServiceClient();

            login_combo_box.ItemsSource = _service.GetAllUsersLoginAsync().Result;
        }


        private async void signin_button_Click(object sender, RoutedEventArgs e)
        {
            string login = login_combo_box.Text;
            string password = password_textbox.Password;
            string hash = PWManagerWCF.Cryptography.GetHashString(password);

            long user_id = await _service.GetUserFromCrdentialsAsync(login, hash);
            if (user_id == -1)
            {
                MessageBox.Show("Incorrect login or password.", "PW Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            MainWindow mainWindow = new MainWindow(_service, user_id);
            mainWindow.Show();

            Close();
        }

        private void signup_button_Click(object sender, RoutedEventArgs e)
        {
            SignUpWindow signUpWindow = new SignUpWindow(_service);

            signUpWindow.Show();

            Close();
        }
    }
}
