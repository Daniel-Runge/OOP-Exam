using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public class Product
    {
        private static int _nextID = 1;
        private decimal _price;

        private Product()
        {
            ID = _nextID++;
        }

        public Product(int id, string name, int price, int active) : this()
        {
            ID = id;
            Name = name ?? "";
            Price = price * 0.01M;
            Active = active == 1;
        }

        public int ID { get; }
        public string Name { get; init; }
        public decimal Price
        {
            get => _price;
            set => _price = value < 0 ? 0 : value;
        }
        // In the data, this is handled by a zero or 1 value. This got me thinking, it might very well be expandable to have an amount instead. Automate restock alerts?
        public virtual bool Active { get; set; }
        public bool CanBeBoughtOnCredit { get; set; } // There is no equivalent field in the data

        public override string ToString() // Fix magic numbers. Should be calculated to longest ID and name, or removed completely
        {
            return $"{ID,-4} | {Name,-40} | {Price,13:C2}"; //This shit puts kr. behind automatically! 
        }

        public static Product FromCsvString(string csvalues, char csvSeparator)
        {
            string[] values = csvalues.Split(csvSeparator);
            return new Product()
            {
                Name = CleanStringFromCsv(values[1]),
                Price = Convert.ToDecimal(values[2]) * 0.01M,
                Active = Convert.ToInt32(values[3]) == 1

            };
        }
        private static string RemoveHtmlTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static string CleanStringFromCsv(string source)
        {
            string htmlTagFreeString = RemoveHtmlTags(source);
            return htmlTagFreeString.Trim('"');
        }
    }
}
