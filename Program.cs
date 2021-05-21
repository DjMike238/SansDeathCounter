using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SansDeathCounter
{
    internal class Program
    {
        private static readonly Regex deathsRx = new Regex("F=\"([0-9]*).000000\"", RegexOptions.Compiled);
        private static readonly string path = Environment.GetEnvironmentVariable("LocalAppData") + @"\UNDERTALE\undertale.ini";

        private static void Main()
        {
            Console.Title = "Sans Death Counter v1.1 - by Dj_Mike238 - PRESS ENTER TO EXIT";

            Console.WriteLine("Sans Death Counter v1.1 - by Dj_Mike238");
            Console.WriteLine("- PRESS ENTER TO EXIT -\n");

            if (File.Exists(path))
            {
                var fileWatcher = new FileSystemWatcher
                {
                    Path = Path.GetDirectoryName(path),
                    Filter = Path.GetFileName(path),
                    NotifyFilter = NotifyFilters.LastWrite
                };
                fileWatcher.Changed += OnChanged;
                fileWatcher.EnableRaisingEvents = true;

                Console.WriteLine("[INFO] undertale.ini loaded.");

                UpdateDeaths();

                while (true)
                {
                    Console.ReadLine();
                    Console.SetCursorPosition(0, Console.CursorTop - 1);

                    Console.Write("[SANS] Are you sure you want to exit? (y/N) ");
                    var ans = Console.ReadLine();

                    if (!string.IsNullOrEmpty(ans) && (ans[0] == 'Y' || ans[0] == 'y')) { break; }

                    Console.WriteLine("[SANS] Fine. Keep trying.");
                }
            }
            else
            {
                Error("undertale.ini not found.");
            }
        }

        private static string GetDeaths()
        {
            var undertaleIni = File.ReadAllText(path);
            var match = deathsRx.Match(undertaleIni);
            var deaths = match.Groups[1].Value;

            if (deaths.Length > 0)
                Console.WriteLine("[DEAD] {0} death(s).", deaths);

            return deaths;
        }

        private static void UpdateDeaths()
        {
            var deaths = GetDeaths();

            if (deaths.Length > 0)
            {
                File.WriteAllText(@".\deaths.txt", "Deaths: " + deaths);
                Console.WriteLine("[INFO] deaths.txt updated.");
            }
            else
            {
                Error("Death count not found.");
            }
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            while (IsFileLocked()) { }
            UpdateDeaths();
        }

        private static void Error(string error)
        {
            Console.WriteLine("[WARN] {0}", error);
            Console.WriteLine("[WARN] Press ENTER to exit.");
            Console.ReadLine();
            Environment.Exit(0);
        }

        private static bool IsFileLocked()
        {
            try
            {
                var fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException)
            {
                return true;
            }

            return false;
        }
    }
}
