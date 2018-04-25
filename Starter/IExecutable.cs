using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Starter
{
    interface IExecutable
    {
        event Action OnFinish;
        XElement GetElement();

        void Execute();
        //bool IsFinish { get; }
    }
}
