using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
	public class Work
	{
		#region Properties

		public int EmployeeId { get; set; }

		public int CinemaId { get; set; }

		public DateTime FromDate { get; set; }
		public DateTime? ToDate { get; set; }

		#endregion

		#region Navigation Properties

		public virtual Employee Employee { get; set; }

		public virtual Cinema Cinema { get; set; }

		#endregion
	}
}
