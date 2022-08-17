using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuOnline_Portal.src.Users
{
    internal class Faculty : Users
    {
        private String faculty_designation;
        private string faculty_education;
        private string assigned_course;
        private int course_credit_hours;
        public string Faculty_designation { get => faculty_designation; set => faculty_designation = value; }
        public string Faculty_education { get => faculty_education; set => faculty_education = value; }
        public string Assigned_course { get => assigned_course; set => assigned_course = value; }
        public int Course_credit_hours { get => course_credit_hours; set => course_credit_hours = value; }

        public void Mark_Attendance(string lec,string course_id,string std_id,string lec_note,string start,string end,string mark)
        {
            
            Connection.Connection.con.Open();
            string which_lec;
            if (lec == "Theory") which_lec = "Attendance";
            else which_lec = "Lab_Attendance";
            String query = "insert into "+which_lec+" values(@Course_Code,@Std_ID,@Lecture_note,cast (getdate() as date),@Lecture_start_time,@Lecture_end_time,@Attendance)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@Course_Code", course_id);
            cmd.Parameters.AddWithValue("@Std_ID", std_id);
            cmd.Parameters.AddWithValue("@Lecture_note", lec_note);
            cmd.Parameters.AddWithValue("@Lecture_start_time", start);
            cmd.Parameters.AddWithValue("@Lecture_end_time", end);
            cmd.Parameters.AddWithValue("@Attendance", mark);
            cmd.ExecuteNonQuery();
            Connection.Connection.con.Close();

        }
        public void viewProfile(Faculty faculty,string id)
        {
            Connection.Connection.con.Open();
            String query = " select Fac_ID,Fac_Name, Fac_Gender,Fac_DOB,Fac_Personal_Email,Fac_Official_Email,Fac_Education,Fac_designation,Fac_Address,Fac_PhoneNo,Fac_Pic from Faculty where Fac_ID = '"+id+"'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    faculty.User_id = reader["Fac_ID"].ToString();
                    faculty.User_name = reader["Fac_Name"].ToString();
                    faculty.User_gender = reader["Fac_Gender"].ToString();
                    faculty.User_DOB = reader["Fac_DOB"].ToString();
                    faculty.User_personal_email = reader["Fac_Personal_Email"].ToString();
                    faculty.User_official_email = reader["Fac_Official_Email"].ToString();
                    faculty.Faculty_education = reader["Fac_Education"].ToString();
                    faculty.Faculty_designation = reader["Fac_designation"].ToString();
                    faculty.User_phoneNo = reader["Fac_PhoneNo"].ToString();
                    faculty.User_address = reader["Fac_Address"].ToString();
                    faculty.User_image = (byte[])reader["Fac_Pic"];


                }
            }
            Connection.Connection.con.Close();

        }

        public void Update_Attendance(string lec,string course,string std_id,string date,string time,string mark)
        {
            Connection.Connection.con.Open();
            string which_lec;
            if(lec == "Theory") which_lec = "Attendance";
            else which_lec = "Lab_Attendance";

            String query = "update "+which_lec+" set Attendance = '" + mark + "' where Std_ID = '"+std_id+ "' AND Lecture_Date = '"+ date + "' and Lecture_start_time = '"+ time + "' and Course_Code = '"+course+"'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.ExecuteNonQuery();
            Connection.Connection.con.Close();
        }

        public void UploadMarks(string course, string std_id, string deliverableName, string deliverableNo, float obtainedMarks, float totalMarks)
        {
            Connection.Connection.con.Open();
            if (deliverableName == "Mid Exam") deliverableName = "Mid";
            else if (deliverableName == "Final Exam") deliverableName = "Final";
            else if (deliverableName == "Lab Assignments") deliverableName = "Lab_Assignments";
            else if (deliverableName == "Lab Mid Exam") deliverableName = "Lab_Mid";
                
            String query = "insert into " + deliverableName + "_Marks values(@Course_Code,@Std_ID, @"+ deliverableName + "_Marks_Id, @"+ deliverableName + "_Marks, @"+ deliverableName + "_total_Marks)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@Course_Code", course);
            cmd.Parameters.AddWithValue("@Std_ID", std_id);
            cmd.Parameters.AddWithValue("@" + deliverableName + "_Marks_Id", deliverableNo);
            cmd.Parameters.AddWithValue("@" + deliverableName + "_Marks", obtainedMarks);
            cmd.Parameters.AddWithValue("@" + deliverableName + "_total_Marks", totalMarks);
            cmd.ExecuteNonQuery();
            Connection.Connection.con.Close();

        }
    }
}
