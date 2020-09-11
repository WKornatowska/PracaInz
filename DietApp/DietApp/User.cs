using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp
{
    public class User
    {
        private string username;
        private string password;
        private int userId;
        private int profileId;
        private int userDataId;
        private string userSex;
        private int userAge;
        private float userWeight;
        private float userHeight;
        private string userType;
        private string userdailyActivity;
        private int userTSWeekly;
        private int userTSDaily;
        private string userTSIntensivity;
        private int userTAWeekly;
        private int userTADaily;
        private string userTAIntensivity;
        private string userFoodRestrictions;
        private int userMealNumber;
        private int[] userMealCaloricty = new int[5];
      
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public int UserId { get => userId; set => userId = value; }
        public int ProfileId { get => profileId; set => profileId = value; }
        public int UserDataId { get => userDataId; set => userDataId = value; }
        public string UserSex { get => userSex; set => userSex = value; }
        public int UserAge { get => userAge; set => userAge = value; }
        public float UserWeight { get => userWeight; set => userWeight = value; }
        public float UserHeight { get => userHeight; set => userHeight = value; }
        public string UserType { get => userType; set => userType = value; }
        public int UserTSWeekly { get => userTSWeekly; set => userTSWeekly = value; }
        public int UserTSDaily { get => userTSDaily; set => userTSDaily = value; }
        public string UserTSIntensivity { get => userTSIntensivity; set => userTSIntensivity = value; }
        public int UserTAWeekly { get => userTAWeekly; set => userTAWeekly = value; }
        public int UserTADaily { get => userTADaily; set => userTADaily = value; }
        public string UserTAIntensivity { get => userTAIntensivity; set => userTAIntensivity = value; }
        public string UserFoodRestrictions { get => userFoodRestrictions; set => userFoodRestrictions = value; }
        public string UserdailyActivity { get => userdailyActivity; set => userdailyActivity = value; }
        public int UserMealNumber { get => userMealNumber; set => userMealNumber = value; }
        public int[] UserMealCaloricty { get => userMealCaloricty; set => userMealCaloricty = value; }

    }
}
