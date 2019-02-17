using Battleships.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Utils
{
    public class ContentWriter : IContentWriter
    {
        public void ClearScreen()
        {
            Console.Clear();
        }

        public void WriteLine(string content)
        {
            Console.WriteLine(content);
        }
    }
}
