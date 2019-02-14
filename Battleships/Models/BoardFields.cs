using System.Collections.Generic;

namespace Battleships.Models
{
    public class BoardFields : Dictionary<string, FieldStatus>
    {
        public BoardFields()
        {

        }

        public BoardFields(IDictionary<string, FieldStatus> dictionary) : base(dictionary)
        {
        }
    }
}