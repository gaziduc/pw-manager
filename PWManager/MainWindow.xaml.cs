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


        private async void LoadItems(string search = "", long id = -1)
        {
            var list = await _service.GetAllServiceCredentialsAsync(user_id);

            var sorted_list = list.Where(x => x.name.ToLower().Contains(search.ToLower())).OrderBy(e => e.is_favorite ? 0 : 1).ToList();

            /*List<PWManagerWCF.Models.service_credentials> final_list;
            if (!CategoryComboBox.Text.Equals("All"))
            {
                Console.WriteLine(_service.GetCategoryFromIdAsync(sorted_list.First().category_id).Result.ToString());
                Console.WriteLine(CategoryComboBox.Text);
                final_list = sorted_list.Where(x =>
                {
                    string cat =  _service.GetCategoryFromIdAsync(x.category_id);

                };
            } else
            {
                final_list = sorted_list.ToList();
            }*/

            List<Models.DataGridView> data = new List<Models.DataGridView>();

            foreach (var elm in sorted_list)
            {
                Models.DataGridView model = new Models.DataGridView();
                model.id = elm.id;
                model.name = elm.name;
                model.url = elm.url;
                model.login = elm.login;
                model.password = elm.password;

                if (id != -1 && elm.id == id)
                    model.hidden_password = elm.password;
                else
                    model.hidden_password = new String('\u2022', model.password.Length);

                model.is_favorite = elm.is_favorite;
                model.user_id = elm.user_id;
                string cat = await _service.GetCategoryFromIdAsync(elm.category_id);
                model.category_name = cat;
                data.Add(model);
            }

            if (!CategoryComboBox.Text.Equals("All"))
            {
                data = data.Where(x => x.category_name.Equals(CategoryComboBox.Text)).ToList();
            }

            grid.ItemsSource = data;
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
            Models.DataGridView serv = (Models.DataGridView)((Button)e.Source).DataContext;
            await _service.ChangeFavoriteStatusAsync(serv.id, serv.is_favorite);
            LoadItems();
        }

        private void OnURLClick(object sender, RoutedEventArgs e)
        {
            Hyperlink link = (Hyperlink)e.OriginalSource;

            if (link.NavigateUri.IsAbsoluteUri)
            {
                var process = new ProcessStartInfo(link.NavigateUri.AbsoluteUri);
                process.UseShellExecute = true;
                Process.Start(process);
            }
            else
                MessageBox.Show(link.NavigateUri.ToString() + " is not a valid absolute URL.", "PW Manager error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadItems(SearchText.Text);
        }

        private void modify_button_Click(object sender, RoutedEventArgs e)
        {
            Models.DataGridView serv = (Models.DataGridView)((Button)e.Source).DataContext;

            ModifyServiceWindow modify = new ModifyServiceWindow(_service, serv);
            modify.Owner = Window.GetWindow(this);
            modify.ShowDialog();

            LoadItems();
        }

        private async void delete_button_Click(object sender, RoutedEventArgs e)
        {
            Models.DataGridView serv = (Models.DataGridView) ((Button) e.Source).DataContext;

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the following service:\n" + serv.name, "Delete service?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                await _service.DeleteServiceAsync(serv.id);
                LoadItems();
            }
        }

        private void SearchText_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadItems(SearchText.Text);
        }

        private void show_pass_button_Click(object sender, RoutedEventArgs e)
        {
            Models.DataGridView serv = (Models.DataGridView)((Button)e.Source).DataContext;

            LoadItems(SearchText.Text, serv.id);
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchText != null)
                LoadItems(SearchText.Text);
        }
    }
}
