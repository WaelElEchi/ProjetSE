using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetSEF
{
    internal class Pwd : Command
    {
        public Pwd(string name) : base(name)
        {
            description = "This command will display the current directory";
        }

        public override string Execute_Command()
        {
            return Directory.GetCurrentDirectory().ToString();
        }

        public override string Execute_Command(string input)
        {
            return commandName + " doesn't take any arguments";
        }
    }
}