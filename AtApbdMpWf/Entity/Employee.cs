using System.Data;

namespace AtApbdMpWf.Entity
{
	public class Employee
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string TelephoneNo { get; set; }
		public string Email { get; set; }

		public string NameSurname
		{
			get { return string.Format("{0} {1}", Name, Surname); }
		}

		public static Employee Create(IDataRecord record)
		{
			return new Employee
			{
				Id =			(int) record["Id"],
				Name =			(string) record["Name"],
				Surname =		(string) record["Surname"],
				TelephoneNo =	(string) record["TelephoneNo"],
				Email =			(string) record["Email"]
			};
		}
	}
}