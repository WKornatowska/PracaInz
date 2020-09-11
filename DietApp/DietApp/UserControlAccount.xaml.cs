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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DietApp
{
    /// <summary>
    /// Interaction logic for UserControlAccount.xaml
    /// </summary>
    public partial class UserControlAccount : UserControl
    {
        public User user1 =new User();
        public UserControlAccount()
        {
            InitializeComponent();
        }

        public UserControlAccount( MainAppWindow main)
        {
            this.user1= main.user;
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            
            user1.UserSex = SexBox.Text;
            user1.UserAge = Int32.Parse(AgeBox.Text);
            user1.UserWeight = float.Parse(WeightBox.Text);
            user1.UserHeight = float.Parse(HeightBox.Text);
            user1.UserType = TypeBox.Text;
            user1.UserdailyActivity = AcitvityBox.Text;
            user1.UserTSWeekly = Int32.Parse(SDays.Text);
            user1.UserTSDaily = Int32.Parse(SMinutes.Text);
            user1.UserTSIntensivity = SIntensivity.Text;
            user1.UserTAWeekly = Int32.Parse(ADays.Text);
            user1.UserTADaily = Int32.Parse(AMinutes.Text);
            user1.UserTAIntensivity = AIntensivity.Text;
            user1.UserFoodRestrictions = Diary.IsChecked.ToString() + "," + Gluten.IsChecked.ToString() + "," + Meat.IsChecked.ToString();
            user1.UserMealNumber = Int32.Parse(MealNumber.Text);
            DataBaseSolution.UpdateUserData(user1);
            SexBox.ClearValue(NameProperty);
            AgeBox.Clear();
            WeightBox.Clear();
            HeightBox.Clear();
            TypeBox.ClearValue(NameProperty);
            AActivityBox.ClearValue(NameProperty);
            AcitvityBox.ClearValue(NameProperty);
            SDays.Clear();
            SMinutes.Clear();
            SIntensivity.ClearValue(NameProperty);
            ADays.Clear();
            AMinutes.Clear();
            AIntensivity.ClearValue(NameProperty);
            Diary.IsChecked = false;
            Gluten.IsChecked = false;
            Meat.IsChecked = false;
            MealNumber.ClearValue(NameProperty);
            MessageBox.Show("Dane użytkownika zostały zaktualizowane");

        }
    }
}
