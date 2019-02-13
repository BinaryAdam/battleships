using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Interfaces
{
    public interface IInputValidator
    {
        bool IsInputValid(string input);
    }
}
