using System.Data.Entity.ModelConfiguration;
using Logic.Entities;

namespace Data.Persistance.Configurations
{
	public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
	{
		public EmployeeConfiguration()
		{
		    ToTable("Employee");
			HasKey(employee => employee.Id);

			Ignore(employee => employee.NameSurname);

			Property(employee => employee.Name)
				.IsRequired()
				.HasMaxLength(50);

			Property(employee => employee.Surname)
				.IsRequired()
				.HasMaxLength(50);

			Property(employee => employee.Email)
				.HasMaxLength(320);

			Property(employee => employee.TelephoneNo)
				.HasMaxLength(20);

			HasMany(employee => employee.ManagedCinemas)
				.WithRequired(cinema => cinema.Manager)
				.HasForeignKey(cinema => cinema.IdManager)
				.WillCascadeOnDelete(false);
		}
	}
}
