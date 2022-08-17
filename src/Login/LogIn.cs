using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CuOnline_Portal.src.Connection;


namespace CuOnline_Portal.src.Login
{
    public class LogIn
    {
        private string admin_id;
        private string admin_pass;
        private string faculty_id;
        private string faculty_pass;
        private string student_id;
        private string student_pass;

       

        public string Admin_id { get => admin_id; set => admin_id = value; }
        public string Admin_pass { get => admin_pass; set => admin_pass = value; }
        public string Faculty_id { get => faculty_id; set => faculty_id = value; }
        public string Faculty_pass { get => faculty_pass; set => faculty_pass = value; }
        public string Student_id { get => student_id; set => student_id = value; }
        public string Student_pass { get => student_pass; set => student_pass = value; }

        public string AdminLogin(string id, string pass)
        {
            Connection.Connection.con.Open();
            String query = "Select Admin_ID,Admin_Password from Admin where Admin_ID = '"+id+"'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                Admin_id = read["Admin_ID"].ToString();
                Admin_pass = read["Admin_Password"].ToString();
                Connection.Connection.con.Close();
                if (Admin_id == id && Admin_pass == pass)
                    return "admin";
                else
                    return "incorrect";
            }
            else
            {
                Connection.Connection.con.Close();
                return "error";
            }
                
        }
        public string FacultyLogin(string id, string pass)
        {
            Connection.Connection.con.Open();
            String query = "Select Fac_ID,Fac_Password from Faculty where Fac_ID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                Faculty_id = read["Fac_ID"].ToString();
                Faculty_pass = read["Fac_Password"].ToString();
                Connection.Connection.con.Close();
                if (Faculty_id == id && Faculty_pass == pass)
                    return "faculty";
                else
                    return "incorrect";
            }
            else
            {
                Connection.Connection.con.Close();
                return "error";
            }

        }

        public string StudentLogin(string id, string pass)
        {
            Connection.Connection.con.Open();
            String query = "Select Std_ID,Std_Password from Student where Std_ID = '" + id + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.Read())
            {
                Student_id = read["Std_ID"].ToString();
                Student_pass = read["Std_Password"].ToString();
                Connection.Connection.con.Close();
                if (Student_id == id && Student_pass == pass)
                    return "student";
                else
                    return "incorrect";
            }
            else
            {
                Connection.Connection.con.Close();
                return "error";
            }

        }

    }
}
