using System;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ComputerMagazin
{
    public partial class AddNewCPU : Window
    {
        public AddNewCPU(string category)
        {
            InitializeComponent();
            SelectName.MaxLength = 55;
            Price.MaxLength = 5;
            Label.Content = $"{category}";
        }

        private pc_CPU currentElement = new pc_CPU();

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }

        private void AddElement_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            
            if (string.IsNullOrWhiteSpace(SelectName.Text))
                errors.AppendLine("Не верно указано поле Корпус");
            if (string.IsNullOrWhiteSpace(Price.Text) || int.Parse(Price.Text) == 0)
                errors.AppendLine("Цена указа не коректно");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (currentElement.id_CPU == 0)
            {
                currentElement.CPU = SelectName.Text;
                currentElement.price_CPU = int.Parse(Price.Text);
                ComputerMagazinEntities.GetContext().pc_CPU.Add(currentElement);
                ComputerMagazinEntities.GetContext().SaveChanges();
            }
            MessageBox.Show($"Процессор: {SelectName.Text}  Цена: {Price.Text}РУБ. Успешно добавлен.");
            SelectName.Clear();
            Price.Clear();
        }

        private void Price_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                e.Handled = true;
        }

        private void Price_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}