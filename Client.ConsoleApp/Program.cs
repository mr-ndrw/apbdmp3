using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Persistance;
using Logic.Entities;

namespace Client.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var context = new CineOsDbContext();

			var cinema = new Cinema() {Id = 2};

			Console.WriteLine(context.Cinemas.Count());
/*
			var result =
				(from entityCinema in context.Cinemas
				where entityCinema.Id == cinema.Id
				let rooms = entityCinema.ProjectionRooms
				let employeesCountInCinema = entityCinema.EmployeesHistory.Count
				select (rooms.Sum(room => room.Projections.Sum(projection => projection.Length)) / (double)employeesCountInCinema));

			

			Console.WriteLine(result.FirstOrDefault());*/

			Console.ReadKey();
		}
	}
}
