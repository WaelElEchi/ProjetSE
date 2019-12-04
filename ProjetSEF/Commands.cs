using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class Commands
    {
        private List<Command> commandList = new List<Command>();

        public void AddCommand(Command com)
        {
            commandList.Add(com);
        }

        public List<Command> getCommandsList()
        {
            return commandList;
        }

        public Command CommandExists(String commandName)
        {
            for (int i = 0; i < commandList.Count; i++)
            {
                for (int j = 0; j < commandList[i].commandNameList.Count; j++)
                {
                    if (commandName.ToLower() == commandList[i].commandNameList[j].ToLower())
                    {
                        return commandList[i];
                    }
                }
            }
            return null;
        }
    }
}