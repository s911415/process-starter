using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

namespace Starter
{
    class ProcessGroup : IExecutable
    {
        public event Action OnFinish;
        private readonly XElement ele;
        private readonly ProcessProperty prop;
        private readonly List<IExecutable> processes;

        public ProcessGroup(XElement ele)
        {
            this.ele = ele;
            this.prop = new ProcessProperty(ele);
            processes = new List<IExecutable>();

            Initialize();
        }

        public void Execute()
        {
            new Thread(() =>
            {
                if (prop.Delay > 0) Thread.Sleep(prop.Delay);
                foreach(IExecutable p in processes)
                {
                    p.Execute();
                }
                OnFinish?.Invoke();
            }).Start();
        }

        public XElement GetElement()
        {
            return this.ele;
        }

        private void Initialize()
        {
            var t = ele.Element("Processes");
            if (t != null)
            {
                foreach (XElement e in t.Elements())
                {
                    string tag = e.Name.ToString().ToUpper();
                    IExecutable executable = null;
                    if (tag == "PROCESS")
                    {
                        executable = new Process(e, this);
                    }
                    else if(tag== "PROCESSGROUP")
                    {
                        executable = new ProcessGroup(e);
                    }

                    executable?.Execute();
                }
            }
        }
    }
}
