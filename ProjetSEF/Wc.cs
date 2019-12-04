using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class Wc : Command
    {
        public Wc(string name) : base(name)
        {
            description = "This command will display the word count in a string";
        }

        public override string Execute_Command(string input)
        {
            List<string> tempList = input.Split(' ').ToList();
            if (tempList != null)
            {
                if (tempList.Count > 0)
                {
                    Console.WriteLine("Word count =" + tempList.Count);
                    return tempList.Count.ToString();
                }
            }
            return "0";
        }
    }
}