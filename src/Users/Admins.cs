using System;
using System.Data.SqlClient;
using CuOnline_Portal.src.Courses;
using Guna.UI2.WinForms;

namespace CuOnline_Portal.src.Users
{
    internal class Admins : Users
    {
        public SqlCommand addCourse(Courses.Courses course)
        {
            String query = "insert into Courses values(@Course_Code,@Course_Name,@Credit_Hours,@Course_Department)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@Course_Code", course.COURSE_CODE);
            cmd.Parameters.AddWithValue("@Course_Name", course.COURSE_NAME);
            cmd.Parameters.AddWithValue("@Credit_Hours", course.CREDIT_HOURS);
            cmd.Parameters.AddWithValue("@Course_Department", course.COURSE_DEPART);
            return cmd;
        }
        public SqlCommand dropCourse(string courseID)
        {
            String query = "DELETE FROM Courses WHERE Course_Code = '" + courseID + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            return cmd;
        }
        public SqlCommand registerStudent(Student student)
        {
            String query = "insert into Student values(@Std_ID,@Std_Name,@Std_Password,@Std_Personal_Email,@Std_Official_Email,@Std_Department,@Std_Programme,@Std_Gender,@Std_DOB,@Std_Address,@Std_PhoneNo,@Std_Pic)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@Std_ID", student.User_id);
            cmd.Parameters.AddWithValue("@Std_Name", student.User_name);
            cmd.Parameters.AddWithValue("@Std_Password", student.User_password);
            cmd.Parameters.AddWithValue("@Std_Personal_Email", student.User_personal_email);
            cmd.Parameters.AddWithValue("@Std_Official_Email", student.User_official_email);
            cmd.Parameters.AddWithValue("@Std_Department", student.Student_department);
            cmd.Parameters.AddWithValue("@Std_Programme", student.Student_programme);
            cmd.Parameters.AddWithValue("@Std_Gender", student.User_gender);
            cmd.Parameters.AddWithValue("@Std_DOB", student.User_DOB);
            cmd.Parameters.AddWithValue("@Std_Address", student.User_address);
            cmd.Parameters.AddWithValue("@Std_PhoneNo", student.User_phoneNo);
            cmd.Parameters.AddWithValue("@Std_Pic", student.User_image);
            return cmd;
        }

        public SqlCommand dropStudent(string stdID)
        {
            String query = "DELETE FROM Student WHERE Std_ID = '" + stdID + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            return cmd;
        }

        public SqlCommand registerFaculty(Faculty faculty)
        {
            String query = "insert into Faculty values(@Fac_ID,@Fac_Name,@Fac_Password,@Fac_Personal_Email,@Fac_Official_Email,@Fac_Education,@Fac_designation,@Fac_Gender,@Fac_DOB,@Fac_Address,@Fac_PhoneNo,@Fac_Pic)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@Fac_ID", faculty.User_id);
            cmd.Parameters.AddWithValue("@Fac_Name", faculty.User_name);
            cmd.Parameters.AddWithValue("@Fac_Password", faculty.User_password);
            cmd.Parameters.AddWithValue("@Fac_Personal_Email", faculty.User_personal_email);
            cmd.Parameters.AddWithValue("@Fac_Official_Email", faculty.User_official_email);
            cmd.Parameters.AddWithValue("@Fac_Education", faculty.Faculty_education);
            cmd.Parameters.AddWithValue("@Fac_designation", faculty.Faculty_designation);
            cmd.Parameters.AddWithValue("@Fac_Gender", faculty.User_gender);
            cmd.Parameters.AddWithValue("@Fac_DOB", faculty.User_DOB);
            cmd.Parameters.AddWithValue("@Fac_Address", faculty.User_address);
            cmd.Parameters.AddWithValue("@Fac_PhoneNo", faculty.User_phoneNo);
            cmd.Parameters.AddWithValue("@Fac_Pic", faculty.User_image);

            return cmd;
        }

        public SqlCommand dropFaculty(string facID)
        {
            String query = "DELETE FROM Faculty WHERE Fac_ID = '" + facID + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            return cmd;
        }

        public SqlCommand assignCourseToFaculty(string fac_ID,string course_ID)
        {
            String query = "insert into Assign_Course values(@Fac_ID,@Course_Code)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@Fac_ID", fac_ID);
            cmd.Parameters.AddWithValue("@Course_Code", course_ID);

            return cmd;
        }

        public void viewProfile(Admins admin)
        {
            String query = " select Admin_ID,Admin_Name, Admin_Gender,Admin_DOB,Admin_Email,Admin_Address,Admin_Pic from Admin";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    admin.User_id = reader["Admin_ID"].ToString();
                    admin.User_name = reader["Admin_Name"].ToString();
                    admin.User_gender = reader["Admin_Gender"].ToString();
                    admin.User_DOB = reader["Admin_DOB"].ToString();
                    admin.User_official_email = reader["Admin_Email"].ToString();
                    admin.User_address = reader["Admin_Address"].ToString();
                    admin.User_image = (byte[])reader["Admin_Pic"];

                }
            }
        }
        public SqlCommand dropAssignedCourse(String facid)
        {
            String query = "DELETE FROM Assign_Course WHERE Fac_ID = '" + facid + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            return cmd;
        }

        public SqlCommand assignCourseToStudent(string course_code, string std_id)
        {
            String query = "insert into Student_Registration values(@course_code,@std_id)";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            cmd.Parameters.AddWithValue("@course_code", course_code);
            cmd.Parameters.AddWithValue("@std_id", std_id);
            return cmd;
        }

        public SqlCommand dropStudentAssignedCourse(string dropCourse, string stdid)
        {
            String query = "DELETE FROM Student_Registration WHERE Course_Code = '" + dropCourse + "' and Std_ID = '" + stdid + "'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            return cmd;
        }
    }
}
