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
    public partial class Form3 : Form
    {
        product p3 = new product();
        public Form3()
        {
            InitializeComponent();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            textBox1.Text = dataGridView1.Rows[rowindex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[rowindex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[rowindex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[rowindex].Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.Rows[rowindex].Cells[3].Value.ToString();
        }
        public void clear()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            DataTable dt = p3.Select();
            dataGridView1.DataSource = dt;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView1.Columns[0].Visible = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int IntProductId) ||
                !decimal.TryParse(textBox3.Text, out decimal productPrice) ||
                !int.TryParse(textBox4.Text, out int quantity) ||
                !int.TryParse(textBox6.Text, out int quantityToDelete) ||
                !int.TryParse(textBox7.Text, out int ResQuantity))
            {
                MessageBox.Show("Enter Quantity To Delete");
                return;
            }

            if (quantityToDelete <= 0 || quantityToDelete > quantity)
            {
                MessageBox.Show("Enter Valid Quantity");
                return;
            }

            p3.Id = IntProductId;
            p3.Quantity = quantityToDelete;
            p3.Total = productPrice * quantityToDelete;
            p3.Received_Quantity = ResQuantity - quantityToDelete;

            bool success = p3.Delete(p3);
            if (success)
            {
                MessageBox.Show("Item Successfully Deleted.");
                clear();
            }
            else
            {
                MessageBox.Show("Item Was Not Deleted.");
            }

            DataTable dt = p3.Select();
            dataGridView1.DataSource = dt;
            //textBox6.Text = P3.CalculateTotalStorePrice().ToString();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
    }
}
