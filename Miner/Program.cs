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
            if (args.Length == 0)
            {
                Usage();
                return;
            }
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--verbose":
                    case "-v":
                        try
                        {
                            Verbose = Int16.Parse(args[i + 1]);
                        }
                        catch
                        {
                            Error("invalid verbose level");
                            return;
                        }
                        break;
                    case "--root":
                    case "-r":
                        try
                        {
                            Root = args[i + 1];
                        }
                        catch
                        {
                            Error("invalid path specified");
                            return;
                        }
                        break;
                    case "--help":
                    case "-h":
                        Usage();
                        return;
                }
            }
            try
            {
                Console.WriteLine("Output: " + SearchDir(Root) / 1024 + "K");
            }
            catch
            {
                Error("invalid path specified and/or no access privileges");
            }
        }

        static void Error(String Message)
        {
            Console.WriteLine(String.Format("miner: {0}", Message));
        }

        static void Usage()
        {
            Console.WriteLine(@"Usage: miner -r [root directory] [-v [verbose level]]
 -r, --root [directory]      root directory
 -v, --verbose [level]       verbose level
                               0 - show nothing (default)
                               1 - show directories
                               2 - show directories and/or files
 -h, --help                  this page");
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
                    Console.WriteLine(String.Format("FILE: {0} - {1}K ({2}K)", File, Info.Length / 1024, Sum / 1024));
                }
            }
            foreach (String Dir in Directory.GetDirectories(DirName))
            {
                Sum += SearchDir(Dir);
            }
            if (Verbose > 0)
            {
                Console.WriteLine(String.Format("DIR: {0} - {1}K", DirName, Sum / 1024));
            }
            return Sum;
        }
    }
}
