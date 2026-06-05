using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Users : Form
    {
        SqlConnection conn = new SqlConnection(
        @"Data Source=localhost\SQLEXPRESS;
        Initial Catalog=Final_DB;
        Integrated Security=True;
        TrustServerCertificate=True");

        public Users()
        {
            InitializeComponent();

            // Automatically load users when form opens
            LoadUsers();
        }

        // =========================================
        // LOAD USERS TO DATAGRIDVIEW
        // =========================================
        private void LoadUsers()
        {
            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                conn.Open();

                string query = @"
                SELECT
                    user_ID AS [ID],
                    last_name AS [Last Name],
                    first_name AS [First Name],
                    username AS [Username],
                    role AS [Role],
                    date_added AS [Date Added]
                FROM Users
                WHERE role='Cashier'";

                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();

                da.Fill(dt);

                dgvUsers.AutoGenerateColumns = true;
                dgvUsers.DataSource = dt;

                dgvUsers.AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill;

                dgvUsers.SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect;

                dgvUsers.MultiSelect = false;
                dgvUsers.ReadOnly = true;

                dgvUsers.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error loading users: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // =========================================
        // ADD CASHIER
        // =========================================
        private void btnAddCashier_Click(object sender, EventArgs e)
        {
            if (txtLastName.Text == "" ||
                txtFirstName.Text == "" ||
                txtUsername.Text == "" ||
                txtPassword.Text == "")
            {
                MessageBox.Show(
                    "Please fill in all fields.");
                return;
            }

            try
            {
                conn.Open();

                string checkUser =
                    "SELECT COUNT(*) FROM Users WHERE username=@username";

                SqlCommand checkCmd =
                    new SqlCommand(checkUser, conn);

                checkCmd.Parameters.AddWithValue(
                    "@username",
                    txtUsername.Text.Trim());

                int exists =
                    Convert.ToInt32(checkCmd.ExecuteScalar());

                if (exists > 0)
                {
                    MessageBox.Show(
                        "Username already exists.");
                    return;
                }

                string query = @"
                INSERT INTO Users
                (
                    first_name,
                    last_name,
                    username,
                    password,
                    role,
                    date_added
                )
                VALUES
                (
                    @fname,
                    @lname,
                    @username,
                    @password,
                    'Cashier',
                    GETDATE()
                )";

                SqlCommand cmd =
                    new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                    "@fname",
                    txtFirstName.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@lname",
                    txtLastName.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@username",
                    txtUsername.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@password",
                    txtPassword.Text.Trim());

                cmd.ExecuteNonQuery();

                MessageBox.Show(
                    "Cashier added successfully!");

                txtLastName.Clear();
                txtFirstName.Clear();
                txtUsername.Clear();
                txtPassword.Clear();

                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error adding cashier: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // =========================================
        // DELETE CASHIER
        // =========================================
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow == null)
            {
                MessageBox.Show(
                    "Please select a cashier.");
                return;
            }

            DialogResult result =
                MessageBox.Show(
                "Delete this cashier?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();

                    int id = Convert.ToInt32(
                        dgvUsers.CurrentRow.Cells["ID"].Value);

                    string query =
                        "DELETE FROM Users WHERE user_ID=@id";

                    SqlCommand cmd =
                        new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Cashier deleted successfully!");

                    LoadUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error deleting cashier: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}