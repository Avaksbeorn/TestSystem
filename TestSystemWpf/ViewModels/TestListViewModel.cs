using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestSystemWpf.Models;
using TestSystemWpf.Services;

namespace TestSystemWpf.ViewModels
{
	/// <summary>
	/// VM списка тестов.
	/// </summary>
	public sealed partial class TestListViewModel : BaseViewModel
	{
		private readonly ITestRepository _repo;
		private readonly INavigationService _nav;

		/// <summary>
		/// Список тестов.
		/// </summary>
		public ObservableCollection<Test> Tests { get; } = new();

		/// <summary>
		/// Инициализация VM.
		/// </summary>
		public TestListViewModel(ITestRepository repo, INavigationService nav)
		{
			_repo = repo;
			_nav = nav;
			_ = RefreshAsync();
		}

		/// <summary>
		/// Конструктор для дизайнера.
		/// </summary>
		public TestListViewModel() : this(null!, null!) { }

		/// <summary>
		/// Обновить тесты.
		/// </summary>
		[RelayCommand]
		private async Task RefreshAsync()
		{
			var all = await _repo.LoadAllAsync();
			Tests.Clear();
			foreach (var t in all)
				Tests.Add(t);
		}

		/// <summary>
		/// Создать тест.
		/// </summary>
		[RelayCommand]
		private void CreateTest()
		{
			var draft = new Test { Title = "Новый тест" };
			var editor = new TestEditorViewModel(_repo, _nav, draft, isNew: true);
			_nav.Navigate(editor);
		}

		/// <summary>
		/// Редактировать тест.
		/// </summary>
		[RelayCommand]
		private void EditTest(Test? selected)
		{
			if (selected is null) return;
			var editor = new TestEditorViewModel(_repo, _nav, selected, isNew: false);
			_nav.Navigate(editor);
		}

		/// <summary>
		/// Пройти тест.
		/// </summary>
		[RelayCommand]
		private void RunTest(Test? selected)
		{
			if (selected is null) return;
			var runner = new TestRunnerViewModel(_nav, selected);
			_nav.Navigate(runner);
		}
	}
}
