using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class CreateFile : Command
    {
        public CreateFile(string name) : base(name)
        {
            description = "This command will open a text editor so you can type in a list of commands";
        }

        public override string Execute_Command(string fileName)
        {
            Process.Start("notepad.exe", fileName);
            return null;
        }

        public override string Execute_Command()
        {
            return "You must add the name of the file as an argument";
        }
    }
}