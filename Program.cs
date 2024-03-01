using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace RunHidden
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 1) { return; }

            string scriptPath = args[0];

            if (!File.Exists(scriptPath)) { return; }

            string extension = Path.GetExtension(scriptPath).ToLower();

            string cmdArgs = Environment.CommandLine;

            //Replace all spaces, within quoted substrings, to xFF
            cmdArgs = Regex.Replace(cmdArgs, "\"([^\"]*)\"", m =>
            {
                return "\"" + m.Groups[1].Value.Replace(" ", "\u00FF") + "\"";
            });

            //Ensure there is only a single space between arguments
            cmdArgs = Regex.Replace(cmdArgs, "\\s+", " ");

            //Split off parameters to be passed through to the script
            string[] parts = cmdArgs.Split(new char[] { ' ' }, 3);
            cmdArgs = "";
            if (parts.Length > 2)
            {
                //Restore xFF to space
                cmdArgs = parts[2].Replace("\u00FF", " ");
            }

            ProcessStartInfo psi = new ProcessStartInfo();

            if (extension.Equals(".ps1"))
            {
                //Ensure quoted trailing backslashes are not escaped to quotes
                cmdArgs = cmdArgs.Replace("\\\"", "\\\\\"");
                cmdArgs = cmdArgs.Replace("\\\\\\\"", "\\\\\"");

                psi.FileName = "powershell.exe";
                psi.Arguments = $"-NoProfile -ExecutionPolicy Bypass -File \"{scriptPath}\" {cmdArgs}";
            }
            else if (extension.Equals(".py"))
            {
                psi.FileName = "python.exe";
                psi.Arguments = $" \"{scriptPath}\" {cmdArgs}";
            }
            else if (extension.Equals(".exe"))
            {
                psi.FileName = scriptPath;
                psi.Arguments = $" {cmdArgs}";
            }
            else
            {
                psi.FileName = "cmd.exe";
                psi.Arguments = $"/c \"\"{scriptPath}\" {cmdArgs}\"";
            }

            psi.RedirectStandardOutput = false;
            psi.RedirectStandardError = false;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;

            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();
            }
        }
    }
}
