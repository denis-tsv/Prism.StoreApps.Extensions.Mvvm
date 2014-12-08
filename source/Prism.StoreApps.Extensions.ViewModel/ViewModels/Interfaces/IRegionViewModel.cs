using System.Threading.Tasks;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Interfaces
{
    public interface IRegionViewModel : IViewModel
    {
        Task InitializeAsync();
    }
}
