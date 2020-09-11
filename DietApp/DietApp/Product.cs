using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DietApp
{
    public class Product
    {
        private string name;
        private int quantity;
        private string unit;
        private float kcal;

        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public string Unit { get => unit; set => unit = value; }
        public float Kcal { get => kcal; set => kcal = value; }
    }
}
