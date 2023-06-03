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
    /// Логика взаимодействия для InfoAdminConf.xaml
    /// </summary>
    public partial class InfoAdminConf : Window
    {
        public InfoAdminConf(order selectedProduct)
        {
            InitializeComponent();
            if (selectedProduct != null)
                selectedProductRepeat = selectedProduct.pc_features_personal;

            DataContext = selectedProductRepeat;
        }

        private pc_features_personal selectedProductRepeat = new pc_features_personal();

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            PreviewOrder previewOrder = new PreviewOrder();
            previewOrder.Show();
            Close();
        }
    }
}
