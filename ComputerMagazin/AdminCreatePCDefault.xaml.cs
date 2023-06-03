using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ComputerMagazin
{
    public partial class AdminCreatePCDefault : Window
    {
        public AdminCreatePCDefault()
        {
            InitializeComponent();
            Body.ItemsSource = ComputerMagazinEntities.GetContext().pc_Body.ToList();
            CPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_CPU.ToList();
            RAM.ItemsSource = ComputerMagazinEntities.GetContext().pc_RAM.ToList();
            GPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_GPU.ToList();
            SSD.ItemsSource = ComputerMagazinEntities.GetContext().pc_SSD.ToList();
            HDD.ItemsSource = ComputerMagazinEntities.GetContext().pc_HDD.ToList();
            type_OC.ItemsSource = ComputerMagazinEntities.GetContext().type_OC.ToList();
        }

        private pc_features_default PcFeaturesDefault = new pc_features_default();
        
        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }
        
        private void CreateNewPCDefault_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
            var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
            var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
            var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
            var selectedBoxOC = type_OC.SelectedItem as type_OC;

            if (Body.SelectedItem != null && CPU.SelectedItem != null && RAM.SelectedItem != null &&
                GPU.SelectedItem != null && SSD.SelectedItem != null
                && HDD.SelectedItem != null && type_OC.SelectedItem != null)
            {
                var countPrice = selectedBoxBody.price_Body + selectedBoxCPU.price_CPU + selectedBoxRAM.price_RAM +
                                 selectedBoxGPU.price_GPU + selectedBoxSSD.price_SSD + selectedBoxHDD.price_HDD +
                                 selectedBoxOC.price_type_OC;
                PcFeaturesDefault.id_Body = selectedBoxBody.id_Body;
                PcFeaturesDefault.id_CPU = selectedBoxCPU.id_CPU;
                PcFeaturesDefault.id_RAM = selectedBoxRAM.id_RAM;
                PcFeaturesDefault.id_GPU = selectedBoxGPU.id_GPU;
                PcFeaturesDefault.id_SSD = selectedBoxSSD.id_SSD;
                PcFeaturesDefault.id_HDD = selectedBoxHDD.id_HDD;
                PcFeaturesDefault.id_type_OC = selectedBoxOC.id_type_OC;
                PcFeaturesDefault.price = countPrice;

                if (countPrice < 51000)
                    PcFeaturesDefault.id_category = 1;
                else if (countPrice > 51000 && countPrice < 90000)
                    PcFeaturesDefault.id_category = 2;
                else
                    PcFeaturesDefault.id_category = 3;

                ComputerMagazinEntities.GetContext().pc_features_default.Add(PcFeaturesDefault);
                ComputerMagazinEntities.GetContext().SaveChanges();

                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Show();
                Close();
            }
        }
        
        private void Body_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedImage = Body.SelectedItem as pc_Body;
            if (selectedImage.photo != null)
            {
                MemoryStream stream = new MemoryStream ( selectedImage.photo );
                ImageTag.Source = BitmapFrame.Create ( stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad );
            }
            else
            {
                var selectedNoneImage = ComputerMagazinEntities.GetContext().TableForSavePictures.FirstOrDefault(x => x.id_pictures == 1);
                MemoryStream stream = new MemoryStream ( selectedNoneImage.pictures );
                ImageTag.Source = BitmapFrame.Create ( stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad );
            }

            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd).ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }

        private void CPU_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd)
                                            .ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }

        private void RAM_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd)
                                            .ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }

        private void GPU_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd)
                                            .ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }

        private void SSD_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd)
                                            .ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }

        private void HDD_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd)
                                            .ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }

        private void Type_OC_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedBoxBody = Body.SelectedItem as pc_Body;
            if (selectedBoxBody != null)
            {
                var priceBody = selectedBoxBody.price_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                if (selectedBoxCPU != null)
                {
                    var priceCpu = selectedBoxCPU.price_CPU;
                    var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                    if (selectedBoxRAM != null)
                    {
                        var priceRam = selectedBoxRAM.price_RAM;
                        var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                        if (selectedBoxGPU != null)
                        {
                            var priceGpu = selectedBoxGPU.price_GPU;
                            var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                            if (selectedBoxSSD != null)
                            {
                                var priceSsd = selectedBoxSSD.price_SSD;
                                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                                if (selectedBoxHDD != null)
                                {
                                    var priceHdd = selectedBoxHDD.price_HDD;
                                    var selectedBoxOC = type_OC.SelectedItem as type_OC;
                                    if (selectedBoxOC != null)
                                    {
                                        var priceTypeOc = selectedBoxOC.price_type_OC;
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd +
                                             priceTypeOc).ToString();
                                    }
                                    else
                                        Price.Text =
                                            (priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd)
                                            .ToString();
                                }
                                else
                                    Price.Text =
                                        (priceBody + priceCpu + priceRam + priceGpu + priceSsd).ToString();
                            }
                            else
                                Price.Text =
                                    (priceBody + priceCpu + priceRam + priceGpu).ToString();
                        }
                        else
                            Price.Text =
                                (priceBody + priceCpu + priceRam).ToString();
                    }
                    else
                        Price.Text =
                            (priceBody + priceCpu).ToString();
                }
                else
                    Price.Text =
                        (priceBody).ToString();
            }
        }
    }
}