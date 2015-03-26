using System.Windows.Forms;
using AtApbdMpWf.BusinessLogic;
using AtApbdMpWf.Data;

namespace AtApbdMpWf.Forms
{
	public partial class MainForm : Form
	{
		private IApbdDataProvider _apbdDataProvider;
		private CinemaService _cinemaService;

		public MainForm(IApbdDataProvider apbdDataProvider)
		{
			_apbdDataProvider = apbdDataProvider;
			_cinemaService = new CinemaService(_apbdDataProvider);

			InitializeComponent();
		}

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			var CinemasSubForm = new CinemaForm(_cinemaService);
			CinemasSubForm.FormBorderStyle = FormBorderStyle.None;
			CinemasSubForm.TopLevel = false;
			CinemasSubForm.ShowInTaskbar = false;
			CinemasSubForm.Show();
			CinemasSubForm.Dock = DockStyle.Fill;

			tabPage1.Controls.Add(CinemasSubForm);
		}
	}
}
