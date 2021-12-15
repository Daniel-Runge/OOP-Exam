using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public record InsertCashTransaction(User User, uint Amount) : Transaction(User, Amount)
    {
        public override void Execute()
        {
            User.Balance += (int)Amount;
        }

        public override string? ToString()
        {
            return "Deposit " + base.ToString();
        }
    }
}
