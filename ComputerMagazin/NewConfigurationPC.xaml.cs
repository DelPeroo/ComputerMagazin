using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Word = Microsoft.Office.Interop.Word;

namespace ComputerMagazin
{
    public partial class NewConfigurationPC : Window
    {
        public NewConfigurationPC(string login)
        {
            InitializeComponent();
            loginSave.Text = login;
            Body.ItemsSource = ComputerMagazinEntities.GetContext().pc_Body.ToList();
            CPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_CPU.ToList();
            RAM.ItemsSource = ComputerMagazinEntities.GetContext().pc_RAM.ToList();
            GPU.ItemsSource = ComputerMagazinEntities.GetContext().pc_GPU.ToList();
            SSD.ItemsSource = ComputerMagazinEntities.GetContext().pc_SSD.ToList();
            HDD.ItemsSource = ComputerMagazinEntities.GetContext().pc_HDD.ToList();
            type_OC.ItemsSource = ComputerMagazinEntities.GetContext().type_OC.ToList();

        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(loginSave.Text);
            mainWindow.Show();
            Close();
        }

        private order neworder = new order();
        
        private pc_features_personal newFP = new pc_features_personal();

        private void CreateNewIndent_OnClick(object sender, RoutedEventArgs e)
        {
            if (Body.SelectedItem != null && CPU.SelectedItem != null && RAM.SelectedItem != null &&
                GPU.SelectedItem != null
                && SSD.SelectedItem != null && HDD.SelectedItem != null && type_OC.SelectedItem != null)
            {
                var selectedBoxBody = Body.SelectedItem as pc_Body;
                var selectedBoxCPU = CPU.SelectedItem as pc_CPU;
                var selectedBoxRAM = RAM.SelectedItem as pc_RAM;
                var selectedBoxGPU = GPU.SelectedItem as pc_GPU;
                var selectedBoxSSD = SSD.SelectedItem as pc_SSD;
                var selectedBoxHDD = HDD.SelectedItem as pc_HDD;
                var selectedBoxOC = type_OC.SelectedItem as type_OC;

                var countPrice = selectedBoxBody.price_Body + selectedBoxCPU.price_CPU + selectedBoxRAM.price_RAM +
                                 selectedBoxGPU.price_GPU + selectedBoxSSD.price_SSD + selectedBoxHDD.price_HDD +
                                 selectedBoxOC.price_type_OC;

                if (countPrice < 51000)
                    newFP.id_category = 1;
                else if (countPrice > 51000 && countPrice < 90000)
                    newFP.id_category = 2;
                else
                    newFP.id_category = 3;

                newFP.login = loginSave.Text;
                newFP.id_Body = selectedBoxBody.id_Body;
                newFP.id_CPU = selectedBoxCPU.id_CPU;
                newFP.id_RAM = selectedBoxRAM.id_RAM;
                newFP.id_GPU = selectedBoxGPU.id_GPU;
                newFP.id_SSD = selectedBoxSSD.id_SSD;
                newFP.id_HDD = selectedBoxHDD.id_HDD;
                newFP.id_type_OC = selectedBoxOC.id_type_OC;
                newFP.price = countPrice;
                var date = DateTime.UtcNow;
                newFP.checkData = date;

                ComputerMagazinEntities.GetContext().pc_features_personal.Add(newFP);
                ComputerMagazinEntities.GetContext().SaveChanges();

                var priceBody = selectedBoxBody.price_Body;
                var priceCpu = selectedBoxCPU.price_CPU;
                var priceRam = selectedBoxRAM.price_RAM;
                var priceGpu = selectedBoxGPU.price_GPU;
                var priceSsd = selectedBoxSSD.price_SSD;
                var priceHdd = selectedBoxHDD.price_HDD;
                var priceTypeOc = selectedBoxOC.price_type_OC;

                var totalPrice = priceBody + priceCpu + priceRam + priceGpu + priceSsd + priceHdd + priceTypeOc;

                var pcFeaturesPersonals = ComputerMagazinEntities.GetContext().pc_features_personal.ToList();
                foreach (var aFP in pcFeaturesPersonals)
                    if (aFP.checkData == date)
                    {
                        neworder.id_pc_features_personal = aFP.id_pc_features_personal;
                        neworder.login = loginSave.Text;
                        neworder.amount = 1;
                        neworder.data_order = date;
                        neworder.data_delivery = null;
                        neworder.total_price = totalPrice;

                        if (MessageBox.Show("Вы хотите совершить заказ?", "Внимание!", MessageBoxButton.YesNo,
                                MessageBoxImage.Warning) ==
                            MessageBoxResult.Yes)
                        {
                            MessageBox.Show(
                                $"Ваш заказ успешно сформирован, все детали можете отследить в личном кабинете.   Сумма заказа {totalPrice}.   Наш контактный номер:   +7 800 555 35 35");
                            ComputerMagazinEntities.GetContext().order.Add(neworder);
                            ComputerMagazinEntities.GetContext().SaveChanges();

                            var pcOrder = ComputerMagazinEntities.GetContext().order.ToList();
                            foreach (var wordOrder in pcOrder)
                                if (wordOrder.data_order == date)
                                {
                                    var Body = ComputerMagazinEntities.GetContext().pc_Body.ToList();
                                    var CPU = ComputerMagazinEntities.GetContext().pc_CPU.ToList();
                                    var RAM = ComputerMagazinEntities.GetContext().pc_RAM.ToList();
                                    var GPU = ComputerMagazinEntities.GetContext().pc_GPU.ToList();
                                    var SSD = ComputerMagazinEntities.GetContext().pc_SSD.ToList();
                                    var HDD = ComputerMagazinEntities.GetContext().pc_HDD.ToList();
                                    var typeOC = ComputerMagazinEntities.GetContext().type_OC.ToList();
                                    var category = ComputerMagazinEntities.GetContext().pc_category.ToList();

                                    foreach (var aBody in Body)
                                    foreach (var aRam in RAM)
                                    foreach (var aGpu in GPU)
                                    foreach (var aSSD in SSD)
                                    foreach (var aHDD in HDD)
                                    foreach (var atypeOC in typeOC)
                                    foreach (var aCategory in category)
                                    foreach (var aCpu in CPU)
                                    {
                                        if (aBody.id_Body == aFP.id_Body &&
                                            aCpu.id_CPU == aFP.id_CPU &&
                                            aRam.id_RAM == aFP.id_RAM && aGpu.id_GPU == aFP.id_GPU
                                            && aSSD.id_SSD == aFP.id_SSD &&
                                            aHDD.id_HDD == aFP.id_HDD &&
                                            atypeOC.id_type_OC == aFP.id_type_OC &&
                                            aCategory.id_category == aFP.id_category)
                                        {
                                            Word.Application wordApp = new Word.Application();
                                            Word.Document wordDoc = wordApp.Documents.Add();
                                            Word.Paragraph paragraph = wordDoc.Content.Paragraphs.Add();

                                            paragraph.Range.Text = "Заказ №" + wordOrder.id_order;
                                            paragraph.Range.InsertParagraphAfter();

                                            paragraph.Range.Text = "Аккаунт: " + loginSave.Text;
                                            paragraph.Range.InsertParagraphAfter();

                                            paragraph.Range.Text = "Дата заказа: " + wordOrder.data_order;
                                            paragraph.Range.InsertParagraphAfter();

                                            Word.Table table = wordDoc.Tables.Add(paragraph.Range, 7, 2);
                                            table.Cell(1, 1).Range.Text = "Корпус";
                                            table.Cell(2, 1).Range.Text = "Процессор";
                                            table.Cell(3, 1).Range.Text = "Оперативная память";
                                            table.Cell(4, 1).Range.Text = "Видеокарта";
                                            table.Cell(5, 1).Range.Text = "Твердотелый диск SSD";
                                            table.Cell(6, 1).Range.Text = "Жесткий диск HDD";
                                            table.Cell(7, 1).Range.Text = "Тип ОС";

                                            table.Cell(1, 2).Range.Text = aBody.Body;
                                            table.Cell(2, 2).Range.Text = aCpu.CPU;
                                            table.Cell(3, 2).Range.Text = aRam.RAM;
                                            table.Cell(4, 2).Range.Text = aGpu.GPU;
                                            table.Cell(5, 2).Range.Text = aSSD.SSD;
                                            table.Cell(6, 2).Range.Text = aHDD.HDD;
                                            table.Cell(7, 2).Range.Text = atypeOC.type_OC_name;

                                            paragraph.Range.Text =
                                                "Сумма заказа " + wordOrder.total_price.ToString("c");
                                            paragraph.Range.InsertParagraphAfter();

                                            wordDoc.SaveAs2(
                                                @"C:\Users\evlad\OneDrive\Рабочий стол\ComputerMagazinSource\Заказ ПК " +
                                                wordOrder.id_order + ".docx");
                                            wordDoc.Close();
                                            wordApp.Quit();
                                            MainWindow mainWindow = new MainWindow(loginSave.Text);
                                            mainWindow.Show();
                                            Close();
                                        }
                                    }
                                }
                        }
                        else
                            return;
                    }
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