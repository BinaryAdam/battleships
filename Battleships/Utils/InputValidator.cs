using System.Text.RegularExpressions;
using Battleships.Interfaces;

namespace Battleships.Utils
{
    public class InputValidator: IInputValidator
    {
        public bool IsInputValid(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;
            return Regex.IsMatch(input, "^[A-J][1-9]0?$");
        }
    }
}