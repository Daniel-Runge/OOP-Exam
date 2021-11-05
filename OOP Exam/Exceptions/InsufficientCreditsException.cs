using OOP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Exceptions
{
    public class InsufficientCreditsException : Exception
    {
        public InsufficientCreditsException(User user, Product product) : base($"{user} has insufficient funds for {product}.")
        {
            User = user;
            Product = product;
        }
        public User User { get; }
        public Product Product { get; }
    }
}
