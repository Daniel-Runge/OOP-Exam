using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public class TallySystem : ITallySystem
    {
        public IEnumerable<Product> ActiveProducts { get; }
        public IDictionary<int, Product> Products { get; }
        public IDictionary<string, User> Users { get; }
        public event UserBalanceNotification UserBalanceWarning;

        public InsertCashTransaction AddCreditsToAccount(User user, int amount)
        {
            throw new NotImplementedException();
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            return new BuyTransaction(user, product);

        }

        public Product GetProductByID(int id)
        {
            return Products.TryGetValue(id, out Product result)
                ? result
                : throw new Exception();
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            return Users.TryGetValue(username, out User result)
                ? result
                : throw new Exception();
        }

        public User GetUsers(Func<User, bool> predicate)
        {
            throw new NotImplementedException();
        }

        private static void ExecuteTransaction(Transaction transaction) // VERY unfinished
        {
            transaction.Execute();
        }
        private static Dictionary<int, Product> ProductsFromCsvFile()
        {
            return File.ReadAllLines(@"Data\products.csv")
                .Skip(1)
                .Select(line => Product.FromCsvString(line, ';'))
                .ToDictionary(product => product.ID);
        }

        private static Dictionary<string, User> UsersFromCsvFile()
        {
            Dictionary<string, User> usersDictionary = new();
            string[] csvUserLines = File.ReadAllLines(@"Data\users.csv");

            foreach (string line in csvUserLines[1..])
            {
                User user = User.FromCsvString(line, ',');
                usersDictionary.Add(user.Username, user);
            }

            return usersDictionary;
        }
    }
}
