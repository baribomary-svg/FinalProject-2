using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class Users : Form
    {
        // =========================================
        // SQL CONNECTION
        // =========================================
        SqlConnection conn = new SqlConnection(
        @"Data Source=localhost\SQLEXPRESS;
        Initial Catalog=Final_DB;
        Integrated Security=True;
        TrustServerCertificate=True");

        // =========================================
        // CONSTRUCTOR
        // =========================================
        public Users()
        {
            InitializeComponent();

            LoadUsers();
        }

        // =========================================
        // LOAD USERS
        // =========================================
        private void LoadUsers()
        {
            try
            {
                // CLOSE IF CONNECTION IS OPEN
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();

                string query = @"
                SELECT
                    user_ID,
                    last_name,
                    first_name,
                    username,
                    role,
                    date_added
                FROM Users
                WHERE role = 'Cashier'";

                SqlDataAdapter da =
                new SqlDataAdapter(query, conn);

                DataTable dt =
                new DataTable();

                da.Fill(dt);

                dgvUsers.DataSource = dt;
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
            // VALIDATION
            if (txtFirstName.Text == "" ||
                txtLastName.Text == "" ||
                txtUsername.Text == "" ||
                txtPassword.Text == "")
            {
                MessageBox.Show(
                    "Please fill in all fields.");

                return;
            }

            try
            {
                // CLOSE IF OPEN
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }

                conn.Open();

                // =====================================
                // CHECK DUPLICATE USERNAME
                // =====================================
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

                    conn.Close();

                    return;
                }

                // =====================================
                // INSERT CASHIER
                // =====================================
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
                    @first_name,
                    @last_name,
                    @username,
                    @password,
                    'Cashier',
                    GETDATE()
                )";

                SqlCommand cmd =
                new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                    "@first_name",
                    txtFirstName.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@last_name",
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

                conn.Close();

                // REFRESH GRID
                LoadUsers();

                // CLEAR FIELDS
                txtFirstName.Clear();
                txtLastName.Clear();
                txtUsername.Clear();
                txtPassword.Clear();
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
                    // CLOSE IF OPEN
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }

                    conn.Open();

                    int id =
                    Convert.ToInt32(
                    dgvUsers.CurrentRow.Cells["user_ID"].Value);

                    string query =
                    "DELETE FROM Users WHERE user_ID=@id";

                    SqlCommand cmd =
                    new SqlCommand(query, conn);

                    cmd.Parameters.AddWithValue(
                        "@id",
                        id);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show(
                        "Cashier deleted successfully!");

                    conn.Close();

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