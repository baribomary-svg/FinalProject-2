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
    public partial class Profile : Form
    {
        public Profile()
        {
            InitializeComponent();

            btnHome.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnHome.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnHome.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnHome.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnProducts.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnProducts.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnProducts.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnProducts.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnMyCart.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnMyCart.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnMyCart.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnMyCart.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnOrders.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnOrders.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnOrders.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnOrders.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnProfile.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnProfile.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnProfile.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnProfile.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnLogout.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnLogout.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnLogout.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnLogout.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
        }
    }
}
