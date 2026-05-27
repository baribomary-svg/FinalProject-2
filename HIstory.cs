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
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();

            btnDashboard.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnDashboard.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnDashboard.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnDashboard.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnRequest.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnRequest.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnRequest.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnRequest.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnHistory.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnHistory.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnHistory.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnHistory.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnParts.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnParts.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnParts.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnParts.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;

            btnLogout.MouseEnter += (s, e) => ((Button)s).BackColor = Color.OliveDrab;
            btnLogout.MouseLeave += (s, e) => ((Button)s).BackColor = Color.DarkGreen;
            btnLogout.MouseDown += (s, e) => ((Button)s).BackColor = Color.FromArgb(40, 90, 40);
            btnLogout.MouseUp += (s, e) => ((Button)s).BackColor = Color.OliveDrab;


        }
    }
}
