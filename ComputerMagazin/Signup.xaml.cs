using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ComputerMagazin
{
    public partial class Signup : Window
    {
        public Signup()
        {
            InitializeComponent();
            LoginTextBox.MaxLength = 25;
            PasswordTextBox.MaxLength = 25;
            PasswordRepeatTextBox.MaxLength = 25;
            FamiliyaTextBox.MaxLength = 25;
            ImyaTextBox.MaxLength = 25;
            SurnameTextBox.MaxLength = 25;
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private client _client = new client();

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();

            _client.login = LoginTextBox.Text;
            _client.password = PasswordTextBox.Text;
            _client.First_name = FamiliyaTextBox.Text;
            _client.Second_name = ImyaTextBox.Text;
            _client.Surname = SurnameTextBox.Text;
            _client.id_role = 1;

            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || LoginTextBox.Text.Length < 5)
                stringBuilder.AppendLine("Не верно заполненное поле Логин");
            if (string.IsNullOrWhiteSpace(PasswordTextBox.Text) || PasswordTextBox.Text.Length < 5)
                stringBuilder.AppendLine("Не верно заполненное поле Пароль -- Пароль не может быть меньше 5 символов");
            if (string.IsNullOrWhiteSpace(PasswordRepeatTextBox.Text) ||
                PasswordRepeatTextBox.Text != PasswordTextBox.Text)
                stringBuilder.AppendLine("Не верно заполненное поле Повторного пароля -- Пароли могут не совпадать");
            if (string.IsNullOrWhiteSpace(FamiliyaTextBox.Text))
                stringBuilder.AppendLine("Не верно заполненное поле Фамилия");
            if (string.IsNullOrWhiteSpace(ImyaTextBox.Text))
                stringBuilder.AppendLine("Не верно заполненное поле Имя");
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
                if (MessageBox.Show("Отсутсвует отчество, ввести его?", "Внимание!", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
                        stringBuilder.AppendLine("Не верно заполненное поле Отчество");
                }
            if (stringBuilder.Length > 0)
            {
                MessageBox.Show(stringBuilder.ToString());
                return;
            }
            
            var currentLogin = ComputerMagazinEntities.GetContext().client
                .FirstOrDefault(x => x.login == LoginTextBox.Text);
            if (currentLogin != null)
                MessageBox.Show("Выбранный логин занят");
            else
            {
                ComputerMagazinEntities.GetContext().client.Add(_client);
                ComputerMagazinEntities.GetContext().SaveChanges();

                Login login = new Login();
                login.Show();
                Close();
            }
        }

        private void BackLogin_OnClick(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void LoginTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void PasswordTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void PasswordRepeatTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void FamiliyaTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void ImyaTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;        
        }

        private void SurnameTextBox_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void LoginTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z0-9]+$"))
                e.Handled = true;
        }

        private void PasswordTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z0-9]+$"))
                e.Handled = true;
        }

        private void PasswordRepeatTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[a-zA-Z0-9]+$"))
                e.Handled = true;
        }

        private void FamiliyaTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Я]+$"))
                e.Handled = true;
        }

        private void ImyaTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Я]+$"))
                e.Handled = true;
        }

        private void SurnameTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "^[а-яА-Я]+$"))
                e.Handled = true;
        }
    }
}