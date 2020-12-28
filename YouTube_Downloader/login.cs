using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YouTube_Downloader
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register =new Register();
            register.Show();
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtemail.Text == "E-mail Address")
            {
                txtemail.Text = "";
            }
           
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPassword.Text == "Type Your Password")
            {
                txtPassword.Text = "";
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;

            SqlConnection sqlcon = new SqlConnection(connectionString);
            string query =
                "Select id from users where email_username= @email and password =@P";
            SqlCommand com =new SqlCommand(query,sqlcon);
            com.Parameters.AddWithValue("@email", txtemail.Text);
            com.Parameters.AddWithValue("@P", txtPassword.Text);
            sqlcon.Open();
            var resualt = com.ExecuteScalar();
            if (resualt !=null)
            {
                MessageBox.Show(" Welcome to Homepage");
                MainForm mn =new MainForm();
                mn.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Login please check username and password");
            }
            sqlcon.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want To Exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(1);
            }
        }
    }
}
