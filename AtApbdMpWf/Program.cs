using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtApbdMpWf.Data;
using AtApbdMpWf.Forms;

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
			var sqlConnection = new SqlConnection(Properties.Settings.Default.ApbdConnectionString);
			sqlConnection.Open();
			var apbdProvider = new ApbdDataProvider(sqlConnection);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(apbdProvider));
		}
	}
}
