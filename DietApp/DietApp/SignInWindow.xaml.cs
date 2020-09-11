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

namespace DietApp
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            if ((!(DataBaseSolution.FindUser(LoginBox.Text))) && (PasswordBox.Password.Length>0) && (PasswordBox.Password == PasswordBox1.Password))
            {
                DataBaseSolution.AddUser(LoginBox.Text, PasswordBox.Password);
                MessageBox.Show("Utworzono nowy profil");
                LoginWindow loginWindow = new LoginWindow();
                this.Visibility = Visibility.Hidden;
                loginWindow.Show();
            }
            else
            {
                if ((PasswordBox.Password.Length > 0) && (PasswordBox.Password == PasswordBox1.Password))
                {
                    MessageBox.Show("Użytkownik o takiej nazwie istnieje");
                }
                else if ((PasswordBox.Password.Length > 0) && (PasswordBox.Password != PasswordBox1.Password)) {
                    MessageBox.Show("Wprowadzone hasła różnią się od siebie");
                }
                else
                {
                    MessageBox.Show("Wprowadź hasło");
                }
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            this.Visibility = Visibility.Hidden;
            loginWindow.Show();
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
