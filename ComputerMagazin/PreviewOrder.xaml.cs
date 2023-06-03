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
using System.Windows.Shapes;

namespace ComputerMagazin
{
    /// <summary>
    /// Логика взаимодействия для PreviewOrder.xaml
    /// </summary>
    public partial class PreviewOrder : Window
    {
        public PreviewOrder()
        {
            InitializeComponent();
            DataGridOrder.ItemsSource = ComputerMagazinEntities.GetContext().order.ToList();
        }
        
        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }

        private void CheckConf_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrder.SelectedItem as order;

            if (selectedOrder != null)
            {
               
                InfoAdminConf checkInfoProduct = new InfoAdminConf(selectedOrder);
                checkInfoProduct.Show();
                Close();
            }
        }

        private void CloseOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrder.SelectedItem as order;

            if (selectedOrder != null)
            {
                selectedOrder.data_delivery = DateTime.UtcNow;
                ComputerMagazinEntities.GetContext().SaveChanges();
                DataGridOrder.ItemsSource = ComputerMagazinEntities.GetContext().order.ToList();
                DataGridOrder.SelectedIndex = -1;
            }
        }

        private void OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = DataGridOrder.SelectedItem as order;


            if (selectedOrder != null)
            {
                selectedOrder.data_delivery = null;
                ComputerMagazinEntities.GetContext().SaveChanges();
                DataGridOrder.ItemsSource = ComputerMagazinEntities.GetContext().order.ToList();
                DataGridOrder.SelectedIndex = -1;
            }
        }
    }
}
