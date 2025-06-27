using CommunityToolkit.Mvvm.ComponentModel;
using TestSystemWpf.ViewModels;

namespace TestSystemWpf.Services
{
	/// <summary>
	/// Служба навигации.
	/// </summary>
	public partial class NavigationService : ObservableObject, INavigationService
	{
		/// <summary>
		/// Текущий ViewModel.
		/// </summary>
		[ObservableProperty]
		private BaseViewModel? currentViewModel;

		/// <summary>
		/// Перейти к указанному ViewModel.
		/// </summary>
		public void Navigate(BaseViewModel viewModel) => CurrentViewModel = viewModel;
	}
}
