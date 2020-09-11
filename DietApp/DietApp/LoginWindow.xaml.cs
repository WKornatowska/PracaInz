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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            SignInWindow signInWindow = new SignInWindow();
            this.Visibility = Visibility.Hidden;
            signInWindow.Show();
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            if (!(DataBaseSolution.FindUser(LoginBox.Text)))
            {
                MessageBox.Show("Wskazany użytkownik nie istnieje");
            }
            else
            {
                if ((DataBaseSolution.CheckPassword(LoginBox.Text, PasswordBox.Password)) == true)
                {
                    MessageBox.Show("Podane hasło jest nieprawidłowe");
                }
                else
                {
                    User user = new User();
                    user.Username = LoginBox.Text;
                    user.Password = PasswordBox.Password;
                    MainAppWindow mainAppWindow = new MainAppWindow(user);
                    this.Visibility = Visibility.Hidden;
                    mainAppWindow.Show();
                }
            }
        }
    }
}
