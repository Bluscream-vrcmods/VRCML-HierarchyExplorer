using System;
using System.IO;
using System.Linq;
using VRCModLoader;

namespace Mod
{
    public class Utils
    {
        public static StreamWriter logFile;
        public static void LogToFile(string msg)
        {
            if (logFile is null) {
                logFile = new StreamWriter(File.Open("hierarchy.txt", FileMode.OpenOrCreate));
                logFile.WriteLine($"HierarchyExplorer: Started dumping at {DateTime.Now}");
            }
            logFile.WriteLine(msg);
        }
        public static void Log(params object[] msgs) {
            var msg = "[HierarchyExplorer]:";
            foreach (var _msg in msgs) {
                try {
                    msg += $" {_msg}";
                } catch {
                    msg += $" {_msg.ToString()}";
                }
            }
            VRCModLogger.Log(msg);
        }
        public static string CombinePaths(string source, params string[] paths)  {
            if (source == null) throw new ArgumentNullException("source");
            if (paths == null) throw new ArgumentNullException("paths");
            return paths.Aggregate(source, (acc, p) => Path.Combine(acc, p));
        }
    }
}
