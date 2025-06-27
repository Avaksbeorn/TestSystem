using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemWpf.ViewModels;

namespace TestSystemWpf.Services
{
	/// <summary>
	/// Служба навигации.
	/// </summary>
	public interface INavigationService
	{
		/// <summary>
		/// Текущий ViewModel.
		/// </summary>
		BaseViewModel? CurrentViewModel { get; }

		/// <summary>
		/// Навигация к ViewModel.
		/// </summary>
		void Navigate(BaseViewModel viewModel);
	}
}
