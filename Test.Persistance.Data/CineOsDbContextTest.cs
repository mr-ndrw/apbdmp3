using Data.Persistance;
using Logic.Entities;
using NUnit.Framework;

namespace Test.Persistance.Data
{
	[TestFixture]
	public class CineOsDbContextTest
	{
		[Test]
		public void Test_If_EVERYTHING_BLOODY_WORSKAADARGH_BLOODFOR_THE_BLOODGOD_SKULLS_FOR_THE_SKULL_THRONE_AAAH()
		{
			var context = new CineOsDbContext();
			context.Employees.Add(new Employee {Name = "Max", Surname = "Power"});
		}
	}
}