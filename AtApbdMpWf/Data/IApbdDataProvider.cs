using System;
using System.Collections.Generic;
using AtApbdMpWf.Entity;

namespace AtApbdMpWf.Data
{
	public interface IApbdDataProvider
	{
		#region Cinema Operation Contracts

		Cinema GetCinema(int id);
		IEnumerable<Cinema> GetAllCinemas();
		void CreateCinema(Cinema cinema);
		void DeleteCinema(Cinema cinema);
		void DeleteCinema(int id);
		void Update(Cinema cinema);
		Employee GetManager(Cinema cinema);
		void UpdateManager(Cinema cinema, Employee newManager);
		IEnumerable<Employee> GetAllEmployees(Cinema cinema);
		IEnumerable<Projection> GetAllProjections(Cinema cinema);
		IEnumerable<Projection> GetAllProjections(Cinema cinema, DateTime dateTime);
		Region GetRegion(int id);
		Region GetRegion(Cinema cinema);
		IEnumerable<ProjectionRoom> GetProjectionRooms(Cinema cinema);

		#endregion

		IEnumerable<Projection> GetAllProjections();
		void AddProjection(Projection projection, ProjectionRoom projectionRoom);
		void DeleteProjection(int id);
		void UpdateProjection(Projection projection);
		Projection GetProjection(int id);
	}
}