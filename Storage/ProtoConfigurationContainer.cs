using System;
using System.IO;
using System.Collections.Generic;

using ProtoBuf;
using Proxier.Core;

namespace Proxier.Storage
{
    [ProtoContract]
    public class ProtoConfigurationContainer : IDisposable
    {
        [ProtoMember(1)]
        public ProxyServer ProxyServer { get; set; } = ProxyServer.BuildDefault();
        [ProtoMember(2)]
        public List<PProcess> Processes = new List<PProcess>();

        public byte[] ToByteArray()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<ProtoConfigurationContainer>(ms, this);            
                return ms.ToArray();
            }
        }

        public static ProtoConfigurationContainer FromBytes(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
                return Serializer.Deserialize<ProtoConfigurationContainer>(ms);
        }

        public void Dispose()
        {
            Processes?.Clear();
            ProxyServer = null;
        }
    }
}
