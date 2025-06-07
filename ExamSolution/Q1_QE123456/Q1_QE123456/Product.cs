using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Q1_QE123456
{
    public delegate double TaxCalculation(Product p);

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }

        public Product(int id, string name, string type, double price)
        {
            Id = id;
            Name = name;
            Type = type;
            Price = price;
        }

        public void Display(TaxCalculation c)
        {
            double tax = c(this);
            Console.WriteLine($"Product: Id:{Id} - Name:{Name} - Type:{Type} - Price:{Price} - Tax:{tax}");

        }

        public static double Display(List<Product> products, TaxCalculation c)
        {
            foreach (Product item in products)
            {
                item.Display(c);
            }
            return 0;
        }




    }
}
