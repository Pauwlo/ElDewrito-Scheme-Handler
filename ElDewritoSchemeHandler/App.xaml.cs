using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ElDewritoSchemeHandler
{
    public partial class App : Application
    {
        public void OnStartup(object sender, StartupEventArgs e)
        {
            string query = ParseArgs(e.Args);
            string eldewritoPath = GetElDewritoPath();

            StartGame(eldewritoPath, query);

            Environment.Exit(0);
        }

        public string ParseArgs(string[] args)
        {
            if (args.Length != 1 || !args[0].StartsWith("eldewrito://"))
            {
                string myName = AppDomain.CurrentDomain.FriendlyName;
                MessageBox.Show($"Usage: {myName} eldewrito://server_ip[:port]");
                Environment.Exit(1);
            }

            string arg = args[0].Replace("eldewrito://", "");

            if (arg.EndsWith('/'))
            {
                arg = arg.Remove(arg.Length - 1, 1);
            }

            return arg;
        }

        public string GetElDewritoPath()
        {
            string? eldewritoPath = ElDewritoSchemeHandler.Properties.Settings.Default.ElDewritoPath;

            if (File.Exists(eldewritoPath))
            {
                return eldewritoPath;
            }

            MessageBoxResult messageResult = MessageBox.Show(
                "Please select your ElDewrito instance (eldorado.exe).",
                "ed where?",
                MessageBoxButton.OKCancel);

            if (messageResult == MessageBoxResult.Cancel)
                Environment.Exit(0);

            OpenFileDialog eldoradoDialog = new OpenFileDialog();
            eldoradoDialog.FileName = "eldorado.exe";

            bool? result = eldoradoDialog.ShowDialog();

            if (result != true) // User cancel
                Environment.Exit(0);

            eldewritoPath = eldoradoDialog.FileName;

            // Update configuration file
            ElDewritoSchemeHandler.Properties.Settings.Default.ElDewritoPath = eldewritoPath;
            ElDewritoSchemeHandler.Properties.Settings.Default.Save();

            return eldewritoPath;
        }

        public void StartGame(string path, string query)
        {
            Process p = new Process();
            p.StartInfo.FileName = path;
            p.StartInfo.Arguments = $"-connect {query}";
            p.StartInfo.UseShellExecute = false;

            p.Start();
        }
    }
}
