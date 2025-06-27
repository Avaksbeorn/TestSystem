using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TestSystemWpf.Models;

namespace TestSystemWpf.Services
{
	/// <summary>
	/// JSON-репозиторий тестов.
	/// </summary>
	public class JsonTestRepository : ITestRepository
	{
		private const string FileName = "tests.json";

		/// <summary>
		/// Загрузить все тесты.
		/// </summary>
		public async Task<IList<Test>> LoadAllAsync()
		{
			if (!File.Exists(FileName)) return new List<Test>();
			await using var stream = File.OpenRead(FileName);
			var data = await JsonSerializer.DeserializeAsync<IList<Test>>(stream);
			return data ?? new List<Test>();
		}

		/// <summary>
		/// Сохранить тест.
		/// </summary>
		public async Task SaveAsync(Test test)
		{
			var all = await LoadAllAsync();

			// безопасный поиск индекса
			int idx = all.ToList().FindIndex(t => t.Id == test.Id);

			if (idx >= 0)
				all[idx] = test;   // обновляем существующий
			else
				all.Add(test);     // добавляем новый

			await using var stream = File.Create(FileName);
			await JsonSerializer.SerializeAsync(stream, all,
				new JsonSerializerOptions { WriteIndented = true });
		}
	}
}
