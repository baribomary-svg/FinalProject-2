using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class SignIn : Form
    {

        // =========================================
        // SQL CONNECTION
        // =========================================
        SqlConnection conn = new SqlConnection(
        @"Data Source=localhost\SQLEXPRESS;
        Initial Catalog=Final_DB;
        Integrated Security=True;
        TrustServerCertificate=True");

        public SignIn()
        {
            InitializeComponent();

            txtPassword.PasswordChar = '•';

        }

        private void SignIn_Load(object sender, EventArgs e)
        {

        }
        // =========================================
        // SIGN IN BUTTON
        // =========================================
        private void bntSignIn_Click(object sender, EventArgs e)
        {
            string username =
            txtUserName.Text.Trim();

            string password =
            txtPassword.Text.Trim();

            // =====================================
            // VALIDATION
            // =====================================
            if (username == "" || password == "")
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            try
            {
                conn.Open();

                // =====================================
                // GET USER ROLE
                // =====================================
                string query = @"
                SELECT role
                FROM Users
                WHERE username=@username
                AND password=@password";

                SqlCommand cmd =
                new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                    "@username",
                    username);

                cmd.Parameters.AddWithValue(
                    "@password",
                    password);

                object result =
                cmd.ExecuteScalar();

                // =====================================
                // LOGIN FAILED
                // =====================================
                if (result == null)
                {
                    MessageBox.Show(
                        "Invalid Username or Password.",
                        "Login Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    conn.Close();

                    return;
                }

                // =====================================
                // GET ROLE
                // =====================================
                string role =
                result.ToString();

                MessageBox.Show(
                    "Login Successful!",
                    "Welcome",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // =====================================
                // ADMIN
                // =====================================
                if (role == "Admin")
                {
                    AdminDashboard admin =
                    new AdminDashboard();

                    admin.Show();

                    this.Hide();
                }

                // =====================================
                // CASHIER
                // =====================================
                else if (role == "Cashier")
                {
                    CashierDashboard cashier =
                    new CashierDashboard();

                    cashier.Show();

                    this.Hide();
                }

                // =====================================
                // CUSTOMER
                // =====================================
                else if (role == "Customer")
                {
                    DashBoardCustomer customer =
                    new DashBoardCustomer();

                    customer.Show();

                    this.Hide();
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                conn.Close();
            }
        }

        // =========================================
        // GO TO REGISTER
        // =========================================
        private void label5_Click(object sender, EventArgs e)
        {
            LogIn signup = new LogIn();

            signup.Show();

            this.Hide();
        }

        // =========================================
        // TOGGLE PASSWORD
        // =========================================
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '•')
            {
                txtPassword.PasswordChar = '\0'; // SHOW PASSWORD
                btnTogglePassword.Text = "👁";
            }
            else
            {
                txtPassword.PasswordChar = '•'; // HIDE PASSWORD
                btnTogglePassword.Text = "🚫";
            }

        }
    }
}