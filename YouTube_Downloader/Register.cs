using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Configuration;

namespace YouTube_Downloader
{
    public partial class Register : Form
    {
        
        
        public Register()
        {
            InitializeComponent();
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtEmail.Text == "E-mail Address | User-Name")
            {
                txtEmail.Text = "";
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtPassword.Text == "Type Your Password")
            {
                txtPassword.Text = "";
            }

        }

        private void txtRePassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (txtRePassword.Text == "Repeat Your Password")
            {
                txtRePassword.Text = "";
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text != txtRePassword.Text)
            {
                MessageBox.Show("Your Password Not the same!");
            }
            else if (txtRePassword.Text == "" || txtRePassword.Text == "" || txtEmail.Text == "")
            {
                MessageBox.Show("You Must fill all field with data");
            }
            else
            {

                string connectionString = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;

                SqlConnection sqlcon =new SqlConnection(connectionString);
                string query= "insert into [dbo].[users] values(@email_username,@password)";
                sqlcon.Open();
                SqlCommand com = new SqlCommand(query,sqlcon);
                com.Parameters.AddWithValue("@email_username", txtEmail.Text);
                com.Parameters.AddWithValue("@password", txtPassword.Text);
                com.ExecuteNonQuery();
                sqlcon.Close();
                
                this.Close();
                login login = new login();
                login.Show();
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do You Want To Exit ?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                Environment.Exit(1);
            }
        }

        private void loginBack_Click(object sender, EventArgs e)
        {
            this.Close();
            login log =new login();
            log.Show();
        }
    }

}
