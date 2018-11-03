using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using CapstoneApp.Shared.Entities;

namespace CapstoneApp.Shared.Models
{
    public class SmartMirrorModel
    {
        public SmartMirrorModel()
        {

        }
        public SmartMirrorModel(SmartMirror sm)
        {
            Id = sm.Id;
            HostName = sm.HostName;
            IP = IPAddress.Parse(sm.IpAddress);
        }
        public int? Id { get; set; }
        public string HostName { get; set; }
        public IPAddress IP { get; set; }
        public string IpAddressString => IP.ToString();
    }
}
