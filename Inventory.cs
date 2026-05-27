using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();

            btnDashboard.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnDashboard.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnDashboard.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnDashboard.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnPOS.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnPOS.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnPOS.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnPOS.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnTransaction.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnTransaction.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnTransaction.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnTransaction.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnProducts.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnProducts.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnProducts.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnProducts.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnInventory.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnInventory.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnInventory.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnInventory.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnLogout.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnLogout.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnLogout.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnLogout.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

        }

        private void Inventory_Load(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {

        }
    }
}
