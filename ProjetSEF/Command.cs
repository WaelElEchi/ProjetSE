using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetSEF
{
    public class Command
    {
        public string commandName;
        public List<string> commandNameList = new List<string>();
        public string description;

        public Command(string name)
        {
            commandName = name;
            AddName(name);
        }

        public void AddName(string name)
        {
            commandNameList.Add(name);
        }

        public virtual string Execute_Command(string input)
        {
            return commandName + " executed in " + input;
        }

        public virtual string Execute_Command()
        {
            return commandName + " executed";
        }

        public virtual string Execute_Command(string path1, string path2)
        {
            return commandName + " executed " + path1 + " to " + path2;
        }
    }
}