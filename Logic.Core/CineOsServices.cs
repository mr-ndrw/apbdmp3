using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Data.Persistance;
using Logic.Entities;

namespace Logic.Core
{
	public class CineOsServices : ICineOsServices, IDisposable
	{
		private readonly CineOsDbContext db = new CineOsDbContext();

		public CineOsServices(CineOsDbContext db)
		{
		}

		/// <summary>
		///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			db.Dispose();
		}

		/// <summary>
		///     Gets the collection of Cinemas.
		/// </summary>
		public IEnumerable<Cinema> Cinemas
		{
			get { return db.Cinemas; }
		}

		/// <summary>
		///     Gets the collection of Regions.
		/// </summary>
		public IEnumerable<Region> Regions
		{
			get { return db.Regions; }
		}

		/// <summary>
		///     Gets the collection of Employees.
		/// </summary>
		public IEnumerable<Employee> Employees
		{
			get { return db.Employees; }
		}

		/// <summary>
		///     Gets the collection of Projections.
		/// </summary>
		public IEnumerable<Projection> Projections
		{
			get { return db.Projections; }
		}

		/// <summary>
		///     Gets the collection of ProjectionRooms.
		/// </summary>
		public IEnumerable<ProjectionRoom> ProjectionRooms
		{
			get { return db.ProjectionRooms; }
		}

		/// <summary>
		///     Gets the collection of Work objects.
		/// </summary>
		public IEnumerable<Work> EmploymentHistories
		{
			get { return db.EmploymentHistories; }
		}

		/// <summary>
		///     Returns the collection of Employees employed in the specified Cinema.
		/// </summary>
		/// <param name="cinema">
		///     Cinema for which we are returning employed Employees.
		/// </param>
		/// <returns>
		///     Collection of Employees.
		/// </returns>
		public IEnumerable<Employee> GetEmployeesIn(Cinema cinema)
		{
			var employeesWithIncludedHistories = db.Employees;

			var result =	from employee in employeesWithIncludedHistories
							from work in employee.EmploymentHistory
							where work.Cinema.Id == cinema.Id
							select employee;

			return result.ToList();
		}

		/// <summary>
		///     Returns a collection of all projections for specified Cinema.
		/// </summary>
		/// <param name="cinema">
		///     Cinema for which we are returning all linked Projections.
		/// </param>
		/// <returns>
		///     Collection of Projections.
		/// </returns>
		public IEnumerable<Projection> GetProjectionsFor(Cinema cinema)
		{
			var result = db.Projections.Where(projection => projection.ProjectionRoom.IdCinema == cinema.Id);

			return result.ToList();
		}

		/// <summary>
		///     Returns the collection of all projections which will happen in future for the specified Cinema.
		/// </summary>
		/// <param name="cinema">
		///     Cinema for which we are returning all linked Projections.
		/// </param>
		/// <returns>
		///     Collection of Projections.
		/// </returns>
		public IEnumerable<Projection> GetFutureProjectionsFor(Cinema cinema)
		{
			var inclusionPredicate = new Func<Projection, bool>(projection => projection.ProjectionRoom.IdCinema == cinema.Id);
			var futurePredicate = new Func<Projection, bool>(projection => projection.DateTime > DateTime.Now);

			var result = Projections.Where(inclusionPredicate)
				.Where(futurePredicate);

			return result;
		}

		/// <summary>
		///		Returns the collection of ProjectionRooms contained in specified cinema.
		/// </summary>
		public IEnumerable<ProjectionRoom> GetProjectionRoomsFor(Cinema cinema)
		{
			return db.ProjectionRooms.Where(room => room.Cinema.Id == cinema.Id);
		}

		/// <summary>
		///		Retrieves the ProjectionRoom which exists by specified identifier.
		/// </summary>
		public Projection GetProjectionById(int id)
		{
			return db.Projections.Find(id);
		}

		/// <summary>
		///		Returns the collection of Employees employed in Cinemas in specified Region.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public IEnumerable<Employee> GetEmployeesIn(Region region)
		{
			var result = 
				from employee in Employees
				from work in employee.EmploymentHistory
				where work.Cinema.Region.Id == region.Id
				select employee;

			return result.ToList();
		}

		/// <summary>
		///		Returns the collection of Employees employed in Cinemas in Region of specified Id..
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public IEnumerable<Employee> GetEmployeesInRegionOf(int idRegion)
		{
			var region = db.Regions.Find(idRegion);

			return region == null ? new List<Employee>() : GetEmployeesIn(region);
		}

		/// <summary>
		///		Returns the collection of Employees employed in Cinema of specified Id..
		/// </summary>
		public IEnumerable<Employee> GetEmmployeesInCinemaOf(int idCinema)
		{
			var cinema = db.Cinemas.Find(idCinema);

			return cinema == null ? new List<Employee>() : GetEmployeesIn(cinema);
		}

		/// <summary>
		///		Returns the Employee of speceified id.
		/// </summary>
		public Employee GetEmployeeOf(int idEmployee)
		{
			return db.Employees.Find(idEmployee);
		}

		/// <summary>
		///		Returns the value of average projection time for given Cinema.
		/// </summary>
		public double CalculateProjectionTimePerEmployeeFor(Cinema cinema)
		{
			//	Tested using SQL queries on the database.
			//	Below LINQ-to-Entities query and and SQL query yielded the same results.

			var result =
				from entityCinema in Cinemas
				where entityCinema.Id == cinema.Id
				let rooms = entityCinema.ProjectionRooms
                let employeesCountInCinema = (entityCinema.EmployeesHistory != null) ? entityCinema.EmployeesHistory.Count : 0
				select (rooms.Sum(room => room.Projections.Sum(projection => projection.Length))/(double)employeesCountInCinema);

			return result.FirstOrDefault();
		}

	    public void AddProjection(Projection projection, ProjectionRoom selectedItem)
	    {
	        var projectionRoom = db.ProjectionRooms.Find(selectedItem.Id);

            db.Entry(projection).State = EntityState.Added;
	        projection.ProjectionRoom = projectionRoom;

	        db.SaveChanges();

	    }

	    public void UpdateManager(Cinema currentlyViewedCinema, Employee newManager)
	    {
	        var cinema = db.Cinemas.Find(currentlyViewedCinema.Id);
	        var emp = db.Employees.Find(newManager.Id);

	        cinema.Manager = emp;

            db.Entry(cinema).State = EntityState.Modified;
            db.Entry(newManager).State = EntityState.Modified;

	        db.SaveChanges();
	    }

	    public void UpdateEmployee(Employee employee)
	    {
	        var emp = db.Employees.Find(employee.Id);

	        emp.Email = employee.Email;
	        emp.Name = employee.Name;
	        emp.Surname = employee.Surname;
	        emp.TelephoneNo = employee.TelephoneNo;

            db.Entry(emp).State = EntityState.Modified;

	        db.SaveChanges();
	    }

	    public void UpdateProjection(Projection projectionToExecute)
	    {
	        var projection = db.Projections.Find(projectionToExecute.Id);

	        projection.DateTime = projectionToExecute.DateTime;
	        projection.Length = projectionToExecute.Length;
	        projection.IdProjectionRoom = projectionToExecute.IdProjectionRoom;

            db.Entry(projection).State = EntityState.Modified;

	        db.SaveChanges();
	    }

	    public void DeleteProjection(int projectionId)
	    {
            var projection = db.Projections.Find(projectionId);
            
            db.Entry(projection).State = EntityState.Deleted;

	        db.SaveChanges();
	    }

	    public void AddExsistingEmployeeToCinema(int id, Employee currentlyViewedEmployee)
	    {
            var cinema = db.Cinemas.Find(id);
	        var employee = db.Employees.Find(currentlyViewedEmployee.Id);
            var work = new Work() { Cinema = cinema, Employee = employee, FromDate = DateTime.Now };
            db.Entry(work).State = EntityState.Added;

	        db.SaveChanges();
	    }

	    public void AddEmployeeToCinema(int idCinema, Employee employee)
	    {
	        db.Entry(employee).State = EntityState.Added;

	        var cinema = db.Cinemas.Find(idCinema);

            var work = new Work(){Cinema =  cinema, Employee = employee, FromDate = DateTime.Now};
            db.Entry(work).State = EntityState.Added;

	        db.SaveChanges();
	    }

	    public void CreateCinema(Cinema cinema)
	    {
	        db.Entry(cinema).State = EntityState.Added;
	        db.SaveChanges();
	    }

	    public void AddRegion(string regionName)
	    {
	        var region = new Region(){Name = regionName };

            db.Entry(region).State = EntityState.Added;
	        db.SaveChanges();
	    }

	    public void EditRegion(Region editedRegion)
	    {
	        var region = db.Regions.Find(editedRegion.Id);
	        region.Name = editedRegion.Name;

            db.Entry(region).State = EntityState.Modified;

	        db.SaveChanges();
	    }

	    public void DeleteRegion(Region currentlyViewedRegion)
	    {
            var region = db.Regions.Find(currentlyViewedRegion.Id);

	        var connectedCinemas = Cinemas.Where(cinema => cinema.IdRegion == region.Id);

	        foreach (var connectedCinema in connectedCinemas)
	        {
	            foreach (var projectionRoom in ProjectionRooms.Where(room => room.IdCinema == connectedCinema.Id))
	            {
	                foreach (var source in Projections.Where(proj => proj.IdProjectionRoom == projectionRoom.Id))
	                {
	                    db.Entry(source).State = EntityState.Deleted;
	                }
                    db.Entry(projectionRoom).State = EntityState.Deleted;
	            }
                db.Entry(connectedCinema).State = EntityState.Deleted;
	        }

            db.Entry(region).State = EntityState.Deleted;

            db.SaveChanges();
	    }

	    public void DeleteCinema(Cinema currentlyViewedCinema)
	    {
            var cinema = db.Cinemas.Find(currentlyViewedCinema.Id);
            db.Entry(cinema).State = EntityState.Deleted;

            foreach (var projectionRoom in ProjectionRooms.Where(room => room.IdCinema == cinema.Id))
            {
                foreach (var source in Projections.Where(proj => proj.IdProjectionRoom == projectionRoom.Id))
                {
                    db.Entry(source).State = EntityState.Deleted;
                }
                db.Entry(projectionRoom).State = EntityState.Deleted;
            }

            db.SaveChanges();
	    }

	    public void DeleteEmployee(int idEmployee)
	    {
	        var emp = db.Employees.Find(idEmployee);
            db.Entry(emp).State = EntityState.Deleted;

	        foreach (var work in EmploymentHistories.Where(work => work.EmployeeId == idEmployee))
	        {
	            db.Entry(work).State = EntityState.Deleted;
	        }

	        db.SaveChanges();
	    }

	    public void Update(Cinema editedCinema)
	    {

	        var cinema = db.Cinemas.Find(editedCinema.Id);
	        cinema.Address = editedCinema.Address;
	        cinema.Name = editedCinema.Name;
	        cinema.TelephoneNo = editedCinema.TelephoneNo;
	    }
	}
}
