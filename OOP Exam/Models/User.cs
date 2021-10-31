using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOP_Exam.Models
{
    class User
    {
        private const string _usernamePattern = @"^[0-9a-z_]+$";
        private const string _emailPattern = @"^[a-zA-Z0-9\._-]+@[a-zA-Z0-9]+[a-zA-Z0-9-]?\.[a-zA-Z\.-]+[a-zA-Z0-9]$";
        private static int _nextID;
        public int ID { get; init; }
        private string _firstname;
        private string _lastname;
        private string _username;
        private string _email;

        public string Firstname
        {
            get => _firstname;
            set => _firstname = value ?? "";
        }

        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value ?? ""; }
        }

        public string Username
        {
            get { return _username; }
            set
            {
                if (!Regex.IsMatch(value, _usernamePattern))
                {
                    throw new FormatException("Provide a proper username please");
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
                    throw new FormatException("Provide a proper password please");
                }
                _email = value;
            }
        }

        public User()
        {
            ID = _nextID;
            _nextID++;
        }
    }
}
