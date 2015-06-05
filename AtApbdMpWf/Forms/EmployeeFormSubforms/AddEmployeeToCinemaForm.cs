using System;
using System.Linq;
using System.Windows.Forms;
using Logic.Core;
using Logic.Entities;

namespace AtApbdMpWf.Forms.EmployeeFormSubforms
{
	public partial class AddEmployeeToCinemaForm : Form
	{
		private ICineOsServices _cineOsServices;
		private Employee _currentlyViewedEmployee;

		public AddEmployeeToCinemaForm(ICineOsServices cineOsServices, Employee currentlyViewedEmployee)
		{
			_cineOsServices = cineOsServices;
			_currentlyViewedEmployee = currentlyViewedEmployee;

			InitializeComponent();
		}

		private void AddEmployeeToCinemaForm_Load(object sender, EventArgs e)
		{
			employeeNameSurnameLabel.Text = _currentlyViewedEmployee.NameSurname;
			CinemaComboBox.DataSource = _cineOsServices.Cinemas.ToList();
			CinemaComboBox.ValueMember = "Id";
			CinemaComboBox.DisplayMember = "Name";
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selected = CinemaComboBox.SelectedItem as Cinema;
			RegionNameLabel.Text = selected.Region.Name;
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			var selected = CinemaComboBox.SelectedItem as Cinema;

			_cineOsServices.AddExsistingEmployeeToCinema(selected.Id, _currentlyViewedEmployee);
			Close();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
