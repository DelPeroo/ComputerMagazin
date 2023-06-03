using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ComputerMagazin
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            ComboboxAdmin.ItemsSource = ComputerMagazinEntities.GetContext().TableForComboBox.ToList();
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void ComboboxAdmin_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboboxSelected = ComboboxAdmin.SelectedItem as TableForComboBox;
            if (comboboxSelected != null)
            {
                if (comboboxSelected.id_combobox == 1)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Visible;
                    DataGridBody.ItemsSource = ComputerMagazinEntities.GetContext().pc_Body.ToList();
                }

                if (comboboxSelected.id_combobox == 2)
                {
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridCPU.Visibility = Visibility.Visible;
                    DataGridCPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_CPU.ToList();
                }

                if (comboboxSelected.id_combobox == 3)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Visible;
                    DataGridRAM.ItemsSource = ComputerMagazinEntities.GetContext().pc_RAM.ToList();
                }

                if (comboboxSelected.id_combobox == 4)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Visible;
                    DataGridGPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_GPU.ToList();
                }

                if (comboboxSelected.id_combobox == 5)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Visible;
                    DataGridSSD.ItemsSource = ComputerMagazinEntities.GetContext().pc_SSD.ToList();
                }

                if (comboboxSelected.id_combobox == 6)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Visible;
                    DataGridHDD.ItemsSource = ComputerMagazinEntities.GetContext().pc_HDD.ToList();
                }

                if (comboboxSelected.id_combobox == 7)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Visible;
                    DataGridtypeOC.ItemsSource = ComputerMagazinEntities.GetContext().type_OC.ToList();
                }
                
                if (comboboxSelected.id_combobox == 8)
                {
                    DataGridCPU.Visibility = Visibility.Hidden;
                    DataGridRAM.Visibility = Visibility.Hidden;
                    DataGridGPU.Visibility = Visibility.Hidden;
                    DataGridSSD.Visibility = Visibility.Hidden;
                    DataGridHDD.Visibility = Visibility.Hidden;
                    DataGridBody.Visibility = Visibility.Hidden;
                    DataGridtypeOC.Visibility = Visibility.Hidden;
                    DataGridPCDefault.Visibility = Visibility.Visible;
                    DataGridPCDefault.ItemsSource = ComputerMagazinEntities.GetContext().pc_features_default.ToList();
                }
            }
        }

        private void AddElement_OnClick(object sender, RoutedEventArgs e)
        {
            var comboboxSelected = ComboboxAdmin.SelectedItem as TableForComboBox;
            if (comboboxSelected != null)
            {
                if (comboboxSelected.id_combobox == 1)
                {
                    AddNewBody addNewBody = new AddNewBody(comboboxSelected.select_combobox);
                    addNewBody.Show(); 
                    Close();
                }

                if (comboboxSelected.id_combobox == 2)
                {
                    
                    AddNewCPU addNewCpu = new AddNewCPU(comboboxSelected.select_combobox);
                    addNewCpu.Show(); 
                    Close();
                }

                if (comboboxSelected.id_combobox == 3)
                {
                    AddNewRAM addNewRam = new AddNewRAM(comboboxSelected.select_combobox);
                    addNewRam.Show(); 
                    Close();
                }

                if (comboboxSelected.id_combobox == 4)
                {
                    AddNewGPU addNewGpu = new AddNewGPU(comboboxSelected.select_combobox);
                    addNewGpu.Show(); 
                    Close();
                }

                if (comboboxSelected.id_combobox == 5)
                {
                    AddNewSSD addNewSsd = new AddNewSSD(comboboxSelected.select_combobox);
                    addNewSsd.Show(); 
                    Close();
                }

                if (comboboxSelected.id_combobox == 6)
                {
                    AddNewHDD addNewHdd = new AddNewHDD(comboboxSelected.select_combobox);
                    addNewHdd.Show(); 
                    Close();
                }

                if (comboboxSelected.id_combobox == 7)
                {
                    AddNewTypeOC addNewTypeOC = new AddNewTypeOC(comboboxSelected.select_combobox);
                    addNewTypeOC.Show(); 
                    Close();
                }
            }
        }

        private void RemoveElement_OnClick(object sender, RoutedEventArgs e)
        {
            var comboboxSelected = ComboboxAdmin.SelectedItem as TableForComboBox;
            if (comboboxSelected != null)
            {
                if (comboboxSelected.id_combobox == 1)
                {
                    var deleteElementBody = DataGridBody.SelectedItems.Cast<pc_Body>();
                    if (DataGridBody != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_Body.RemoveRange(deleteElementBody);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridBody.ItemsSource = ComputerMagazinEntities.GetContext().pc_Body.ToList();
                        DataGridBody.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 2)
                {
                    var deleteElementCPU = DataGridCPU.SelectedItems.Cast<pc_CPU>();
                    if (DataGridCPU != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_CPU.RemoveRange(deleteElementCPU);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridCPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_CPU.ToList();
                        DataGridCPU.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 3)
                {
                    var deleteElementRAM = DataGridRAM.SelectedItems.Cast<pc_RAM>();
                    if (DataGridRAM != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_RAM.RemoveRange(deleteElementRAM);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridRAM.ItemsSource = ComputerMagazinEntities.GetContext().pc_RAM.ToList();
                        DataGridRAM.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 4)
                {
                    var deleteElementGPU = DataGridGPU.SelectedItems.Cast<pc_GPU>();
                    if (DataGridGPU != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_GPU.RemoveRange(deleteElementGPU);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridGPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_GPU.ToList();
                        DataGridGPU.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 5)
                {
                    var deleteElementSSD = DataGridSSD.SelectedItems.Cast<pc_SSD>();
                    if (DataGridSSD != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_SSD.RemoveRange(deleteElementSSD);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridSSD.ItemsSource = ComputerMagazinEntities.GetContext().pc_SSD.ToList();
                        DataGridSSD.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 6)
                {
                    var deleteElementHDD = DataGridHDD.SelectedItems.Cast<pc_HDD>();
                    if (DataGridHDD != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_HDD.RemoveRange(deleteElementHDD);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridHDD.ItemsSource = ComputerMagazinEntities.GetContext().pc_HDD.ToList();
                        DataGridHDD.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 7)
                {
                    var deleteElementTypeOC = DataGridtypeOC.SelectedItems.Cast<type_OC>();
                    if (DataGridtypeOC != null)
                    {
                        ComputerMagazinEntities.GetContext().type_OC.RemoveRange(deleteElementTypeOC);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridtypeOC.ItemsSource = ComputerMagazinEntities.GetContext().type_OC.ToList();
                        DataGridtypeOC.SelectedIndex = -1;
                    }
                }

                if (comboboxSelected.id_combobox == 8)
                {
                    var deletePCConfiguration = DataGridPCDefault.SelectedItems.Cast<pc_features_default>();
                    if (DataGridPCDefault != null)
                    {
                        ComputerMagazinEntities.GetContext().pc_features_default.RemoveRange(deletePCConfiguration);
                        ComputerMagazinEntities.GetContext().SaveChanges();
                        DataGridPCDefault.ItemsSource =
                            ComputerMagazinEntities.GetContext().pc_features_default.ToList();
                        DataGridPCDefault.SelectedIndex = -1;
                    }
                }
            }
        }

        private void CreateNewConfigurationPC_OnClick(object sender, RoutedEventArgs e)
        {
            AdminCreatePCDefault adminCreatePcDefault = new AdminCreatePCDefault();
            adminCreatePcDefault.Show();
            Close();
        }

        private void AdminOrderCheck_Click(object sender, RoutedEventArgs e)
        {
            PreviewOrder previewOrder = new PreviewOrder();
            previewOrder.Show();
            Close();
        }
    }
}