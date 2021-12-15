using OOP_Exam.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public record BuyTransaction(User User, Product Product) : Transaction(User, Product.Price)
    {
        public override void Execute()
        {
            if (Product.Active is not 1) throw new InactiveProductException(Product);
            if (User.Balance < Amount && !Product.CanBeBoughtOnCredit) throw new InsufficientCreditsException(User, Product);

            User.Balance -= (int)Amount;
        }

        public override string? ToString()
        {
            return "Purchase " + base.ToString() + $", Product: {Product.Name}";
        }
    }
}
