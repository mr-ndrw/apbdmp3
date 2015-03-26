using System.Data;

namespace AtApbdMpWf.Entity
{
	/// <summary>
	///		POCO for Cinema.
	/// </summary>
	public class Cinema
	{
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
		///		Id of the Region to which the Cinema belongs.
		/// </summary>
		public int IdRegion { get; set; }

		/// <summary>
		///		Id of the Employee which manages this Cinema.
		/// </summary>
		public int IdManager { get; set; }

		/// <summary>
		///		Name of the Cinema.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		///		Returns a Cinema object based on the data contained in the interface.
		/// </summary>
		/// <param name="record"></param>
		/// <returns></returns>
		public static Cinema Create(IDataRecord record)
		{
			return new Cinema
			{
				Id =			(int) record["Id"],
				Address =		(string) record["Address"],
				TelephoneNo =	(string) record["TelephoneNo"],
				IdRegion =		(int) record["IdRegion"],
				IdManager =		(int) record["IdManager"],
				Name =			(string) record["Name"]
			};
		}
	}
}