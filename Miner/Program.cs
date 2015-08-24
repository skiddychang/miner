using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miner
{
    class Program
    {
        static Int16 Verbose = 0;
        static String Root = String.Empty;
        static void Main(String[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--verbose":
                    case "-v":
                        Verbose = Int16.Parse(args[i + 1]);
                        break;
                    case "--root":
                    case "-r":
                        Root = args[i + 1];
                        break;
                    case "--help":
                    case "-h":
                        Console.WriteLine("Under construction...");
                        return;
                }

            }
            Console.WriteLine("Output: " + SearchDir(Root) / 1024 + "K");

        }

        static long SearchDir(String DirName)
        {
            long Sum = 0;
            foreach (String File in Directory.GetFiles(DirName))
            {
                FileInfo Info = new FileInfo(File);
                Sum += Info.Length;
                if (Verbose == 2)
                {
                    Console.WriteLine(String.Format("miner: FILE: {0} - {1}K ({2}K)", File, Info.Length / 1024, Sum / 1024));
                }
            }
            foreach (String Dir in Directory.GetDirectories(DirName))
            {
                Sum += SearchDir(Dir);
            }
            if(Verbose > 0)
            {
                Console.WriteLine(String.Format("miner: DIR: {0} - {1}K", DirName, Sum / 1024));
            }
            return Sum;
        }
    }
}
