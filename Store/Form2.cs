using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Store
{
    public partial class Form2 : Form
    {
        product p2 = new product();
        public Form2()
        {
            InitializeComponent();
        }
        public void clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = p2.Select(); // Example: Reload data using your Select method
            dataGridView1.DataSource = dt;
            clear();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = p2.Select();
            dataGridView1.DataSource = dt;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;

            textBox6.Text = p2.CalculateTotalStorePrice().ToString();

        }

        private void resetbtn_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            string productName = textBox2.Text;
            string priceString = textBox3.Text;
            string quantityString = textBox4.Text;

            if (string.IsNullOrWhiteSpace(productName) || string.IsNullOrWhiteSpace(priceString) || string.IsNullOrWhiteSpace(quantityString))
            {
                MessageBox.Show("Please fill in all required fields.");
                return; // Exit the method without performing the insertion
            }

            if (!decimal.TryParse(priceString, out decimal productPrice))
            {
                MessageBox.Show("Invalid Price value entered.");
                return; // Exit the method without performing the insertion
            }

            if (!int.TryParse(quantityString, out int Quantity))
            {
                MessageBox.Show("Invalid Quantity value entered.");
                return; // Exit the method without performing the insertion
            }

            if (Quantity <= 0)
            {
                MessageBox.Show("invalid Quantity");
                return;
            }

            p2.Name = productName;
            p2.Price = productPrice;
            p2.Quantity = Quantity;
            p2.Price = productPrice;
            p2.Total = productPrice * Quantity;
            p2.Received_Quantity = Quantity;
            p2.Received_Date = DateTime.Now;
            p2.IsSold = false;



            bool success = p2.Insert(p2);
            if (success)
            {
                MessageBox.Show("Item Successfully Inserted.");
                clear();
            }
            else
            {
                MessageBox.Show("Item Was Not Inserted.");
            }

            DataTable dt = p2.Select();
            dataGridView1.DataSource = dt;
            textBox6.Text = p2.CalculateTotalStorePrice().ToString();
            

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();

        }
    }
}
