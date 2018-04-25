using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Starter
{
    class ProcessProperty
    {
        private readonly XElement ele;
        private readonly XElement defEle;
        private int delay;
        private PowerLineStatus[] powerModes;
        private string path;
        private string workingDirectory;
        private PairArray args;
        private PairArray vars;
        private bool hide;
        
        public ProcessProperty(XElement ele, XElement defEle = null)
        {
            this.ele = ele;
            this.defEle = defEle;
            Initialize();
        }

        private void Initialize()
        {
            delay = int.Parse(GetContent("Delay", "0"));
            SetPowerModes();
            path = GetContent("Path", null);
            workingDirectory = GetContent("WorkingDirectory", null);
            args = new PairArray(GetElementChild("Arguments"));
            vars = new PairArray(GetElementChild("EnvironmentVariables"));
            hide = Boolean.Parse(GetContent("hide", Boolean.FalseString));
        }

        private void SetPowerModes()
        {
            var str = GetContent("PowerMode").ToUpper().Trim();
            var list = new List<PowerLineStatus>();
            if (str.Length > 0)
            {
                var strArr = str.Split(',');
                if (Array.IndexOf(strArr, "ONLINE") >= 0) list.Add(PowerLineStatus.Online);
                if (Array.IndexOf(strArr, "OFFLINE") >= 0) list.Add(PowerLineStatus.Offline);
                if (Array.IndexOf(strArr, "UNKNOWN") >= 0) list.Add(PowerLineStatus.Unknown);
            }
            else
            {
                list.Add(PowerLineStatus.Offline);
                list.Add(PowerLineStatus.Online);
                list.Add(PowerLineStatus.Unknown);
            }

            this.powerModes = list.ToArray();
        }

        private string GetContent(string key, string defaultVal = "")
        {
            var t = ele.Element(key);
            if (t == null && defEle!=null) t = defEle.Element(key);

            if (t != null)
            {
                XNode[] nodes = t.Nodes().ToArray();
                if (nodes.Length > 0)
                {
                    return (
                                from x in nodes
                                where x.NodeType == XmlNodeType.Text
                                select ((XText)x).Value).Aggregate((a, b) => a + b
                            );
                }
            }
            return defaultVal;
        }

        private IEnumerable<XElement> GetElementChild(string key)
        {
            var t = ele.Element(key);
            if (t == null && defEle != null) t = defEle.Element(key);
            if (t != null)
            {
                return t.Elements();
            }

            return null;
        }

        public int Delay => delay;
        public PowerLineStatus[] PowerModes => powerModes;
        public string Path => path;
        public string WorkingDirectory => workingDirectory;
        public PairArray Args => args;
        public PairArray EnvVars => vars;
        public bool Hide => hide;
    }
}
