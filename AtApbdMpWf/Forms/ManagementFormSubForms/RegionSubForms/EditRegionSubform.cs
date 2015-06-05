using System;
using System.Windows.Forms;
using Logic.Core;
using Logic.Core.SubServices;
using Logic.Entities;

namespace AtApbdMpWf.Forms.ManagementFormSubForms.RegionSubForms
{
	public partial class EditRegionSubform
	{
		private ICineOsServices _cineOsServices;
		private Region _editedRegion;
		public EditRegionSubform(ICineOsServices cineOsServices, Region editedRegion)
		{
			_cineOsServices = cineOsServices;
			_editedRegion = editedRegion;
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

			_editedRegion.Name = regionName;

            _cineOsServices.EditRegion(_editedRegion);

			Close();
		}

		private void ExitButton_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void EditRegionSubform_Load(object sender, EventArgs e)
		{
			RegionNameTextBox.Text = _editedRegion.Name;
		}


	}
}
