using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetSEF
{
    internal class Cd : Command
    {
        public Cd(string name) : base(name)
        {
            description = "This command will change the active directory to the path given as an argument";
        }

        public override string Execute_Command(string _path)
        {
            try
            {
                if (_path != null)
                {
                    //change directory
                    Directory.SetCurrentDirectory(_path);
                    // path = _path;

                    return _path;
                }
                else
                {
                    Console.WriteLine("You didn't specify a path");
                    return null;
                }
            }
            catch (Exception E)
            {
                Console.WriteLine("path non existant");
                return null;
            }
            return _path;
        }

        public override string Execute_Command()
        {
            return "You didn't specify a path";
        }
    }
}