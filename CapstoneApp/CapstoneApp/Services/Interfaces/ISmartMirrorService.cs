using System.Collections.Generic;
using System.Threading.Tasks;
using CapstoneApp.Shared.Models;

namespace CapstoneApp.Shared.Services.Interfaces
{
    public interface ISmartMirrorService
    {
        Task<List<SmartMirrorModel>> SearchForSmartMirrors();
        void SetInstance(SmartMirrorModel sm);
        SmartMirrorModel GetInstance();
    }
}
