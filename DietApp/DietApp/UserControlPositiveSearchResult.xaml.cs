using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for UserControlPositiveSearchResult.xaml
    /// </summary>
    public partial class UserControlPositiveSearchResult : UserControl
    {
        private Product product1;
        public UserControlPositiveSearchResult(Product product)
        {
            InitializeComponent();
            product1 = product;
            nazwa.Text = product1.Name;
            jednostka.Text = product1.Unit;
            ilosc.Text = (product1.Quantity).ToString();
            kcal.Text = (product1.Kcal).ToString();
            
            
        }

    }
}
