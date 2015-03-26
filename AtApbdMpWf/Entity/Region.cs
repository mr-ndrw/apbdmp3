using System.Data;

namespace AtApbdMpWf.Entity
{
	public class Region
	{
		public int Id { get; set; }
		public string Name { get; set; }


		public static Region Create(IDataRecord record)
		{
			return new Region
			{
				Id = (int) record["Id"],
				Name = (string) record["Name"]
			};
		}
	}
}