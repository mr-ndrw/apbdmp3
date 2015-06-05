using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Logic.Core;
using Logic.Core.SubServices;
using Logic.Entities;

namespace AtApbdMpWf.Forms
{
	public partial class UpdateProjectionForm : Form
	{
		private readonly ICineOsServices _cineOsServices;
		private readonly Cinema _currentlyViewedCinema;
		private readonly Projection _projection;
		private readonly CinemaForm _parent;
		public UpdateProjectionForm(Cinema currentlyViewedCinema, Projection projection, ICineOsServices cineOsServices, CinemaForm parent)
		{
			_currentlyViewedCinema = currentlyViewedCinema;
			_projection = projection;
			_cineOsServices = cineOsServices;
			_parent = parent;
			InitializeComponent();
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
		}

		private void UpdateProjectionForm_Load(object sender, EventArgs e)
		{
			//	Populate the combo box
			var projectionRooms = _cineOsServices.GetProjectionRoomsFor(_currentlyViewedCinema).ToList();
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

            _cineOsServices.UpdateProjection(projectionToExecute);

			_parent.UpdateForm();
			Close();
		}




	}
}
