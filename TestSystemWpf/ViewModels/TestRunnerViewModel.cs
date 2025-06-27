using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestSystemWpf.Models;
using TestSystemWpf.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows;

namespace TestSystemWpf.ViewModels
{
	/// <summary>
	/// Прохождение теста.
	/// </summary>
	public sealed partial class TestRunnerViewModel : BaseViewModel
	{
		/// <summary>
		/// Варианты ответа.
		/// </summary>
		public ObservableCollection<AnswerChoiceViewModel> Answers { get; } = new();

		/// <summary>
		/// Текст вопроса.
		/// </summary>
		[ObservableProperty]
		private string questionText = string.Empty;

		/// <summary>
		/// Номер вопроса.
		/// </summary>
		public int CurrentNumber => _currentIndex + 1;

		/// <summary>
		/// Количество вопросов.
		/// </summary>
		public int TotalQuestions => _model.Questions.Count;

		/// <summary>
		/// Текст кнопки далее.
		/// </summary>
		public string NextButtonText => _currentIndex == _model.Questions.Count - 1 ? "Завершить" : "Далее";

		private readonly INavigationService _nav;
		private readonly Test _model;
		private int _currentIndex = 0;
		private readonly List<List<int>> _chosen = new();

		/// <summary>
		/// Создание VM.
		/// </summary>
		public TestRunnerViewModel(INavigationService nav, Test model)
		{
			_nav = nav;
			_model = model;
			LoadQuestion();
		}

		/// <summary>
		/// Перейти к следующему.
		/// </summary>
		[RelayCommand(CanExecute = nameof(CanGoNext))]
		private void Next()
		{
			_chosen.Add(Answers
				.Select((a, i) => (a.IsSelected, i))
				.Where(t => t.IsSelected)
				.Select(t => t.i)
				.ToList());

			if (_currentIndex < _model.Questions.Count - 1)
			{
				_currentIndex++;
				LoadQuestion();
			}
			else
			{
				Finish();
			}
		}

		/// <summary>
		/// Проверить возможность перехода.
		/// </summary>
		private bool CanGoNext() => Answers.Any(a => a.IsSelected);

		/// <summary>
		/// Отменить тест.
		/// </summary>
		[RelayCommand]
		private void Cancel() => _nav.Navigate(new TestListViewModel(
			App.Current.Services.GetRequiredService<ITestRepository>(),
			_nav));

		/// <summary>
		/// Загрузить вопрос.
		/// </summary>
		private void LoadQuestion()
		{
			foreach (var a in Answers)
				a.PropertyChanged -= AnswerChanged;

			Answers.Clear();

			var q = _model.Questions[_currentIndex];
			QuestionText = q.Text;

			foreach (var a in q.Answers)
			{
				var vm = new AnswerChoiceViewModel(a.Text);
				vm.PropertyChanged += AnswerChanged;
				Answers.Add(vm);
			}

			OnPropertyChanged(nameof(CurrentNumber));
			OnPropertyChanged(nameof(NextButtonText));
			NextCommand.NotifyCanExecuteChanged();
		}

		/// <summary>
		/// Обновить состояние.
		/// </summary>
		private void AnswerChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(AnswerChoiceViewModel.IsSelected))
				NextCommand.NotifyCanExecuteChanged();
		}

		/// <summary>
		/// Завершить тест.
		/// </summary>
		private void Finish()
		{
			var score = 0;

			for (int i = 0; i < _model.Questions.Count; i++)
			{
				var q = _model.Questions[i];
				var correct = q.CorrectIndexes.Any()
					? q.CorrectIndexes
					: (q.CorrectIndex >= 0 ? new List<int> { q.CorrectIndex } : new List<int>());

				var chosenSet = _chosen[i].OrderBy(x => x);
				var correctSet = correct.OrderBy(x => x);

				if (chosenSet.SequenceEqual(correctSet))
					score++;
			}

			MessageBox.Show($"Результат: {score} из {_model.Questions.Count}",
							"Тест завершён",
							MessageBoxButton.OK,
							MessageBoxImage.Information);

			_nav.Navigate(new TestListViewModel(
				App.Current.Services.GetRequiredService<ITestRepository>(),
				_nav));
		}
	}
}
