using OOP_Exam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP_Exam.User_Interface
{
    public class TallySystemCLI : ITallySystemUI
    {
        private readonly ITallySystem _tallySystem;

        public TallySystemCLI(ITallySystem tallySystem)
        {
            _tallySystem = tallySystem;
            _tallySystem.UserBalanceWarning += DisplayUserBalanceWarning;
        }

        private void DisplayUserBalanceWarning(User user, decimal balance)
        {
            Console.WriteLine($"User [{user.Username}] balance is below 50 [{balance:C2}]");
        }

        public event TallySystemEvent? CommandEntered; //EVENT

        public void Close()
        {
            Environment.Exit(1);
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"Command [{adminCommand}] not found!");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"System error: [{errorString}].");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"[{user.Username}] has insufficient cash for [{product.Name}].");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Product [{product}] not found!");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Command [{command}] contains too many arguments ({command.Split(' ').Length}).");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            DisplayUserBuysProduct(1, transaction);
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"[{transaction.User.Username}] has bought {count}x[{transaction.Product.Name}].");
        }

        public void DisplayUserInfo(User user)
        {
            Console.WriteLine($"[{user.Username}]: {user.Firstname} {user.Lastname} balance: {user.Balance * 0.01M:C2}");
            Console.WriteLine("Previous purchases:");
            foreach (BuyTransaction transaction in _tallySystem.GetTransactions(user, 10))
            {
                Console.WriteLine(transaction);
            }
            if (user.Balance < 5000)
            {
                DisplayUserBalanceWarning(user, user.Balance * 0.01M);
            }
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"User [{username}] not found!");
        }

        public void Start()
        {
            DisplayMenu();
            while (true)
            {
                Console.Write("Awaiting command: ");
                string? command = Console.ReadLine();
                Console.Clear();
                DisplayMenu();
                CommandEntered?.Invoke(command ?? "");
            }
        }
        private void DisplayMenu()
        {
            Console.WriteLine("You have three commands available to you:");
            Console.WriteLine("1. Enter your username to see your account details.");
            Console.WriteLine("2. Enter your username and a product ID (Separated by space) to buy that product.");
            Console.WriteLine("3. Enter your username, an amount, and a product ID (Separated by spaces) to buy that amount of that product.");
            foreach (var activeProduct in _tallySystem.ActiveProducts)
            {
                Console.WriteLine(activeProduct);
            }
        }
    }
}
