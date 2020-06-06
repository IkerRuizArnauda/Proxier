using System;
using System.Text;

using ProtoBuf;
using Proxier.Helpers.Enums;

namespace Proxier.Core
{
    [ProtoContract]
    public class ProxyServer
    {
        [ProtoMember(1)]
        public string IPAddress { get; set; }
        [ProtoMember(2)]
        public string Port { get; set; }
        [ProtoMember(3)]
        public ProxyType ProxyType { get; set; }
        [ProtoMember(4)]
        public string Username { get; set; }
        [ProtoMember(5)]
        public string Password { get; set; }
        [ProtoIgnore]
        public string ProxyURL => $"{(ProxyType == ProxyType.Socks ? "socks=" : "")}{IPAddress}:{Port}";
        [ProtoIgnore]
        public string Credentials => HasAuthentication ? Convert.ToBase64String(Encoding.ASCII.GetBytes($"{Username}:{Password}")) : string.Empty;
        [ProtoIgnore]
        public bool HasAuthentication => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);

        /// <summary>
        /// Default Proxy (TOR).
        /// </summary>
        /// <returns></returns>
        public static ProxyServer BuildDefault()
        {
            return new ProxyServer()
            {
                IPAddress = "127.0.0.1",
                Port = "9150",
                ProxyType = ProxyType.Http,
                Username = "",
                Password = "",
            };
        }
    }

    
}
