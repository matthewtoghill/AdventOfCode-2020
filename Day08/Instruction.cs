namespace Day08
{
    public class Instruction
    {
        public string Value { get; private set; }
        public string Command { get; set; }
        public int Param { get; private set; }
        public int TimesRun { get; set; }

        public Instruction(string value)
        {
            Value = value;
            string[] split = value.Split();
            Command = split[0];
            Param = int.Parse(split[1]);
            TimesRun = 0;
        }

        // Copy Constructor
        public Instruction(Instruction copy)
        {
            Value = copy.Value;
            Command = copy.Command;
            Param = copy.Param;
            TimesRun = 0;
        }
    }
}
