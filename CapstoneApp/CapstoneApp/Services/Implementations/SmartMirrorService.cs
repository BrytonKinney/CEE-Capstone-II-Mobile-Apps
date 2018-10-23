using System;
using Android.Net.Wifi;
using CapstoneApp.Shared.Models;
using CapstoneApp.Shared.Services.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Linq;
using System.Net;
using Android.Content;
using Zeroconf;

namespace CapstoneApp.Shared.Services.Implementations
{
    public class SmartMirrorService : ISmartMirrorService
    {
        public async Task<List<SmartMirror>> SearchForSmartMirrors()
        {
            List<SmartMirror> mirrors = new List<SmartMirror>();
            WifiManager wifi = (WifiManager) Android.App.Application.Context.GetSystemService(Context.WifiService);
            var wifiLock = wifi.CreateMulticastLock("Zeroconf lock");
            try
            {
                wifiLock.Acquire();
                ILookup<string, string> domains = await ZeroconfResolver.BrowseDomainsAsync();
                var responses = await ZeroconfResolver.ResolveAsync(domains.Select(g => g.Key));
                foreach (var domain in responses)
                {
                    if (domain.DisplayName.Contains("smartmirror"))
                    {
                        mirrors.Add(new SmartMirror()
                        {
                            HostName = domain.DisplayName,
                            IP = IPAddress.Parse(domain.IPAddress)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                wifiLock.Release();
            }
            return mirrors;
        }
    }
}
