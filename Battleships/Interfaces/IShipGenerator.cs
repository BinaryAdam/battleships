using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Interfaces
{
    public interface IShipGenerator
    {
        List<string> GenerateShipFields(int shipSize);
    }
}
