using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Logic.Core;
using Logic.Entities;

namespace AtApbdMpWf.Forms
{
	public partial class CinemaForm : Form
	{
		private ICineOsServices _cineOsServices;
		private Cinema _currentlyViewedCinema;

		public CinemaForm(ICineOsServices cineOsServices)
		{
			_cineOsServices = cineOsServices;
			InitializeComponent();
			InitializeFormData();
		}

		public void InitializeFormData()
		{
			PopulateComboBoxCinemas();
			_currentlyViewedCinema = (Cinema) ComboBoxCinemas.SelectedItem;
			UpdateForm();
			EmployeesDgv.Columns[0].Visible = false;
		}

		public void UpdateForm()
		{
			//	Update the information corresponding to the currently chosen cinema
			LabelCinemaName.Text = _currentlyViewedCinema.Name;
			LabelAddress.Text = _currentlyViewedCinema.Address;
			LabelTelephone.Text = _currentlyViewedCinema.TelephoneNo;
			LabelRegion.Text = _currentlyViewedCinema.Region.Name;

			//	Set the link label of the manager
			var manager = _currentlyViewedCinema.Manager;
			var managerNameSurname = manager.NameSurname;

			LinkLabelManager.Text = managerNameSurname;

			//	Populate employees data grid view
			EmployeesDgv.DataSource = PopulateEmployeesDgv();

			AvgTimePerEmpLabel.Text = _cineOsServices.CalculateProjectionTimePerEmployeeFor(_currentlyViewedCinema).ToString(CultureInfo.CurrentCulture);

			//	Bolden the dates in month calendar
			ProjectionCalendar.BoldedDates = _cineOsServices.GetProjectionsFor(_currentlyViewedCinema)
				.Select(projection => projection.DateTime).ToArray();
			
		}

		DataTable PopulateProjectionDGVWithFutureProjections()
		{
			

			var dataTable = new DataTable();

			dataTable.Columns.Add("Id");
			dataTable.Columns.Add("Date and Time");
			dataTable.Columns.Add("Length");


			var futureProjections = _cineOsServices.GetFutureProjectionsFor(_currentlyViewedCinema);
			foreach (var futureProjection in futureProjections)
			{
				dataTable.Rows.Add(futureProjection.Id, futureProjection.DateTime, futureProjection.Length);
			}

			return dataTable;
		}

		DataTable PopulateProjectionDGVWithFutureProjections(DateTime selectedDateTime)
		{
			var projections = _cineOsServices.GetProjectionsFor(_currentlyViewedCinema);

			var dataTable = new DataTable();

			dataTable.Columns.Add("Id");
			dataTable.Columns.Add("Date and Time");
			dataTable.Columns.Add("Length");

			var futureProjections = projections.Where(projection => projection.DateTime.Date.Equals(selectedDateTime.Date));

			foreach (var futureProjection in futureProjections)
			{
				dataTable.Rows.Add(futureProjection.Id, futureProjection.DateTime, futureProjection.Length);
			}

			return dataTable;
		}

		void PopulateComboBoxCinemas()
		{
			var cinemaList = _cineOsServices.Cinemas.ToList();

			ComboBoxCinemas.DataSource = cinemaList;
			ComboBoxCinemas.ValueMember = "Id";
			ComboBoxCinemas.DisplayMember = "Name";
		}

		DataTable PopulateEmployeesDgv()
		{
			var dataTable = new DataTable();

			dataTable.Columns.Add("Id");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Surname");
			dataTable.Columns.Add("TelephoneNo");
			dataTable.Columns.Add("Email");


			var employees = _cineOsServices.GetEmployeesIn(_currentlyViewedCinema);

			foreach (var employee in employees)
			{
				dataTable.Rows.Add(employee.Id, employee.Name, employee.Surname, employee.TelephoneNo, employee.Email);
			}

			return dataTable;
		}


		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			ProjectionsDataGridView.DataSource = PopulateProjectionDGVWithFutureProjections(ProjectionCalendar.SelectionStart);
			ProjectionsDataGridView.Columns[0].Visible = false;
			var date = ProjectionCalendar.SelectionStart.Date;
			ShowingProjectionsLabel.Text = string.Format("Showing projections for {0}/{1}/{2}", date.Day, date.Month, date.Year);

		}

		private void ComboBoxCinemas_SelectedIndexChanged(object sender, EventArgs e)
		{
			_currentlyViewedCinema = (Cinema) ComboBoxCinemas.SelectedItem;
			UpdateForm();
		}

		private void ButtonShowFuturePredictions_Click(object sender, EventArgs e)
		{
			ShowFutureProjections();
		}

		private void ShowFutureProjections()
		{
			ProjectionsDataGridView.DataSource = PopulateProjectionDGVWithFutureProjections();
			ProjectionsDataGridView.Columns[0].Visible = false;
			ShowingProjectionsLabel.Text = "Showing Future Projections";
		}

		private void ShowingProjectionsLabel_Click(object sender, EventArgs e)
		{

		}

		private void ButtonAddProjection_Click(object sender, EventArgs e)
		{
			ShowAddProjectionForm();
		}

		private void ShowAddProjectionForm()
		{
			var addProjectionForm = new AddProjectionForm(_cineOsServices, _currentlyViewedCinema, this);
			addProjectionForm.Show();
		}

		private void ButtonDeleteProjection_Click(object sender, EventArgs e)
		{
			DeleteProjection();
		}

		private void DeleteProjection()
		{
			if (ProjectionsDataGridView.SelectedRows.Count == 0)
			{
				// Do nothing.
				MessageBox.Show("No Row selected!", "Now row selected!", MessageBoxButtons.OK);
				return;
			}

			var result = MessageBox.Show("Are you sure you want to this projection?", "Delete?", MessageBoxButtons.YesNo);

			var projectionId = int.Parse(ProjectionsDataGridView.SelectedRows[0].Cells["Id"].Value.ToString());

			if (result == DialogResult.Yes)
			{
                _cineOsServices.DeleteProjection(projectionId);
				UpdateForm();
			}
		}

		private void ButtonUpdateProjection_Click(object sender, EventArgs e)
		{
			ShowUpdateProjection();
		}

		private void ShowUpdateProjection()
		{
			if (ProjectionsDataGridView.SelectedRows.Count == 0)
			{
				// Do nothing.
				MessageBox.Show("No Row selected!", "Now row selected!", MessageBoxButtons.OK);
				return;
			}
			//	get the id of the selected row with projection
			var projectionId = int.Parse(ProjectionsDataGridView.SelectedRows[0].Cells["Id"].Value.ToString());

			var projectionToUpdate = _cineOsServices.GetProjectionById(projectionId);


			var updateProjectionForm = new UpdateProjectionForm(_currentlyViewedCinema, projectionToUpdate, _cineOsServices, this);

			updateProjectionForm.Show();
		}

		private void LinkLabelChangeManager_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			ShowChangeManagerForm();
		}

		private void ShowChangeManagerForm()
		{
			var updateManagerSubform = new ChangeManagerForm(this, _cineOsServices, _currentlyViewedCinema.Manager, _currentlyViewedCinema);
			updateManagerSubform.Show();
		}

		private void CinemaForm_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.Control)
				ProcessControlKeyPressed(e);
			else if(e.Alt)
 				ProcessAltKeyPressed(e);
			else 
				ProcessBareKeyPressed(e);

		}

		private void ProcessControlKeyPressed(KeyEventArgs e)
		{
		
		}

		private void ProcessAltKeyPressed(KeyEventArgs e)
		{
			
		}

		private void ProcessBareKeyPressed(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.M:
				{
					ShowChangeManagerForm();
					break;
				}
				case Keys.D:
				{
					DeleteProjection();
					break;
				}
				case Keys.A:
				{
					ShowAddProjectionForm();
					break;
				}
				case Keys.Z:
				{
					ShowUpdateProjection();
					break;
				}
				case Keys.F:
				{
					ShowFutureProjections();
					break;
				}
				case Keys.F5:
				{
					InitializeFormData();
					break;
				}
			}
		}

		private void CinemaForm_Load(object sender, EventArgs e)
		{

		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			InitializeFormData();
		}

		private void CinemaForm_Enter(object sender, EventArgs e)
		{
			InitializeFormData();
		}
	}

}

