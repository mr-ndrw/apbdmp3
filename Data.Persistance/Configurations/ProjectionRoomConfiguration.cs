using System.Data.Entity.ModelConfiguration;
using Logic.Entities;

namespace Data.Persistance.Configurations
{
	public class ProjectionRoomConfiguration : EntityTypeConfiguration<ProjectionRoom>
	{
		public ProjectionRoomConfiguration()
		{
		    ToTable("ProjectionRoom");
			HasKey(room => room.Id);

			Property(room => room.Name)
				.HasMaxLength(50);

			Property(room => room.RoomNumber)
				.HasMaxLength(10)
				.IsRequired(); //	strings are by default set to null by the EF in the database. IsRequired() counters that behaviour

			HasMany(room => room.Projections)
				.WithRequired(projection => projection.ProjectionRoom)
				.HasForeignKey(projection => projection.IdProjectionRoom)
				.WillCascadeOnDelete(true);
		}
	}
}