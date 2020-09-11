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
using System.Windows.Data;

namespace DietApp
{
    /// <summary>
    /// Interaction logic for UserControlFoodPlan.xaml
    /// </summary>
    public partial class UserControlFoodPlan : UserControl
    {
        private User user1 = new User();
        private  UserControl usc=null;
        private FoodPlan foodplan = new FoodPlan();

        public UserControlFoodPlan()
        {
            DataBaseSolution.GetUserData( user1);
            InitializeComponent();
            if (DataBaseSolution.FoodPlanExist(user1) == true)
            {
                foodplan = DataBaseSolution.GetFoodPlan(user1);
                if (user1.UserMealNumber == 3)
                {
                    GridFoodPlan.Children.Clear();
                    usc = new UserControlFoodPlanGenerated(foodplan);
                    GridFoodPlan.Children.Add(usc);

                }
                if (user1.UserMealNumber == 4)
                {
                    GridFoodPlan.Children.Clear();
                    usc = new UserControlFoodPlanGenerated4M(foodplan);
                    GridFoodPlan.Children.Add(usc);
                }
                if (user1.UserMealNumber == 5)
                {
                    GridFoodPlan.Children.Clear();
                    usc = new UserControlFoodPlanGenerated5M(foodplan);
                    GridFoodPlan.Children.Add(usc);

                }
            }
        }

        public UserControlFoodPlan(MainAppWindow main)
        {
            this.user1 = main.user;
            InitializeComponent();
            if (DataBaseSolution.FoodPlanExist(user1) == true)
            {
                foodplan = DataBaseSolution.GetFoodPlan(user1);
                if (user1.UserMealNumber == 3)
                {
                    GridFoodPlan.Children.Clear();
                    usc = new UserControlFoodPlanGenerated(foodplan);
                    GridFoodPlan.Children.Add(usc);
                }
                if (user1.UserMealNumber == 4)
                {
                    GridFoodPlan.Children.Clear();
                    usc = new UserControlFoodPlanGenerated4M(foodplan);
                    GridFoodPlan.Children.Add(usc);
                }
                if (user1.UserMealNumber == 5)
                {
                    GridFoodPlan.Children.Clear();
                    usc = new UserControlFoodPlanGenerated5M(foodplan);
                    GridFoodPlan.Children.Add(usc);
                }
            }
        }

      
        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            DataBaseSolution.GetUserData(user1);     
            Algorithm algorithm = new Algorithm(user1);
            foodplan =  algorithm.GetFoodData();         
            if (user1.UserMealNumber == 3)
            {
                GridFoodPlan.Children.Clear();
                usc = new UserControlFoodPlanGenerated(foodplan);
                GridFoodPlan.Children.Add(usc);
    
            }
            if (user1.UserMealNumber == 4)
            {
                GridFoodPlan.Children.Clear();
                usc = new UserControlFoodPlanGenerated4M(foodplan);
                GridFoodPlan.Children.Add(usc);
            }
            if (user1.UserMealNumber == 5)
            {
                GridFoodPlan.Children.Clear();
                usc = new UserControlFoodPlanGenerated5M(foodplan);
                GridFoodPlan.Children.Add(usc);
            }
        }
    }
}

