﻿using System;
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
    /// Interaction logic for UserControlFoodPlanGenerated.xaml
    /// </summary>
    public partial class UserControlFoodPlanGenerated : UserControl
    {
        private FoodPlan food = new FoodPlan();

        public UserControlFoodPlanGenerated()
        {
            
            InitializeComponent();
        }
        public UserControlFoodPlanGenerated(FoodPlan foodPlan)
        {
            this.food = foodPlan;
            InitializeComponent();
            
            Name1.Text ="   " + food.meal1.Name;
            Kcal1.Text ="   " + (food.meal1.Caloricity).ToString() + " KCAL";
            Ing1.Text ="    "+ food.meal1.Ingredients;
            Desc1.Text = "    " + food.meal1.Description;

            Name2.Text = "   " + food.meal2.Name;
            Kcal2.Text = "   " + (food.meal2.Caloricity).ToString() + " KCAL";
            Ing2.Text = "    " + food.meal2.Ingredients;
            Desc2.Text = "    " + food.meal2.Description;

            Name3.Text = "   " + food.meal3.Name;
            Kcal3.Text = "   " + (food.meal3.Caloricity).ToString() + " KCAL";
            Ing3.Text = "    " + food.meal3.Ingredients;
            Desc3.Text = "    " + food.meal3.Description;
        }

    }
}
