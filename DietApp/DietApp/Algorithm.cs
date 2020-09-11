using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DietApp
{
    public class Algorithm
    {

        public User user = new User();
        private float BMI, BMR, TEF, NEAT, TEA, EPOC, TDEE, KCAL;
        public Algorithm(User user1) { this.user = user1; }

        public void CountBMI(){  BMI = (float)(user.UserWeight / user.UserHeight);  }
        public void CountBMR()
        {
            if (user.UserSex == "Kobieta")
            {
                BMR = (float)(((9.99) * user.UserWeight) + (6.25 * user.UserHeight) - (4.92 * user.UserAge) - 161);
            }
            else
            {
                BMR = (float)(((9.99) * user.UserWeight) + (6.25 * user.UserHeight) - (4.92 * user.UserAge) + 5);
            }
        }
        public void CountTEF() { TEF = (float)(BMR * 0.1); }
        public void CountNEAT()
        {
            if (user.UserType == "Typ endomorficzny")
            {
                if (user.UserdailyActivity == "Niska") NEAT = (float)200;
                if (user.UserdailyActivity == "Średnia") NEAT = (float)300;
                if (user.UserdailyActivity == "Wysoka") NEAT = (float)400;
            }
            if (user.UserType == "Typ ektomorficzny")
            {
                if (user.UserdailyActivity == "Niska") NEAT = (float)700;
                if (user.UserdailyActivity == "Średnia") NEAT = (float)800;
                if (user.UserdailyActivity == "Wysoka") NEAT = (float)900;
            }
            if (user.UserType == "Typ mezomorficzny")
            {
                if (user.UserdailyActivity == "Niska") NEAT = (float)400;
                if (user.UserdailyActivity == "Średnia") NEAT = (float)450;
                if (user.UserdailyActivity == "Wysoka") NEAT = (float)500;
            }
        }

        public void CountTEA_EPOC()
        {
            float TEA_S = 0, TEA_A = 0, EPOC_S = 0, EPOC_A = 0;
            if (user.UserTSIntensivity == "Niska")
            {
                TEA_S = (float)((user.UserTSWeekly * user.UserTSDaily * 7.0));
                EPOC_S = (float)(BMR * 0.04);
            }
            if (user.UserTSIntensivity == "Średnia")
            {
                TEA_S = (float)((user.UserTSWeekly * user.UserTSDaily * 8.0));
                EPOC_S = (float)(BMR * 0.055);
            }
            if (user.UserTSIntensivity == "Wysoka")
            {
                TEA_S = (float)((user.UserTSWeekly * user.UserTSDaily * 9.0));
                EPOC_S = (float)(BMR * 0.07);
            }
            if (user.UserTAIntensivity == "Niska")
            {
                TEA_A = (float)(user.UserTAWeekly * user.UserTADaily * 100 * 5.0);
                EPOC_A = (float)5.0;
            }
            if (user.UserTAIntensivity == "Średnia")
            {
                TEA_A = (float)(user.UserTAWeekly * user.UserTADaily * 100 * 8.0);
                EPOC_A = (float)35.0;
            }
            if (user.UserTAIntensivity == "Wysoka")
            {
                TEA_A = (float)(user.UserTAWeekly * user.UserTADaily * 100 * 10.0);
                EPOC_A = (float)180.0;
            }
            TEA = (float)((TEA_S + TEA_A) / 7);
            EPOC = (float)(EPOC_S + EPOC_A);
        }
        public void CountTDEE()
        {
            TDEE = (float)(BMR + TEF + NEAT + TEA + EPOC);
        }

        public float FinalKCALResult()
        {
            CountBMI(); CountBMR();   CountTEF(); CountNEAT(); CountTEA_EPOC();  CountTDEE();
            if (BMI < 18.5) KCAL = (float)(TDEE + 0.1 * TDEE);       
            if ((BMI >= 18.5) && (BMI < 24.99))  KCAL = (float)TDEE;
            if (BMI >= 24.99)   KCAL = (float)(TDEE - 0.1 * TDEE);
            return KCAL;
        }

        public void CountMealCaloricty()
        {
            FinalKCALResult();
            if (user.UserMealNumber == 3)
            {

                user.UserMealCaloricty[0] = Convert.ToInt32(Math.Round((0.35 * KCAL) / 50) * 50);
                user.UserMealCaloricty[1] = Convert.ToInt32(Math.Round((0.45 * KCAL) / 50) * 50);
                user.UserMealCaloricty[2] = Convert.ToInt32(Math.Round((0.2 * KCAL) / 50) * 50);
            }
            if (user.UserMealNumber == 4)
            {
                user.UserMealCaloricty[0] = Convert.ToInt32(Math.Round((0.3 * KCAL) / 50) * 50);
                user.UserMealCaloricty[1] = Convert.ToInt32(Math.Round((0.1 * KCAL) / 50) * 50);
                user.UserMealCaloricty[2] = Convert.ToInt32(Math.Round((0.4 * KCAL) / 50) * 50);
                user.UserMealCaloricty[3] = Convert.ToInt32(Math.Round((0.2 * KCAL) / 50) * 50);
            }
            if (user.UserMealNumber == 5)
            {
                user.UserMealCaloricty[0] = Convert.ToInt32(Math.Round((0.25 * KCAL) / 50) * 50);
                user.UserMealCaloricty[1] = Convert.ToInt32(Math.Round((0.1 * KCAL) / 50) * 50);
                user.UserMealCaloricty[2] = Convert.ToInt32(Math.Round((0.4 * KCAL) / 50) * 50);
                user.UserMealCaloricty[3] = Convert.ToInt32(Math.Round((0.1 * KCAL) / 50) * 50);
                user.UserMealCaloricty[4] = Convert.ToInt32(Math.Round((0.15 * KCAL) / 50) * 50);

            }
        }

        public FoodPlan GetFoodData()
        {
            CountMealCaloricty();
            FoodPlan food = new FoodPlan();
            food = DataBaseSolution.GenerateFoodPlan(user);
            DataBaseSolution.SaveFoodPlan(food);
            return food;
        }
    }
}
