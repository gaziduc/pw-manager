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
using PWManager.Models;

namespace PWManager
{
    /// <summary>
    /// Logique d'interaction pour AddPasswordWindow.xaml
    /// </summary>
    public partial class AddPasswordWindow : Window
    {
        private PWManagerContext context;

        public AddPasswordWindow(PWManagerContext context)
        {
            InitializeComponent();

            this.context = context;
        }

        private async void add_button_Click(object sender, RoutedEventArgs e)
        {
            ServiceCredential entity = new ServiceCredential
            {
                Name = service_name.Text,
                Url = url.Text,
                Login = login.Text,
                Password = password.Text,
                UserId = 1,
                CategoryId = 1
            };

            context.ServiceCredentials.Add(entity);
            await context.SaveChangesAsync();

            Close();
        }
    }
}
