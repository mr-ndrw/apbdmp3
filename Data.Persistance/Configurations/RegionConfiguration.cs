using System.Data.Entity.ModelConfiguration;
using Logic.Entities;

namespace Data.Persistance.Configurations
{
	public class RegionConfiguration : EntityTypeConfiguration<Region>
	{
		public RegionConfiguration()
		{
		    ToTable("Region");
			HasKey(region => region.Id);

			Property(region => region.Name)
				.IsRequired()
				.HasMaxLength(50);

			HasMany(region => region.Cinemas)
				.WithRequired(cinema => cinema.Region)
				.HasForeignKey(cinema => cinema.IdRegion)
				.WillCascadeOnDelete(true);

		}
	}
}