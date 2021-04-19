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
        }
    }
}
