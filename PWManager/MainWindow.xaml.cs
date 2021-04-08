using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PWManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private AddPasswordWindow addPasswordWindow;
        private MainService.MainServiceClient _service;

        public MainWindow(MainService.MainServiceClient service)
        {
            InitializeComponent();
            this._service = service;

            LoadItems();
        }

        private async void LoadItems()
        {
            var tmp = await _service.GetAllServiceCredentialsAsync();
            grid.ItemsSource = tmp;
        }

        private void add_password_Click(object sender, RoutedEventArgs e)
        {
            addPasswordWindow = new AddPasswordWindow(_service);

            addPasswordWindow.Owner = Window.GetWindow(this);
            addPasswordWindow.ShowDialog();

            LoadItems();
        }
    }
}
