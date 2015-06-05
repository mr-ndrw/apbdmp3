﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using AtApbdMpWf.Forms.EmployeeFormSubforms;
using Logic.Core;
using Logic.Entities;

namespace AtApbdMpWf.Forms
{
	public partial class EmployeeForm : Form
	{
		private readonly ICineOsServices _cineOsServices;

		public EmployeeForm(ICineOsServices cineOsServices)
		{
			_cineOsServices = cineOsServices;
			InitializeComponent();
			
		}

		private void EmployeeForm_Load(object sender, EventArgs e)
		{
			BuildRegionCinemaTreeView(RegionCinemaTreeView);
			
		}

		private void BuildRegionCinemaTreeView(TreeView treeView)
		{
			var regions = _cineOsServices.Regions;

			var rootNode = new TreeNode {Name = "0", Text = "Employees By Regions and Cinemas"};

			foreach (var regionWithCinemas in regions)
			{
				var regionNode = new TreeNode {Name = regionWithCinemas.Id.ToString(), Text = regionWithCinemas.Name};

				foreach (var cinema in regionWithCinemas.Cinemas)
				{
					var cinemaNode = new TreeNode {Name = cinema.Id.ToString(), Text = cinema.Name};
					regionNode.Nodes.Add(cinemaNode);
				}

				rootNode.Nodes.Add(regionNode);
			}

			treeView.Nodes.Clear();

			treeView.Nodes.Add(rootNode);

			//return resultTreeView;
		}

		private void RegionCinemaTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			var selectedNode = e.Node;

			IEnumerable<Employee> employees = new List<Employee>();


			switch (selectedNode.Level)
			{
				case 0:
				{
					employees = _cineOsServices.Employees;
					break;
				}
				case 1:
				{
					var idRegion = Int32.Parse(selectedNode.Name);
					employees = _cineOsServices.GetEmployeesInRegionOf(idRegion);
					break;
				}
				case 2:
				{
					var idCinema = Int32.Parse(selectedNode.Name);
					employees = _cineOsServices.GetEmmployeesInCinemaOf(idCinema);
					
					break;
				}
			}

			var dataTable = new DataTable();

			dataTable.Columns.Add("Id");
			dataTable.Columns.Add("Name");
			dataTable.Columns.Add("Surname");
			dataTable.Columns.Add("TelephoneNo");
			dataTable.Columns.Add("Email");

			foreach (var employee in employees)
			{
				dataTable.Rows.Add(employee.Id, employee.Name, employee.Surname, employee.TelephoneNo, employee.Email);
			}

			EmployeesDataGridView.DataSource = dataTable;
			EmployeesDataGridView.Columns[0].Visible = false;
			TotalNumberOfEmployeesLabel.Text = string.Format("Number of employees: {0}", employees.Count());
		}

		private void AddNewEmpButton_Click(object sender, EventArgs e)
		{
			ShowAddNewEmployeeForm();
		}


		// ---- Helpers

		private void ShowAddNewEmployeeForm()
		{
			var addnewempform = new AddNewEmployeeForm(_cineOsServices, this);
			addnewempform.Show();
		}

		private void ShowAddEmpoloyeeToCinemaForm()
		{
			if (!IsEmployeeSelected())
				return;

			var idEmployee = int.Parse(EmployeesDataGridView.SelectedRows[0].Cells["Id"].Value.ToString());
			var employee = _cineOsServices.GetEmployeeOf(idEmployee);

			var form = new AddEmployeeToCinemaForm(_cineOsServices, employee);
			form.Show();
		}

		private void ShowEditEmployeeForm()
		{
			if (!IsEmployeeSelected())
			{
				return;
			}

			var idEmployee = int.Parse(EmployeesDataGridView.SelectedRows[0].Cells["Id"].Value.ToString());
			var employee = _cineOsServices.GetEmployeeOf(idEmployee);

			var form = new EditEmployeeForm(_cineOsServices, employee);
			form.Show();
		}

		private void DeleteEmployee()
		{
			if (!IsEmployeeSelected())
				return;

			var idEmployee = int.Parse(EmployeesDataGridView.SelectedRows[0].Cells["Id"].Value.ToString());

			var deleteOrNot = MessageBox.Show("Are you sure you want to delete this employee?", "", MessageBoxButtons.YesNo);

			if (deleteOrNot == DialogResult.Yes)
			{
				_cineOsServices.DeleteEmployee(idEmployee);
			}

		}

		private void ProcessBareKeyPressed(KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.V:
				{
					ShowAddNewEmployeeForm();
					break;
				}
				case Keys.C:
				{
					ShowAddEmpoloyeeToCinemaForm();
					break;
				}
				case Keys.X:
				{
					ShowEditEmployeeForm();
					break;
				}
				case Keys.Z:
				{
					DeleteEmployee();
					break;
				}
				case Keys.F5:
				{
					BuildRegionCinemaTreeView(RegionCinemaTreeView);
					break;
				}
			}
		}

		private void EmployeeForm_KeyDown(object sender, KeyEventArgs e)
		{
			ProcessBareKeyPressed(e);
		}

		private void AddEmpToCinemaButton_Click(object sender, EventArgs e)
		{
			ShowAddEmpoloyeeToCinemaForm();
		}

		private void EditEmpButton_Click(object sender, EventArgs e)
		{
			ShowEditEmployeeForm();
		}

		private void DeleteEmpButton_Click(object sender, EventArgs e)
		{
			DeleteEmployee();
		}

		private bool IsEmployeeSelected()
		{
			if (EmployeesDataGridView.SelectedRows.Count != 0) return true;
			// Do nothing.
			MessageBox.Show("No Employee was selected!", "Error", MessageBoxButtons.OK);
			return false;
		}

		private void RefreshButton_Click(object sender, EventArgs e)
		{
			BuildRegionCinemaTreeView(RegionCinemaTreeView);
		}
	}
}
