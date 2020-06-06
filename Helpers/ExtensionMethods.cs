using System;
using System.Diagnostics;

using Fiddler;

namespace Proxier.Helpers
{
    public static class ExtensionMethods
    {
        public static bool TryGetProcessName(this Session oSession, out string processName)
        {
            processName = oSession.LocalProcess;
            try
            {
                if (!string.IsNullOrEmpty(processName))
                {
                    processName = processName.Substring(0, processName.IndexOf(':'));
                    return true;
                }


                processName = Process.GetProcessById(oSession.LocalProcessID)?.ProcessName;
                return !string.IsNullOrEmpty(processName);
            }
            catch
            {
                return false;
            }
        }

        public static bool TryToInjectHeader(this Session oSession, bool hasAuthentication, string proxyURL, string credentials)
        {
            try
            {
                oSession["X-OverrideGateway"] = $"{proxyURL}";

                if (hasAuthentication)
                    oSession.RequestHeaders["Proxy-Authorization"] = $"Basic {credentials}";

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        /// <summary>
        /// https://stackoverflow.com/questions/14488796/does-net-provide-an-easy-way-convert-bytes-to-kb-mb-gb-etc
        /// </summary>
        public static string SizeSuffix(this Int64 value, int decimalPlaces = 1)
        {
            if (decimalPlaces < 0) { throw new ArgumentOutOfRangeException("decimalPlaces"); }
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
    }
}
