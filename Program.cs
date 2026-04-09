using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace RunHidden
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Trace.WriteLine("error: no arguments given, return code: 1");
                return 1;
            }

            string scriptPath = args[0];

            Trace.WriteLine($"Script Path: {scriptPath}");

            bool pwsh = scriptPath.Contains("*");
            scriptPath = scriptPath.Replace("*", "");

            if (!File.Exists(scriptPath))
            {
                Trace.WriteLine("error: file does not exist, return code: 1");
                return 1;
            }

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
                if (pwsh) psi.FileName = "pwsh.exe";
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
                Trace.WriteLine($"Starting {psi.FileName} {psi.Arguments}");

                process.StartInfo = psi;
                process.Start();

#if WAIT_MODE
                process.WaitForExit();
                Trace.WriteLine($"{psi.FileName} exited with code {process.ExitCode}");
                return process.ExitCode;
#else
                return 0;
#endif
            }
        }
    }
}
