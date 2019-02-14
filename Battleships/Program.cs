using System;
using System.Text;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(" 1-3-5");
            stringBuilder.AppendLine("A xo ");
            Console.WriteLine(stringBuilder.ToString());
            Console.ReadLine();
        }
    }
}
