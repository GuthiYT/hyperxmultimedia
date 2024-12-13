using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WBR
{

    /// <summary>
    /// Handles basic file reading/writing
    /// </summary>
    internal class FileHandler
    {

        public static string EnvironmentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "//";
        public static string Appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\WBR\\";
        public static void WriteToFile(string path, string text)
        {
            File.WriteAllText(EnvironmentPath + path, text);
        }
        // TODO: actually write to appdata!
        public static void WriteToAppData(string path, string text)
        {
            System.IO.Directory.CreateDirectory(Appdata);
            File.WriteAllText(Appdata + path, text);
        }
        public static void AddToAppData(string path, string text)
        {
            System.IO.Directory.CreateDirectory(Appdata);
            File.AppendAllText(Appdata + path, text + Environment.NewLine);
        }
        public static string ReadFromAppData(string path)
        {
            if (!File.Exists(Appdata + path)) return null;

            return File.ReadAllText(Appdata + path);
        }
        public static string[] ReadLinesFromFile(string path)
        {
            if (!File.Exists(EnvironmentPath + path)) return null;

            return File.ReadLines(EnvironmentPath + path).ToArray();
        }
    }
}
