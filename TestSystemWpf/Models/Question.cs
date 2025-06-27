using System;
using System.Collections.Generic;

namespace TestSystemWpf.Models
{
	/// <summary>
	/// Вопрос теста.
	/// </summary>
	public class Question
	{
		/// <summary>
		/// Текст вопроса.
		/// </summary>
		public string Text { get; set; } = string.Empty;

		/// <summary>
		/// Варианты ответа.
		/// </summary>
		public IList<Answer> Answers { get; set; } = new List<Answer>();

		/// <summary>
		/// Индекс правильного ответа.
		/// </summary>
		public int CorrectIndex { get; set; } = -1;

		/// <summary>
		/// Индексы правильных ответов.
		/// </summary>
		public IList<int> CorrectIndexes { get; set; } = new List<int>();
	}
}
