using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Reg_Click(object sender, EventArgs e)
        {
            Readers_form form = new Readers_form();
            form.Show();
        }

        private void Bookings_Click(object sender, EventArgs e)
        {
            Bookings_form form = new Bookings_form();
            form.Show();
        }

        private void Orders_Click(object sender, EventArgs e)
        {
            Orders_form form = new Orders_form();
            form.Show();
        }

        private void Books_Click(object sender, EventArgs e)
        {
            Books_form form = new Books_form();
            form.Show();
        }

        private void Authors_Click(object sender, EventArgs e)
        {
            Authors_form form = new Authors_form();
            form.Show();
        }
    }
}
