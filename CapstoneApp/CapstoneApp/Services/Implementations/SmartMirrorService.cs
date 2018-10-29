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
        private static SmartMirrorModel _smartMirror;
        private static readonly object _lock = new object();

        public async Task<List<SmartMirrorModel>> SearchForSmartMirrors()
        {
            List<SmartMirrorModel> mirrors = new List<SmartMirrorModel>();
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
                        mirrors.Add(new SmartMirrorModel()
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

        public void SetInstance(SmartMirrorModel sm)
        {
            lock (_lock)
            {
                _smartMirror = sm;
            }
        }

        public SmartMirrorModel GetInstance()
        {
            lock (_lock)
            {
                return _smartMirror;
            }
        }
    }
}
