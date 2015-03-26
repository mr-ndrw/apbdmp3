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
using AtApbdMpWf.Data;
using AtApbdMpWf.Entity;

namespace AtApbdMpWf.Forms
{
	public partial class AddProjectionForm : Form
	{
		private CinemaService _cinemaService;
		private Cinema _currentlyViewedCinema;
		private CinemaForm _parent;
		public AddProjectionForm(CinemaService cinemaService, Cinema currentlyViewedCinema, CinemaForm parent)
		{
			_cinemaService = cinemaService;
			_currentlyViewedCinema = currentlyViewedCinema;
			_parent = parent;

			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;

			InitializeComponent();
		}

		private void AddProjectionForm_Load(object sender, EventArgs e)
		{
			//	Populate the combo box
			var projectionRooms = _cinemaService.GetProjectionRooms(_currentlyViewedCinema);
			ComboBoxRoom.DataSource = projectionRooms;
			ComboBoxRoom.ValueMember = "Id";
			ComboBoxRoom.DisplayMember = "RoomNumber";

			//	Set the name of the cinema label
			LabelCurrentlyViewedCinemaName.Text = _currentlyViewedCinema.Name;

			//	Set the Minimal Date to Now, so that there is no way for the user to insert incorrect data.
			PickerDateTime.MinDate = DateTime.Now;
		}

		bool Validate()
		{
			return false;
		}

		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(MaskedTextBoxLength.Text))
			{
				MaskedTextBoxLength.BackColor = Color.Red;
				return;
			}

			var projection = new Projection
			{
				DateTime = PickerDateTime.Value,
				Length =  int.Parse(MaskedTextBoxLength.Text)
			};

			_cinemaService.AddProjection(projection, (ProjectionRoom) ComboBoxRoom.SelectedItem);

			_parent.UpdateForm();
			Close();
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MaskedTextBoxLength_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
		{

		}

		private void MaskedTextBoxLength_TextChanged(object sender, EventArgs e)
		{
		}

	}
}
