using System.Collections.Generic;

namespace Logic.Entities
{
	/// <summary>
	///		POCO for Cinema.
	/// </summary>
	public class Cinema
	{
		#region Properties
		/// <summary>
		///		Id of the Cinema.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		///		Address of the Cinema.
		/// </summary>
		public string Address { get; set; }

		/// <summary>
		///		Telephone number to the Cinema.
		/// </summary>
		public string TelephoneNo { get; set; }

		/// <summary>
		///		Name of the Cinema.
		/// </summary>
		public string Name { get; set; } 
		#endregion

		#region Navigation Properties

		/// <summary>
		///		Manager of this Cinema.
		/// </summary>
		public virtual Employee Manager { get; set; }

		/// <summary>
		///		Id of the Region to which the Cinema belongs.
		/// </summary>
		public int IdRegion { get; set; }

		/// <summary>
		///		Id of the Employee which manages this Cinema.
		/// </summary>
		public int IdManager { get; set; }

		/// <summary>
		///		Region in which this Cinema is situated.
		/// </summary>
		public virtual Region Region { get; set; }

		/// <summary>
		///		Collection of Projection Rooms contained within this Cinema.
		/// </summary>
		public virtual ICollection<ProjectionRoom> ProjectionRooms { get; set; }

		/// <summary>
		///		Employment history between this Cinema and Employees.
		/// </summary>
		public virtual ICollection<Work> EmployeesHistory { get; set; }

		#endregion

	}
}