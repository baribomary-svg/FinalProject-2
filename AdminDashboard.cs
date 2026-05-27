using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.Util;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FinalProject
{
    public partial class AdminDashboard : Form
    {
        // =========================================
        // SQL CONNECTION
        // =========================================
        SqlConnection con = new SqlConnection(
        @"Data Source=localhost\SQLEXPRESS;
        Initial Catalog=Final_DB;
        Integrated Security=True;
        TrustServerCertificate=True");

        public AdminDashboard()
        {
            InitializeComponent();
        }

        // =========================================
        // FORM LOAD
        // =========================================
        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            SetupOrderListView();

            SetupSalesChart();

            //LoadDashboardData();

            //LoadRecentTransactions();

            //LoadOrderList();

            //LoadSalesChart();
        }

        // =========================================
        // SETUP LISTVIEW
        // =========================================
        private void SetupOrderListView()
        {
            dgvOrders.ColumnCount = 7;

            dgvOrders.Columns[0].Name = "Item ID";
            dgvOrders.Columns[1].Name = "Customer Name";
            dgvOrders.Columns[2].Name = "Product";
            dgvOrders.Columns[3].Name = "Payment Type";
            dgvOrders.Columns[4].Name = "Qty";
            dgvOrders.Columns[5].Name = "Total";
            dgvOrders.Columns[6].Name = "Status";

            dgvOrders.Columns[0].Width = 80;
            dgvOrders.Columns[1].Width = 150;
            dgvOrders.Columns[2].Width = 150;
            dgvOrders.Columns[3].Width = 120;
            dgvOrders.Columns[4].Width = 70;
            dgvOrders.Columns[5].Width = 100;
            dgvOrders.Columns[6].Width = 100;

            dgvOrders.BackgroundColor = Color.FromArgb(20, 20, 20);

            dgvOrders.DefaultCellStyle.BackColor = Color.FromArgb(30, 30, 30);
            dgvOrders.DefaultCellStyle.ForeColor = Color.White;

            dgvOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.Green;
            dgvOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dgvOrders.EnableHeadersVisualStyles = false;
        }

        // =========================================
        // SETUP SALES CHART
        // =========================================
        private void SetupSalesChart()
        {
            chartSales.Series.Clear();
            chartSales.ChartAreas.Clear();

            ChartArea area = new ChartArea();

            area.AxisX.Title = "Date";
            area.AxisY.Title = "Sales";

            chartSales.ChartAreas.Add(area);

            Series series = new Series();

            series.Name = "Sales";
            series.ChartType = SeriesChartType.Column;

            chartSales.Series.Add(series);
        }

        // =========================================
        // LOAD DASHBOARD DATA
        // =========================================
        private void LoadDashboardData()
        {
            try
            {
                con.Open();

                // TOTAL SALES
                string salesQuery =
                "SELECT ISNULL(SUM(TotalAmount),0) FROM Sales";

                SqlCommand cmdSales =
                new SqlCommand(salesQuery, con);

                decimal totalSales =
                Convert.ToDecimal(cmdSales.ExecuteScalar());

                lblTotalSales.Text =
                "₱" + totalSales.ToString("N2");



                // TOTAL TRANSACTIONS
                string transactionQuery =
                "SELECT COUNT(*) FROM Sales";

                SqlCommand cmdTransaction =
                new SqlCommand(transactionQuery, con);

                int totalTransactions =
                Convert.ToInt32(cmdTransaction.ExecuteScalar());

                lblTransactions.Text =
                totalTransactions.ToString();



                // TOTAL PRODUCTS
                string productQuery =
                "SELECT COUNT(*) FROM Products";

                SqlCommand cmdProducts =
                new SqlCommand(productQuery, con);

                int totalProducts =
                Convert.ToInt32(cmdProducts.ExecuteScalar());

                lblTotalProducts.Text =
                totalProducts.ToString();



                // TOTAL STOCKS
                string stockQuery =
                "SELECT ISNULL(SUM(Stock),0) FROM Products";

                SqlCommand cmdStocks =
                new SqlCommand(stockQuery, con);

                int totalStocks =
                Convert.ToInt32(cmdStocks.ExecuteScalar());

                lblStocks.Text =
                totalStocks.ToString();



                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                con.Close();
            }
        }

        // =========================================
        // RECENT TRANSACTIONS DATAGRIDVIEW
        // =========================================
        private void LoadRecentTransactions()
        {
            try
            {
                con.Open();

                string query = @"
                SELECT TOP 10
                    SaleID AS [Item ID],
                    CustomerName AS [Customer Name],
                    ProductName AS [Product],
                    PaymentType AS [Payment Type],
                    Quantity AS [Qty],
                    TotalAmount AS [Total],
                    SaleDate AS [Date],
                    Status
                FROM Sales
                ORDER BY SaleDate DESC";

                SqlDataAdapter da =
                new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvRecentTransactions.DataSource = dt;

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                con.Close();
            }
        }

        // =========================================
        // LOAD ORDER LIST
        // =========================================
        private void LoadOrderList()
        {
            try
            {
                dgvOrders.Rows.Clear();

                con.Open();

                string query = @"
                SELECT
                    SaleID,
                    CustomerName,
                    ProductName,
                    PaymentType,
                    Quantity,
                    TotalAmount,
                    SaleDate,
                    Status
                FROM Sales
                ORDER BY SaleDate DESC";

                SqlCommand cmd =
                new SqlCommand(query, con);

                SqlDataReader reader =
                cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item =
                    new ListViewItem(
                    reader["SaleID"].ToString());

                    item.SubItems.Add(
                    reader["CustomerName"].ToString());

                    item.SubItems.Add(
                    reader["ProductName"].ToString());

                    item.SubItems.Add(
                    reader["PaymentType"].ToString());

                    item.SubItems.Add(
                    reader["Quantity"].ToString());

                    item.SubItems.Add(
                    "₱" + Convert.ToDecimal(
                    reader["TotalAmount"])
                    .ToString("N2"));

                    item.SubItems.Add(
                    Convert.ToDateTime(
                    reader["SaleDate"])
                    .ToString("MM/dd/yyyy"));

                    item.SubItems.Add(
                    reader["Status"].ToString());

                    dgvOrders.Rows.Add(item);
                }

                reader.Close();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                con.Close();
            }
        }

        // =========================================
        // LOAD SALES CHART
        // =========================================
        private void LoadSalesChart()
        {
            try
            {
                chartSales.Series["Sales"].Points.Clear();

                con.Open();

                string query = @"
                SELECT 
                    CONVERT(date, SaleDate) AS SalesDate,
                    ISNULL(SUM(TotalAmount),0) AS TotalSales
                FROM Sales
                GROUP BY CONVERT(date, SaleDate)
                ORDER BY SalesDate";

                SqlCommand cmd =
                new SqlCommand(query, con);

                SqlDataReader reader =
                cmd.ExecuteReader();

                while (reader.Read())
                {
                    string salesDate =
                    Convert.ToDateTime(
                    reader["SalesDate"])
                    .ToString("MM/dd");

                    decimal totalSales =
                    Convert.ToDecimal(
                    reader["TotalSales"]);

                    chartSales.Series["Sales"]
                    .Points.AddXY(salesDate, totalSales);
                }

                reader.Close();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                con.Close();
            }
        }

        private void dgvRecentTransactions_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnUsers_Click_1(object sender, EventArgs e)
        {
            Users users =
            new Users();

            users.Show();

            this.Hide();
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            AdminDashboard admin = new AdminDashboard();

            admin.Show();
            this.Hide();
        }

        private void btnSales_Click(object sender, EventArgs e)
        {
            Sales sales = new Sales();

            sales.Show();
            this.Hide();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            Transaction trans = new Transaction();

            trans.Show();
            this.Hide();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            Products prod = new Products();

            prod.Show();
            this.Hide();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            Inventory inventory = new Inventory();

            inventory.Show();
            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result =
            MessageBox.Show(
            "Are you sure you want to logout?",
            "Logout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                SignIn login =
                new SignIn();

                login.Show();

                this.Hide();
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {

        }
    }
}
