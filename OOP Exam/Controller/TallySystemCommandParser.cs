using OOP_Exam.Exceptions;
using OOP_Exam.Models;
using OOP_Exam.User_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Exam.Controller
{
    public class TallySystemCommandParser : ITallySystemCommandParser
    {
        private readonly Dictionary<string, Action<string[]>> _adminCommands;
        private readonly ITallySystemUI _ui;
        private readonly ITallySystem _system;

        public TallySystemCommandParser(ITallySystemUI ui, ITallySystem system)
        {
            _ui = ui;
            _system = system;
            _adminCommands = new()
            {
                { ":quit", (args) => _ui.Close() },
                { ":q", (args) => _ui.Close() },
                { ":activate", (args) => _system.GetProductByID(uint.Parse(args[0])).Active = 1 },
                { ":deactivate", (args) => _system.GetProductByID(uint.Parse(args[0])).Active = 0 },
                { ":crediton", (args) => _system.GetProductByID(uint.Parse(args[0])).CanBeBoughtOnCredit = true },
                { ":creditoff", (args) => _system.GetProductByID(uint.Parse(args[0])).CanBeBoughtOnCredit = false },
                { ":addcredits", (args) => _system.AddCreditsToAccount(_system.GetUserByUsername(args[0]), uint.Parse(args[1])).Execute() }
            };
        }
        public void ParseCommand(string commandString)
        {
            string[] command = commandString.Split(' ');
            if (_adminCommands.ContainsKey(command[0]))
            {
                _adminCommands[command[0]](command[1..]);
            }
            if (command[0][0] == ':')
            {
                _ui.DisplayAdminCommandNotFoundMessage(commandString);
            }
            else
            {
                switch (command.Length)
                {
                    case 1:
                        UserInformationCommand(command[0]);
                        break;
                    case 2:
                        SingleBuyProduct(command[0], command[1]);
                        break;
                    case 3:
                        MultiBuyProduct(command[0], command[1], command[2]);
                        break;
                    case > 3:
                        _ui.DisplayTooManyArgumentsError(commandString);
                        break;
                    default:
                        _ui.DisplayGeneralError("Unknown command");
                        break;
                }
            }
        }

        private void UserInformationCommand(string username)
        {
            User user = _system.GetUserByUsername(username);
            _ui.DisplayUserInfo(user);

        }

        private void SingleBuyProduct(string username, string productId)
        {
            User user = _system.GetUserByUsername(username);
            Product product = _system.GetProductByID(uint.Parse(productId));
            BuyTransaction transaction = _system.BuyProduct(user, product);
            _ui.DisplayUserBuysProduct(transaction);

        }

        private void MultiBuyProduct(string username, string count, string productId)
        {
            int amount = int.Parse(count);
            if (amount < 1) throw new ArgumentException("Please order at least 1 product...");

            User user = _system.GetUserByUsername(username);
            Product product = _system.GetProductByID(uint.Parse(productId));
            BuyTransaction transaction = _system.BuyProduct(user, product);

            for (int i = 0; i < amount - 1; i++)
            {
                try
                {
                    transaction = _system.BuyProduct(user, product);
                }
                catch (InsufficientCreditsException e)
                {
                    if (i > 0) _ui.DisplayUserBuysProduct(i, transaction);
                    throw e;
                }
            }
            _ui.DisplayUserBuysProduct(amount, transaction);

        }
    }
}
