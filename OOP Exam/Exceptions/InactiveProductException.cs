using OOP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Exceptions
{
    public class InactiveProductException : Exception
    {
        public InactiveProductException(Product product) : base($"{product} is not active.")
        {
            Product = product;
        }
        public Product Product { get; }
    }
}
