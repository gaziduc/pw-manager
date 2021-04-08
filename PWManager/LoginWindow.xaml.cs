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
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        private async void signin_button_Click(object sender, RoutedEventArgs e)
        {
            string login = login_textbox.Text;
            string password = password_textbox.Text;
            string hash = GetHashString(password);

            if (!await _service.IsCredentialsCorrectAsync(login, hash))
            {
                MessageBox.Show("Incorrect password or login", "PWManager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            MainWindow mainWindow = new MainWindow(_service);
            mainWindow.Show();

            this.Close();
        }
    }
}
