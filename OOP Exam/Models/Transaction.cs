using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public abstract record Transaction(User User, uint Amount)
    {
        private static uint _nextId = 1;
        public uint Id { get; init; } = _nextId++;
        public DateTime Date { get; init; } = DateTime.Now;

        public abstract void Execute();

        public override string? ToString()
        {
            return $"TransactionID: [{Id}], Date: [{Date.ToShortDateString()}], Amount: [{Amount * 0.01M:C2}], User: [{User.Username}]";
        }
    }
}
