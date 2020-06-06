using System;

namespace Proxier.Core
{
    public class RoutingEventArgs : EventArgs, IDisposable
    {
        public PProcess Caller;
        public string Url;
        public string Dt;

        public RoutingEventArgs(PProcess p, string u) : base()
        {
            this.Dt = DateTime.Now.ToString();
            this.Caller = p;
            this.Url = u;
        }

        public void Dispose()
        {
            Dt = null;
            Caller = null;
            Url = string.Empty;
        }
    }
}
