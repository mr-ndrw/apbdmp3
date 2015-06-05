using System.Data.Entity.ModelConfiguration;
using Logic.Entities;

namespace Data.Persistance.Configurations
{
	public class CinemaConfiguration : EntityTypeConfiguration<Cinema>
	{
		public CinemaConfiguration()
		{
		    ToTable("Cinema");
			HasKey(cinema => cinema.Id);

			Property(cinema => cinema.Name)
				.IsOptional();

			Property(cinema => cinema.Address)
				.IsRequired();

			Property(cinema => cinema.TelephoneNo)
				.IsRequired()
				.HasMaxLength(20);

			HasMany(cinema => cinema.ProjectionRooms)
				.WithRequired(room => room.Cinema)
				.HasForeignKey(room => room.IdCinema)
				.WillCascadeOnDelete(true);
		}
	}
}