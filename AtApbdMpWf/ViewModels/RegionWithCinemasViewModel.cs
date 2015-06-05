using System.Collections.Generic;
using Logic.Entities;

namespace AtApbdMpWf.ViewModels
{
	public class RegionWithCinemasViewModel
	{
		public Region Region { get; set; }
		public List<Cinema> Cinemas { get; set; } 
	}
}
