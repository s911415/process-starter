using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Starter
{
    class PairArray : IEnumerable<Tuple<string, string>>
    {
        private List<Tuple<string, string>> list;
        public PairArray(IEnumerable<XElement> itor)
        {
            list = new List<Tuple<string, string>>();
            if (itor != null)
            {
                foreach(XElement e in itor){
                    list.Add(
                        new Tuple<string, string>(
                            e.Attribute("key").Value,
                            e.Attribute("value")?.Value
                        )
                    );
                }
            }
        }

        public IEnumerator<Tuple<string, string>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }
    }
}
