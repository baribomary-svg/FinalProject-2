using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Shop : Form
    {
        public Shop()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void frmShop_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }

        private void LoadProducts()
        {
            string connStr = @"Data Source=localhost\SQLEXPRESS;
                               Initial Catalog=Final_DB;
                               Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                SELECT p.ProductID,
                       p.ProductName,
                       p.Price,
                       i.StockQuantity,
                       p.ImagePath
                FROM Products p
                INNER JOIN Inventory i
                ON p.ProductID = i.ProductID";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(167, 199);
                    panel.BorderStyle = BorderStyle.FixedSingle;

                    PictureBox pic = new PictureBox();
                    pic.Size = new Size(141, 90);
                    pic.Location = new Point(13, 6);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;

                    string imagePath = @"C:\Users\aleli\" + dr["ImagePath"].ToString();

                    if (File.Exists(imagePath))
                    {
                        pic.Image = Image.FromFile(imagePath);
                    }

                    Label lblName = new Label();
                    lblName.Text = dr["ProductName"].ToString();
                    lblName.Location = new Point(20, 108);
                    lblName.AutoSize = true;

                    Label lblPrice = new Label();
                    lblPrice.Text = "₱" + dr["Price"].ToString();
                    lblPrice.Location = new Point(10, 128);
                    lblPrice.AutoSize = true;

                    Label lblStock = new Label();
                    lblStock.Text = "Stock: " + dr["StockQuantity"].ToString();
                    lblStock.Location = new Point(90, 128);
                    lblStock.AutoSize = true;

                    Button btn = new Button();
                    btn.Text = "View Details";
                    btn.Size = new Size(141, 30);
                    btn.Location = new Point(13, 159);

                    panel.Controls.Add(pic);
                    panel.Controls.Add(lblName);
                    panel.Controls.Add(lblPrice);
                    panel.Controls.Add(lblStock);
                    panel.Controls.Add(btn);

                    flowLayoutPanel1.Controls.Add(panel);
                }
            }
        }
    }
}