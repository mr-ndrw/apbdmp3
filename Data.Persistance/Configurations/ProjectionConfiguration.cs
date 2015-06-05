using System.Data.Entity.ModelConfiguration;
using Logic.Entities;

namespace Data.Persistance.Configurations
{
	public class ProjectionConfiguration : EntityTypeConfiguration<Projection>
	{
		public ProjectionConfiguration()
		{
		    ToTable("Projection");
			HasKey(projection => projection.Id);

			Property(projection => projection.DateTime)
				.IsRequired();

			Property(projection => projection.Length)
				.IsRequired();
		}
	}
}