using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;

namespace Data.Persistance.Configurations
{
	public class WorkConfiguration : EntityTypeConfiguration<Work>
	{
		public WorkConfiguration()
		{
		    ToTable("Work");
			HasKey(work => new {work.CinemaId, work.EmployeeId}); //use anonymous type to configure two Primary Keys at once.

			Property(work => work.FromDate)
				.IsRequired();

			Property(work => work.ToDate)
				.IsOptional();

			HasRequired(work => work.Employee)
				.WithMany(employee => employee.EmploymentHistory)
				.HasForeignKey(work => work.EmployeeId)
				.WillCascadeOnDelete(true);

			HasRequired(work => work.Cinema)
				.WithMany(cinema => cinema.EmployeesHistory)
				.HasForeignKey(work => work.CinemaId)
				.WillCascadeOnDelete(true);

		}
	}
}
