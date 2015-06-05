using System.Collections.Generic;

namespace Logic.Entities
{
	public class Employee
	{
		#region Properties
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string TelephoneNo { get; set; }
		public string Email { get; set; }

		public string NameSurname
		{
			get { return string.Format("{0} {1}", Name, Surname); }
		} 
		#endregion

		#region Navigation Properties

		public virtual ICollection<Work> EmploymentHistory { get; set; }

		public virtual ICollection<Cinema> ManagedCinemas{ get; set; }

		#endregion
	}
}