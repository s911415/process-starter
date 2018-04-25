using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starter
{
    class Program
    {
        static void Main(string[] args)
        {
            XMLParser parser = new XMLParser("ProcessList.xml");
            new ProcessGroup(parser.Root).Execute();
        }
    }
}
