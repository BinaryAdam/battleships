using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Utils
{
    public class RandomGenerator: IRandomGenerator
    {
        private Random _random;

        public RandomGenerator()
        {
            _random = new Random();
        }

        public int GetRandomNumber(int range)
        {
            return _random.Next(range);
        }
    }
}
