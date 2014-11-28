using System.Threading.Tasks;
using Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs.Interfaces;

namespace Prism.StoreApps.Extensions.ViewModel.ViewModels.Childs
{
	public abstract class ModalChildViewModel<T> : ChildViewModel, IModalChildViewModel<T>
	{
		private T _modalResult;
		public T ModalResult
		{
			get { return _modalResult; }
			protected set
			{
				_modalResult = value;
				Close();
			}
		}

		object IModalChildViewModel.ModalResult
		{
			get
			{
				return ModalResult;
			}
		}

        public new async Task<T> ShowAsync()
        {
            await OnShow();
            var result = await ChildViewModelManager.ShowViewModelAsync(this);
            return (T)result;
        }
	}
}
