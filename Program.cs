using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

using Fiddler;
using Microsoft.Win32;

namespace Proxier
{
    static class Program
    {
        private static Proxier Proxier { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ApplicationExit += new EventHandler(ApplicationExit);
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(ProcessExit);
            SystemEvents.PowerModeChanged += OnPowerChange;
            SystemEvents.SessionEnding += SystemEvents_SessionEnding;
            Proxier = new Proxier();
            Application.Run(Proxier);
        }

        private static void SystemEvents_SessionEnding(object sender, SessionEndingEventArgs e)
        {
            ProcessExit(null, null);
            Application.Exit();
        }

        private static void OnPowerChange(object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Resume:
                    break;
                case PowerModes.Suspend:
                    ProcessExit(null, null);
                    Application.Exit();
                    break;
            }
        }

        private static void ProcessExit(object sender, EventArgs e)
        {
            KillFiddler();
            Proxier?.Dispose();
        }

        private static void ApplicationExit(object sender, EventArgs e)
        {
            KillFiddler();
            Proxier?.Dispose();
        }

        private static void KillFiddler()
        {
            try
            {
                if (FiddlerApplication.oProxy != null)
                {
                    if (FiddlerApplication.oProxy.IsAttached)
                    {
                        FiddlerApplication.oProxy?.Detach();
                        FiddlerApplication.Shutdown();
                    }
                }
            }
            catch { }
        }
    }
}
