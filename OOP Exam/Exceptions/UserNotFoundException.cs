using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Exceptions
{
    class UserNotFoundException : Exception
    {
        public UserNotFoundException(string username)
        {
            Username = username;
        }
        public string Username { get; }
    }
}
