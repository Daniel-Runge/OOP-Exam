using OOP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Exceptions
{
    public class NonExistentProductException : ArgumentException
    {
        public NonExistentProductException(uint id) : base($"No product with id:{id} exists")
        {
            Id = id;
        }
        public uint Id { get; }
    }
}
