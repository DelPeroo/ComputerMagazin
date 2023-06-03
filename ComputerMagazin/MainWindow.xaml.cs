using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerMagazin
{
    public partial class MainWindow : Window
    {
        public MainWindow(string login)
        {
            InitializeComponent();
            loginSave.Text = login;
            ListViewProduct.ItemsSource = ComputerMagazinEntities.GetContext().pc_features_default.ToList();
            FilterCategory.ItemsSource = ComputerMagazinEntities.GetContext().pc_category.ToList();
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckProduct_OnClick(object sender, RoutedEventArgs e)
        {
            ListViewProduct.Visibility = Visibility.Visible;
            CheckOrderDetails.Visibility = Visibility.Visible;
            CheckInfoProduct.Visibility = Visibility.Visible;
            FilterCategory.Visibility = Visibility.Visible;
            DataGridOrder.Visibility = Visibility.Hidden;
            PreviewOrder.Visibility = Visibility.Hidden;
        }

        private void CheckInfoProduct_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ListViewProduct.SelectedItem as pc_features_default;
            if (ListViewProduct.SelectedItem != null)
            {
                CheckInfoProduct edit = new CheckInfoProduct(selectedProduct, loginSave.Text);
                edit.Show();
                Close();
                ListViewProduct.SelectedIndex = -1;
            }
        }

        private void CreateConfiguration_OnClick(object sender, RoutedEventArgs e)
        {
            NewConfigurationPC newConfigurationPc = new NewConfigurationPC(loginSave.Text);
            newConfigurationPc.Show();
            Close();
        }

        private void CheckOrderDetails_OnClick(object sender, RoutedEventArgs e)
        {
            ListViewProduct.Visibility = Visibility.Hidden;
            CheckOrderDetails.Visibility = Visibility.Hidden;
            CheckInfoProduct.Visibility = Visibility.Hidden;
            FilterCategory.Visibility = Visibility.Hidden;
            DataGridOrder.Visibility = Visibility.Visible;
            PreviewOrder.Visibility = Visibility.Visible;
            
            var currentIndent = ComputerMagazinEntities.GetContext().order.Where(p => p.login == loginSave.Text)
                .ToList();
            DataGridOrder.ItemsSource = currentIndent;
        }

        private void PreviewOrder_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrder.SelectedItem as order;
            if (selectedOrder != null)
            {
                var filePath = @"C:\Users\evlad\OneDrive\Рабочий стол\ComputerMagazinSource\Заказ ПК " +
                               selectedOrder.id_order + ".docx";
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                    DataGridOrder.SelectedIndex = -1;
                }
                else
                    MessageBox.Show("Чек не найден обратитесь в тех.поддержку");
            }
            else
                MessageBox.Show("Выберите заказ");
        }

        private void FilterCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = FilterCategory.SelectedItem as pc_category;
            if (selectedFilter != null)
            {
                if (selectedFilter.id_category == 1)
                    ListViewProduct.ItemsSource = ComputerMagazinEntities.GetContext().pc_features_default
                        .Where(x => x.id_category == 1).ToList();
                if (selectedFilter.id_category == 2)
                    ListViewProduct.ItemsSource = ComputerMagazinEntities.GetContext().pc_features_default
                        .Where(x => x.id_category == 2).ToList();
                if (selectedFilter.id_category == 3)
                    ListViewProduct.ItemsSource = ComputerMagazinEntities.GetContext().pc_features_default
                        .Where(x => x.id_category == 3).ToList();
                ReturnFilter.Visibility = Visibility.Visible;
            }
        }

        private void ReturnFilter_OnClick(object sender, RoutedEventArgs e)
        {
            ListViewProduct.ItemsSource = ComputerMagazinEntities.GetContext().pc_features_default.ToList();
            FilterCategory.SelectedIndex = -1;
            ReturnFilter.Visibility = Visibility.Hidden;
        }
    }
}