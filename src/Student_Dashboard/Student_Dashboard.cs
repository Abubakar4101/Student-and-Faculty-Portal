using CuOnline_Portal.src.Boxes;
using CuOnline_Portal.src.Users;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace CuOnline_Portal.src.Student_Dashboard
{
    public partial class Student_Dashboard : Form
    {
        DateTime dateTime = DateTime.Now;
        Student student = new Student();
        string selectedCourse;
        int selectedCourseCreditHours;
        ErrorBox error_box = null;
        public Student_Dashboard(string std_id)
        {
            InitializeComponent();
            student.User_id = std_id;
        }

        private void Student_Dashboard_Load(object sender, EventArgs e)
        {
            Date.Text = dateTime.ToString("MM/dd/yyyy");
            showCoursesList(student.User_id);
            showProfile(student.User_id);
        }

        private void showProfile(string std_id)
        {
            

            SqlDataReader reader = student.viewProfile(std_id);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Student_Name.Text = std_id + " (" + reader["Std_Name"].ToString() + ")";
                    P_Student_ID.Text = reader["Std_ID"].ToString();
                    P_Student_Name.Text = reader["Std_Name"].ToString();
                    P_Student_Gender.Text = reader["Std_Gender"].ToString();
                    P_Student_DOB.Text = reader["Std_DOB"].ToString();
                    P_Student_Dep.Text = reader["Std_Department"].ToString();
                    P_Student_Pro.Text = reader["Std_Programme"].ToString();
                    P_Student_PEmail.Text = reader["Std_Personal_Email"].ToString();
                    P_Student_OEmail.Text = reader["Std_Official_Email"].ToString();
                    P_Student_Address.Text = reader["Std_Address"].ToString();
                    P_Student_Phone.Text = reader["Std_PhoneNo"].ToString();
                    MemoryStream ms = new MemoryStream((byte[])reader["Std_Pic"]);
                    Student_Pic.Image = Image.FromStream(ms);
                }
            }

            Connection.Connection.con.Close();
        }

        private void showCoursesList(string std_id)
        {

            
            string[] batch = std_id.Split('-');
            string student_batch = batch[0] + "-" + batch[1];
            SqlDataReader reader = student.viewCourses(std_id);
            if (reader.HasRows)
            {
                int i = 0;
                while (reader.Read())
                {
                    
                    Student_registered_Courses_List.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Course_Code"].ToString(),
                            reader["Course_Name"].ToString(),
                            reader["Credit_Hours"].ToString(),
                            student_batch,
                       }
                    );
                    i++;

                }
            }
            Connection.Connection.con.Close();
        }

        private void Student_Dashboard_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Student_registered_Courses_List_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            selectedCourse = Student_registered_Courses_List.Rows[index].Cells[0].Value.ToString();
            selectedCourseCreditHours = int.Parse(Student_registered_Courses_List.Rows[index].Cells[2].Value.ToString());
            Course_Home.Text = Student_registered_Courses_List.Rows[index].Cells[1].Value.ToString();
            bunifuPages1.SetPage(1);


        }


        private void Home_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(0);
        }

        private void Back_Course_Content_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(1);
        }

        private void view_Assignments_Click(object sender, EventArgs e)
        {
            Show_Assignments_Data.Rows.Clear();
            SqlDataReader reader = student.viewAssignments(selectedCourse);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Show_Assignments_Data.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Assignment_Name"].ToString(),
                            reader["Assignment_deadline"].ToString(),
                            
                       }
                    );

                }
            }
            bunifuPages1.SetPage(2);
            Connection.Connection.con.Close();
        }

        private void Show_Assignments_Data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(Show_Assignments_Data.Columns[e.ColumnIndex].Name == "View_File")
            {
                Connection.Connection.con.Open();
                String assignment = Show_Assignments_Data.Rows[e.RowIndex].Cells[0].Value.ToString();
                string query = "select Assignment_Name, Assignment_ext,Assignment_file from Deliverable where Course_Code = '" + selectedCourse + "' and Assignment_Name = '" + assignment + "'";
                SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader["Assignment_Name"].ToString();
                    string ext = reader["Assignment_ext"].ToString();
                    var data = (byte[])reader["Assignment_file"];
                    var newFileName = name + "." + ext;
                    File.WriteAllBytes(newFileName, data);
                    System.Diagnostics.Process.Start(newFileName);
                    
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = "No File Attached";
                    error_box.Show();
                    
                }
                Connection.Connection.con.Close();
            }
        }

        private void ViewAttendance_Click(object sender, EventArgs e)
        {
            
            if(selectedCourseCreditHours == 4)
            {
                Back.Controls.Add(Back_Course_Content);
                viewAttendance("Theory",Theory_Attendance_4);
                viewAttendance("Lab",Lab_Attendance_4);
                fillCircle(Lab_Attendance_4, Circle_l_4);
                fillCircle(Theory_Attendance_4, Circle_t_4);
                bunifuPages1.SetPage(3);


            }
            else
            {
                Back_A.Controls.Add(Back_Course_Content);
                viewAttendance("Theory", Theory_Attendance_3);
                fillCircle(Theory_Attendance_3, Circle_t_3);
                bunifuPages1.SetPage(4);
            }
        }

        private void fillCircle(Guna2DataGridView Attendance_panel, Guna2CircleProgressBar circle)
        {
            if (Attendance_panel.Rows.Count == 0)
                circle.Value = 0;
            else
            {
                int present = 0;
                for (int i = 0; i < Attendance_panel.Rows.Count; i++)
                {
                    if (Attendance_panel.Rows[i].Cells[3].Value.ToString() == "Present")
                        present++;
                }

                double percentage = (double)((present * 100) / Attendance_panel.Rows.Count);
                circle.Value = (int)percentage;
            }
            

        }

        private void viewAttendance(string lec, Guna.UI2.WinForms.Guna2DataGridView Attendance_panel)
        {
            
            SqlDataReader reader = student.viewAttendance(lec, student.User_id,selectedCourse);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Attendance_panel.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Lecture_note"].ToString(),
                            reader["Lecture_Date"].ToString(),
                            reader["Lecture_start_time"].ToString(),
                            reader["Attendance"].ToString(),

                       }
                    );

                }
            }
            else
                Attendance_panel.Show();
            Connection.Connection.con.Close();
        }

        private void ViewMarks_Click(object sender, EventArgs e)
        {
            if (selectedCourseCreditHours == 4)
            {
                Back_M2.Controls.Add(Back_Course_Content);
                viewMarks("Theory");
                viewMarks("Lab");
                bunifuPages1.SetPage(6);


            }
            else
            {
                Back_M1.Controls.Add(Back_Course_Content);
                viewMarks("Theory");
                bunifuPages1.SetPage(5);
            }
        }

        private void viewMarks(string lec)
        {
            List<String> tables = new List<String>();
            List<Guna2DataGridView> datagrids = new List<Guna2DataGridView>();
            
            if (lec == "Theory")
            {
                
                String[] tableRange = { "Assignments", "Quizes", "Mid", "Final" };
                tables.AddRange(tableRange);
                Guna2DataGridView[] gridRange = { Assignment_marks_3, Quiz_marks_3, Mid_marks_3, Final_marks_3 };
                datagrids.AddRange(gridRange);
            }
            else
            {
                String[] tableRange = { "Assignments", "Quizes", "Mid", "Final", "Lab_Assignments", "Lab_Mid", "Lab_Final" };
                tables.AddRange(tableRange);
                Guna2DataGridView[] gridRange = { Assignment_marks_4, Quiz_marks_4, Mid_marks_4, Final_marks_4, Lab_assignment_marks_4, Lab_Mid_marks_4, Lab_final_marks_4 };
                datagrids.AddRange(gridRange);
            }
            for(int i = 0; i < tables.Count;i++)
            {
                datagrids[i].Rows.Clear();
                SqlDataReader reader = student.viewMarks(student.User_id, selectedCourse ,tables[i]);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        datagrids[i].Rows.Add
                        (
                           new Object[]
                           {

                            reader[tables[i] + "_Marks_Id"].ToString(),
                            reader[tables[i] + "_Marks"].ToString(),
                            reader[tables[i] + "_total_Marks"].ToString(),

                           }
                        );

                    }
                }
                else
                {
                    int index = datagrids[i].Rows.Add();
                    datagrids[i].Rows[index].Cells[1].Value = "No Data Available";
                }
                    
                Connection.Connection.con.Close();
            }
            

        }

        private void Show_Profile_Click(object sender, EventArgs e)
        {
            
            bunifuPages1.SetPage(7);
        }

        private void Dashboard_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(0);
        }

        private void Student_Logout_Click(object sender, EventArgs e)
        {
            
            new LogIn_Interface().Show();
            this.Hide();

        }
    }
}

