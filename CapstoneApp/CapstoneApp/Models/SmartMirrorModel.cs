using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CapstoneApp.Shared.Models
{
    public class SmartMirrorModel
    {
        public string HostName { get; set; }
        public IPAddress IP { get; set; }
        public string IpAddressString => IP.ToString();
    }
}
