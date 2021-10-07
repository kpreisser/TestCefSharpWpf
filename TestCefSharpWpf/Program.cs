using System;
using System.Diagnostics;
using System.IO;

using CefSharp;
using CefSharp.BrowserSubprocess;
using CefSharp.Wpf.HwndHost;

namespace TestCefSharpWpf
{    
    internal static class Program
    {
        [STAThread]
        private static int Main(string[] args)
        {
            // Determine if we need to run the CEFSharp browser subprocess.
            if (args.Length > 0 && args[0].StartsWith( "--type=", StringComparison.Ordinal)) {
                return SelfHost.Main(args);
            }
            else {
                // Run the regular application.
                // Initialize CEFSharp.
                InitializeCefSharp();

                // Run the program with WPF GUI.
                App.Main();

                // Shutdown CEFSharp.
                Cef.Shutdown();
                return Environment.ExitCode;
            }
        }

        private static void InitializeCefSharp()
        {
            CefSharpSettings.ShutdownOnExit = false;

            var settings = new CefSettings {
                LogSeverity = LogSeverity.Default,
                LogFile = Path.GetFullPath(@"TestCefSharpChromiumLog.txt"),
                CachePath = Path.GetFullPath(@"CEF-Cache"),
            };

            // Self-host the subprocess.
            using (var currentProcess = Process.GetCurrentProcess())
                settings.BrowserSubprocessPath = currentProcess.MainModule.FileName;

            if (!Cef.Initialize(settings))
                throw new InvalidOperationException("Could not initialize CefSharp.");

            Cef.EnableHighDPISupport();
        }
    }
}
