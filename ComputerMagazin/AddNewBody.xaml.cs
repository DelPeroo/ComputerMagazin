using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace ComputerMagazin
{
    public partial class AddNewBody : Window
    {
        public AddNewBody(string category)
        {
            InitializeComponent();
            SelectName.MaxLength = 55;
            Price.MaxLength = 5;
            Label.Content = $"{category}";
        }

        private pc_Body currentElement = new pc_Body();

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

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображение | *.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                byte[] imageData = File.ReadAllBytes(imagePath);
                
                currentElement.Body = SelectName.Text;
                currentElement.price_Body = int.Parse(Price.Text);
                currentElement.photo = imageData;
                ComputerMagazinEntities.GetContext().pc_Body.Add(currentElement);
                ComputerMagazinEntities.GetContext().SaveChanges();
            }
            else
            {
                currentElement.Body = SelectName.Text;
                currentElement.price_Body = int.Parse(Price.Text);
                ComputerMagazinEntities.GetContext().pc_Body.Add(currentElement);
                ComputerMagazinEntities.GetContext().SaveChanges();
            }
            

            MessageBox.Show($"Корпус: {SelectName.Text}  Цена: {Price.Text}РУБ. Успешно добавлен.");
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