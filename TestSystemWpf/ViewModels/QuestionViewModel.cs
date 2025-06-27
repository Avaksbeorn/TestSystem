using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TestSystemWpf.Models;

namespace TestSystemWpf.ViewModels
{
	/// <summary>
	/// Обёртка вопроса.
	/// </summary>
	public partial class QuestionViewModel : ObservableObject
	{
		/// <summary>
		/// Модель вопроса.
		/// </summary>
		public Question Model { get; }

		/// <summary>
		/// Варианты ответа.
		/// </summary>
		public ObservableCollection<AnswerViewModel> Answers { get; }

		/// <summary>
		/// Создание обёртки.
		/// </summary>
		public QuestionViewModel(Question model)
		{
			Model = model;
			Answers = new ObservableCollection<AnswerViewModel>(
				model.Answers.Select(a => new AnswerViewModel(a)));

			for (int i = 0; i < Answers.Count; i++)
			{
				Answers[i].IsCorrect =
					model.CorrectIndexes.Contains(i) ||
					i == model.CorrectIndex ||
					model.Answers[i].IsCorrect;
			}
		}

		/// <summary>
		/// Текст вопроса.
		/// </summary>
		public string Text
		{
			get => Model.Text;
			set => SetProperty(Model.Text, value, Model, (m, v) => m.Text = v);
		}

		/// <summary>
		/// Добавить ответ.
		/// </summary>
		[RelayCommand]
		private void AddAnswer()
		{
			var a = new Answer { Text = "Вариант" };
			var vm = new AnswerViewModel(a);
			Answers.Add(vm);
			Model.Answers.Add(a);
		}

		/// <summary>
		/// Удалить ответ.
		/// </summary>
		[RelayCommand]
		private void RemoveAnswer(AnswerViewModel? answer)
		{
			if (answer is null) return;
			var idx = Answers.IndexOf(answer);
			Answers.Remove(answer);
			Model.Answers.RemoveAt(idx);
			Model.CorrectIndex = -1;
		}

		/// <summary>
		/// Обновить флаги правильности.
		/// </summary>
		public void CommitCorrectFlags()
		{
			foreach (var (vm, i) in Answers.Select((a, i) => (a, i)))
				Model.Answers[i].IsCorrect = vm.IsCorrect;

			Model.CorrectIndexes = Answers
				.Select((a, i) => a.IsCorrect ? i : -1)
				.Where(i => i >= 0)
				.ToList();

			Model.CorrectIndex = Model.CorrectIndexes.FirstOrDefault(-1);
		}
	}
}
