using System;
using System.Windows.Forms;
using AtApbdMpWf.Forms;
using Data.Persistance;
using Logic.Core;

namespace AtApbdMpWf
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(new CineOsServices(new CineOsDbContext())));
		}
	}
}
