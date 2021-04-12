using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        private async void favorite_button_Click(object sender, RoutedEventArgs e)
        {
            PWManagerWCF.Models.service_credentials serv = (PWManagerWCF.Models.service_credentials)((Button)e.Source).DataContext;
            await _service.ChangeFavoriteStatusAsync(serv.id, serv.is_favorite);
            LoadItems();
            /*var dc = (sender as Button);
            var children = LogicalTreeHelper.GetChildren(dc);
            foreach (var child in children)
            {
                var srcImg = ((Image)child);
                if (serv.is_favorite)
                {
                    srcImg.Source = this.img_favorite_empty;
                } else
                {
                    srcImg.Source = this.img_favorite_full;
                }
                break;
            }*/
        }

        private void OnURLClick(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink) e.OriginalSource;

            if (link.NavigateUri.IsAbsoluteUri)
            {
                var process = new ProcessStartInfo(link.NavigateUri.AbsoluteUri);
                process.UseShellExecute = true;
                Process.Start(process);
            }
            else
                MessageBox.Show(link.NavigateUri.ToString() + " is not a valid absolute URL.", "PW Manager error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
