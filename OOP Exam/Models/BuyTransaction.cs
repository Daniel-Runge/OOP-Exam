using OOP_Exam.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public class BuyTransaction : Transaction
    {
        public BuyTransaction(User user, Product product) : base(user, product.Price)
        {
            Product = product;
        }
        public Product Product { get; init; }

        public override string ToString() => $"Purchase | ({Product}) {base.ToString()}";

        public override void Execute()
        {
            if (User.Balance < Product.Price && !Product.CanBeBoughtOnCredit)
            {
                throw new InsufficientCreditsException(User, Product);
            }
            if (!Product.Active)
            {
                throw new InactiveProductException(Product);
            }
            User.Balance -= Amount;
        }
    }
}
