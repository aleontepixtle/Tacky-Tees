using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tacky_Tees
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void customColorRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (customColorRadio.Checked)
            {
                customColorLabel.Visible = true;
                customColorTextBox.Visible = true;
            }
            else
            {
                customColorLabel.Visible = false;
                customColorTextBox.Visible = false;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            decimal subTotal, taxes, discount, afterTaxTotal;

            decimal quantity = Quantity();
            decimal sizePrice = GetSizePrice(), colorPrice = GetColorPrice(), letterPrice = GetLetterPrice();

            subTotal = (sizePrice + colorPrice + letterPrice) * quantity;

            if (!preferredChkbox.Checked)
            {
                taxes = AddTax(subTotal);
                afterTaxTotal = subTotal + taxes;
                priceOutputLabel.Text = subTotal.ToString("C2");
                taxOutputLabel.Text = taxes.ToString("C2");
                orderTotalOutputLabel.Text = afterTaxTotal.ToString("C2");
            }
            else
            {
                discount = AddDiscount(subTotal);
                subTotal -= discount;
                taxes = AddTax(subTotal);
                afterTaxTotal = subTotal + taxes;
                priceOutputLabel.Text = subTotal.ToString("C2");
                taxOutputLabel.Text = taxes.ToString("C2");
                orderTotalOutputLabel.Text = afterTaxTotal.ToString("C2");
            }
                
        }

        private decimal GetSizePrice()
        {
            decimal sizePrice;
            if (smRadButton.Checked)
            {
                sizePrice = 4m;
            }
            else if (medRadButton.Checked)
            {
                sizePrice = 5m;
            }
            else if (lgRadButton.Checked)
            {
                sizePrice = 6m;
            }
            else
                sizePrice = 7m;

            return sizePrice;
                
        }

        private decimal GetColorPrice()
        {
            decimal colorPrice;
            if (redRadButton.Checked || orangeRadButton.Checked || yellowRadButton.Checked
                || greenRadButton.Checked || blueRadButton.Checked || purpleRadButton.Checked)
            {
                colorPrice = 1m;
            }
            else if (customColorRadio.Checked)
            {
                colorPrice = 2m;
                ColorName();
            }
            else
                colorPrice = 0m;

            return colorPrice;
        }

        private decimal Quantity()
        {
            decimal max = 30;
            if (decimal.TryParse(quantityTextBox.Text, out decimal quantity))
            { 
                if (quantity > max)
                {
                    MessageBox.Show("Max quantity has been reached." + "\n" +
                        "Quantity has been updated to " + max.ToString());
                    quantityTextBox.Text = max.ToString();
                    return max;
                }
                else
                    return quantity;
            }
            else
            {
                MessageBox.Show("Invalid quantity entered. Quantity has been updated to 1.");
                quantityTextBox.Text = "1";
            }
            return quantity += 1;
        }

        private void ColorName()
        {
            if (customColorTextBox.Text == " " || customColorTextBox.Text == "" && customColorRadio.Checked)
            {
                MessageBox.Show("Please enter a custom Color!");
            }
        }

        private decimal GetLetterPrice()
        {
            string message = msgTextbox.Text;
            decimal lettreingPrice = 0m;
            for (int index = 0; index < message.Length; index++)
            {
                if (char.IsWhiteSpace(message, index))
                    lettreingPrice += 0m;
                else
                    lettreingPrice += 1m;
            }

            return lettreingPrice * .10m;
        }

        private decimal AddTax(decimal subTotal)
        {
            decimal taxRate = .05m, taxesOnTotal;
            taxesOnTotal = subTotal * taxRate;
            return taxesOnTotal;
        }


        private decimal AddDiscount(decimal subTotal)
        {
            decimal prefferedDiscount = .10m;
            return subTotal * prefferedDiscount;
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            msgTextbox.Clear();
            customColorTextBox.Clear();
            quantityTextBox.Clear();
            preferredChkbox.Checked = false;
            priceOutputLabel.Text = "$0.00";
            taxOutputLabel.Text = "$0.00";
            orderTotalOutputLabel.Text = "$0.00";
            blackRadButton.Checked = true;
        }
    }
}
