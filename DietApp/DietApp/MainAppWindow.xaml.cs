using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for MainAppWindow.xaml
    /// </summary>
    public partial class MainAppWindow : Window
    {
        public User user = new User();


        public MainAppWindow(User user1)
        {
            InitializeComponent();
            this.user = user1;
            user.UserId = DataBaseSolution.GetUserId(user.Username);
            UserControl usc = null;
            usc = new UserControlAccount(this);
            GridMain.Children.Add(usc);
        }

        public MainAppWindow()
        {
            InitializeComponent();
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

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            this.Visibility = Visibility.Hidden;
            main.Show();
            DataBaseSolution.DeleteUser(user);
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemAccount":
                    usc = new UserControlAccount(this);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemNotebook":
                    usc = new UserControlNotebook(this);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemFoodPlan":
                    usc = new UserControlFoodPlan(this);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemProducts":
                    usc = new UserProductsRecipes(this);
                    GridMain.Children.Add(usc);
                    break;
                case "ItemSearch":
                    usc = new UserControlSearch();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
    }
}
