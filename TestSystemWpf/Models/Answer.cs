using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSystemWpf.Models
{
	/// <summary>
	/// Модель ответа.
	/// </summary>
	public class Answer
	{
		/// <summary>
		/// Текст.
		/// </summary>
		public string Text { get; set; } = string.Empty;

		/// <summary>
		/// Правильный ответ.
		/// </summary>
		public bool IsCorrect { get; set; }
	}
}
