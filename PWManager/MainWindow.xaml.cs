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
using PWManager.Models;

namespace PWManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PWManagerContext context;
        private AddPasswordWindow addPasswordWindow;

        public MainWindow()
        {
            InitializeComponent();

            context = new PWManagerContext();

            LoadItems();
        }

        private void LoadItems()
        {
            grid.ItemsSource = context.ServiceCredentials.Local.ToObservableCollection();
        }

        private void add_password_Click(object sender, RoutedEventArgs e)
        {
            addPasswordWindow = new AddPasswordWindow(context);

            addPasswordWindow.Owner = this;
            addPasswordWindow.ShowDialog();

            LoadItems();
        }
    }
}
