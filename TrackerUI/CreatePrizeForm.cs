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
    public partial class CreatePrizeForm : Form
    { 
        IPrizeRequester Callingform;

        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            Callingform = caller; 
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidadeForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNameValue.Text,
                    placeNumberValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text);

                GlobalConfig.Connection.CreatePrize(model);
                Callingform.PrizeComplete(model);

                this.Close();
                //placeNameValue.Text = "";
                //placeNumberValue.Text = "";
                //prizeAmountValue.Text = "0";
                //prizePercentageValue.Text = "0";

            }
            else
            {
                MessageBox.Show("This form has invalid information");
            }
        }

        private bool ValidadeForm()
        {
            bool output = true;
            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out placeNumber);

            if (!placeNumberValidNumber)
            {
                output = false;
            }
            if (placeNumber < 1)
            {
                output = false;
            }
            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            decimal prizeAmount = 0;
            double prizePercentage = 0;

            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out prizeAmount);
            bool prizeAmountPercentageValid = double.TryParse(prizePercentageValue.Text, out prizePercentage);

            if (!prizeAmountValid || !prizeAmountPercentageValid)
            {
                output = false;
            }
            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            if (prizePercentage < 0 || prizePercentage < 0)
            {
                output = false;
            }

            return output;
        }
    }
}
