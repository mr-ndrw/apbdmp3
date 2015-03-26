using System.Data;

namespace AtApbdMpWf.Entity
{
	public class ProjectionRoom
	{
		public int Id { get; set; }
		public int IdCinema { get; set; }
		public string Name { get; set; }
		public string RoomNumber { get; set; }


		public static ProjectionRoom Create(IDataRecord record)
		{
			return new ProjectionRoom
			{
				Id = (int) record["Id"],
				IdCinema = (int) record["IdCinema"],
				Name = (string) record["Name"],
				RoomNumber = (string) record["RoomNumber"]
			};
		}
	}
}