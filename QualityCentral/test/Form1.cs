using OrkIdea.QC.Business;
using OrkIdea.QC.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<Customer> customers = BizCustomer.GetList();

            if (customers.Count() > 0)
                dataGridView1.DataSource = customers;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            Customer c = new Customer() { 
                 activo = true,
                  nombre = "prueba1",                   
            };
            Customer d = new Customer()
            {
                activo = true,
                nombre = "prueba2",
            };
            Customer E = new Customer()
            {
                activo = true,
                nombre = "prueba3",
            };
            Customer F = new Customer()
            {
                activo = true,
                nombre = "prueba4",
            };

            BizCustomer.Add(c, d, E, F);

            MessageBox.Show("terminado");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Customer c = new Customer()
            {
                id = 7,
                activo = true,
                nombre = "prueba12",
            };

            BizCustomer.Update(c);
            MessageBox.Show("terminado");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Customer c = new Customer()
            {
                id = 8,                
            };

            BizCustomer.Remove(c);
            MessageBox.Show("terminado");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            IList<Customer> customers = BizCustomer.GetList(false);

            if (customers.Count() > 0)
                dataGridView1.DataSource = customers;
        }
    }
}
