using System.Collections.Generic;

namespace Logic.Entities
{
	public class Region
	{
		#region Properties
		public int Id { get; set; }
		public string Name { get; set; } 
		#endregion

		#region Navigation Properties

		/// <summary>
		///		Collection of Cinemas located within this Region.
		/// </summary>
		public virtual ICollection<Cinema> Cinemas { get; set; }

		#endregion

	}
}