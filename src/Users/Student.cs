using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuOnline_Portal.src.Users
{
    internal class Student : Users
    {
        private int student_semester;
        private string student_department;
        private string student_programme;


        public int Student_semester { get => student_semester; set => student_semester = value; }
        public string Student_department { get => student_department; set => student_department = value; }
        public string Student_programme { get => student_programme; set => student_programme = value; }


        public SqlDataReader viewCourses(string std_id)
        {
            Connection.Connection.con.Open();
            string query = "select Student_Registration.Course_Code,Courses.Course_Name,Courses.Credit_Hours from Student_Registration inner join Courses on Student_Registration.Course_Code = Courses.Course_Code where Std_ID = '" + std_id + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;

        }

        public SqlDataReader viewAssignments(string selectedCourse)
        {
            Connection.Connection.con.Open();
            string query = "select Assignment_Name,Assignment_deadline from Deliverable where Course_Code = '" + selectedCourse + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader viewAttendance(string lec, string std_id, string selectedCourse)
        {
            Connection.Connection.con.Open();
            string query = null;
            if (lec == "Lab")
                query = "select Lecture_note,Lecture_Date,Lecture_start_time,Attendance from Lab_Attendance where Course_Code = '" + selectedCourse + "' and Std_ID = '" + std_id + "'";
            else
                query = "select Lecture_note,Lecture_Date,Lecture_start_time,Attendance from Attendance where Course_Code = '" + selectedCourse + "' and Std_ID = '" + std_id + "'";

            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;

        }
        public SqlDataReader viewMarks(string std_id, string selectedCourse,string table)
        {
            Connection.Connection.con.Open();
            string query = "select "+ table + "_Marks_Id, " + table + "_Marks, " + table + "_total_Marks from " + table + "_Marks where Course_Code = '" + selectedCourse + "' and Std_ID = '" + std_id + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader viewProfile(string std_id)
        {
            Connection.Connection.con.Open();
            string query = "select * from Student where Std_ID = '" + std_id + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }
    }
}
