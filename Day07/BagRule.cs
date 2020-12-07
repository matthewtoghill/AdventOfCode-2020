using System.Collections.Generic;

namespace Day07
{
    public class BagRule
    {
        public List<BagRule> BagContents { get; set; } = new List<BagRule>();
        public string Name { get; set; }
        public int Quantity { get; set; }

        public BagRule()
        {
        }

        public BagRule(string name, string quantity)
        {
            Name = name;
            Quantity = int.Parse(quantity);
        }
    }
}
