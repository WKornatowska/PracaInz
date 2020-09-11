using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp
{
    public class FoodPlan
    {
        public  User user1 = new User();
        public Meal meal1 = new Meal();
        public Meal meal2 = new Meal();
        public Meal meal3 = new Meal();
        public Meal meal4 = new Meal();
        public Meal meal5 = new Meal();

        public FoodPlan() { }
        public FoodPlan(User user, Meal meal1, Meal meal2, Meal meal3)
        {
            this.user1 = user;
            this.meal1 = meal1;
            this.meal2 = meal2;
            this.meal3 = meal3;

        }
        public FoodPlan(User user,Meal meal1, Meal meal2, Meal meal3, Meal meal4)
        {
            this.user1 = user;
            this.meal1 = meal1;
            this.meal2 = meal2;
            this.meal3 = meal3;
            this.meal4 = meal4;
        }

        public FoodPlan(User user,Meal meal1, Meal meal2, Meal meal3, Meal meal4, Meal meal5)
        {
            this.user1 = user;
            this.meal1 = meal1;
            this.meal2 = meal2;
            this.meal3 = meal3;
            this.meal4 = meal4;
            this.meal5 = meal5;
        }


    }
}
