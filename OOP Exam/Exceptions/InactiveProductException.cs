using OOP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Exceptions
{
    public class InactiveProductException : ArgumentException
    {
        public InactiveProductException(Product product) : base($"{product.Id} {product.Name} is not active. Flag: {product.Active}")
        {
            Product = product;
        }
        public Product Product { get; }
    }
}
