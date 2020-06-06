using System;
using System.Net.Security;

using Proxier.Helpers;
using Proxier.Storage;

using Fiddler;

namespace Proxier.Core
{
    public class ProxierRouter : IDisposable
    {
        public Int64 InboundTotal = 0;
        public Int64 OutboundTotal = 0;
        public bool IsAttached = false;
        public EventHandler ProxyOnline;
        public EventHandler ProxyOffline;
        public EventHandler RoutingRequest;
        public ProxyServer ProxyServer { get; private set; }
        public int FiddlerPort { set; get; }

        public void Start()
        {
            ProxyServer = SerializationHandler.Configuration.ProxyServer;
            Console.WriteLine($"Starting hooks..." +
                $"\nRedirecting traffic to {SerializationHandler.Configuration.ProxyServer.IPAddress}:{SerializationHandler.Configuration.ProxyServer.Port}" +
                $"\nUsing credentials {SerializationHandler.Configuration.ProxyServer.Username}:{SerializationHandler.Configuration.ProxyServer.Password}");

            FiddlerApplication.FiddlerAttach += FiddlerAttach;
            FiddlerApplication.FiddlerDetach += FiddlerDetach;
            FiddlerApplication.BeforeRequest += BeforeRequest;
            FiddlerApplication.BeforeResponse += BeforeResponse;
            FiddlerApplication.AfterSessionComplete += AfterSessionComplete;
            FiddlerApplication.OnNotification += OnNotification;
            FiddlerApplication.OnWebSocketMessage += OnWebSocketMessage;
            FiddlerApplication.RequestHeadersAvailable += RequestHeadersAvailable;
            FiddlerApplication.ResponseHeadersAvailable += ResponseHeadersAvailable;
            FiddlerApplication.OnReadRequestBuffer += OnBufferRead;
            FiddlerApplication.OnReadResponseBuffer += OnBufferWrite;
            FiddlerApplication.OnValidateServerCertificate += OnValidateServerCertificate;
            FiddlerApplication.Startup(FiddlerPort, FiddlerCoreStartupFlags.RegisterAsSystemProxy | FiddlerCoreStartupFlags.MonitorAllConnections);
        }

        private void ResponseHeadersAvailable(Session oSession) { }
        private void RequestHeadersAvailable(Session oSession) { }
        private void OnWebSocketMessage(object sender, WebSocketMessageEventArgs e) { }
        private void OnNotification(object sender, NotificationEventArgs e) { }
        private void BeforeResponse(Session oSession) { }
        private void AfterSessionComplete(Session oSession) { }

        private void FiddlerDetach()
        {
            IsAttached = false;
            ProxyOffline?.Invoke(null, null);
        }

        private void FiddlerAttach()
        {
            IsAttached = true;
            ProxyOnline?.Invoke(null, null);
        }


        private void OnBufferRead(object sender, RawReadEventArgs e)
        {
            if (e.sessionOwner.TryGetProcessName(out string processName))
                if (ProcessHandler.TryMatchProcess(processName, out PProcess pp))
                    pp.Outbound += e.iCountOfBytes;

            OutboundTotal += e.iCountOfBytes;
        }

        private void OnBufferWrite(object sender, RawReadEventArgs e)
        {
            if (e.sessionOwner.TryGetProcessName(out string processName))
                if (ProcessHandler.TryMatchProcess(processName, out PProcess pp))
                    pp.Inbound += e.iCountOfBytes;

            InboundTotal += e.iCountOfBytes;
        }

        private static void OnValidateServerCertificate(object sender, ValidateServerCertificateEventArgs e)
        {
            if (SslPolicyErrors.None == e.CertificatePolicyErrors)
                return;

            e.ValidityState = CertificateValidity.ForceValid;
        }

        private void BeforeRequest(Session oSession)
        {
            if (oSession.TryGetProcessName(out string processName))
                if (ProcessHandler.HasRoutingEnabled(processName, out PProcess target))
                    if (oSession.TryToInjectHeader(ProxyServer.HasAuthentication, ProxyServer.ProxyURL, ProxyServer.Credentials))
                        RoutingRequest?.Invoke(this, new RoutingEventArgs(target, oSession.fullUrl));
        }

        public void Dispose()
        {
            FiddlerApplication.oProxy?.Detach();
            FiddlerApplication.Shutdown();
            FiddlerApplication.FiddlerAttach -= FiddlerAttach;
            FiddlerApplication.FiddlerDetach -= FiddlerDetach;
            FiddlerApplication.OnReadRequestBuffer -= OnBufferRead;
            FiddlerApplication.OnReadResponseBuffer -= OnBufferWrite;
            FiddlerApplication.BeforeRequest -= BeforeRequest;
            FiddlerApplication.OnValidateServerCertificate -= OnValidateServerCertificate;
        }
    }
}