using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;    //*local DB
using System.Data;
using System.Windows.Documents;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Security.Cryptography.X509Certificates;

namespace DietApp
{
    class DataBaseSolution
    {
        public static SqlConnection Get_DB_Connection()

        {
            string cn_String = Properties.Settings.Default.Connection_string;

            SqlConnection cn_connection = new SqlConnection(cn_String);

            if (cn_connection.State != ConnectionState.Open) cn_connection.Open();

            return cn_connection;

        }



        public static DataTable Get_DataTable(string SQL_Text)

        {


            SqlConnection cn_connection = Get_DB_Connection();

            DataTable table = new DataTable();

            SqlDataAdapter adapter = new SqlDataAdapter(SQL_Text, cn_connection);

            adapter.Fill(table);

            return table;


        }



        public static void Execute_SQL(string SQL_Text)

        {
            SqlConnection cn_connection = Get_DB_Connection();

            SqlCommand cmd_Command = new SqlCommand(SQL_Text, cn_connection);

            cmd_Command.ExecuteNonQuery();

        }




        public static void Close_DB_Connection()

        {

            string cn_String = Properties.Settings.Default.Connection_string;

            SqlConnection cn_connection = new SqlConnection(cn_String);

            if (cn_connection.State != ConnectionState.Closed) cn_connection.Close();

        }

        //-------------------</ Class: DB >-------------------
        public static bool FindUser(string nazwa)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [Uzytkownik] WHERE ([Nazwa] = @nazwa)", sqlConnection);
            check_User_Name.Parameters.AddWithValue("@Nazwa", nazwa);
            int UserExist = (int)check_User_Name.ExecuteScalar();

            if (UserExist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckPassword(string nazwa, string haslo)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [Uzytkownik] WHERE ([Nazwa] = @nazwa AND [Haslo] = @haslo)", sqlConnection);
            check_User_Name.Parameters.AddWithValue("@Nazwa", nazwa);
            check_User_Name.Parameters.AddWithValue("@Haslo", haslo);

            int UserExist = (int)check_User_Name.ExecuteScalar();

            if (UserExist == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static void AddUser(string nazwa, string haslo)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [Uzytkownik] WHERE ([Nazwa] = @nazwa)", sqlConnection);
            check_User_Name.Parameters.AddWithValue("@Nazwa", nazwa);
            int UserExist = (int)check_User_Name.ExecuteScalar();



            if (UserExist == 0)

            {

                //< add >

                string sql_Add = "INSERT INTO Uzytkownik (Nazwa, Haslo) Values ('" + nazwa + "' , '" + haslo + "');";

                DataBaseSolution.Execute_SQL(sql_Add);


                string sql_Add_Profile = "INSERT INTO ProfileUzytkownikow (IdUzytkownika) SELECT (Id) FROM Uzytkownik WHERE (Nazwa='" + nazwa + "') ;";

                DataBaseSolution.Execute_SQL(sql_Add_Profile);

                string sql_Add_UserData = "INSERT INTO DaneUzytkownika (IdUzytkownika) SELECT (Id) FROM Uzytkownik WHERE (Nazwa='" + nazwa + "') ;";

                DataBaseSolution.Execute_SQL(sql_Add_UserData);
                //</ add >

            }


        }

        public static int GetUserId(string nazwa)
        {
            int userId = 0;
            nazwa = nazwa.Replace("'", "''");
            string sql = "SELECT Id FROM [Uzytkownik] WHERE ([Nazwa] = @nazwa);";
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@Nazwa", nazwa);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    userId = int.Parse((reader["Id"]).ToString());
                }
            }

            return (int)userId;
        }
        /* public static int GetProfileId(int id)
         {
             Int32 profileId=0;
             string sql = "SELECT Id FROM [ProfileUzytkownikow] WHERE ([IdUzytkownika] = @id)";
             SqlConnection sqlConnection = new SqlConnection();
             sqlConnection = DataBaseSolution.Get_DB_Connection();
             using (sqlConnection)
             {
                 SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                 cmd.Parameters.Add("@IdUzytkownika", SqlDbType.Int);
                 cmd.Parameters["@IdUzytkownika"].Value = id;
                 try
                 {
                     sqlConnection.Open();
                     profileId = (Int32)cmd.ExecuteScalar();
                 }
                 catch (Exception ex)
                 {
                     Console.WriteLine(ex.Message);
                 }
             }
             return (int)profileId;
         }
         */
        public static void UpdateUserData(User user)
        {
            var Username = user.Username.Trim();
            var UserSex = user.UserSex.Trim();
            var UserType = user.UserType.Trim();
            var UserdailyActivity = user.UserdailyActivity.Trim();
            var UserTSIntensivity = user.UserTSIntensivity.Trim();
            var UserTAIntensivity = user.UserTAIntensivity.Trim();
            var UserFoodRestrictions = user.UserFoodRestrictions.Trim();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql = "UPDATE DaneUzytkownika SET Plec = @plec,Wiek = @wiek, Waga=@waga, Wzrost=@wzrost,Somatotyp=@typ, DziennaAktywnosc=@da,TStygodniowo=@tst, TSdziennie=@tsd,TSintensywnosc=@tsi,TAtygodniowo=@tat, TAdziennie=@tad,TAintensywnosc=@tai, Nietolerancje=@nietolerancje, LiczbaPosilkow=@liczba WHERE IdUzytkownika=(Select Id from Uzytkownik Where ([Nazwa] = @nazwa));";
            SqlCommand command = new SqlCommand(sql, sqlConnection);
            command.Parameters.AddWithValue("@plec", UserSex);
            command.Parameters.AddWithValue("@waga", user.UserWeight);
            command.Parameters.AddWithValue("@wiek", user.UserAge);
            command.Parameters.AddWithValue("@wzrost", user.UserHeight);
            command.Parameters.AddWithValue("@typ", UserType);
            command.Parameters.AddWithValue("@da", UserdailyActivity);
            command.Parameters.AddWithValue("@tst", user.UserTSWeekly);
            command.Parameters.AddWithValue("@tsd", user.UserTSDaily);
            command.Parameters.AddWithValue("@tsi", UserTSIntensivity);
            command.Parameters.AddWithValue("@tat", user.UserTAWeekly);
            command.Parameters.AddWithValue("@tad", user.UserTADaily);
            command.Parameters.AddWithValue("@tai", UserTAIntensivity);
            command.Parameters.AddWithValue("@nietolerancje", UserFoodRestrictions);
            command.Parameters.AddWithValue("@nazwa", Username);
            command.Parameters.AddWithValue("@liczba", user.UserMealNumber);
            command.ExecuteNonQuery();
        }


        public static void GetUserData(User user)
        {

            var id = (GetUserId(user.Username));
            string sql = "SELECT * FROM [DaneUzytkownika] WHERE ([IdUzytkownika] = " + @id + ");";
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@IdUzytkownika", id);

            using (SqlDataReader read = cmd.ExecuteReader())
            {
                if (read.Read())
                {
                    user.UserWeight = int.Parse((read["Waga"]).ToString());
                    user.UserHeight = int.Parse((read["Wzrost"]).ToString());
                    user.UserType = (read["Somatotyp"]).ToString();
                    user.UserdailyActivity = (read["DziennaAktywnosc"]).ToString();
                    user.UserTSWeekly = int.Parse((read["TStygodniowo"]).ToString());
                    user.UserTSDaily = int.Parse((read["TSdziennie"]).ToString());
                    user.UserTSIntensivity = (read["TSintensywnosc"]).ToString();
                    user.UserTAIntensivity = (read["TAintensywnosc"]).ToString();
                    user.UserTADaily = int.Parse((read["TAdziennie"]).ToString());
                    user.UserTAWeekly = int.Parse((read["TAtygodniowo"]).ToString());
                    user.UserFoodRestrictions = (read["Nietolerancje"]).ToString();
                    user.UserMealNumber = int.Parse((read["LiczbaPosilkow"]).ToString());
                }
            }

        }

        public static void DeleteUser(User user)
        {

            var id = (GetUserId(user.Username));
            string sql = "DELETE FROM [DaneUzytkownika] WHERE ([IdUzytkownika] = " + @id + ");";
            string sql1 = "DELETE FROM [ProfileUzytkownikow] WHERE ([IdUzytkownika] = " + @id + ");";
            string sql2 = "DELETE FROM [Uzytkownik] WHERE ([Id] = " + @id + ");";

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@IdUzytkownika", id);
            SqlCommand cmd1 = new SqlCommand(sql1, sqlConnection);
            cmd1.Parameters.AddWithValue("@IdUzytkownika", id);
            SqlCommand cmd2 = new SqlCommand(sql2, sqlConnection);
            cmd2.Parameters.AddWithValue("@Id", id);

            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
        }




        public static void AddKCAL(int id, int kcal)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand check_User_Name = new SqlCommand("SELECT COUNT(*) FROM [PlanZywieniowy] WHERE ([IdUzytkownika] = " + @id + ");", sqlConnection);
            check_User_Name.Parameters.AddWithValue("@IdUzytkownika", id);
            int UserExist = (int)check_User_Name.ExecuteScalar();

            if (UserExist == 0)
            {

                string sql_Add = "INSERT INTO PlanZywieniowy (IdUzytkownika, DzienneKCAL) Values ('" + id + "' , '" + kcal + "');";
                DataBaseSolution.Execute_SQL(sql_Add);
            }
            else if (UserExist > 0)
            {

                string sql = "UPDATE PlanZywieniowy SET DzienneKCAL = @kcal WHERE ([IdUzytkownika] = " + @id + ");";
                SqlCommand command = new SqlCommand(sql, sqlConnection);
                command.Parameters.AddWithValue("@kcal", kcal);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        public static void ADDWeight(int id, float weight)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql_Add = "INSERT INTO DziennikUzytkownika (IdUzytkownika, Waga, Data) Values ('" + id + "' , '" + weight + "', '" + DateTime.Now + "');";
            DataBaseSolution.Execute_SQL(sql_Add);
        }

        public static List<Tuple<DateTime, float>> GetWeightList(int id)
        {
            List<Tuple<DateTime, float>> WeightList = new List<Tuple<DateTime, float>>();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            SqlCommand command = new SqlCommand("SELECT [Data],[Waga] FROM [DziennikUzytkownika] WHERE ([IdUzytkownika] = " + @id + ");", sqlConnection);
            command.Parameters.AddWithValue("@IdUzytkownika", id);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var date = DateTime.Parse((reader[0]).ToString());
                var weight = Int32.Parse((reader[1]).ToString());

                WeightList.Add(new Tuple<DateTime, float>(date, weight));
            }
            return WeightList;
        }

        public static Product SearchProduct(string name)
        {
            Product product1 = new Product();
            var name1 = (name).Replace("'", "''");
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql = "SELECT  COUNT(*) FROM [Produkty] WHERE ([Nazwa] = '" + name1 + "');";
            SqlCommand check_Name = new SqlCommand(sql, sqlConnection);
            check_Name.Parameters.AddWithValue("@Nazwa", @name1);
            int UserExist = (int)check_Name.ExecuteScalar();
            if (UserExist != 0)
            {
                string sql1 = "SELECT * FROM [Produkty] WHERE ([Nazwa] = '" + name1 + "');";
                SqlCommand cmd = new SqlCommand(sql1, sqlConnection);
                cmd.Parameters.AddWithValue("@Nazwa", @name1);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        product1.Name = (read["Nazwa"]).ToString();
                        product1.Quantity = int.Parse((read["Ilosc"]).ToString());
                        product1.Unit = (read["Jednostka"]).ToString();
                        product1.Kcal = float.Parse((read["KCAL"]).ToString());

                    }
                }
            }

            return product1;
        }

        public static void AddProduct(Product product)
        {

            var name = (product.Name).Replace("'", "''");
            int quan = (product.Quantity);
            var unit = (product.Unit).Replace("'", "''");
            float kcal = product.Kcal;
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql = "SELECT  COUNT(*) FROM [Produkty] WHERE ([Nazwa] = '" + name + "');";
            SqlCommand check_Name = new SqlCommand(sql, sqlConnection);
            check_Name.Parameters.AddWithValue("@Nazwa", @name);
            int ProductExist = (int)check_Name.ExecuteScalar();
            if (ProductExist == 0)
            {
                string sql_Add = "INSERT INTO Produkty (Nazwa, Ilosc, Jednostka, KCAL)  Values (@nazwa, @ilosc, @jednostka, @kcal);";
                SqlCommand command = new SqlCommand(sql_Add, sqlConnection);
                command.Parameters.AddWithValue("@nazwa", name);
                command.Parameters.AddWithValue("@ilosc", quan);
                command.Parameters.AddWithValue("@jednostka", unit);
                command.Parameters.AddWithValue("@kcal", kcal);
                command.ExecuteNonQuery();
                MessageBox.Show("Produkt został dodany");

            }
        }

        public static void AddRecipe(Meal meal)
        {
            var name = (meal.Name).Replace("'", "''");
            var daytime = (meal.Daytime).Trim();
            var desc = (meal.Description).Replace("'", "''");
            float kcal = meal.Caloricity;
            var skladniki = (meal.Ingredients).Replace("'", "''");
            var ograniczenia = (meal.FoodRestrictions11).Replace("'", "''");
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql_Add = "INSERT INTO Jadlospisy (Nazwa, PoraDnia, KCAL, Opis, Skladniki,Ograniczenia)  Values (@nazwa, @pd, @kcal, @opis, @skladniki, @ograniczenia);";
            SqlCommand command = new SqlCommand(sql_Add, sqlConnection);
            command.Parameters.AddWithValue("@nazwa", name);
            command.Parameters.AddWithValue("@pd", daytime);
            command.Parameters.AddWithValue("@kcal", kcal);
            command.Parameters.AddWithValue("@opis", desc);
            command.Parameters.AddWithValue("@skladniki", skladniki);
            command.Parameters.AddWithValue("@ograniczenia", ograniczenia);
            command.ExecuteNonQuery();
            MessageBox.Show("Przepis został dodany");
        }

        public static FoodPlan GenerateFoodPlan(User user)
        {
            Algorithm algorithm = new Algorithm(user);
            algorithm.CountMealCaloricty();
            FoodPlan foodPlan = new FoodPlan();
            //float kcal = algorithm.FinalKCALResult();

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();

            Meal meal1 = new Meal();
            Meal meal2 = new Meal();
            Meal meal3 = new Meal();
            Meal meal4 = new Meal();
            Meal meal5 = new Meal();
            string userRestrictions = user.UserFoodRestrictions;
            string resultRestricitons = "";
            String[] userRestrictions1 = userRestrictions.Split(',');

            /*  foreach (string uFR in userRestrictions1)
              {
                  if (uFR == "False") { resultRestricitons += "True,"; }
                  else { resultRestricitons += "False,"; }
              }
              resultRestricitons = resultRestricitons.Substring(0, resultRestricitons.Length - 1);
              resultRestricitons = "%" + resultRestricitons + "%";
              resultRestricitons.Trim();*/
            resultRestricitons = userRestrictions;

            if (user.UserMealNumber == 3)
            {
                //ograniczenia
                string sql1 = "SELECT TOP 1 * FROM  Jadlospisy WHERE(([PoraDnia] LIKE '%Sniadanie%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[0] + "'))   ORDER BY NEWID() ; ";// AND ([Ograniczenia] LIKE ('" + resultRestricitons + "'))
                string sql2 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%Obiad%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[1] + "'))  ORDER BY NEWID() ; ";
                string sql3 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%Kolacja%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[2] + "'))  ORDER BY NEWID() ; ";

                SqlCommand cmd = new SqlCommand(sql1, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal1.MealId = Int32.Parse(read["Id"].ToString());
                        meal1.Name = (read["Nazwa"]).ToString();
                        meal1.Daytime = (read["PoraDnia"]).ToString();
                        meal1.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal1.Description = (read["Opis"]).ToString();
                        meal1.Ingredients = (read["Skladniki"]).ToString();
                        meal1.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql2, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal2.MealId = Int32.Parse(read["Id"].ToString());
                        meal2.Name = (read["Nazwa"]).ToString();
                        meal2.Daytime = (read["PoraDnia"]).ToString();
                        meal2.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal2.Description = (read["Opis"]).ToString();
                        meal2.Ingredients = (read["Skladniki"]).ToString();
                        meal2.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql3, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal3.MealId = Int32.Parse(read["Id"].ToString());
                        meal3.Name = (read["Nazwa"]).ToString();
                        meal3.Daytime = (read["PoraDnia"]).ToString();
                        meal3.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal3.Description = (read["Opis"]).ToString();
                        meal3.Ingredients = (read["Skladniki"]).ToString();
                        meal3.FoodRestrictions11 = (read["Ograniczenia"]).ToString();
            

                    }
                }
              
                foodPlan = new FoodPlan(user, meal1, meal2, meal3);
            }

            if (user.UserMealNumber == 4)
            {
                string sql1 = "SELECT TOP 1 * FROM  Jadlospisy WHERE(([PoraDnia] LIKE '%Sniadanie%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[0] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; ";
                string sql2 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%II sniadanie/podwieczorek%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[1] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; ";
                string sql3 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%Obiad%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[2] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; ";
                string sql4 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%Kolacja%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[3] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; ";
                SqlCommand cmd = new SqlCommand(sql1, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal1.MealId = Int32.Parse(read["Id"].ToString());
                        meal1.Name = (read["Nazwa"]).ToString();
                        meal1.Daytime = (read["PoraDnia"]).ToString();
                        meal1.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal1.Description = (read["Opis"]).ToString();
                        meal1.Ingredients = (read["Skladniki"]).ToString();
                        meal1.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql2, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal2.MealId = Int32.Parse(read["Id"].ToString());
                        meal2.Name = (read["Nazwa"]).ToString();
                        meal2.Daytime = (read["PoraDnia"]).ToString();
                        meal2.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal2.Description = (read["Opis"]).ToString();
                        meal2.Ingredients = (read["Skladniki"]).ToString();
                        meal2.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql3, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal3.MealId = Int32.Parse(read["Id"].ToString());
                        meal3.Name = (read["Nazwa"]).ToString();
                        meal3.Daytime = (read["PoraDnia"]).ToString();
                        meal3.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal3.Description = (read["Opis"]).ToString();
                        meal3.Ingredients = (read["Skladniki"]).ToString();
                        meal3.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql4, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal4.MealId = Int32.Parse(read["Id"].ToString());
                        meal4.Name = (read["Nazwa"]).ToString();
                        meal4.Daytime = (read["PoraDnia"]).ToString();
                        meal4.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal4.Description = (read["Opis"]).ToString();
                        meal4.Ingredients = (read["Skladniki"]).ToString();
                        meal4.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                foodPlan = new FoodPlan(user, meal1, meal2, meal3, meal4);
            }

            if (user.UserMealNumber == 5)
            {
                string sql1 = "SELECT TOP 1 * FROM  Jadlospisy WHERE(([PoraDnia] LIKE '%Sniadanie%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[0] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; "; //AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))
                string sql2 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%II sniadanie/podwieczorek%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[1] + "') )  ORDER BY NEWID() ; ";//AND ([Ograniczenia] LIKE ('" + resultRestricitons + "'))
                string sql3 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%Obiad%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[2] + "') )  ORDER BY NEWID() ; ";//AND ([Ograniczenia] LIKE ('" + resultRestricitons + "'))
                string sql4 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%II sniadanie/podwieczorek%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[3] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; "; SqlCommand cmd = new SqlCommand(sql1, sqlConnection);
                string sql5 = "SELECT TOP 1 * FROM  Jadlospisy WHERE( ([PoraDnia] LIKE '%Kolacja%') AND ([KCAL] LIKE '" + user.UserMealCaloricty[4] + "') AND ([Ograniczenia] LIKE ('" + resultRestricitons + "')))  ORDER BY NEWID() ; ";

                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal1.MealId = Int32.Parse(read["Id"].ToString());
                        meal1.Name = (read["Nazwa"]).ToString();
                        meal1.Daytime = (read["PoraDnia"]).ToString();
                        meal1.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal1.Description = (read["Opis"]).ToString();
                        meal1.Ingredients = (read["Skladniki"]).ToString();
                        meal1.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql2, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal2.MealId = Int32.Parse(read["Id"].ToString());
                        meal2.Name = (read["Nazwa"]).ToString();
                        meal2.Daytime = (read["PoraDnia"]).ToString();
                        meal2.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal2.Description = (read["Opis"]).ToString();
                        meal2.Ingredients = (read["Skladniki"]).ToString();
                        meal2.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql3, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal3.MealId = Int32.Parse(read["Id"].ToString());
                        meal3.Name = (read["Nazwa"]).ToString();
                        meal3.Daytime = (read["PoraDnia"]).ToString();
                        meal3.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal3.Description = (read["Opis"]).ToString();
                        meal3.Ingredients = (read["Skladniki"]).ToString();
                        meal3.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql4, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal4.MealId = Int32.Parse(read["Id"].ToString());
                        meal4.Name = (read["Nazwa"]).ToString();
                        meal4.Daytime = (read["PoraDnia"]).ToString();
                        meal4.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal4.Description = (read["Opis"]).ToString();
                        meal4.Ingredients = (read["Skladniki"]).ToString();
                        meal4.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                cmd = new SqlCommand(sql5, sqlConnection);
                using (SqlDataReader read = cmd.ExecuteReader())
                {
                    if (read.Read())
                    {
                        meal5.MealId = Int32.Parse(read["Id"].ToString());
                        meal5.Name = (read["Nazwa"]).ToString();
                        meal5.Daytime = (read["PoraDnia"]).ToString();
                        meal5.Caloricity = float.Parse((read["KCAL"]).ToString());
                        meal5.Description = (read["Opis"]).ToString();
                        meal5.Ingredients = (read["Skladniki"]).ToString();
                        meal5.FoodRestrictions11 = (read["Ograniczenia"]).ToString();

                    }
                }
                foodPlan = new FoodPlan(user, meal1, meal2, meal3, meal4, meal5);
            }
            return foodPlan;
        }

        public static void SaveFoodPlan(FoodPlan food)
        {
            Algorithm algorithm = new Algorithm(food.user1);
            float kcal = algorithm.FinalKCALResult();

            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();

            SqlCommand check_1 = new SqlCommand("SELECT COUNT(*) FROM [PlanZywieniowy] WHERE (([PoraDnia] LIKE '%Sniadanie%') AND [IdUzytkownika] = '" + food.user1.UserId + "');", sqlConnection);
            int Exist1 = (int)check_1.ExecuteScalar();
            SqlCommand check_2 = new SqlCommand("SELECT COUNT(*) FROM [PlanZywieniowy] WHERE (([PoraDnia] LIKE '%II sniadanie/podwieczorek%') AND [IdUzytkownika] = '" + food.user1.UserId + "');", sqlConnection);
            int Exist2 = (int)check_1.ExecuteScalar();
            SqlCommand check_3 = new SqlCommand("SELECT COUNT(*) FROM [PlanZywieniowy] WHERE ((([PoraDnia] LIKE '%Obiad%') AND [IdUzytkownika] = '" + food.user1.UserId + "');", sqlConnection);
            int Exist3 = (int)check_1.ExecuteScalar();
            SqlCommand check_5 = new SqlCommand("SELECT COUNT(*) FROM [PlanZywieniowy] (([PoraDnia] LIKE '%Kolacja%') AND [IdUzytkownika] = '" + food.user1.UserId + "');", sqlConnection);
            int Exist4 = (int)check_1.ExecuteScalar();
            MessageBox.Show((food.user1.UserMealCaloricty[0], food.user1.UserMealCaloricty[1], food.user1.UserMealCaloricty[2]).ToString());

            if (food.user1.UserMealNumber == 3)
            {
                if ((Exist1 == 0) && (Exist3 == 0) && (Exist4 == 0))

                {

                    string sql1 = "INSERT INTO PlanZywieniowy (IdUzytkownika, DzienneKCAL, PoraDnia, IDJadlospisu)  Values (@id, @kcal, @pd, @idj);";
                    SqlCommand command = new SqlCommand(sql1, sqlConnection);
                    command.Parameters.AddWithValue("@id", food.user1.UserId);
                    command.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command.Parameters.AddWithValue("@pd", food.meal1.Daytime);
                    command.Parameters.AddWithValue("@idj", food.meal1.MealId);
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand(sql1, sqlConnection);
                    command1.Parameters.AddWithValue("@id", food.user1.UserId);
                    command1.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command1.Parameters.AddWithValue("@pd", food.meal2.Daytime);
                    command1.Parameters.AddWithValue("@idj", food.meal2.MealId);
                    command1.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand(sql1, sqlConnection);
                    command2.Parameters.AddWithValue("@id", food.user1.UserId);
                    command2.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command2.Parameters.AddWithValue("@pd", food.meal3.Daytime);
                    command2.Parameters.AddWithValue("@idj", food.meal3.MealId);
                    command2.ExecuteNonQuery();


                }
                else if ((Exist1 > 0))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command = new SqlCommand(sql1, sqlConnection);
                    command.Parameters.AddWithValue("@id", food.user1.UserId);
                    command.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command.Parameters.AddWithValue("@pd", food.meal1.Daytime);
                    command.Parameters.AddWithValue("@idj", food.meal1.MealId);
                    command.ExecuteNonQuery();
                }

                else if ((Exist3 > 0))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command1 = new SqlCommand(sql1, sqlConnection);
                    command1.Parameters.AddWithValue("@id", food.user1.UserId);
                    command1.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command1.Parameters.AddWithValue("@pd", food.meal2.Daytime);
                    command1.Parameters.AddWithValue("@idj", food.meal2.MealId);
                    command1.ExecuteNonQuery();
                }
                else if ((Exist4 > 0))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command2 = new SqlCommand(sql1, sqlConnection);
                    command2.Parameters.AddWithValue("@id", food.user1.UserId);
                    command2.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command2.Parameters.AddWithValue("@pd", food.meal3.Daytime);
                    command2.Parameters.AddWithValue("@idj", food.meal3.MealId);
                    command2.ExecuteNonQuery();
                }

            }
            if (food.user1.UserMealNumber > 3)
            {
                if ((Exist1 == 0) && ((Exist2 == 0 || Exist2 == 1)) && (Exist3 == 0) && (Exist4 == 0))

                {

                    string sql1 = "INSERT INTO PlanZywieniowy (IdUzytkownika, DzienneKCAL, PoraDnia, IDJadlospisu)  Values (@id, @kcal, @pd, @idj);";
                    SqlCommand command = new SqlCommand(sql1, sqlConnection);
                    command.Parameters.AddWithValue("@id", food.user1.UserId);
                    command.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command.Parameters.AddWithValue("@pd", food.meal1.Daytime);
                    command.Parameters.AddWithValue("@idj", food.meal1.MealId);
                    command.ExecuteNonQuery();

                    SqlCommand command1 = new SqlCommand(sql1, sqlConnection);
                    command1.Parameters.AddWithValue("@id", food.user1.UserId);
                    command1.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command1.Parameters.AddWithValue("@pd", food.meal2.Daytime);
                    command1.Parameters.AddWithValue("@idj", food.meal2.MealId);
                    command1.ExecuteNonQuery();

                    SqlCommand command2 = new SqlCommand(sql1, sqlConnection);
                    command2.Parameters.AddWithValue("@id", food.user1.UserId);
                    command2.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command2.Parameters.AddWithValue("@pd", food.meal3.Daytime);
                    command2.Parameters.AddWithValue("@idj", food.meal3.MealId);
                    command2.ExecuteNonQuery();

                    if ((food.user1.UserMealNumber == 4) || (food.user1.UserMealNumber == 5))
                    {
                        SqlCommand command3 = new SqlCommand(sql1, sqlConnection);
                        command3.Parameters.AddWithValue("@id", food.user1.UserId);
                        command3.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                        command3.Parameters.AddWithValue("@pd", food.meal4.Daytime);
                        command3.Parameters.AddWithValue("@idj", food.meal4.MealId);
                        command3.ExecuteNonQuery();
                    }

                    if ((food.user1.UserMealNumber == 5))
                    {
                        SqlCommand command4 = new SqlCommand(sql1, sqlConnection);
                        command4.Parameters.AddWithValue("@id", food.user1.UserId);
                        command4.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                        command4.Parameters.AddWithValue("@pd", food.meal5.Daytime);
                        command4.Parameters.AddWithValue("@idj", food.meal5.MealId);
                        command4.ExecuteNonQuery();
                    }
                }
                else if ((Exist1 > 0))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command = new SqlCommand(sql1, sqlConnection);
                    command.Parameters.AddWithValue("@id", food.user1.UserId);
                    command.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command.Parameters.AddWithValue("@pd", food.meal1.Daytime);
                    command.Parameters.AddWithValue("@idj", food.meal1.MealId);
                    command.ExecuteNonQuery();
                }
                else if ((Exist3 > 0))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command1 = new SqlCommand(sql1, sqlConnection);
                    command1.Parameters.AddWithValue("@id", food.user1.UserId);
                    command1.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command1.Parameters.AddWithValue("@pd", food.meal2.Daytime);
                    command1.Parameters.AddWithValue("@idj", food.meal2.MealId);
                    command1.ExecuteNonQuery();
                }
                else if ((Exist2 > 1))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command2 = new SqlCommand(sql1, sqlConnection);
                    command2.Parameters.AddWithValue("@id", food.user1.UserId);
                    command2.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command2.Parameters.AddWithValue("@pd", food.meal3.Daytime);
                    command2.Parameters.AddWithValue("@idj", food.meal3.MealId);
                    command2.ExecuteNonQuery();
                }
                else if ((Exist4 > 0))
                {
                    string sql1 = "UPDATE PlanZywieniowy SET  DzienneKCAL = @kcal, PoraDnia = @pd , IDJadlospisu = @idj WHERE IdUzytkownika = @id;";
                    SqlCommand command2 = new SqlCommand(sql1, sqlConnection);
                    command2.Parameters.AddWithValue("@id", food.user1.UserId);
                    command2.Parameters.AddWithValue("@kcal", algorithm.FinalKCALResult());
                    command2.Parameters.AddWithValue("@pd", food.meal3.Daytime);
                    command2.Parameters.AddWithValue("@idj", food.meal3.MealId);
                    command2.ExecuteNonQuery();
                }
            }
        } 


        public static bool FoodPlanExist(User user)
        {
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql = "SELECT Count(*) FROM [PlanZywieniowy] WHERE IdUzytkownika = @id;";
            SqlCommand check_User_Name = new SqlCommand(sql, sqlConnection);
            check_User_Name.Parameters.AddWithValue("@id", user.UserId);
            int UserExist = (int)check_User_Name.ExecuteScalar();
            if(UserExist ==0) { return false; }    
            return true;
        }

        public static Meal GetMeal(int id)
        {
            Meal meal = new Meal();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql = "SELECT* FROM [Jadlospisy] WHERE Id = @id;";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);
            using (SqlDataReader read = cmd.ExecuteReader())
            {
                if (read.Read())
                {
                    meal.Name = (read["Nazwa"].ToString());
                    meal.Daytime= (read["PoraDnia"].ToString());
                    meal.Caloricity = float.Parse(read["KCAL"].ToString());
                    meal.Description = (read["Opis"].ToString());
                    meal.Ingredients = (read["Skladniki"].ToString());
                    meal.FoodRestrictions11 = (read["Ograniczenia"].ToString());
                }
            }
            return meal;
        }
        public static FoodPlan GetFoodPlan(User user)
        {
            FoodPlan food = new FoodPlan();
            SqlConnection sqlConnection = new SqlConnection();
            sqlConnection = DataBaseSolution.Get_DB_Connection();
            string sql = "SELECT * FROM [PlanZywieniowy] WHERE IdUzytkownika = @id;";
            SqlCommand cmd1 = new SqlCommand(sql, sqlConnection);
            cmd1.Parameters.AddWithValue("@id", user.UserId);
            using (SqlDataReader read = cmd1.ExecuteReader())
            {
                if (read.Read())
                {
                    food.meal1.MealId = Int32.Parse(read["IdJadlospisu"].ToString());
                }
                
            }
            food.meal1 = GetMeal(food.meal1.MealId);
            string sql1 = "SELECT * FROM  [PlanZywieniowy] WHERE(([PoraDnia] LIKE '%Sniadanie%') AND (IdUzytkownika = @id)); ";
            SqlCommand cmd2 = new SqlCommand(sql1, sqlConnection);
            cmd2.Parameters.AddWithValue("@id", user.UserId);
            using (SqlDataReader read = cmd2.ExecuteReader())
            {
                if (read.Read())
                {
                    food.meal2.MealId = Int32.Parse(read["IdJadlospisu"].ToString());
                }
            }
            food.meal2 = GetMeal(food.meal2.MealId);
            string sql2 = "SELECT * FROM  [PlanZywieniowy] WHERE( ([PoraDnia] LIKE '%II sniadanie/podwieczorek%') AND (IdUzytkownika = @id)); ";
            SqlCommand cmd3 = new SqlCommand(sql2, sqlConnection);
            cmd3.Parameters.AddWithValue("@id", user.UserId);
            using (SqlDataReader read = cmd3.ExecuteReader())
            {
                if (read.Read())
                {
                    food.meal3.MealId = Int32.Parse(read["IdJadlospisu"].ToString());
                }
            }
            food.meal3 = GetMeal(food.meal3.MealId);
            if ((user.UserMealNumber == 4) || (user.UserMealNumber == 5))
            {
                string sql3 = "SELECT * FROM  [PlanZywieniowy] WHERE( ([PoraDnia] LIKE '%Obiad%') AND (IdUzytkownika = @id)); ";
                SqlCommand cmd4 = new SqlCommand(sql3, sqlConnection);
                cmd4.Parameters.AddWithValue("@id", user.UserId);
                using (SqlDataReader read = cmd4.ExecuteReader())
                {
                    if (read.Read())
                    {
                        food.meal4.MealId = Int32.Parse(read["IdJadlospisu"].ToString());
                    }
                }
                food.meal4 = GetMeal(food.meal4.MealId);
            }
            if (user.UserMealNumber == 5)
            {
                string sql4 = "SELECT * FROM  [PlanZywieniowy] WHERE( ([PoraDnia] LIKE '%Kolacja%') AND (IdUzytkownika = @id)); ";
                SqlCommand cmd5 = new SqlCommand(sql4, sqlConnection);
                cmd5.Parameters.AddWithValue("@id", user.UserId);
                using (SqlDataReader read = cmd5.ExecuteReader())
                {
                    if (read.Read())
                    {
                        food.meal5.MealId = Int32.Parse(read["IdJadlospisu"].ToString());
                    }
                }
                food.meal5 = GetMeal(food.meal5.MealId);
            }
            return food;
        }

        
    }
    }
