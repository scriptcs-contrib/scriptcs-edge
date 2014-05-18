using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security;
using System.Text;
using ScriptCs.Contracts;
using EdgeJs;
using System.IO;

namespace ScriptCs.Edge
{
    public class EdgeScriptPack : ScriptPack<EdgePack>
    {
        public EdgeScriptPack()
        {
            Context = new EdgePack();
        }
        
        public override void Initialize(IScriptPackSession session)
        {
            var edgeLibFolder = Path.GetDirectoryName(typeof (EdgeJs.Edge).Assembly.Location);
            var edgeContentFolder = Path.GetFullPath(Path.Combine(edgeLibFolder, "..", "content", "edge"));
            var edgeLibEdgeFolder = Path.Combine(edgeLibFolder, "edge");
            
            if (!Directory.Exists(edgeLibEdgeFolder))
            {
                var arch = "x" + (Environment.Is64BitOperatingSystem ? "64" : "86");
                var bin = Path.Combine(edgeContentFolder, arch);
                var binDest = Path.Combine(edgeLibEdgeFolder, arch);

                Directory.CreateDirectory(edgeLibEdgeFolder);
                Directory.CreateDirectory(binDest);
                var items = new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>(Path.Combine(edgeContentFolder, "*.js"), edgeLibEdgeFolder),
                    new Tuple<string, string>(Path.Combine(bin, "node.dll"), edgeLibFolder),
                    new Tuple<string, string>(Path.Combine(bin, "edge.node"), binDest)
                };
                Copy(items);
            }
            session.ImportNamespace("EdgeJs");
        }

        private void Copy(List<Tuple<string, string>> items )
        {
            var process = new Process();
            process.StartInfo.FileName= "cmd.exe";
            var args = new StringBuilder();
            args.Append("/c ");
            for (int i = 1; i <= items.Count; i++)
            {
                var item = items[i-1];
                args.Append(string.Format("copy \"{0}\" \"{1}\"", item.Item1, item.Item2));
                if (i < items.Count)
                {
                    args.Append(" && ");
                }
            }
            process.StartInfo.Arguments = args.ToString();
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            process.WaitForExit();

        }
    }
}
