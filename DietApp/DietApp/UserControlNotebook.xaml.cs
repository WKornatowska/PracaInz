using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
    /// Interaction logic for UserControlNotebook.xaml
    /// </summary>
    public partial class UserControlNotebook : UserControl
    {
        private User user1;
        private List<KeyValuePair<string, double>> valueList;
        private MainAppWindow mainApp;

        public UserControlNotebook()
        {
            InitializeComponent();
            ShowChart();
        }
        public UserControlNotebook(MainAppWindow main)
        {
            this.user1 =main.user;
            this.mainApp = main;
            InitializeComponent();
            ShowChart();
                
            }

        public void ShowChart()
        {
            List<Tuple<DateTime, float>> tuples;
            tuples = DataBaseSolution.GetWeightList(user1.UserId);
            if (tuples.Count() > 0)
            {
                valueList = new List<KeyValuePair<string, double>>();
                foreach (Tuple<DateTime, float> item in tuples)
                {
                    var date = (item.Item1).ToString("MM/dd/yyyy");
                    var weight = Math.Round(float.Parse(item.Item2.ToString()), 2);
                    valueList.Add(new KeyValuePair<string, double>(date, weight));
                }
                lineChart.DataContext = valueList;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            float weight = (float.Parse(WeightBox.Text));
            DataBaseSolution.ADDWeight(user1.UserId, weight);
            UserControl usc = null;
            GridMain.Children.Clear();
            usc = new UserControlNotebook(mainApp);
            GridMain.Children.Add(usc);
        }
    }
}
