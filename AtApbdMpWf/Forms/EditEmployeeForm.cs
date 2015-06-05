﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Logic.Core;
using Logic.Entities;

namespace AtApbdMpWf.Forms
{
	public partial class EditEmployeeForm : Form
	{
		private ICineOsServices _cineOsServices;
		private Employee _employeeToUpdate;

		public EditEmployeeForm(ICineOsServices cineOsServices, Employee employeeToUpdate)
		{
			_cineOsServices = cineOsServices;
			_employeeToUpdate = employeeToUpdate;
			InitializeComponent();
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var validationMessages = ValidateControlsAnReturnMessage();

			if (validationMessages.Count != 0)
			{
				var messageBoxMessage = validationMessages.Aggregate("", (current, message) => current + (message + "\n"));

				MessageBox.Show(messageBoxMessage, "Error!", MessageBoxButtons.OK);
				return;
			}

			var employee = new Employee { Id=_employeeToUpdate.Id, Name = NameTextBox.Text, Surname = SurnameTextBox.Text, TelephoneNo = TelephoneMaskedTextBox.Text, Email = EmailTextBox.Text };

			_cineOsServices.UpdateEmployee(employee);

			Close();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private List<string> ValidateControlsAnReturnMessage()
		{
			var result = new List<string>();

			if (string.IsNullOrWhiteSpace(NameTextBox.Text))
			{
				result.Add("- You must provide a name!");
			}
			if (string.IsNullOrWhiteSpace(SurnameTextBox.Text))
			{
				result.Add("- You must provide a surname!");
			}
			const string emailRegExPattern = @"[\w-]+@([\w-]+\.)+[\w-]+";

			if (!Regex.IsMatch(EmailTextBox.Text, emailRegExPattern))
			{
				result.Add("- Please provide a valid email address!");
			}

			return result;
		}

		private void EditEmployeeForm_Load(object sender, EventArgs e)
		{
			NameTextBox.Text = _employeeToUpdate.Name;
			SurnameTextBox.Text = _employeeToUpdate.Surname;
			TelephoneMaskedTextBox.Text = _employeeToUpdate.TelephoneNo;
			EmailTextBox.Text = _employeeToUpdate.Email;
		}

	}
}
