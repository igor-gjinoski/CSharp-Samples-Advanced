
namespace StructuralPatterns_Decorator.DB
{
    using System.Collections.Generic;

    public static class WannabeDb
    {
        public const int QuotaLimit = 3;

        public static List<LogEntry> Db = new List<LogEntry>();
    }


    public class LogEntry
    {
        public string Id { get; set; }

        public int Quota { get; set; }

        public List<string> Logs { get; set; }
    }
}
