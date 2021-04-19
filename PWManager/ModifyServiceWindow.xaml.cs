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
        private PWManagerWCF.Models.service_credentials _serv;

        public ModifyServiceWindow(MainService.MainServiceClient service, PWManagerWCF.Models.service_credentials serv)
        {
            InitializeComponent();

            _service = service;

            service_name.Text = serv.name;
            url.Text = serv.url;
            login.Text = serv.login;
            password.Text = serv.password;

            GetCategoryName(serv.category_id);

            _serv = serv;
        }


        private async void GetCategoryName(short id) 
        {
            string category = await _service.GetCategoryFromIdAsync(id);
            CategoryComboBox.Text = category;
        }

        private async void modify_button_Click(object sender, RoutedEventArgs e)
        {
            await _service.UpdateServiceAsync(_serv.id, service_name.Text, url.Text, login.Text, password.Text, CategoryComboBox.Text);
            Close();
        }
    }
}
