using System;
using System.Linq;
using System.Timers;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;

using Proxier.Helpers;
using Proxier.Storage;

namespace Proxier.Core
{
    public static class ProcessHandler
    {
        private static Process ThisProcess { get; set; }  
        private static readonly Timer UpdateTimer = new Timer(5000); //5 sec update rate
        public static EventHandler ProcessesUpdated;
        public static readonly Dictionary<string, Dictionary<int, PProcess>> CachedProcesses = new Dictionary<string, Dictionary<int, PProcess>>();
        public static readonly HashSet<string> Ignore = new HashSet<string>();
        public static void Start()
        {
            ThisProcess = Process.GetCurrentProcess();
            UpdateTimer.Elapsed += UpdateTimer_Elapsed;
            UpdateProcesses();
            UpdateTimer.Start();
        }

        private static void UpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateProcesses();
        }

        public static void Stop()
        {
            UpdateTimer?.Stop();
            UpdateTimer?.Dispose();

            foreach (var procName in CachedProcesses.Keys)
                foreach (var pp in CachedProcesses[procName].Values)
                    pp.Dispose();
        }

        public static bool HasRoutingEnabled(string processName, out PProcess target)
        {
            target = null;
            try
            {
                if (CachedProcesses.ContainsKey(processName))
                {
                    target = CachedProcesses[processName].Values.First();
                    return CachedProcesses[processName].Values.First().RoutingEnabled;
                }
            }
            catch { }
            return false;
        }

        public static bool TryMatchProcess(string processName, out PProcess oProcess)
        {
            oProcess = null;
            try
            {
                if (CachedProcesses.ContainsKey(processName))
                {
                    oProcess = CachedProcesses[processName].Values.First();
                    return oProcess.RoutingEnabled;
                }
            }
            catch { }
            return false;
        }

        private static void UpdateProcesses()
        {
            foreach (var process in Process.GetProcesses().OrderBy(x => x.ProcessName))
            {
                //Be self aware.
                if (process.Id == ThisProcess.Id)
                    continue;

                var toLower = process.ProcessName.ToLower();

                if (Ignore.Contains(toLower))
                    continue;

                if (CachedProcesses.ContainsKey(toLower) && CachedProcesses[toLower].ContainsKey(process.Id))
                    continue;
                else
                {
                    PProcess pp = new PProcess()
                    {
                        Process = process,
                        RoutingEnabled = SerializationHandler.Configuration.Processes.Any(p => p.ProcessName.Equals(process.ProcessName)),
                    };

                    if (IconGrabber.TryGetIconBitmap(process, 16, out Bitmap icon))
                        pp.Icon = icon;
                    else
                    {
                        // If we fail to load the icon, we probably dont have access rights to the given process,
                        // we wont be able to hook the socket, so from now on, we ignore this process.
                        Ignore.Add(toLower);
                        continue;
                    }

                    if (!CachedProcesses.ContainsKey(toLower))
                        CachedProcesses.Add(toLower, new Dictionary<int, PProcess>());

                    if (!CachedProcesses[toLower].ContainsKey(process.Id))
                        CachedProcesses[toLower].Add(process.Id, pp);
                }
            }

            //Notify UI the latest processes.
            ProcessesUpdated?.Invoke(null, null);
        }
    }
}