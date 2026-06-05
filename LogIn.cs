using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class LogIn : Form
    {
       
        SqlConnection conn = new SqlConnection(
        @"Data Source=localhost\SQLEXPRESS;
        Initial Catalog=Final_DB;
        Integrated Security=True;
        TrustServerCertificate=True");

       
        public LogIn()
        {
            InitializeComponent();

            
            txtPassword.PasswordChar = '•';
            txtConfirm.PasswordChar = '•';

        
            btnTogglePassword.Text = "🚫👁";
            btnToggleConfirm.Text = "🚫👁";
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {

        }

      
        private void btnLogIn_Click(object sender, EventArgs e)
        {
           
            if (txtFirstName.Text == "" ||
                txtLastName.Text == "" ||
                txtEmail.Text == "" ||
                txtUserName.Text == "" ||
                txtPassword.Text == "" ||
                txtConfirm.Text == "")
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

        
            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show(
                    "Passwords do not match.",
                    "Validation",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

         
            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show(
                    "Password must be at least 8 characters.",
                    "Password Requirement",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

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
                    txtUserName.Text.Trim());

                int userExists =
                Convert.ToInt32(checkCmd.ExecuteScalar());

                if (userExists > 0)
                {
                    MessageBox.Show(
                        "This username already exists.",
                        "Duplicate Username",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    conn.Close();

                    return;
                }

               
                string query = @"
                INSERT INTO Users
                (
                    first_name,
                    last_name,
                    email,
                    username,
                    password,
                    role
                )

                OUTPUT INSERTED.user_ID

                VALUES
                (
                    @fname,
                    @lname,
                    @email,
                    @username,
                    @password,
                    'Customer'
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
                    "@email",
                    txtEmail.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@username",
                    txtUserName.Text.Trim());

                cmd.Parameters.AddWithValue(
                    "@password",
                    txtPassword.Text.Trim());

                int generatedID =
                Convert.ToInt32(cmd.ExecuteScalar());

                conn.Close();

                string formattedID =
                "USR-" + generatedID.ToString("D3");

                MessageBox.Show(
                    "Registered Successfully!\n\n" +
                    "User ID: " + formattedID,
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                SignIn signin = new SignIn();

                signin.Show();

                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                conn.Close();
            }
        }

 
        private void lblSignIn_Click_1(object sender, EventArgs e)
        {
            SignIn signin = new SignIn();

            signin.Show();

            this.Hide();
        }

       
        private void btnTogglePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '•')
            {
                txtPassword.PasswordChar = '\0';

                btnTogglePassword.Text = "👁";
            }
            else
            {
                txtPassword.PasswordChar = '•';

                btnTogglePassword.Text = "🚫👁";
            }
        }

       
        private void btnToggleConfirm_Click(object sender, EventArgs e)
        {
            if (txtConfirm.PasswordChar == '•')
            {
                txtConfirm.PasswordChar = '\0';

                btnToggleConfirm.Text = "👁";
            }
            else
            {
                txtConfirm.PasswordChar = '•';

                btnToggleConfirm.Text = "🚫👁";
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}