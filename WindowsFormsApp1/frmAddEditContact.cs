using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contact
{
    public partial class frmAddEditContact : Form
    {
        public enum enMode { AddNew = 0, Update = 1 }
        private enMode _Mode;

        int _ContactID;
        clsContact _Contact;
        public frmAddEditContact(int ContactID)
        {
           
            InitializeComponent();

            _ContactID = ContactID;

            if (_ContactID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

        }

        private void _FillCountryInComboBox()
        {
            DataTable dtCountry = clsCountry.GetAllCountries();
            foreach (DataRow Row in dtCountry.Rows)
            {
                cbCounriys.Items.Add(Row["CountryName"]);
            }

        }


        private void _LoadData()
        {
            _FillCountryInComboBox();
            cbCounriys.SelectedIndex = 0;
            if (_Mode == enMode.AddNew)
            {
                lblMode.Text = "Add New Contact";
                _Contact = new clsContact();
                return;
            }
            _Contact = clsContact.Find(_ContactID);
            if (_Contact == null)
            {
                MessageBox.Show("Contact Not Found");
                this.Close();
                return;
            }
            lblContactID.Text = _Contact.ID.ToString();
            lblMode.Text = "Update Contact ID " + _ContactID ;
            txtFirstName.Text = _Contact.FirstName;
            txtLastName.Text = _Contact.LastName;
            txtEmail.Text = _Contact.Email;
            txtPhone.Text = _Contact.Phone;
            txtAddress.Text = _Contact.Address;
            dtpDateOfBirth.Value = _Contact.DateOfBirth;

            if (_Contact.ImagePath != "")
            {
                pictureBox1.Load(_Contact.ImagePath);
            }

            llRemove.Visible = (_Contact.ImagePath != "");
            cbCounriys.SelectedIndex = cbCounriys.FindString(clsCountry.Find(_Contact.CountryID).CountryName);
        }

        private void frmAddEditContact_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountry.Find(cbCounriys.Text).ID;
            _Contact.FirstName = txtFirstName.Text;
            _Contact.LastName = txtLastName.Text;
            _Contact.Email = txtEmail.Text;
            _Contact.Phone = txtPhone.Text;
            _Contact.Address = txtAddress.Text;
            _Contact.DateOfBirth = dtpDateOfBirth.Value;
            _Contact.CountryID = CountryID;
            if (pictureBox1.Location != null)
                _Contact.ImagePath = pictureBox1.ImageLocation;
            else
                _Contact.ImagePath = "";
            if (_Contact.Save())
                MessageBox.Show("Contact Saved Successfully");
            else
                MessageBox.Show("Error Occured While Saving Contact");
            _Mode = enMode.Update;
            lblMode.Text = "Update Contact ID " + _Contact.ID;
            lblContactID.Text = _Contact.ID.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.InitialDirectory = "C:\\Users\\hp\\Desktop\\Photos";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                llRemove.Visible = true;
            }
        }

        private void llRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
            _Contact.ImagePath = "";
            
            llRemove.Visible = false;
            
            
        }
    }
}
