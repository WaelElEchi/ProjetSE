using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSEF
{
    internal class Program
    {
        public static string path;
        public static string home;
        public static string FilePath;

        public static Commands commands = new Commands();

        public static List<Proc> procs = new List<Proc>();
        public static List<Proc> procsHolder = new List<Proc>();

        private static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Clear();

            Console.WriteLine("Bienveu");
            Console.WriteLine("Pour accéder à la 1er partie (l'interpreteur de commandes) tapez 1");
            Console.WriteLine("Pour accéder à la 2éme partie (l'ordonnanceur) tapez 2");

            Console.ResetColor();

            var choiceInput = Console.ReadLine();

            Console.Clear();

            if (choiceInput == "1")
            {
                Init();

                //Commands commandList = new Commands();

                CreateCommands();

                Console.WriteLine("Welcome to our custom shell!");
                Console.WriteLine("Here is the list of the commands you can use:");
                Console.WriteLine("In order to understand a command use <Help commad>");
                for (int i = 0; i < commands.getCommandsList().Count; i++)
                {
                    Console.WriteLine("\t \t +" + commands.getCommandsList()[i].commandName);
                }

                string mainInput = "";

                do
                {
                    Console.Write(path + " >");
                    mainInput = Console.ReadLine();

                    List<string> inputList = mainInput.Split(' ').ToList();

                    //for (int i = 0; i < inputList.Count; i++)
                    //{
                    //    Console.Write(inputList[i] + "*");
                    //}

                    var command = commands.CommandExists(inputList[0]);

                    if (command != null)
                    {
                        //Console.WriteLine("command exists");
                        if (inputList.Count > 2)
                        {
                            if (inputList[1] == "alias" && inputList.Count == 3)
                            {
                                //Ajouter le nom a la liste des noms de la commande
                                command.AddName(inputList[2]);
                            }
                            else
                            if (inputList[inputList.Count - 2] == ">" && isATextFile(inputList[inputList.Count - 1]))
                            {
                                if (inputList.Count == 3)
                                {
                                    File.AppendAllText(inputList[inputList.Count - 1], command.Execute_Command());
                                    Console.WriteLine("INTO FILE");
                                    continue;
                                }
                                else if (inputList.Count == 4)
                                {
                                    File.AppendAllText(inputList[inputList.Count - 1], command.Execute_Command(inputList[1]));
                                    Console.WriteLine("INTO FILE WITH ARGUMENT");
                                    continue;
                                }
                            }
                            else if (inputList.Count == 3 && inputList[1] == "|")
                            {
                                Command tempCmd1 = commands.CommandExists(inputList[2]);
                                Command tempCmd2 = commands.CommandExists(inputList[0]);

                                if (tempCmd1 != null && tempCmd2 != null)
                                    tempCmd1.Execute_Command(tempCmd2.Execute_Command());
                            }
                            else if (inputList.Count == 4 && inputList[1] == "|")
                            {
                                Command tempCmd1 = commands.CommandExists(inputList[2]);
                                Command tempCmd2 = commands.CommandExists(inputList[0]);

                                if (tempCmd1 != null && tempCmd2 != null)
                                    tempCmd1.Execute_Command(tempCmd2.Execute_Command(), inputList[3]);
                            }
                        }

                        if (inputList.Count == 2)
                        {
                            // Console.WriteLine(command.Execute_Command(inputList[1]));

                            if (command.GetType() == typeof(Cd))
                            {
                                if (command.Execute_Command(inputList[1]) != null)
                                    path = inputList[1];
                            }
                            else if (command.GetType() == typeof(Help))
                            {
                                if (commands.CommandExists(inputList[1]) != null)
                                {
                                    Command tempComm = commands.CommandExists(inputList[1]);

                                    Console.WriteLine(tempComm.commandName + ": " + tempComm.description);
                                }
                            }
                            else if (command.GetType() == typeof(ExecuteList))
                            {
                                string file = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + "/" + inputList[1]);
                                ExecuteList(file);
                            }
                            else if (command.GetType() == typeof(CreateFile))
                            {
                                command.Execute_Command(inputList[1]);
                            }
                        }
                        else if (inputList.Count == 1)
                        {
                            Console.WriteLine(command.Execute_Command());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command doesn't exist");
                    }
                } while (mainInput.ToLower() != "exit");
            }
            else if (choiceInput == "2")
            {
                Console.WriteLine("Ordonnanceur");

                var nb = "";
                var tempStr = "";
                int value;

                Console.WriteLine("Donner le nombre de processus");
                do
                {
                    nb = Console.ReadLine();
                } while (!int.TryParse(nb, out value));

                for (int i = 0; i < value; i++)
                {
                    Console.WriteLine("Temps Exec P" + i);
                    tempStr = Console.ReadLine();
                    int exec = int.Parse(tempStr);

                    Console.WriteLine("Temps entrée P" + i);
                    tempStr = Console.ReadLine();
                    int entre = int.Parse(tempStr);

                    Proc p = new Proc("P" + i.ToString(), exec, entre);
                    procs.Add(p);
                    procsHolder.Add(p);
                }

                Console.WriteLine("Choissez l'agorithme");

                Console.WriteLine("FIFO : 1");
                Console.WriteLine("SJF : 2");
                Console.WriteLine("TOURNIQUET : 3");

                do
                {
                    tempStr = Console.ReadLine();
                    switch (tempStr)
                    {
                        case "1":
                            FIFO();
                            break;

                        case "2":
                            SJF3();
                            break;

                        case "3":
                            Console.WriteLine("Quantum?");
                            var q = Console.ReadLine();
                            TOURNIQUET4(int.Parse(q));
                            break;

                        default:
                            break;
                    }
                } while (tempStr != "exit");
            }
        }

        public static void CreateCommands()
        {
            Command Ls = new Ls("Ls");
            Command Pwd = new Pwd("Pwd");
            Command Cd = new Cd("Cd");
            Command Help = new Help("Help");
            Command Wc = new Wc("Wc");
            Command Createf = new CreateFile("CreateF");
            Command ExecList = new ExecuteList("ExecuteList");
            Command Grep = new Grep("Grep");

            commands.AddCommand(Ls);
            commands.AddCommand(Pwd);
            commands.AddCommand(Cd);
            commands.AddCommand(Help);
            commands.AddCommand(Wc);
            commands.AddCommand(Createf);
            commands.AddCommand(ExecList);
            commands.AddCommand(Grep);
        }

        private static void Init()
        {
            //Enregistrer le path de profile.txt
            string fileName = System.IO.Path.GetFullPath(Directory.GetCurrentDirectory() + @"\profile.txt");
            //ouvrir Profile.txt
            StreamReader sr = File.OpenText(fileName);

            path = "";
            home = "";

            //Lire le fichier et assigner path et home
            for (int i = 0; i < 2; i++)
            {
                string line = sr.ReadLine();
                if (line.Contains("HOME="))
                {
                    home = line.Substring(5);
                }

                if (line.Contains("PATH="))
                {
                    path = line.Substring(5);
                }
            }
            //Changer le répértoire actuel au path
            FilePath = path;
            Directory.SetCurrentDirectory(path);
        }

        private static bool isATextFile(string fileName)
        {
            return fileName.EndsWith(".txt") ? true : false;
        }

        private static void ExecuteList(string file)
        {
            string input = "";

            using (StreamReader srs = new StreamReader(file))
            {
                while (!srs.EndOfStream)
                {
                    input = srs.ReadLine();
                    List<string> inputList = input.Split(' ').ToList();

                    var command = commands.CommandExists(inputList[0]);

                    if (command != null)
                    {
                        if (inputList.Count > 2)
                        {
                            if (inputList[1] == "alias" && inputList.Count == 3)
                            {
                                command.AddName(inputList[2]);
                            }
                            else
                            if (inputList[inputList.Count - 2] == ">" && isATextFile(inputList[inputList.Count - 1]))
                            {
                                if (inputList.Count == 3)
                                {
                                    File.AppendAllText(inputList[inputList.Count - 1], command.Execute_Command());
                                    Console.WriteLine("INTO FILE");
                                    continue;
                                }
                                else if (inputList.Count == 4)
                                {
                                    // Console.WriteLine(inputList[inputList.Count - 1]);
                                    // Console.WriteLine(inputList[1]);
                                    File.AppendAllText(inputList[inputList.Count - 1], command.Execute_Command(inputList[1]));
                                    Console.WriteLine("INTO FILE WITH ARGUMENT");
                                    continue;
                                }
                            }
                            else if (inputList.Count == 3 && inputList[1] == "|")
                            {
                                Command tempCmd1 = commands.CommandExists(inputList[2]);
                                Command tempCmd2 = commands.CommandExists(inputList[0]);

                                if (tempCmd1 != null && tempCmd2 != null)
                                    tempCmd1.Execute_Command(tempCmd2.Execute_Command());
                            }
                        }

                        if (inputList.Count == 2)
                        {
                            Console.WriteLine(command.Execute_Command(inputList[1]));

                            if (command.GetType() == typeof(Cd))
                            {
                                path = inputList[1];
                            }
                            else if (command.GetType() == typeof(Help))
                            {
                                if (commands.CommandExists(inputList[1]) != null)
                                {
                                    Command tempComm = commands.CommandExists(inputList[1]);

                                    Console.WriteLine(tempComm.commandName + ": " + tempComm.description);
                                }
                            }
                        }
                        else if (inputList.Count == 1)
                        {
                            Console.WriteLine(command.Execute_Command());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command doesn't exist");
                    }
                }
            }
        }

        public static void FIFO()
        {
            Console.WriteLine("FIFO");

            List<Proc> ordList = procs;

            int time = 0;
            int sommeAttente = 0;
            int t = time;

            ordList = ordList.OrderBy(o => o.tempEntre).ToList();

            while (ordList.Count > 0)
            {
                if (ordList[0].tempEntre <= time)
                {
                    t = time;
                    Console.WriteLine(ordList[0].name + " s'éxécute de " + t + " à " + (time + ordList[0].tempsExec));
                    time += ordList[0].tempsExec;

                    sommeAttente += (t - ordList[0].tempEntre);
                    ordList.Remove(ordList[0]);
                }
                else
                {
                    time++;
                    Console.WriteLine(time);
                }
            }

            Console.WriteLine("Temps d'attente moyen =" + (float)sommeAttente / procs.Count);
        }

        public static void SJF()
        {
            Console.WriteLine("SJF");

            int time = 0;
            int sommeAttente = 0;
            int t = time;

            List<Proc> ordList = procs;

            ordList = ordList.OrderBy(o => o.tempsExec).ToList();

            for (int i = 0; i < ordList.Count; i++)
            {
                t = ordList[i].tempEntre;
                Console.WriteLine(ordList[i].name);
                time += ordList[i].tempsExec;

                sommeAttente += time - t;
            }
            float f = (float)sommeAttente / (float)procs.Count;
            Console.WriteLine("Temps d'attente moyen =" + f);
        }

        public static void SJF2()
        {
            Console.WriteLine("SJF");

            int time = 0;
            int sommeAttente = 0;
            int t = time;

            int turnAround;

            List<Proc> ordList = procs;

            ordList = ordList.OrderBy(o => o.tempsExec).ToList();

            while (ordList.Count > 0)
            {
                if (ordList[0].tempEntre <= time)
                {
                    t = time;
                    Console.WriteLine(ordList[0].name + " s'éxécute de " + t + " à " + (time + ordList[0].tempsExec));
                    time += ordList[0].tempsExec;

                    turnAround = time - ordList[0].tempEntre;

                    sommeAttente += turnAround - ordList[0].tempsExec;
                    ordList.Remove(ordList[0]);
                }
                else
                {
                    time++;
                    Console.WriteLine(time);
                }
            }

            Console.WriteLine("Temps d'attente moyen =" + (float)sommeAttente / procs.Count);
        }

        public static void SJF3()
        {
            Console.WriteLine("SJF");

            int time = 0;
            int sommeAttente = 0;
            int t = time;

            int turnAround;

            List<Proc> ordList = procs;

            List<Proc> readyList = new List<Proc>();
            ordList = ordList.OrderBy(o => o.tempEntre).ToList();

            while (ordList.Count > 0)
            {
                readyList.Clear();
                for (int i = 0; i < ordList.Count; i++)
                {
                    if (ordList[i].tempEntre <= time)
                    {
                        readyList.Add(ordList[i]);
                    }
                }

                readyList = readyList.OrderBy(p => p.tempsExec).ToList();

                if (readyList.Count > 0)
                {
                    t = time;
                    Console.WriteLine(readyList[0].name + " s'éxécute de " + t + " à " + (time + readyList[0].tempsExec));
                    time += readyList[0].tempsExec;

                    turnAround = time - readyList[0].tempEntre;

                    sommeAttente += turnAround - readyList[0].tempsExec;
                    ordList.Remove(readyList[0]);
                    readyList.Remove(readyList[0]);
                }
                else
                {
                    time++;
                    Console.WriteLine(time);
                }
            }

            Console.WriteLine("Temps d'attente moyen =" + (float)sommeAttente / procs.Count);
        }

        //public static void TOURNIQUET(int quantum)
        //{
        //    Console.WriteLine("Tourniquet");

        //    List<Proc> ordList = procs;
        //    // List<Proc> tourList = new List<Proc>();
        //    Queue<Proc> ordQueue = new Queue<Proc>();

        //    //ordList.Reverse();

        //    int timer = 0;

        //    for (int i = 0; i < ordList.Count; i++)
        //    {
        //        if (ordList[i].tempEntre == timer)
        //        {
        //            ordQueue.Enqueue(ordList[i]);
        //            ordList.Remove(ordList[i]);
        //        }
        //    }

        //    //int index = 0;

        //    //while (ordList.Count > 0)
        //    //{
        //    //    //timer++;

        //    //    if (ordQueue.Count > 0)
        //    //    {
        //    //        Proc p = ordQueue.Dequeue();
        //    //    }
        //    //}

        //    while (ordQueue.Count > 0)
        //    {
        //        //timer++;

        //        Proc p = ordQueue.Dequeue();
        //        int t = timer;

        //        if (p.tempRestant - quantum > 0)
        //        {
        //            p.tempRestant -= quantum;
        //            timer += quantum;
        //            ordQueue.Enqueue(p);
        //        }
        //        else
        //        {
        //            timer += p.tempRestant;
        //        }

        //        Console.WriteLine(t + ":" + p.name);

        //        for (int i = 0; i < ordList.Count; i++)
        //        {
        //            if (ordList[i].tempEntre >= timer)
        //            {
        //                ordQueue.Enqueue(ordList[i]);
        //                ordList.Remove(ordList[i]);
        //            }
        //        }
        //    }
        //}

        public static void TOURNIQUET(int quantum)
        {
            Console.WriteLine("Tourniquet");

            List<Proc> ordList = procs;
            List<Proc> tempList = procs;
            Queue<Proc> ordQueue = new Queue<Proc>();
            Proc p;

            bool done = false;

            int time = 0;
            int t;
            int sommeAttente = 0;

            while (ordList.Count >= 0 && done == false)
            {
                for (int i = 0; i < ordList.Count; i++)
                {
                    if (ordList[i].tempEntre <= time)
                    {
                        ordList[i].lastExecTime = ordList[i].tempEntre;
                        ordQueue.Enqueue(ordList[i]);

                        ordList.Remove(ordList[i]);
                    }
                }

                if (ordQueue.Count > 0)
                {
                    p = ordQueue.Dequeue();
                    t = time;
                    sommeAttente += t - p.lastExecTime;
                    if (p.tempRestant - quantum > 0)
                    {
                        p.tempRestant -= quantum;
                        ordQueue.Enqueue(p);
                        time += quantum;

                        p.lastExecTime = time;
                    }
                    else if (p.tempRestant - quantum <= 0)
                    {
                        time += p.tempRestant;
                    }
                    Console.WriteLine(p.name + " s'execute de " + t + " à " + (time));
                }
                else
                {
                    time++;
                    Console.WriteLine(time);
                }

                if (ordList.Count <= 0 && ordQueue.Count <= 0)
                {
                    done = true;
                }
            }
            Console.WriteLine("Temps d'attente moyen =" + (float)sommeAttente / procsHolder.Count);
        }

        public static void TOURNIQUET4(int quantum)
        {
            Console.WriteLine("Tourniquet");

            List<Proc> ordList = procs;
            List<Proc> tempList = procs;
            Queue<Proc> ordQueue = new Queue<Proc>();
            Proc p;

            bool done = false;

            int time = 0;
            int t;
            int sommeAttente = 0;

            while (ordList.Count >= 0 && done == false)
            {
                for (int i = 0; i < ordList.Count; i++)
                {
                    if (ordList[i].tempEntre <= time)
                    {
                        ordQueue.Enqueue(ordList[i]);

                        ordList.Remove(ordList[i]);
                    }
                }

                if (ordQueue.Count > 0)
                {
                    p = ordQueue.Dequeue();
                    t = time;
                    sommeAttente += t - p.lastExecTime;
                    if (p.tempRestant - quantum > 0)
                    {
                        p.tempRestant -= quantum;
                        ordQueue.Enqueue(p);
                        time += quantum;
                        p.lastExecTime = time;
                        p.executionTimings.Add(time);
                    }
                    else if (p.tempRestant - quantum <= 0)
                    {
                        time += p.tempRestant;
                    }
                    Console.WriteLine(p.name + " s'execute de " + t + " à " + (time));
                }
                else
                {
                    time++;
                    Console.WriteLine(time);
                }

                if (ordList.Count <= 0 && ordQueue.Count <= 0)
                {
                    done = true;
                }
            }

            int s = 0;
            for (int i = 0; i < procsHolder.Count; i++)
            {
                s += procsHolder[i].tempEntre;
            }

            sommeAttente -= s;
            Console.WriteLine("Temps d'attente moyen =" + (float)sommeAttente / procsHolder.Count);
        }

        public static void TOURNIQUET3(int quantum)
        {
            Console.WriteLine("Tourniquet");

            List<Proc> ordList = procs;
            List<Proc> tempList = procs;
            Queue<Proc> ordQueue = new Queue<Proc>();
            Proc p;
            Proc aux = null;
            bool done = false;

            int time = 0;
            int t;
            int sommeAttente = 0;

            while (ordList.Count >= 0 && done == false)
            {
                for (int i = 0; i < ordList.Count; i++)
                {
                    if (ordList[i].tempEntre <= time)
                    {
                        ordList[i].lastExecTime = ordList[i].tempEntre;
                        ordQueue.Enqueue(ordList[i]);
                        ordList.Remove(ordList[i]);
                    }
                }

                if (aux != null)
                {
                    ordQueue.Enqueue(aux);
                    aux = null;
                }

                if (ordQueue.Count > 0)
                {
                    p = ordQueue.Dequeue();
                    t = time;
                    sommeAttente += t - p.lastExecTime;
                    if (p.tempRestant - quantum > 0)
                    {
                        p.tempRestant -= quantum;
                        ordQueue.Enqueue(p);
                        if (ordQueue.Count == 1)
                        {
                            aux = ordQueue.Dequeue();
                        }
                        time += quantum;

                        p.lastExecTime = time;
                    }
                    else if (p.tempRestant - quantum <= 0)
                    {
                        time += p.tempRestant;
                    }
                    Console.WriteLine(p.name + " s'execute de " + t + " à " + (time));
                }
                else
                {
                    time++;
                    Console.WriteLine(time);
                }

                if (ordList.Count <= 0 && ordQueue.Count <= 0)
                {
                    done = true;
                }
            }
            Console.WriteLine("Temps d'attente moyen =" + (float)sommeAttente / procsHolder.Count);
        }
    }
}