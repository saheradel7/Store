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
    public partial class Form1 : Form
    {

        product p = new product();
        order o = new order();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataTable dt = p.Select();
            SellGridView.DataSource = dt;

            SellGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            SellGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            SellGridView.Columns[0].Visible = false;
            SellGridView.Columns[3].Visible = false;
            SellGridView.Columns[4].Visible = false;
            SellGridView.Columns[6].Visible = false;
            SellGridView.Columns[7].Visible = false;
            SellGridView.Columns[8].Visible = false;


        }
        public void clear()
        {
            SellTb1.Text = string.Empty;
            SellTb2.Text = string.Empty;
            SellTb3.Text = string.Empty;
            SellTb4.Text = string.Empty;
            SellSearchTb.Text = string.Empty;
            QuantityToSellTb.Text = string.Empty;

        }

        private void SellGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            SellTb1.Text = SellGridView.Rows[rowindex].Cells[0].Value.ToString();
            SellTb2.Text = SellGridView.Rows[rowindex].Cells[1].Value.ToString();
            SellTb3.Text = SellGridView.Rows[rowindex].Cells[2].Value.ToString();
            SellTb4.Text = SellGridView.Rows[rowindex].Cells[5].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(SellTb1.Text, out int IntProductId) ||
                !decimal.TryParse(SellTb3.Text, out decimal productPrice) ||
                !int.TryParse(SellTb4.Text, out int quantity) ||
                !int.TryParse(QuantityToSellTb.Text, out int quantityToSell))
            {
                MessageBox.Show("Enter Quantity To Sell");
                return;
            }

            if (quantityToSell <= 0 || quantityToSell > quantity)
            {
                MessageBox.Show("Enter Valid Quantity");
                return;
            }

            decimal OrderPrice = (productPrice * quantityToSell);
            p.Id = IntProductId;
            p.Price = productPrice;
            p.Quantity = (quantity - quantityToSell);
            p.Quantity_Sold = quantityToSell;
            p.Total = OrderPrice;

            bool sellingproduct = p.SellProduct(p);
            if (sellingproduct)
            {
                o.Name = SellTb2.Text;
                o.Price = productPrice;
                o.Quantity = quantityToSell;
                o.TotalPrice = OrderPrice;
                o.OrderDate = DateTime.Now;

                bool savingOrder = o.Insert(o);
                if (savingOrder)
                {
                    MessageBox.Show("Total Price = " + o.TotalPrice);
                    clear();
                }
                else
                {
                    MessageBox.Show("Error while saving order");
                }
            }
            else
            {
                MessageBox.Show("Error While Selling Product.");
            }

            DataTable dt = p.Select();
            SellGridView.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void SellSearchTb_TextChanged(object sender, EventArgs e)
        {
            string keyword = SellSearchTb.Text;

                using (var context = new StoreDataBaseEntities())
                {
                    var filteredProducts = context.products
                        .Where(p => p.Name.Contains(keyword))
                        .ToList();

                    SellGridView.DataSource = filteredProducts;
                }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DataTable dt = p.Select(); // Example: Reload data using your Select method
            SellGridView.DataSource = dt;
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
