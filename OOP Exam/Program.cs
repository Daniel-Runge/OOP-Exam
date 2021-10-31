using OOP_Exam.Models;
using System;

namespace OOP_Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            User daniel = new();
            daniel.Username = "daniel_91";
            Console.WriteLine(daniel.Username);
        }
    }
}
