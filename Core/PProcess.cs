using System;
using System.Drawing;
using System.Diagnostics;

using ProtoBuf;

namespace Proxier.Core
{
    [ProtoContract]
    public class PProcess : IDisposable
    {
        [ProtoMember(1)]
        public string ProcessName { get; set; }
        [ProtoMember(2)]
        public int ID { get; set; }
        [ProtoIgnore]
        public bool RoutingEnabled { get; set; }
        [ProtoIgnore]
        public Bitmap Icon { set; get; }
        [ProtoIgnore]
        public Decimal Inbound { set; get; } = 0;
        [ProtoIgnore]
        public Decimal Outbound { set; get; } = 0;

        [ProtoIgnore]
        private Process PrivateProcess { get; set; }
        public Process Process
        {
            get
            {
                return PrivateProcess;
            }
            set
            {
                PrivateProcess = value;
                ProcessName = value.ProcessName;
            }
        }

        [ProtoIgnore]
        public bool IsValid
        {
            get { try { if (PrivateProcess != null && !PrivateProcess.HasExited) { return true; } } catch { } return false; }
        }

        public void Dispose()
        {
            PrivateProcess?.Dispose();
            ProcessName = string.Empty;
            Icon = null; //This is referenced to out Icons cache, do not dispose just derefence here.
            Inbound = 0;
            Outbound = 0;
        }
    }
}
