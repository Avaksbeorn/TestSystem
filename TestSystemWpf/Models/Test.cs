using System;
using System.Collections.Generic;

namespace TestSystemWpf.Models
{
	/// <summary>
	/// Тест.
	/// </summary>
	public class Test
	{
		/// <summary>
		/// Идентификатор теста.
		/// </summary>
		public string Id { get; set; } = Guid.NewGuid().ToString();

		/// <summary>
		/// Заголовок теста.
		/// </summary>
		public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Вопросы теста.
		/// </summary>
		public IList<Question> Questions { get; set; } = new List<Question>();
	}
}
