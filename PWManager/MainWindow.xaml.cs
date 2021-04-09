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
        private long user_id;

        public MainWindow(MainService.MainServiceClient service, long user_id)
        {
            InitializeComponent();
            this._service = service;

            this.user_id = user_id;

            LoadItems();
        }

        private async void LoadItems()
        {
            var tmp = await _service.GetAllServiceCredentialsAsync(user_id);
            grid.ItemsSource = tmp;
        }

        private void add_password_Click(object sender, RoutedEventArgs e)
        {
            addPasswordWindow = new AddPasswordWindow(_service, user_id);

            addPasswordWindow.Owner = Window.GetWindow(this);
            addPasswordWindow.ShowDialog();

            LoadItems();
        }
    }
}
