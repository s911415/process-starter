using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLParser parser = new XMLParser(
                Path.Combine(
                    Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase),
                    "ProcessList.xml"
                )
            );
            new ProcessGroup(parser.Root).Execute();
        }
    }
}
