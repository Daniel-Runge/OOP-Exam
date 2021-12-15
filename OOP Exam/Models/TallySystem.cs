using OOP_Exam.Exceptions;
using OOP_Exam.Services;
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
        public TallySystem(ICsvSerdeService csvHandler)
        {
            Transactions = new List<Transaction>();
            Products = csvHandler
                .Deserialize<Product>(@"Data\products.csv", ';')
                .ToDictionary(product => product.Id);
            Users = csvHandler
                .Deserialize<User>(@"Data\users.csv", ',')
                .ToDictionary(user => user.Username);
        }

        public IEnumerable<Product> ActiveProducts => Products.Values.Where(product => product.Active == 1);
        public IDictionary<uint, Product> Products { get; }
        public IDictionary<string, User> Users { get; }
        public List<Transaction> Transactions { get; }

        public event UserBalanceNotification? UserBalanceWarning; // EVENT!!
        public InsertCashTransaction AddCreditsToAccount(User user, uint amount)
        {
            InsertCashTransaction transation = new(user, amount);
            ExecuteTransaction(transation);
            return transation;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new(user, product);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public Product GetProductByID(uint id)
        {
            return Products.TryGetValue(id, out var result)
                ? result
                : throw new NonExistentProductException(id);
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            return Enumerable.Reverse(Transactions)
                .Where(transaction => transaction.User == user && transaction is BuyTransaction)
                .Take(count);
            //return Transactions.Where(transaction => transaction.User == user)
            //    .Reverse()
            //    .Take(count);
        }

        public User GetUserByUsername(string username)
        {
            if (Users.TryGetValue(username, out var user))
            {
                return user;
            }
            throw new UserNotFoundException(username);
        }

        public IEnumerable<User> GetUsers(Func<User, bool> predicate)
        {
            return Users.Values.Where(user => predicate(user));
        }

        private void ExecuteTransaction(Transaction transaction)
        {
            transaction.Execute();
            if (transaction.User.Balance < 5000) UserBalanceWarning?.Invoke(transaction.User, transaction.User.Balance * 0.01M);
            Transactions.Add(transaction);
        }

    }
}
