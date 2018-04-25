using System;
using System.Text;
using System.Xml.Linq;
using System.Threading;

namespace Starter
{
    class Process : IExecutable
    {
        private readonly XElement ele;
        private readonly ProcessGroup parent;
        private readonly ProcessProperty prop;
        public event Action OnFinish;
        private System.Diagnostics.Process p = null;

        public Process(XElement ele, ProcessGroup parent)
        {
            this.ele = ele;
            this.parent = parent;
            this.prop = new ProcessProperty(ele, parent?.GetElement());
        }

        public Process(XElement ele) : this(ele, null)
        {
        }

        public void Execute()
        {
            if (IsAllowRun())
            {
                new Thread(() =>
                {
                    if(prop.Delay>0) Thread.Sleep(prop.Delay);
                    var startInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        UseShellExecute = false,
                        FileName = prop.Path,
                        WorkingDirectory = prop.WorkingDirectory
                    };
                    var sb = new StringBuilder();
                    foreach (Tuple<string, string> pair in prop.Args)
                    {
                        sb.Append(pair.Item1);
                        if(pair.Item2!=null)
                            sb.Append(" \"").Append(pair.Item2).Append("\" ");
                    }

                    startInfo.Arguments = sb.ToString();

                    foreach (Tuple<string, string> pair in prop.EnvVars)
                        startInfo.EnvironmentVariables[pair.Item1] = pair.Item2;

                    startInfo.CreateNoWindow = prop.Hide;
                    p = System.Diagnostics.Process.Start(startInfo);

                    OnFinish?.Invoke();
                }).Start();
            }
        }

        private bool IsAllowRun()
        {
            System.Windows.Forms.PowerLineStatus curPower = System.Windows.Forms.SystemInformation.PowerStatus.PowerLineStatus;
            return (Array.IndexOf(prop.PowerModes, curPower) >= 0);
        }

        public XElement GetElement()
        {
            return this.ele;
        }
    }
}
