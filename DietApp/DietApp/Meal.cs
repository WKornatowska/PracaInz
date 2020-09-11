using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp
{
    public class Meal
    {
        private int mealId;
        private string daytime;
        private string name;
        private string ingredients;
        private string description;
        private float caloricity;
        private string FoodRestrictions1;

        public Meal()
        {
        }

        public string Daytime { get => daytime; set => daytime = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public float Caloricity { get => caloricity; set => caloricity = value; }
        public string Ingredients { get => ingredients; set => ingredients = value; }
        public string FoodRestrictions11 { get => FoodRestrictions1; set => FoodRestrictions1 = value; }
        public int MealId { get => mealId; set => mealId = value; }
    }
}
