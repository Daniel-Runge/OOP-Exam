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

        public SeasonalProduct() : base()
        {
        }

        public SeasonalProduct(string name, uint price, int active, DateTime startDate, DateTime endDate) : base(name, price, active)
        {
            SeasonStartDate = startDate;
            SeasonEndDate = endDate;
        }

        public override int Active
        {
            get => base.Active == 1
                   && DateTime.Now >= SeasonStartDate
                   && DateTime.Now <= SeasonEndDate 
                   ? 1 : 0;

            set => base.Active = value;
        }
    }
}
