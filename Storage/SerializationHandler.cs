using System.IO;
using System.Linq;
using Proxier.Core;

namespace Proxier.Storage
{
    public static class SerializationHandler
    {
        public static ProtoConfigurationContainer Configuration { get; private set; }

        public static bool SaveCurrent(ProxierRouter router)
        {
            try
            {
                using (ProtoConfigurationContainer ProtoContainer = new ProtoConfigurationContainer())
                {
                    foreach (var item in ProcessHandler.CachedProcesses.Values)
                        foreach (PProcess pp in item.Values)
                            if (pp.RoutingEnabled && !ProtoContainer.Processes.Any(p => p.ProcessName.Equals(pp.ProcessName)))
                                ProtoContainer.Processes.Add(pp);

                    ProtoContainer.ProxyServer = router.ProxyServer;

                    File.WriteAllBytes("Config.proxier", ProtoContainer.ToByteArray());
                    return true;
                }
            }
            catch { }
            return false;
        }

        public static bool DeserializeConfiguration()
        {
            try
            {
                if (File.Exists("Config.proxier"))
                {
                    Configuration = ProtoConfigurationContainer.FromBytes(File.ReadAllBytes("Config.proxier"));
                    return true;
                }
            }
            catch { }

            if (Configuration == null)
                Configuration = new ProtoConfigurationContainer();

            return false;
        }

        public static void Flush()
        {
            Configuration?.Dispose();
        }
    }
}
