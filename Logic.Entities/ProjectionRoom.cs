using System.Collections.Generic;

namespace Logic.Entities
{
	public class ProjectionRoom
	{
		#region Properties

		public int Id { get; set; }
		public string Name { get; set; }
		public string RoomNumber { get; set; }

		#endregion

		#region Navigation Properties

		/// <summary>
		///		Id of the Cinema in which this ProjectionRoom is situated.
		/// </summary>
		public int IdCinema { get; set; }

		/// <summary>
		///		Reference to the Cinema in which this ProjectionRoom is situated.
		/// </summary>
		public virtual Cinema Cinema { get; set; }

		/// <summary>
		///		Collection of Projection which took or will take place in this Projection Room.
		/// </summary>
		public virtual ICollection<Projection> Projections { get; set; }

		#endregion
	}
}