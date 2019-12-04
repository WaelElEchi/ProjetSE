using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class Grep : Command
    {
        public Grep(string name) : base(name)
        {
            description = "This command will print the lines containing a given word";
        }

        public override string Execute_Command(string input)
        {
            //List<string> tempList = input.Split('\n').ToList();
            //string res = "";
            //Console.WriteLine(tempList.Count());
            //if (tempList != null)
            //{
            //    if (tempList.Count > 0)
            //    {
            //        for (int i = 0; i < tempList.Count; i++)
            //        {
            //            if (tempList[i].Contains(input))
            //            {
            //                res += tempList[i];
            //            }
            //        }
            //        return res;
            //    }
            //}
            return "you have to specify the word to search for";
        }

        public override string Execute_Command(string input, string word)
        {
            List<string> tempList = input.Split('\n').ToList();
            string res = "";
            //Console.WriteLine(tempList.Count());
            if (tempList != null)
            {
                if (tempList.Count > 0)
                {
                    for (int i = 0; i < tempList.Count; i++)
                    {
                        if (tempList[i].Contains(word))
                        {
                            res += tempList[i];
                            Console.WriteLine(tempList[i]);
                        }
                    }
                    return res;
                }
            }
            return word + " doesn't exist in " + input;
        }
    }
}