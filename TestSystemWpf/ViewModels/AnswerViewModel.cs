using CommunityToolkit.Mvvm.ComponentModel;
using TestSystemWpf.Models;

namespace TestSystemWpf.ViewModels
{
	/// <summary>
	/// Обёртка ответа.
	/// </summary>
	public partial class AnswerViewModel : ObservableObject
	{
		/// <summary>
		/// Модель ответа.
		/// </summary>
		public Answer Model { get; }

		/// <summary>
		/// Создание обёртки.
		/// </summary>
		public AnswerViewModel(Answer model)
		{
			Model = model;
			isCorrect = model.IsCorrect;
		}

		[ObservableProperty]
		private bool isCorrect;

		/// <summary>
		/// Сохранить флаг.
		/// </summary>
		partial void OnIsCorrectChanged(bool value)
		{
			Model.IsCorrect = value;
		}

		/// <summary>
		/// Текст ответа.
		/// </summary>
		public string Text
		{
			get => Model.Text;
			set => SetProperty(Model.Text, value, Model, (m, v) => m.Text = v);
		}
	}
}
