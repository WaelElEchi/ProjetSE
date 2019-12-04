using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class ExecuteList : Command
    {
        public ExecuteList(string name) : base(name)
        {
            description = "This command will execute a list of commands existant in the file you give as an argument";
        }

        public override string Execute_Command(string input)
        {
            return "Execution of commands";
        }

        public override string Execute_Command()
        {
            return "ExecuteList takes one argument";
        }
    }
}