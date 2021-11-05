using OOP_Exam.Models;
using OOP_Exam.Services;
using System;
using System.Collections.Generic;

namespace OOP_Exam
{
    public delegate void UserBalanceNotification(User user, decimal balance);

    class Program
    {
        static void Main(string[] args)
        {
            ITallySystem system = new TallySystem();
            ICsvSerdeService deserializer = new CsvSerde();
            IEnumerable<User> users = deserializer.Deserialize<User>(@"Data\users.csv", ',');
            IEnumerable<Product> products = deserializer.Deserialize<Product>(@"Data\products.csv", ';');

            foreach (var user in users)
            {
                Console.Write(user);
                Console.WriteLine($" With ID: {user.ID} and current balance: {user.Balance}");

            }

            foreach (var product in products)
            {
                Console.Write(product);
                Console.WriteLine($" With ID: {product.ID} and status: {product.Active}");
            }
        }
    }
}
