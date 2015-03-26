using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AtApbdMpWf.BusinessLogic;
using AtApbdMpWf.Entity;

namespace AtApbdMpWf.Forms
{
	public partial class CinemaForm : Form
	{
		private CinemaService _cinemaService;
		private Cinema _currentlyViewedCinema;

		public CinemaForm(CinemaService cinemaService)
		{
			_cinemaService = cinemaService;
			InitializeComponent();
			InitializeFormData();
		}

		private void InitializeFormData()
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
			LabelRegion.Text = _cinemaService.GetRegion(_currentlyViewedCinema).Name;

			//	Set the link label of the manager
			var manager = _cinemaService.GetManager(_currentlyViewedCinema);
			var managerNameSurname = string.Format("{0} {1}", manager.Name, manager.Surname);

			LinkLabelManager.Text = managerNameSurname;

			//	Populate employees data grid view
			EmployeesDgv.DataSource = PopulateEmployeesDgv();

			//	Bolden the dates in month calendar
			ProjectionCalendar.BoldedDates = _cinemaService.GetAllProjections(_currentlyViewedCinema)
				.Select(projection => projection.DateTime).ToArray();
			
		}

		DataTable PopulateProjectionDGVWithFutureProjections()
		{
			var projections = _cinemaService.GetAllProjections(_currentlyViewedCinema);

			var dataTable = new DataTable();

			dataTable.Columns.Add("Id");
			dataTable.Columns.Add("Date and Time");
			dataTable.Columns.Add("Length");

			var futureProjections = projections.Where(projection => projection.DateTime.CompareTo(new DateTime()) < 0);

			foreach (var futureProjection in futureProjections)
			{
				dataTable.Rows.Add(futureProjection.Id, futureProjection.DateTime, futureProjection.Length);
			}

			return dataTable;
		}

		DataTable PopulateProjectionDGVWithFutureProjections(DateTime selectedDateTime)
		{
			var projections = _cinemaService.GetAllProjections(_currentlyViewedCinema, selectedDateTime);

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
			var cinemaList = _cinemaService.GetAllCinemas();

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


			var employees = _cinemaService.GetAllEmployees(_currentlyViewedCinema);

			foreach (var employee in employees)
			{
				dataTable.Rows.Add(employee.Id, employee.Name, employee.Surname, employee.TelephoneNo, employee.Email);
			}

			return dataTable;
		}


		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
			ProjectionsDataGridView.DataSource = PopulateProjectionDGVWithFutureProjections(ProjectionCalendar.SelectionStart);

			var date = ProjectionCalendar.SelectionStart.Date;
			ShowingProjectionsLabel.Text = string.Format("{0}/{1}/{2}", date.Day, date.Month, date.Year);

		}

		private void ComboBoxCinemas_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_currentlyViewedCinema = (Cinema) ComboBoxCinemas.SelectedItem;
			UpdateForm();
		}

		private void ButtonShowFuturePredictions_Click(object sender, EventArgs e)
		{
			ProjectionsDataGridView.DataSource = PopulateProjectionDGVWithFutureProjections();
			ShowingProjectionsLabel.Text = "Future Projections";
		}

		private void ShowingProjectionsLabel_Click(object sender, EventArgs e)
		{

		}

		private void ButtonAddProjection_Click(object sender, EventArgs e)
		{
			var addProjectionForm = new AddProjectionForm(_cinemaService, _currentlyViewedCinema, this);
			addProjectionForm.Show();
		}

		private void ButtonDeleteProjection_Click(object sender, EventArgs e)
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
				_cinemaService.DeleteProjection(projectionId);
				UpdateForm();
			}
		}

		private void ButtonUpdateProjection_Click(object sender, EventArgs e)
		{
			if (ProjectionsDataGridView.SelectedRows.Count == 0)
			{
				// Do nothing.
				MessageBox.Show("No Row selected!", "Now row selected!", MessageBoxButtons.OK);
				return;
			}
			//	get the id of the selected row with projection
			var projectionId = int.Parse(ProjectionsDataGridView.SelectedRows[0].Cells["Id"].Value.ToString());

			var projectionToUpdate = _cinemaService.GetProjection(projectionId);


			var updateProjectionForm = new UpdateProjectionForm(_currentlyViewedCinema, projectionToUpdate, _cinemaService, this);

			updateProjectionForm.Show();
		}

		private void LinkLabelChangeManager_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			var updateManagerSubform = new ChangeManagerForm(this, _cinemaService, _cinemaService.GetManager(_currentlyViewedCinema), _currentlyViewedCinema);
			updateManagerSubform.Show();
		}


	}
}
