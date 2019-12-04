using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class Help : Command
    {
        public Help(string name) : base(name)
        {
            description = "This command will tell you the functionality about the command in argument";
        }

        public override string Execute_Command(string input)
        {
            return "About " + input;
        }

        public override string Execute_Command()
        {
            return "The command help needs one argument";
        }
    }
}