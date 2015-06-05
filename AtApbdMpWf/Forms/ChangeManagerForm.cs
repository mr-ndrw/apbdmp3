using System;
using System.Windows.Forms;
using Logic.Core;
using Logic.Entities;

namespace AtApbdMpWf.Forms
{
	public partial class ChangeManagerForm : Form
	{
		private Cinema _currentlyViewedCinema;
		private Employee _currentManager;
		private ICineOsServices _cineOsServices;
		private CinemaForm _parent;

		public ChangeManagerForm(CinemaForm parent, ICineOsServices cineOsServices, Employee currentManager, Cinema currentlyViewedCinema)
		{
			_parent = parent;
			_cineOsServices = cineOsServices;
			_currentManager = currentManager;
			_currentlyViewedCinema = currentlyViewedCinema;

			InitializeComponent();
		}

		private void ChangeManagerForm_Load(object sender, EventArgs e)
		{
			//	Populate the combobox
			ComboBoxCinemaEmployees.DataSource = _cineOsServices.GetEmployeesIn(_currentlyViewedCinema);
			ComboBoxCinemaEmployees.ValueMember = "Id";
			ComboBoxCinemaEmployees.DisplayMember = "NameSurname";

			LabelCurrentlyViewedCinemaName.Text = _currentlyViewedCinema.Name;
			LabelCurrentManager.Text = _currentManager.NameSurname;
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonUpdate_Click(object sender, EventArgs e)
		{
			var newManager = (Employee) ComboBoxCinemaEmployees.SelectedItem;

			_cineOsServices.UpdateManager(_currentlyViewedCinema, newManager);

			_parent.UpdateForm();
			Close();
		}
	}
}
