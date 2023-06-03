using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace ComputerMagazin
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            LoginTextBox.MaxLength = 25;
            PasswordTextBox.MaxLength = 25;
        }

        private void CloseWindow_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogIn_OnClick(object sender, RoutedEventArgs e)
        {
            PasswordRepeat.Text = PasswordTextBox.Password;
            var checkLoginClient = ComputerMagazinEntities.GetContext().client.Where(x => x.login == LoginTextBox.Text && x.password == PasswordRepeat.Text && x.id_role == 1).FirstOrDefault();
            var checkLoginAdmin = ComputerMagazinEntities.GetContext().admin.Where(x => x.login == LoginTextBox.Text && x.password == PasswordRepeat.Text && x.id_role == 2).FirstOrDefault();

            if (checkLoginClient != null && checkLoginAdmin == null)
            {
                MainWindow mainWindow = new MainWindow(LoginTextBox.Text);
                mainWindow.Show();
                Close();
            }
            else if (checkLoginClient == null && checkLoginAdmin != null)
            {
                AdminWindow adminWindowWindow = new AdminWindow();
                adminWindowWindow.Show();
                Close();
            }
            else
                MessageBox.Show("Данные неверные или некореткнно введенны");
        }

        private void SignUp_OnClick(object sender, RoutedEventArgs e)
        {
            Signup signup = new Signup();
            signup.Show();
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

        private void Check_Checked(object sender, RoutedEventArgs e)
        {
                PasswordRepeat.Text = PasswordTextBox.Password;
                PasswordTextBox.Visibility = Visibility.Hidden;
                PasswordRepeat.Visibility = Visibility.Visible;
            
        }

        private void Check_Unchecked(object sender, RoutedEventArgs e)
        {
                PasswordTextBox.Password = PasswordRepeat.Text;
                PasswordTextBox.Visibility = Visibility.Visible;
                PasswordRepeat.Visibility = Visibility.Hidden;

        }
    }
}
