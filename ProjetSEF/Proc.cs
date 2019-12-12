using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class Proc
    {
        public string name;
        public int tempsExec;
        public int tempEntre;
        public int tempRestant;

        public int lastExecTime;
        public List<int> executionTimings = new List<int>();

        public Proc(string _name, int _tempExec, int _tempEntre)
        {
            tempsExec = _tempExec;
            tempEntre = _tempEntre;
            tempRestant = _tempExec;
            name = _name;
        }

        public Proc()
        {
        }
    }
}