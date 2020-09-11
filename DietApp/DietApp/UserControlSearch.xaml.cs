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
    /// Interaction logic for UserControlSearch.xaml
    /// </summary>
    public partial class UserControlSearch : UserControl
    {
        public bool result = false;
        public UserControlSearch()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            UserControl usc = null;
            GridResult.Children.Clear();
            Product product = new Product();
            product = DataBaseSolution.SearchProduct((ProductName.Text).ToString());
            if (product.Name!= null)
            {
                ProductName.Clear();
                usc = new UserControlPositiveSearchResult(product);
                GridResult.Children.Add(usc);
            }
            else
            {
                usc = new UserControlNegativeSearchResult();
                GridResult.Children.Add(usc);
            }
        }
    }
}
