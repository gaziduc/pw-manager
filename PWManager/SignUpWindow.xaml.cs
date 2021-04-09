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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (await _service.LoginExistsAsync(login_textbox.Text))
            {
                MessageBox.Show("Sorry, this login is already use, choose another one", "PW Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (login_textbox.Text.Length == 0 || password_textbox.Password.Length == 0 || password_confirmation_textbox.Password.Length == 0)
            {
                MessageBox.Show("One of the field is empty, please verify all the fiels", "PW Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            if (!password_textbox.Password.Equals(password_confirmation_textbox.Password))
            {
                MessageBox.Show("The passwords are not the same", "PW Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!PWManagerWCF.Password.IsValid(password_textbox.Password))
            {
                MessageBox.Show("Your password has to follow the CNIL specification", "PW Manager", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            await _service.CreateUserAsync(login_textbox.Text, password_textbox.Password);

            MainWindow mainWindow = new MainWindow(_service);
            mainWindow.Show();

            this.Close();
        }
    }
}
