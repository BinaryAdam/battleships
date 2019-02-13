using System;
using Battleships.Interfaces;

namespace Battleships.Utils
{
    public class InputReader: IInputReader
    {
        public string ReadInput()
        {
            return Console.ReadLine();
        }
    }
}