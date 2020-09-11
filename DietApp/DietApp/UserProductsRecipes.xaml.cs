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
    /// Interaction logic for UserProductsRecipes.xaml
    /// </summary>
    public partial class UserProductsRecipes : UserControl
    {
        private User user;
        public UserProductsRecipes()
        {
            InitializeComponent();
        }
        public UserProductsRecipes(MainAppWindow main)
        {
            this.user = main.user;
            InitializeComponent();
        }


        private void SaveProductButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            product.Name = ProductName.Text;
            product.Kcal = float.Parse((ProductKCAL.Text).ToString());
            product.Quantity = int.Parse((Quan.Text).ToString());
            product.Unit = Unit.Text;
            DataBaseSolution.AddProduct(product);
            ProductName.Clear();
            ProductKCAL.Clear();
            Unit.ClearValue(NameProperty);
            Quan.Clear();
        }

        private void SaveRecipetButton_Click(object sender, RoutedEventArgs e)
        {
            Meal meal = new Meal();
            meal.Name = RecipeName.Text;
            meal.Caloricity = float.Parse((RecipeKCAL.Text).ToString());
            if (DT.Text == "Śniadanie") { meal.Daytime = "Sniadanie"; }
            if (DT.Text == "II śniadanie/podwieczorek") { meal.Daytime = "II sniadanie/podwieczorek"; }
            meal.Description = RecipeDesc.Text;
            meal.Ingredients = RecipeIng.Text;
            meal.FoodRestrictions11 = Diary.IsChecked.ToString() + "," + Gluten.IsChecked.ToString() + "," + Meat.IsChecked.ToString();
            DataBaseSolution.AddRecipe(meal);
            RecipeName.Clear();
            RecipeKCAL.Clear();
            RecipeDesc.Clear();
            DT.ClearValue(NameProperty);
            RecipeIng.Clear();
            Diary.IsChecked = false;
            Gluten.IsChecked = false;
            Meat.IsChecked = false;
                
        }
    }
}
