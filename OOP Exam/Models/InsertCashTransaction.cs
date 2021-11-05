using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public class InsertCashTransaction : Transaction
    {
        public InsertCashTransaction(User user, decimal amount) : base(user, amount)
        {
        }

        public override string ToString() => $"Deposit | {base.ToString()}";
        public override void Execute() => User.Balance += Amount;

    }
}
