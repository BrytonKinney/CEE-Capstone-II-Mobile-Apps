using System;
using System.Threading.Tasks;
using System.Timers;

namespace CapstoneApp.Shared.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        Task RefreshAccessToken();
        Task StartTimer();
    }
}
