using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TestSystemWpf.Services;
using TestSystemWpf.ViewModels;

namespace TestSystemWpf
{
	/// <summary>
	/// Класс приложения.
	/// </summary>
	public partial class App : Application
	{
		/// <summary>
		/// Текущий экземпляр.
		/// </summary>
		public new static App Current => (App)Application.Current;

		/// <summary>
		/// Провайдер служб.
		/// </summary>
		public IServiceProvider Services { get; }

		/// <summary>
		/// Инициализация приложения.
		/// </summary>
		public App()
		{
			var services = new ServiceCollection();
			ConfigureServices(services);
			Services = services.BuildServiceProvider();
		}

		/// <summary>
		/// Настройка служб.
		/// </summary>
		private static void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<ITestRepository, JsonTestRepository>();
			services.AddSingleton<INavigationService, NavigationService>();
			services.AddSingleton<MainViewModel>();
			services.AddSingleton<MainWindow>();
		}

		/// <summary>
		/// Старт приложения.
		/// </summary>
		protected override void OnStartup(StartupEventArgs e)
		{
			var window = Services.GetRequiredService<MainWindow>();
			MainWindow = window;
			window.Show();
			base.OnStartup(e);
		}
	}
}
