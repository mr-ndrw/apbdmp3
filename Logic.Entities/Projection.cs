using System;

namespace Logic.Entities
{
	public class Projection
	{
		#region Properties

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

		#endregion

		#region Navigation Properties

		/// <summary>
		///		Id of the ProjectionRoom in which this Projection takes place.
		/// </summary>
		public int IdProjectionRoom { get; set; }

		/// <summary>
		///		Reference to the ProjectionRoom in which this Projection takes place.
		/// </summary>
		public virtual ProjectionRoom ProjectionRoom { get; set; }

		#endregion
	}
}