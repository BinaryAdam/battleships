using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Utils
{
    public interface IRandomGenerator
    {
        int GetRandomNumber(int range);
    }
}
