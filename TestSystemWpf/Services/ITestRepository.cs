using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestSystemWpf.Models;

namespace TestSystemWpf.Services
{
	/// <summary>
	/// Репозиторий тестов.
	/// </summary>
	public interface ITestRepository
	{
		/// <summary>
		/// Загрузить все тесты.
		/// </summary>
		Task<IList<Test>> LoadAllAsync();

		/// <summary>
		/// Сохранить тест.
		/// </summary>
		Task SaveAsync(Test test);
	}
}
