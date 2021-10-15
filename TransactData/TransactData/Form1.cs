using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TransactData.Models;

namespace TransactData
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Add();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView1.Rows.Add();
                dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ProductModel> lst = new List<ProductModel>();
            foreach(DataGridViewRow data in dataGridView1.Rows)
            {
                var product = new ProductModel()
                {
                    _Category = Convert.ToString(data.Cells["CATEGORY"].Value),
                    _Mark = Convert.ToString(data.Cells["MARK"].Value),
                    _Description =Convert.ToString(data.Cells["DESCRIPTION"].Value),
                    _Cost = (float)Convert.ToDouble(data.Cells["COST"].Value)
                };
                lst.Add(product);
            }
            ProductModel model = new ProductModel();
            model.InsertBigData(lst);
            MessageBox.Show("added data");
        }
    }
}
