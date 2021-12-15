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
    public class TallySystemController
    {
        private readonly ITallySystemUI _ui;
        private readonly ITallySystem _system;
        private ITallySystemCommandParser _commandParser;

        public TallySystemController(ITallySystemUI ui, ITallySystem system)
        {
            _ui = ui;
            _system = system;
            _commandParser = new TallySystemCommandParser(ui, system);
            _ui.CommandEntered += HandleCommand;
        }
        private void HandleCommand(string commandString)
        {
            try
            {
                _commandParser.ParseCommand(commandString);
            }
            catch (InsufficientCreditsException e)
            {
                _ui.DisplayInsufficientCash(e.User, e.Product);
            }
            catch (NonExistentProductException e)
            {
                _ui.DisplayProductNotFound(e.Message);
            }
            catch (UserNotFoundException e)
            {
                _ui.DisplayUserNotFound(e.Username);
            }
            catch(FormatException e)
            {
                _ui.DisplayGeneralError(e.Message);
            }
            catch (Exception e)
            {
                _ui.DisplayGeneralError(e.Message);
            }
        }
    }
}
