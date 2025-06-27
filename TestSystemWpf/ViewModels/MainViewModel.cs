using CommunityToolkit.Mvvm.Input;
using TestSystemWpf.Services;

namespace TestSystemWpf.ViewModels
{
	/// <summary>
	/// Главная ViewModel.
	/// </summary>
	public partial class MainViewModel(INavigationService nav, ITestRepository repo) : BaseViewModel
	{
		/// <summary>
		/// Служба навигации.
		/// </summary>
		public INavigationService NavigationService { get; } = nav;

		/// <summary>
		/// Выход из приложения.
		/// </summary>
		[RelayCommand]
		private void Exit() => App.Current.Shutdown();

		/// <summary>
		/// Открыть список тестов.
		/// </summary>
		[RelayCommand]
		private void OpenTests()
		{
			var vm = new TestListViewModel(repo, nav);
			nav.Navigate(vm);
		}
	}
}
