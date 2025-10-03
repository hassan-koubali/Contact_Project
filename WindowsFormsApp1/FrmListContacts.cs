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
    public partial class FrmListContacts : Form
    {
        public FrmListContacts()
        {
            InitializeComponent();
        }

        private void _RefreshContactsList()
        {
            dgvAllContacts.DataSource = clsContact.GetAllContacts();
        }
        private void FrmListContacts_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditContact(-1);
            frm.ShowDialog();
            _RefreshContactsList();
        }

        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddEditContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactsList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Delete This Contact?", "Delete Contact", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                if (clsContact.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value))
                {
                    _RefreshContactsList();
                    MessageBox.Show("Contact Deleted Successfully");
                }
                else
                    MessageBox.Show("Contact Deletion Failed");
            }
            else
            {
                MessageBox.Show("Contact Deletion Cancelled");
            }

                
        }
    }
}
