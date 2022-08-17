using CuOnline_Portal.Admin;
using CuOnline_Portal.src.Boxes;
using CuOnline_Portal.src.Connection;
using CuOnline_Portal.src.Faculty_Dashboard;
using CuOnline_Portal.src.Login;
using CuOnline_Portal.src.Student_Dashboard;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuOnline_Portal
{
    public partial class LogIn_Interface : Form
    {
        //SuccessBox success_box = null;
        ErrorBox error_box = null;
        SuccessBox success_box = null;
        LogIn login = new LogIn();
        

        public LogIn_Interface()
        {
            InitializeComponent();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            /*Student_Pass.PasswordChar = '*';
            Admin_Pass.PasswordChar = '*';
            Teacher_Pass.PasswordChar = '*';*/
            Student_Pass.UseSystemPasswordChar = true;
            Admin_Pass.UseSystemPasswordChar = true;
            Teacher_Pass.UseSystemPasswordChar = true;
            new_pass.UseSystemPasswordChar = true;
            Conirm_pass.UseSystemPasswordChar = true;
            Change_Pass_Panel.Hide();
           

        }

        private void Admin_b_Click(object sender, EventArgs e)
        {
            Student_ID.Hide();
            Student_Pass.Hide();
            Student_Sign.Hide();
            Teacher_ID.Hide();
            Teacher_Pass.Hide();
            Teacehr_Sign.Hide();
            Admin_ID.Show();
            Admin_Pass.Show();
            Admin_Sign.Show();
            Forgot_Pass.Hide();
            Click_Here.Hide();
        }

        private void Faculty_b_Click(object sender, EventArgs e)
        {
            Student_ID.Hide();
            Student_Pass.Hide();
            Student_Sign.Hide();
            Teacher_ID.Show();
            Teacher_Pass.Show();
            Teacehr_Sign.Show();
            Admin_ID.Hide();
            Admin_Pass.Hide();
            Admin_Sign.Hide();
            Forgot_Pass.Show();
            Click_Here.Show();
        }

        private void Student_b_Click(object sender, EventArgs e)
        {
            Student_ID.Show();
            Student_Pass.Show();
            Student_Sign.Show();
            Teacher_ID.Hide();
            Teacher_Pass.Hide();
            Teacehr_Sign.Hide();
            Admin_ID.Hide();
            Admin_Pass.Hide();
            Admin_Sign.Hide();
            Forgot_Pass.Show();
            Click_Here.Show();
        }

        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Student_Sign_Click(object sender, EventArgs e)
        {
            string message = login.StudentLogin(Student_ID.Text, Student_Pass.Text);
            Verify_Login(message);
        }
        private void Admin_Sign_Click(object sender, EventArgs e)
        {
            string message = login.AdminLogin(Admin_ID.Text, Admin_Pass.Text);
            Verify_Login(message);
        }
        private void Teacehr_Sign_Click(object sender, EventArgs e)
        {
            string message = login.FacultyLogin(Teacher_ID.Text, Teacher_Pass.Text);
            Verify_Login(message);
        }
        private void Verify_Login(string message)
        {
           
           
            if (message == "admin")
            {
                new Admin_Dashboard().Show();
                this.Close();
            }
            else if(message == "faculty")
            {
                new Faculty_Dashboard(Teacher_ID.Text).Show();
                this.Close();
            }
            else if(message == "student")
            {
                new Student_Dashboard(Student_ID.Text).Show();
                this.Close();
            }
            else if(message == "incorrect")
            {
                error_box = new ErrorBox();
                error_box.Location = new Point(480, 320);
                error_box.text = "Incorrect ID or Password";
                error_box.Show();

            }
            
            else
            {
                error_box = new ErrorBox();
                error_box.Location = new Point(480, 320);
                error_box.text = "Sorry, This ID does not Exist";
                error_box.Show();
            }
        }

        private bool NotValidEmail()
        {
            string user = null;
            if (select_drop.SelectedItem.ToString() == "Student")
                user = "Std_Personal_Email";
            else
                user = "Fac_Personal_Email";
            Connection.con.Open();
            String query = "Select " + user + " from " + select_drop.SelectedItem.ToString() + " where " + user + " = '" + Email.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Connection.con.Close();
                return false;
            }
            else
            {
                Connection.con.Close();
                return true;
            }
        }
        private bool NotValidID()
        {
            string user = null;
            if (select_drop.SelectedItem.ToString() == "Student")
                user = "Std_ID";
            else
                user = "Fac_ID";
            Connection.con.Open();
            String query = "Select "+ user + " from "+ select_drop.SelectedItem.ToString() + " where "+ user + " = '" + ID.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Connection.con.Close();
                return false;
            }
            else
            {
                Connection.con.Close();
                return true;
            }

        }
        private void Click_Here_Click(object sender, EventArgs e)
        {
            select_drop.Text = "Select";
            ID.ResetText();
            Email.ResetText();
            new_pass.ResetText();
            Conirm_pass.ResetText();
            Transition.ShowSync(Change_Pass_Panel);
        }

        private void Change_Pass_Panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Change_Pass_Panel);
        }

        private void Change_pass_btn_Click(object sender, EventArgs e)
        {
            if(select_drop.Text == "Select")
            {
                error_box = new ErrorBox();
                error_box.Location = new Point(480, 320);
                error_box.text = "Select the Portal Type";
                error_box.Show();
                return;
            }
            else if(ID.Text == "" || NotValidID())
            {
                error_box = new ErrorBox();
                error_box.Location = new Point(480, 320);
                error_box.text = "ID has not been entered or not Correct";
                error_box.Show();
                return;
            }
            else if (NotValidEmail())
            {
                error_box = new ErrorBox();
                error_box.Location = new Point(480, 320);
                error_box.text = "This email is not in record";
                error_box.Show();
                return;
            }
            else if(new_pass.Text != Conirm_pass.Text)
            {
                error_box = new ErrorBox();
                error_box.Location = new Point(480, 320);
                error_box.text = "Confirm Password not matched";
                error_box.Show();
                return;
            }
            
            else
            {
                MailMessage mail = new MailMessage()
                {
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.Default
                };
                mail.To.Add(Email.Text);
                mail.From = new MailAddress("cuonline.portal@gmail.com");
                mail.Subject = "CuOnline Password Changed";
                mail.Body = "<b>Dear " + ID.Text + ",<b><br><br>You recently requested to reset the password for your " + select_drop.SelectedItem.ToString() + " portal account. So, It is informed you that your CuOnline Password has been changed.<br><br><b>ID: " + ID.Text + "<b><br><b>New Password: " + Conirm_pass.Text + "<b><br><br>Regards<br><b>CuOnline Team<b>";
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.EnableSsl = true;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("cuonline.portal@gmail.com", "qmprkvplpspngfkh");
                smtp.Send(mail);
                Change_Pass_Panel.Hide();
                success_box = new SuccessBox();
                success_box.Location = new Point(480, 320);
                success_box.text = "Please Check you Mail";
                success_box.Show();
                Connection.con.Open();
                string pass = null;
                string id = null;
                if (select_drop.SelectedItem.ToString() == "Faculty")
                {
                    pass = "Fac_Password";
                    id = "Fac_Id";
                }
                    
                else
                {
                    id = "Std_Id";
                    pass = "Std_Password";
                }

                string query = "Update " + select_drop.SelectedItem.ToString() + " set "+ pass+" = '" + Conirm_pass.Text + "' where " + id + " = '" + ID.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Connection.con);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
            }
            
        }

        private void ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPass.Checked)
            {
                new_pass.UseSystemPasswordChar = false;
                Conirm_pass.UseSystemPasswordChar = false;
            }
            else
            {
                new_pass.UseSystemPasswordChar = true;
                Conirm_pass.UseSystemPasswordChar = true;
            }
        }

        private void Login_ShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (Login_ShowPass.Checked)
            {
                Student_Pass.UseSystemPasswordChar = false;
                Admin_Pass.UseSystemPasswordChar = false;
                Teacher_Pass.UseSystemPasswordChar = false;
            }
            else
            {
                Student_Pass.UseSystemPasswordChar = true;
                Admin_Pass.UseSystemPasswordChar = true;
                Teacher_Pass.UseSystemPasswordChar = true;
            }
        }

        
    }

        
    }
