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
        private static uint _nextId = 1;
        private uint _id;
        private uint _price;
        private string _name = "";

        public Product() : this("", 0, 0)
        {
        }

        public Product(string name, uint price, int active)
        {
            Id = _nextId++;
            Name = name;
            Price = price;
            Active = active;
        }

        public uint Id
        {
            get { return _id; }
            init
            {
                if (value < _nextId - 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Id already taken {value}");
                }
                _nextId = value + 1;
                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            init => _name = CleanString(value ?? "");
        }
        public uint Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public virtual int Active { get; set; } = 0;
        public bool CanBeBoughtOnCredit { get; set; } = false;

        public override string ToString() // Fix magic numbers. Should be calculated to longest ID and name, or removed completely
        {
            return $"{Id,-4} | {Name,-40} | {Price * 0.01M,13:C2}";
        }

        private static string RemoveHtmlTags(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        public static string CleanString(string source)
        {
            string htmlTagFreeString = RemoveHtmlTags(source);
            return htmlTagFreeString.Trim('"').Trim();
        }
    }
}
