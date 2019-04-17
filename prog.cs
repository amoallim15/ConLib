using ConLib.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConLib
{
    class Prog
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hello");
            STLBStreamer stl = new STLBStreamer();
            stl.Read(@"C:\Users\meca\Desktop\test2.stl", new BaseParser(), null);
        }
    }
}
