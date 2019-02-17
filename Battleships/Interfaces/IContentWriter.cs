using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Interfaces
{
    public interface IContentWriter
    {
        void ClearScreen();
        void WriteLine(string content);
    }
}
