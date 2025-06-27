using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestSystemWpf.Models;
using TestSystemWpf.Services;

namespace TestSystemWpf.ViewModels
{
	/// <summary>
	/// Редактор теста.
	/// </summary>
	public sealed partial class TestEditorViewModel : BaseViewModel
	{
		private readonly ITestRepository _repo;
		private readonly INavigationService _nav;
		private readonly bool _isNew;

		/// <summary>
		/// Вопросы теста.
		/// </summary>
		public ObservableCollection<QuestionViewModel> Questions { get; }

		/// <summary>
		/// Выбранный вопрос.
		/// </summary>
		[ObservableProperty]
		private QuestionViewModel? selectedQuestion;

		/// <summary>
		/// Заголовок теста.
		/// </summary>
		[ObservableProperty]
		private string title;

		/// <summary>
		/// Модель теста.
		/// </summary>
		public Test Model { get; }

		/// <summary>
		/// Инициализация редактора.
		/// </summary>
		public TestEditorViewModel(ITestRepository repo, INavigationService nav, Test test, bool isNew)
		{
			_repo = repo;
			_nav = nav;
			_isNew = isNew;
			Model = test;
			Title = test.Title;
			Questions = new ObservableCollection<QuestionViewModel>(
				test.Questions.Select(q => new QuestionViewModel(q)));
		}

		/// <summary>
		/// Добавить вопрос.
		/// </summary>
		[RelayCommand]
		private void AddQuestion()
		{
			var q = new Question { Text = "Новый вопрос" };
			var vm = new QuestionViewModel(q);
			Questions.Add(vm);
			Model.Questions.Add(q);
			SelectedQuestion = vm;
		}

		/// <summary>
		/// Удалить вопрос.
		/// </summary>
		[RelayCommand]
		private void RemoveQuestion(QuestionViewModel? q)
		{
			if (q is null) return;
			var idx = Questions.IndexOf(q);
			Questions.Remove(q);
			Model.Questions.RemoveAt(idx);
			SelectedQuestion = null;
		}

		/// <summary>
		/// Сохранить тест.
		/// </summary>
		[RelayCommand]
		private async Task SaveAsync()
		{
			Model.Title = Title;
			foreach (var q in Questions)
				q.CommitCorrectFlags();

			await _repo.SaveAsync(Model);
			_nav.Navigate(new TestListViewModel(_repo, _nav));
		}

		/// <summary>
		/// Отменить редактирование.
		/// </summary>
		[RelayCommand]
		private void Cancel()
		{
			_nav.Navigate(new TestListViewModel(_repo, _nav));
		}
	}
}
