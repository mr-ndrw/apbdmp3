using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Logic.Core;
using Logic.Entities;

namespace AtApbdMpWf.Forms.ManagementFormSubForms.CinemaSubForms
{
	public partial class EditCinemaSubForm
	{
		private ICineOsServices _cineOsServices;
		private readonly Cinema _editedCinema;
		private readonly ManagementForm _parentForm;

		public EditCinemaSubForm(ICineOsServices cineOsServices, Cinema editedCinema, ManagementForm parentForm)
		{
			_cineOsServices = cineOsServices;
			_editedCinema = editedCinema;
			_parentForm = parentForm;
			InitializeComponent();
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var validationMessages = ValidateForm();

			if (validationMessages.Count != 0)
			{
				var acccumulatedMessage = validationMessages.Aggregate("", (current, validationMessage) => current + validationMessage);

				MessageBox.Show("Some errors occured:\n" + acccumulatedMessage, "", MessageBoxButtons.OK);
				return;
			}


			_editedCinema.Address = AddressTextBox.Text;
			_editedCinema.Name = CinemaNameTextBox.Text; 
			_editedCinema.TelephoneNo = TelephoneTextBox.Text;

			_cineOsServices.Update(_editedCinema);

			_parentForm.Update();
			Close();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		List<string> ValidateForm()
		{
			var result = new List<string>();
			if (string.IsNullOrWhiteSpace(CinemaNameTextBox.Text))
				result.Add("- Please provide a name!\n");
			if (string.IsNullOrWhiteSpace(AddressTextBox.Text))
				result.Add("- Please provide an address!\n");
			if (string.IsNullOrWhiteSpace(TelephoneTextBox.Text))
				result.Add("- Please provide a telephone number!\n");

			return result;
		}

		private void AddCinemaSubForm_Load(object sender, EventArgs e)
		{
			CinemaNameTextBox.Text = _editedCinema.Name;
			AddressTextBox.Text = _editedCinema.Address;
			TelephoneTextBox.Text = _editedCinema.TelephoneNo;
		}

	}
}
