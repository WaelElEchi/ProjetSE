using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProjetSEF
{
    public class Ls : Command
    {
        public Ls(string name) : base(name)
        {
            description = "This command will display the content of the current directory";
        }

        public override string Execute_Command(string path)
        {
            string result = "";

            if (path == null)
            {
                DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    //Console.WriteLine("{0, -30}\t directory", d.Name);
                    result += d.Name + "\t directory \n";
                }

                foreach (FileInfo f in dir.GetFiles())
                {
                    // Console.WriteLine("{0, -30}\t file", f.Name);
                    result += f.Name + "\t file \n";
                }
            }
            else
            {
                try
                {
                    DirectoryInfo dir = new DirectoryInfo(path);

                    foreach (DirectoryInfo d in dir.GetDirectories())
                    {
                        // Console.WriteLine("{0, -30}\t directory", d.Name);
                        result += d.Name + "\t directory \n";
                    }

                    foreach (FileInfo f in dir.GetFiles())
                    {
                        //Console.WriteLine("{0, -30}\t File", f.Name);
                        result += f.Name + "\t file \n";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("path non existant");
                }
            }

            return result;
        }

        public override string Execute_Command()
        {
            return Execute_Command(null);
        }

        public override string Execute_Command(string path1, string path2)
        {
            return "Invalid arguments";
        }
    }
}