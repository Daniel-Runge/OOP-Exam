using OOP_Exam.Controller;
using OOP_Exam.Models;
using OOP_Exam.Services;
using OOP_Exam.User_Interface;
using System;
using System.Collections.Generic;

namespace OOP_Exam
{
    public delegate void UserBalanceNotification(User user, decimal balance);
    public delegate void TallySystemEvent(string command); // probably incomplete
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ICsvSerdeService csvSerde = new CsvService();

            ITallySystem tallySystem = new TallySystem(csvSerde);
            ITallySystemUI ui = new TallySystemCLI(tallySystem);

            TallySystemController tc = new(ui, tallySystem);

            ui.Start();
        }
    }
}
