using CommunityToolkit.Mvvm.ComponentModel;

namespace TestSystemWpf.ViewModels
{

	/// <summary>
	/// Вариант ответа на этапе прохождения теста.
	/// </summary>
	public partial class AnswerChoiceViewModel(string text) : ObservableObject
	{
		public string Text { get; } = text;

		[ObservableProperty]
		private bool isSelected;
	}
}