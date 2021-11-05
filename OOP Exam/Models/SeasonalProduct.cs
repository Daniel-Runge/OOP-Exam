using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public class SeasonalProduct : Product
    {
        public DateTime SeasonStartDate { get; set; }
        public DateTime SeasonEndDate { get; set; }

        public SeasonalProduct(int id, string name, int price, int active) : base(id, name, price, active)
        {
        }

        public SeasonalProduct(int id, string name, int price, int active, DateTime startDate, DateTime endDate) : base(id, name, price, active)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }

        public override bool Active
        {
            get => base.Active
                   && DateTime.Now >= SeasonStartDate
                   && DateTime.Now <= SeasonEndDate;

            set => base.Active = value;
        }
    }
}
