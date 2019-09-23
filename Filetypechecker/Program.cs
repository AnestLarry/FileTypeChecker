using System;
using System.IO;
using getdata;

namespace Filetypechecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "File Type Checker";
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + "file.db") == false)
            {
                Console.WriteLine("file.db is not exists!\nFind or Download it!");
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (args.Length > 0)
            {
                if (File.Exists(args[0]))
                {
                    Console.WriteLine("Check File:\n"+args[0]+"\n");
                    byte[] fileheader = getfileheader(args[0]);
                    GetData data = new GetData();
                    data.Input(fileheader);
                }
                else
                {
                    Console.WriteLine("It is no a file path.");
                }
            }
            else
            {
                Console.WriteLine("Hello World!");
            }
        }

        static byte[] getfileheader(string name)
        {
            FileStream f = new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] data = new byte[1024];
            f.Read(data, 0, 1024);
            f.Close();
            return data;
        }
    }
}
