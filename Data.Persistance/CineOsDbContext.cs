using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Data.Persistance.Configurations;
using Logic.Entities;

namespace Data.Persistance
{
	public class CineOsDbContext : DbContext
	{
		public CineOsDbContext()
			: base("name=CineOsDbContext")
		{
			this.Configuration.LazyLoadingEnabled = true;
		}

		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Cinema> Cinemas { get; set; }
		public virtual DbSet<Projection> Projections { get; set; }
		public virtual DbSet<ProjectionRoom> ProjectionRooms { get; set; }
		public virtual DbSet<Work> EmploymentHistories { get; set; }
		public virtual DbSet<Region> Regions { get; set; }

		/// <summary>
		/// This method is called when the model for a derived context has been initialized, but
		///             before the model has been locked down and used to initialize the context.  The default
		///             implementation of this method does nothing, but it can be overridden in a derived class
		///             such that the model can be further configured before it is locked down.
		/// </summary>
		/// <remarks>
		/// Typically, this method is called only once when the first instance of a derived context
		///             is created.  The model for that context is then cached and is for all further instances of
		///             the context in the app domain.  This caching can be disabled by setting the ModelCaching
		///             property on the given ModelBuidler, but note that this can seriously degrade performance.
		///             More control over caching is provided through use of the DbModelBuilder and DbContextFactory
		///             classes directly.
		/// </remarks>
		/// <param name="modelBuilder">The builder that defines the model for the context being created. </param>
		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			//Database.SetInitializer(new DropCreateDatabaseAlways<CineOsDbContext>());

			modelBuilder.Configurations.Add(new EmployeeConfiguration());
			modelBuilder.Configurations.Add(new WorkConfiguration());
			modelBuilder.Configurations.Add(new CinemaConfiguration());
			modelBuilder.Configurations.Add(new ProjectionRoomConfiguration());
			modelBuilder.Configurations.Add(new ProjectionConfiguration());
			modelBuilder.Configurations.Add(new RegionConfiguration());
			base.OnModelCreating(modelBuilder);
		}
	}
}