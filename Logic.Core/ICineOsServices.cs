using System.Collections.Generic;
using Logic.Entities;

namespace Logic.Core
{
	public interface ICineOsServices
	{
		/// <summary>
		///		Gets the collection of Cinemas.
		/// </summary>
		IEnumerable<Cinema> Cinemas { get; }
		/// <summary>
		///		Gets the collection of Regions.
		/// </summary>
		IEnumerable<Region> Regions { get; }
		/// <summary>
		///		Gets the collection of Employees.
		/// </summary>
		IEnumerable<Employee> Employees { get; }
		/// <summary>
		///		Gets the collection of Projections.
		/// </summary>
		IEnumerable<Projection> Projections { get; }
		/// <summary>
		///		Gets the collection of ProjectionRooms.
		/// </summary>
		IEnumerable<ProjectionRoom> ProjectionRooms { get; }
		/// <summary>
		///		Gets the collection of Work objects.
		/// </summary>
		IEnumerable<Work> EmploymentHistories { get; }

		/// <summary>
		///		Returns the collection of Employees employed in the specified Cinema.
		/// </summary>
		/// <param name="cinema">
		///		Cinema for which we are returning employed Employees.
		/// </param>
		/// <returns>
		///		Collection of Employees.
		/// </returns>
		IEnumerable<Employee> GetEmployeesIn(Cinema cinema);

		/// <summary>
		///		Returns a collection of all projections for specified Cinema.
		/// </summary>
		/// <param name="cinema">
		///		Cinema for which we are returning all linked Projections.
		/// </param>
		/// <returns>
		///		Collection of Projections.
		/// </returns>
		IEnumerable<Projection> GetProjectionsFor(Cinema cinema);

		/// <summary>
		///		Returns the collection of all projections which will happen in future for the specified Cinema.
		/// </summary>
		/// <param name="cinema">
		///		Cinema for which we are returning all linked Projections.
		/// </param>
		/// <returns>
		///		Collection of Projections.
		/// </returns>
		IEnumerable<Projection> GetFutureProjectionsFor(Cinema cinema);

		/// <summary>
		///		Returns the collection of ProjectionRooms contained in specified cinema.
		/// </summary>
		IEnumerable<ProjectionRoom> GetProjectionRoomsFor(Cinema cinema);

		/// <summary>
		///		Retrieves the ProjectionRoom which exists by specified identifier.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		Projection GetProjectionById(int id);

		/// <summary>
		///		Returns the collection of Employees employed in Cinemas in specified Region.
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		IEnumerable<Employee> GetEmployeesIn(Region region);

		/// <summary>
		///		Returns the collection of Employees employed in Cinemas in Region of specified Id..
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		IEnumerable<Employee> GetEmployeesInRegionOf(int idRegion);

		/// <summary>
		///		Returns the collection of Employees employed in Cinema of specified Id..
		/// </summary>
		IEnumerable<Employee> GetEmmployeesInCinemaOf(int idCinema);

		/// <summary>
		///		Returns the Employee of speceified id.
		/// </summary>
		Employee GetEmployeeOf(int idEmployee);

		/// <summary>
		///		Returns the value of average projection time for given Cinema.
		/// </summary>
		double CalculateProjectionTimePerEmployeeFor(Cinema cinema);

	    void AddProjection(Projection projection, ProjectionRoom selectedItem);
	    void UpdateManager(Cinema currentlyViewedCinema, Employee newManager);
	    void UpdateEmployee(Employee employee);
	    void UpdateProjection(Projection projectionToExecute);
	    void DeleteProjection(int projectionId);
	    void AddExsistingEmployeeToCinema(int id, Employee currentlyViewedEmployee);
	    void AddEmployeeToCinema(int idCinema, Employee employee);
	    void CreateCinema(Cinema cinema);
	    void AddRegion(string regionName);
	    void EditRegion(Region editedRegion);
	    void DeleteRegion(Region currentlyViewedRegion);
	    void DeleteCinema(Cinema currentlyViewedCinema);
	    void DeleteEmployee(int idEmployee);
	    void Update(Cinema editedCinema);
	}
}