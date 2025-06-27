using System.Windows;
using TestSystemWpf.ViewModels;

namespace TestSystemWpf
{
	/// <summary>
	/// Главное окно.
	/// </summary>
	public partial class MainWindow : Window
	{
		/// <summary>
		/// Инициализация окна.
		/// </summary>
		public MainWindow(MainViewModel vm)
		{
			InitializeComponent();
			DataContext = vm;
		}
	}
}
