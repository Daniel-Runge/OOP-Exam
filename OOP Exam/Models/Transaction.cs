using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public abstract class Transaction
    {
        private static int _nextID = 1; // use uint?

        protected Transaction(User user, decimal amount)
        {
            ID = _nextID++;
            User = user ?? throw new ArgumentNullException(nameof(user));
            TransactionDate = DateTime.Now;
            Amount = amount;
        }

        public int ID { get; }
        public User User { get; }
        public DateTime TransactionDate { get; }
        public decimal Amount { get; }

        public override string ToString() => $"{ID} {User} {Amount:C2} {TransactionDate.ToLongDateString()}";
        public abstract void Execute();
    }
}
