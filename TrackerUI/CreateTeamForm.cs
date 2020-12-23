using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Connections;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTeamForm : Form
    {
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectectTeamMembers = new List<PersonModel>();

        public CreateTeamForm()
        {
            InitializeComponent();
            //CreateSampleData(); 
            WireupList();
        }

        private void CreateSampleData()
        {
            availableTeamMembers.Add(new PersonModel { FirstName = "Bruno", LastName = "Silva" });
            availableTeamMembers.Add(new PersonModel { FirstName = "john", LastName = "wick" });
            availableTeamMembers.Add(new PersonModel { FirstName = "laila", LastName = "maria" });

            selectectTeamMembers.Add(new PersonModel { FirstName = "roberto", LastName = "carlos" });
            selectectTeamMembers.Add(new PersonModel { FirstName = "mari", LastName = "creide" });
            selectectTeamMembers.Add(new PersonModel { FirstName = "sonic", LastName = "hedgehog" });
        }

        private void WireupList()
        {
            selectTeamMwmberDropDown.DataSource = null;
            selectTeamMwmberDropDown.DataSource = availableTeamMembers;
            selectTeamMwmberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectectTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

        }

        private void createMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PersonModel p = new PersonModel();

                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAdress = emailValue.Text;
                p.CellPhoneNumber = cellPhoneValue.Text;

                p = GlobalConfig.Connection.CreatePerson(p);
                selectectTeamMembers.Add(p);
                WireupList();

                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                cellPhoneValue.Text = "";

            }
            else
            {
                MessageBox.Show("Not all fields filled.");
            }
        }

        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0)
            {
                return false;
            }

            if (lastNameValue.Text.Length == 0)
            {
                return false;
            }
            if (emailValue.Text.Length == 0)
            {
                return false;
            }
            if (cellPhoneValue.Text.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void addTeamMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMwmberDropDown.SelectedItem;
            if (p != null)
            {
                availableTeamMembers.Remove(p); //Remove from available list
                selectectTeamMembers.Add(p);   //Added to Selected List

                WireupList();
            }

        }

        private void removeSelectedMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)teamMembersListBox.SelectedItem;

            if (p != null)
            {
                selectectTeamMembers.Remove(p);
                availableTeamMembers.Add(p);

                WireupList();
            }
        }
    }
}
