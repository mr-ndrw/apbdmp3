using System;
using System.Collections.Generic;
using System.Linq;
using AtApbdMpWf.Data;
using AtApbdMpWf.Entity;

namespace AtApbdMpWf.BusinessLogic
{
	public class CinemaService
	{
		private readonly IApbdDataProvider _dataConnection;

		public CinemaService(IApbdDataProvider dataConnection)
		{
			_dataConnection = dataConnection;
		}

		public Projection GetProjection(int id)
		{
			return _dataConnection.GetProjection(id);
		}


		public Cinema GetCinema(int id)
		{
			return _dataConnection.GetCinema(id);
		}

		public List<Cinema> GetAllCinemas()
		{
			return _dataConnection.GetAllCinemas().ToList();
		}
		public void CreateCinema(Cinema cinema)
		{
			_dataConnection.CreateCinema(cinema);
		}

		public void DeleteCinema(Cinema cinema)
		{
			_dataConnection.DeleteCinema(cinema);	
		}

		public void DeleteCinema(int id)
		{
			_dataConnection.DeleteCinema(id);
		}

		public void Update(Cinema cinema)
		{
			_dataConnection.Update(cinema);
		}

		public Employee GetManager(Cinema cinema)
		{
			return _dataConnection.GetManager(cinema);
		}

		public void UpdateManager(Cinema cinema, Employee newManager)
		{
			_dataConnection.UpdateManager(cinema, newManager);
		}

		public List<Employee> GetAllEmployees(Cinema cinema)
		{
			return _dataConnection.GetAllEmployees(cinema).ToList();
		}

		public List<Projection> GetAllProjections(Cinema cinema)
		{
			return _dataConnection.GetAllProjections(cinema).ToList();
		}

		public List<Projection> GetAllProjections(Cinema cinema, DateTime dateTime)
		{
			return _dataConnection.GetAllProjections(cinema, dateTime)
				.ToList();
		}

		public List<ProjectionRoom> GetProjectionRooms(Cinema cinema)
		{
			return _dataConnection.GetProjectionRooms(cinema).ToList();
		}

		public void AddProjection(Projection projection, ProjectionRoom projectionRoom)
		{
			_dataConnection.AddProjection(projection,  projectionRoom);
		}

		public Region GetRegion(int id)
		{
			return _dataConnection.GetRegion(id);
		}

		public Region GetRegion(Cinema cinema)
		{
			return _dataConnection.GetRegion(cinema);
		}

		public void DeleteProjection(int id)
		{
			_dataConnection.DeleteProjection(id);
		}

		public void UpdateProjection(Projection projection)
		{
			_dataConnection.UpdateProjection(projection);
		}
	}
}