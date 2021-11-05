using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    public class User : IComparable
    {
        private const string _usernamePattern = @"^[0-9a-z_]+$"; // Not the idiomatic placement for these patterns
        private const string _emailPattern = @"^[a-zA-Z0-9\._-]+@[a-zA-Z0-9]+[a-zA-Z0-9-]?\.[a-zA-Z\.-]+[a-zA-Z0-9]$";
        private static int _nextID = 1;
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;

        public User(int id, string firstname, string lastname, string username, int balance, string email)
        {
            ID = id;
            Firstname = firstname;
            Lastname = lastname;
            Balance = balance * 0.01M;
            Username = username;
            Email = email;
        }



        public int CompareTo(object obj)
        {
            return obj is User user ? ID.CompareTo(user.ID) : 1;
        }

        public int ID { get; }

        public string Firstname
        {
            get => _firstname;
            set => _firstname = value ?? "";
        }

        public string Lastname
        {
            get => _lastname;
            set => _lastname = value ?? "";
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (!Regex.IsMatch(value, _usernamePattern))
                {
                    throw new ArgumentException("Provide a proper username please", nameof(value)); // Add custom/detailed exception?
                }
                _username = value;
            }
        }
        public string Email
        {
            get { return _email; }
            set
            {
                if (!Regex.IsMatch(value, _emailPattern))
                {
                    throw new ArgumentException("Provide a proper email please", nameof(value)); // Add custom/detailed exception?
                }
                _email = value;
            }
        }

        public decimal Balance { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   ID.Equals(user.ID);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public override string ToString() // Use string.Format() or string interpolation instead?
        {
            StringBuilder sb = new();
            sb.Append(Firstname);
            if (Firstname.Length > 0) sb.Append(' ');

            sb.Append(Lastname);
            if (Lastname.Length > 0) sb.Append(' ');

            sb.Append($"({Email})");

            return sb.ToString();
        }

        public static User FromCsvString(string csvalues, char csvSeparator)
        {
            string[] values = csvalues.Split(csvSeparator);
            return new User(Convert.ToInt32(values[0]), values[1], values[2], values[3], Convert.ToInt32(values[4]), values[5]);
        }
    }
}
