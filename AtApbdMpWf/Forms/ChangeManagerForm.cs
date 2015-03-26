using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AtApbdMpWf.BusinessLogic;
using AtApbdMpWf.Entity;

namespace AtApbdMpWf.Forms
{
	public partial class ChangeManagerForm : Form
	{
		private Cinema _currentlyViewedCinema;
		private Employee _currentManager;
		private CinemaService _cinemaService;
		private CinemaForm _parent;

		public ChangeManagerForm(CinemaForm parent, CinemaService cinemaService, Employee currentManager, Cinema currentlyViewedCinema)
		{
			_parent = parent;
			_cinemaService = cinemaService;
			_currentManager = currentManager;
			_currentlyViewedCinema = currentlyViewedCinema;

			InitializeComponent();
		}

		private void ChangeManagerForm_Load(object sender, EventArgs e)
		{
			//	Populate the combobox
			ComboBoxCinemaEmployees.DataSource = _cinemaService.GetAllEmployees(_currentlyViewedCinema);
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

			_cinemaService.UpdateManager(_currentlyViewedCinema, newManager);

			_parent.UpdateForm();
			Close();
		}
	}
}
