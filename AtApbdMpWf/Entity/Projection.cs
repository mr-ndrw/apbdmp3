using System;
using System.Data;

namespace AtApbdMpWf.Entity
{
	public class Projection
	{
		/// <summary>
		///		Id of the Projection.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Date and Time on which Projection takes place.
		/// </summary>
		public DateTime DateTime { get; set; }

		/// <summary>
		///		Length in minutes of the Projection.
		/// </summary>
		public int Length { get; set; }		
		
		/// <summary>
		///		Id of the Projection room in which Projection takes place.
		/// </summary>
		public int IdProjectionRoom { get; set; }



		public static Projection Create(IDataRecord record)
		{
			return new Projection
			{
				Id =				(int) record["Id"],
				DateTime =			(DateTime) record["DateTime"],
 				Length =			(int) record["Length"],
				IdProjectionRoom =	(int) record["IdProjectionRoom"]
			};
		}
	}
}