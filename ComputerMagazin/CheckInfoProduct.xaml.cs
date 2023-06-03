using System;
using System.Linq;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace ComputerMagazin
{
    public partial class CheckInfoProduct : Window
    {
        public CheckInfoProduct(pc_features_default selectedProduct, string login)
        {
            InitializeComponent();
            loginSave.Text = login;
            if (selectedProduct != null)
                selectedProductRepeat = selectedProduct;

            DataContext = selectedProductRepeat;
        }
        
        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(loginSave.Text);
            mainWindow.Show();
            Close();
        }

        private pc_features_default selectedProductRepeat = new pc_features_default();
        
        private order neworder = new order();
        
        private pc_features_personal newFP = new pc_features_personal();

        private void CreateNewIndent_OnClick(object sender, RoutedEventArgs e)
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
                if (aBody.id_Body == selectedProductRepeat.id_Body && aCpu.id_CPU == selectedProductRepeat.id_CPU &&
                    aRam.id_RAM == selectedProductRepeat.id_RAM && aGpu.id_GPU == selectedProductRepeat.id_GPU
                    && aSSD.id_SSD == selectedProductRepeat.id_SSD && aHDD.id_HDD == selectedProductRepeat.id_HDD &&
                    atypeOC.id_type_OC == selectedProductRepeat.id_type_OC && aCategory.id_category == selectedProductRepeat.id_category)
                {

                    newFP.login = loginSave.Text;
                    newFP.id_Body = aBody.id_Body;
                    newFP.id_CPU = aCpu.id_CPU;
                    newFP.id_RAM = aRam.id_RAM;
                    newFP.id_GPU = aGpu.id_GPU;
                    newFP.id_SSD = aSSD.id_SSD;
                    newFP.id_HDD = aHDD.id_HDD;
                    newFP.id_type_OC = atypeOC.id_type_OC;
                    newFP.id_category = aCategory.id_category;
                    var totalPrice = aBody.price_Body + aCpu.price_CPU + aRam.price_RAM + aGpu.price_GPU + aSSD.price_SSD + aHDD.price_HDD + atypeOC.price_type_OC;
                    newFP.price = totalPrice;
                    var date = DateTime.UtcNow;
                    newFP.checkData = date;
                    
                    ComputerMagazinEntities.GetContext().pc_features_personal.Add(newFP);
                    ComputerMagazinEntities.GetContext().SaveChanges();

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

                                            paragraph.Range.Text = "Сумма заказа " + wordOrder.total_price.ToString("c");
                                            paragraph.Range.InsertParagraphAfter();

                                            wordDoc.SaveAs2(@"C:\Users\evlad\OneDrive\Рабочий стол\ComputerMagazinSource\Заказ ПК " +
                                                            wordOrder.id_order + ".docx");
                                            wordDoc.Close();
                                            wordApp.Quit();
                                            MainWindow mainWindow = new MainWindow(loginSave.Text);
                                            mainWindow.Show();
                                            Close();
                                        }
                                    } 
                            }
                            else
                                return;
                        }
                }
            }
        }
    }
}
