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
	public partial class UpdateProjectionForm : Form
	{
		private readonly Cinema _currentlyViewedCinema;
		private readonly Projection _projection;
		private readonly CinemaService _cinemaService;
		private readonly CinemaForm _parent;
		public UpdateProjectionForm(Cinema currentlyViewedCinema, Projection projection, CinemaService cinemaService, CinemaForm parent)
		{
			_currentlyViewedCinema = currentlyViewedCinema;
			_projection = projection;
			_cinemaService = cinemaService;
			_parent = parent;
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
		}

		private void UpdateProjectionForm_Load(object sender, EventArgs e)
		{
			//	Populate the combo box
			var projectionRooms = _cinemaService.GetProjectionRooms(_currentlyViewedCinema);
			ComboBoxRoom.DataSource = projectionRooms;
			ComboBoxRoom.ValueMember = "Id";
			ComboBoxRoom.DisplayMember = "RoomNumber";
			ComboBoxRoom.SelectedItem = projectionRooms.First(projectionRoom => projectionRoom.Id == _projection.IdProjectionRoom);

			//	Set the Minimal Date to Now, so that there is no way for the user to insert incorrect data.
			PickerDateTime.MinDate = DateTime.Now;
			MaskedTextBoxLength.Text = _projection.Length.ToString();
		}

		private void ButtonExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void ButtonAdd_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(MaskedTextBoxLength.Text))
			{
				MaskedTextBoxLength.BackColor = Color.Red;
				return;
			}

			var projectionToExecute = new Projection
			{
				Id = _projection.Id,
				Length = int.Parse(MaskedTextBoxLength.Text),
				DateTime = PickerDateTime.Value,
				IdProjectionRoom = ((ProjectionRoom)ComboBoxRoom.SelectedItem).Id
			};

			_cinemaService.UpdateProjection(projectionToExecute);

			_parent.UpdateForm();
			Close();
		}




	}
}
