using System;
using System.Windows.Forms;
using Logic.Core;

namespace AtApbdMpWf.Forms.ManagementFormSubForms.RegionSubForms
{
	public partial class AddRegionSubForm : Form
	{
		private ICineOsServices _cineOsServices;
		private readonly ManagementForm _parentForm;

		public AddRegionSubForm(ICineOsServices cineOsServices, ManagementForm parentForm)
		{
			_cineOsServices = cineOsServices;
			_parentForm = parentForm;
			InitializeComponent();
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			var regionName = RegionNameTextBox.Text;

			if (string.IsNullOrWhiteSpace(regionName))
			{
				MessageBox.Show("Please provide a name!", "Error", MessageBoxButtons.OK);
				return;
			}

			_cineOsServices.AddRegion(regionName);

			_parentForm.Update();
			Close();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}


	}
}
