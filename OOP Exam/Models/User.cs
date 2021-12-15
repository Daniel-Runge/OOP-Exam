using System;
using System.Text.RegularExpressions;

namespace OOP_Exam.Models
{
    public class User : IEquatable<User>, IComparable
    {
        private const string _usernamePattern = @"^[0-9a-z_]+$";
        private const string _emailPattern = @"^[a-zA-Z0-9\._-]+@[a-zA-Z0-9]+[a-zA-Z0-9-]?\.[a-zA-Z\.-]+[a-zA-Z0-9]$";
        private static uint _nextId = 1;
        private uint _id;
        private string _username = "";
        private string _email = "";

        public User() : this("ukendt", "ukendt", "ukendt", "ukendt@ukendt.dk", 0) { }

        public User(string firstname, string lastname, string username, string email, int balance)
        {
            Id = _nextId++;
            Firstname = firstname;
            Lastname = lastname;
            Username = username;
            Email = email;
            Balance = balance;
        }

        public uint Id
        {
            get { return _id; }
            init
            {
                if (value < _nextId - 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), $"Id already taken {value}");
                }
                _nextId = value + 1;
                _id = value;
            }
        }

        public string Firstname { get; init; } = "";

        public string Lastname { get; init; } = "";

        public string Username
        {
            get { return _username; }
            init
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
            init
            {
                if (!Regex.IsMatch(value, _emailPattern))
                {
                    throw new ArgumentException("Provide a proper email please", nameof(value)); // Add custom/detailed exception?
                }
                _email = value;
            }
        }

        public int Balance { get; set; }


        public int CompareTo(object? obj)
        {
            return obj is User user ? Id.CompareTo(user.Id) : 1;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }

        public bool Equals(User? other)
        {
            return other != null &&
                   Username == other.Username;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username);
        }

        public override string ToString()
        {
            return $"{Firstname} {Lastname} {Email}";
        }
    }
}
