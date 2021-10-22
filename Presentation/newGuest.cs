using PhumlaKamnandiBooking.Business;
using PhumlaKamnandiBooking.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhumlaKamnandiBooking.Presentation
{
    public partial class newGuest : Form
    {
        #region Data Members
        private Guest guest;
        private GuestController guestController;
        public bool newGuestClosed = false;
        #endregion


        public newGuest()
        {
            InitializeComponent();
        }

        #region Utility Methods
        private void ShowAll(bool value)
        {
            GuestIDLabel.Visible = value;
            nameLabel.Visible = value;
            surnameLabel.Visible = value;
            ageLabel.Visible = value;
            addressLabel.Visible = value;
            emailLabel.Visible = value;


            idTextBox.Visible = value;
            firstNameTextBox.Visible = value;
            ageTextBox.Visible = value;
            addressTextBox.Visible = value;
            surnameTextBox.Visible = value;
            emailTextBox.Visible = value;

            exitButton.Visible = value;
            submitButton.Visible = value;
            cancelButton.Visible = value;
        }

        private void ClearAll()
        {
            idTextBox.Text = "";
            firstNameTextBox.Text = "";
            ageTextBox.Text = "";
            addressTextBox.Text = "";
            surnameTextBox.Text = "";
            emailTextBox.Text = "";
        }

        private void PopulateObject()
        {
            guest = new Guest();
            guest.getGuestID = Convert.ToInt32(idTextBox.Text);
            guest.getFirstName = firstNameTextBox.Text;
            guest.getSurname = surnameTextBox.Text;
            guest.getAge = Convert.ToInt32(ageTextBox.Text);
            guest.getEmail = emailTextBox.Text;
            guest.getAddress = addressTextBox.Text;

        }
        #endregion

        #region Form events
        private void submitButton_Click(object sender, EventArgs e)
        {

            PopulateObject();
            MessageBox.Show("To be submitted to the Database!");
            guestController.DataMaintenance(guest, DB.DBOperation.Add);
            guestController.FinalizeChanges(guest);
            ClearAll();
            ShowAll(false);

        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            ShowAll(true);
            //employeeController = new EmployeeController();
        }

        private void EmployeeForm_Activated(object sender, EventArgs e)
        {
            ShowAll(true);
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            newGuestClosed = true;
        }
        #endregion

    }
}
