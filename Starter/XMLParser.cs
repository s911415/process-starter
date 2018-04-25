using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Starter
{
    class XMLParser
    {
        private readonly XDocument doc;
        public XMLParser(string path)
        {
            doc = XDocument.Load(path);
        }

        public XElement Root => doc.Root;
    }
}
