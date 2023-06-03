﻿using System;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ComputerMagazin
{
    public partial class AddNewRAM : Window
    {
        public AddNewRAM(string category)
        {
            InitializeComponent();
            SelectName.MaxLength = 55;
            Price.MaxLength = 5;
            Label.Content = $"{category}";
        }

        private pc_RAM currentElement = new pc_RAM();

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

            if (currentElement.id_RAM == 0)
            {
                currentElement.RAM = SelectName.Text;
                currentElement.price_RAM = int.Parse(Price.Text);
                ComputerMagazinEntities.GetContext().pc_RAM.Add(currentElement);
            }
            ComputerMagazinEntities.GetContext().SaveChanges();
            MessageBox.Show($"Оперативная память: {SelectName.Text}  Цена: {Price.Text}РУБ. Успешно добавлен.");
            SelectName.Clear();
            Price.Clear();
        }

        private void Price_OnTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void Price_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
            if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                e.Handled = true;
        }
    }
}