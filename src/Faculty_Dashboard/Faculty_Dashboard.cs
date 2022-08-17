using Bunifu.UI.WinForms;
using CuOnline_Portal.src.Boxes;
using CuOnline_Portal.src.Users;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CuOnline_Portal.src.Faculty_Dashboard
{
    public partial class Faculty_Dashboard : Form
    {
        ErrorBox error_box = null;
        
        SuccessBox success_box = null;
        Faculty faculty = new Faculty();
        DateTime dateTime = DateTime.Now;
        CheckBox[] pradio = new CheckBox[60];
        TextBox[] textBox = new TextBox[60];
        string file_path;
        byte[] buffer;
        public Faculty_Dashboard(string faculty_id)
        {

            InitializeComponent();
            showingProfile(faculty_id);
            string message = fetchAssignedCourse(faculty_id);
            if(message == "login")
            {
                
                
                if (faculty.Course_credit_hours == 3)
                {
                    Class_Name.Text = "Theory";
                    Class_Name.Hide();
                    Marks_Class_name.Text = "Theory";
                    selectDeliverableName(Deliverable_name_drop);
                    selectDeliverableName(up_deliv_name);
                    Marks_Class_name.Hide();
                }
                
            }
                
            else
            {
                
                Faculty_pages.Hide();
                error_box = new ErrorBox();
                error_box.text = "No any Course Assignment to this faculty";
                error_box.Show();
            }
            


        }

        private string fetchAssignedCourse(string faculty_id)
        {
            Connection.Connection.con.Open();
            String query = "select Courses.Credit_Hours,Assign_Course.Course_Code from Assign_Course inner join Courses on Assign_Course.Course_Code = Courses.Course_Code where Fac_ID = '"+ faculty_id+"'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    faculty.Course_credit_hours = int.Parse(reader["Credit_Hours"].ToString());
                    faculty.Assigned_course = reader["Course_Code"].ToString();
                }
                Connection.Connection.con.Close();
                return "login";
            }
            
            else
            {
                Connection.Connection.con.Close();
                return "fail";
            }
            

        }
        
        private void showingProfile(string id)
        {
            
            faculty.viewProfile(faculty, id);
            MemoryStream ms = new MemoryStream(faculty.User_image);
            Fac_Picture.Image = Image.FromStream(ms);
            Fac_Name.Text = faculty.User_name;
            P_Faculty_ID.Text = faculty.User_id;
            P_Faculty_Name.Text = faculty.User_name;
            P_Faculty_Gender.Text = faculty.User_gender;
            P_Faculty_DOB.Text = faculty.User_DOB;
            P_Faculty_Edu.Text = faculty.Faculty_education;
            P_Faculty_Des.Text = faculty.Faculty_designation;
            P_Faculty_PEmail.Text = faculty.User_personal_email;
            P_Faculty_OEmail.Text = faculty.User_official_email;
            P_Faculty_Address.Text = faculty.User_address;
            P_Faculty_Phone.Text = faculty.User_phoneNo;
        }
        private void show_Assignment_Section()
        {
            Connection.Connection.con.Open();
            Added_Assignments_Data.Rows.Clear();
            String query = "Select Assignment_Name,Assignment_deadline,Assignment_file_name from Deliverable where Course_Code = '" + faculty.Assigned_course+"'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Added_Assignments_Data.Rows.Add
                    (
                       new Object[]
                       {
                            
                            reader["Assignment_Name"].ToString(),
                            reader["Assignment_deadline"].ToString(),
                            reader["Assignment_file_name"].ToString(),
                       }
                    );
                    
                }
            }
            else
                Added_Assignments_Data.Hide();


            Connection.Connection.con.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Faculty_Dashboard_Load(object sender, EventArgs e)
        {
            
            Date.Text = dateTime.ToString("MM/dd/yyyy");
            show_Assignment_Section();
        }

        private void Deadline_picker_ValueChanged(object sender, EventArgs e)
        {
            deadline_text.Text = Deadline_picker.Text;
        }

        private void Upload_Assignment_Panel_Close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Upload_Assignment_Panel);
        }

        private void Upload_File_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select file to be upload.";
            
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        file_path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        Upload_File_btn.Text = openFileDialog1.FileName;
                        
                        
                        using(Stream stream = File.OpenRead(Upload_File_btn.Text))
                        {
                            buffer = new byte[stream.Length];
                            stream.Read(buffer, 0, buffer.Length);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                error_box = new ErrorBox();
                error_box.text = ex.Message ;
                error_box.Show();
            }
        }
        
        private void Show_Assignment_btn_Click(object sender, EventArgs e)
        {
            Assignment_title_text.Text = "";
            deadline_text.Text = "";
            Upload_File_btn.Text = "Upload File";
            Transition.ShowSync(Upload_Assignment_Panel);
        }

        private void Show_Assignment_section_Click(object sender, EventArgs e)
        {
            Upload_Assignment_Panel.Hide();
            Faculty_pages.SetPage(1);
            
        }

        private void Upload_the_assignment_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Connection.con.Open();
                string query = "insert into Deliverable values(@Course_Code,@Assignment_Name,@Assignment_deadline,@Assignment_file_name,@Assignment_ext,@Assignment_file)";
                SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
                cmd.Parameters.AddWithValue("@Course_Code", faculty.Assigned_course);
                cmd.Parameters.AddWithValue("@Assignment_Name", Assignment_title_text.Text);
                cmd.Parameters.AddWithValue("@Assignment_deadline", deadline_text.Text);
                cmd.Parameters.AddWithValue("@Assignment_file_name", Upload_File_btn.Text);
                string[] f = Upload_File_btn.Text.Split('.');
                string extension = f.Last();
                cmd.Parameters.AddWithValue("@Assignment_ext", extension);
                cmd.Parameters.AddWithValue("@Assignment_file", buffer);
                cmd.ExecuteNonQuery();
                Connection.Connection.con.Close();
                Transition.HideSync(Upload_Assignment_Panel);
                SuccessBox box = new SuccessBox();
                box.text = "Assignment Upload Successfully";
                box.Show();
                show_Assignment_Section();
                show_Assignment_Section();
            }
            catch(SqlException ex)
            {
                if(ex.Number == 2627)
                {
                    error_box = new ErrorBox();
                    error_box.text = "File Already Exists";
                    error_box.Show();
                }
                else if (ex.Number == 515)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Somthing Missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
                Connection.Connection.con.Close();

            }
            
            
        }

        private void Clear_Assignment_section_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.Connection.con.Open();
                string query = "Delete from Deliverable";
                SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
                cmd.ExecuteNonQuery();
                Connection.Connection.con.Close();
                show_Assignment_Section();
                
            }
            catch (SqlException ex)
            {
                
                error_box = new ErrorBox();
                error_box.text = ex.Message;
                error_box.Show();
                Connection.Connection.con.Close();

            }
        }
        private void showStudentList(string which)
        {
            Connection.Connection.con.Open();
            Student_List.Rows.Clear();
            Marks_sheet.Rows.Clear();
            String query = "SELECT Student_Registration.Std_ID,Student.Std_Name FROM Student_Registration inner join Student on Student_Registration.Std_ID = Student.Std_ID where Course_Code = '" + faculty.Assigned_course+"'";
            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    Student_List.Rows.Add
                    (
                       new Object[]
                       {

                           reader["Std_ID"].ToString(),
                           reader["Std_Name"].ToString(),
                            

                       }
                       
                    );
                    Marks_sheet.Rows.Add
                   (
                      new Object[]
                      {

                           reader["Std_ID"].ToString(),
                           reader["Std_Name"].ToString(),


                      }

                   );
                }
                
                int i = 0, j = 0;
                while (j < Marks_sheet.Rows.Count)
                {
                    if (which == "Attendance")
                        Student_List.Controls.Add(pradio[j] = new CheckBox() { BackColor = Color.Transparent, Location = new Point(800, 60 + i), Size = new Size(30, 30) });
                    else
                        Marks_sheet.Controls.Add(textBox[j] = new TextBox() { BackColor = Color.White, Location = new Point(756, 63 + i), Size = new Size(100, 70), TextAlign = HorizontalAlignment.Center, ForeColor = Color.Black, BorderStyle = BorderStyle.None, Font = new Font("Bahnschrift", 12.0f, FontStyle.Bold)});

                    i = i + 28;
                    j++;
                }
                Student_List.Show();
                Marks_sheet.Show();

            }
            else
            {
                Student_List.Hide();
                Marks_sheet.Hide();
            }
                


            Connection.Connection.con.Close();
        }

        private void Attendance_Section_Click(object sender, EventArgs e)
        {
            Faculty_pages.SetPage(2);
            Update_Att_Panel.Hide();
            showStudentList("Attendance");
            
        }

        

        private void P_radio_CheckStateChanged(object sender, Bunifu.UI.WinForms.BunifuCheckBox.CheckedChangedEventArgs e)
        {
            for (int i = 0; i < Student_List.Rows.Count; i++)
            {
                if(e.Checked == true)
                    pradio[i].Checked = true;
                else
                    pradio[i].Checked = false;
            }
        }

        private void Submit_Attandance_Click(object sender, EventArgs e)
        {
            if (Class_Name.Text == "Theory")
                markTheAttendance("Theory");
            else if (Class_Name.Text == "Lab")
                markTheAttendance("Lab");
            else
            {
                error_box = new ErrorBox();
                error_box.text = "Please enter class name";
                error_box.Show();
            }
            

        }

        private void markTheAttendance(string lec)
        {
            try
            {
                if (Lec_start_time.Text == Lec_end_time.Text || Lec_start_time.Value > Lec_end_time.Value)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Please enter valid time";
                    error_box.Show();
                    return;
                }
                else if (Lecture_note.Text == "")
                {
                    error_box = new ErrorBox();
                    error_box.text = "Please enter lecture note";
                    error_box.Show();
                    return;
                }
                for (int i = 0; i < Student_List.Rows.Count; i++)
                {
                    string student_id = Student_List.Rows[i].Cells[0].Value.ToString();
                    string mark;
                    if (pradio[i].Checked == true)
                        mark = "Present";
                    else
                        mark = "Absent";

                    faculty.Mark_Attendance(lec,faculty.Assigned_course, student_id, Lecture_note.Text, Lec_start_time.Text, Lec_end_time.Text, mark);

                }
                success_box = new SuccessBox();
                success_box.text = "Attendance has been submitted";
                success_box.Show();
            }
            catch (SqlException ex)
            {

                Connection.Connection.con.Close();
                error_box = new ErrorBox();
                if (ex.Number == 2627)
                    error_box.text = "Attendance has already been Marked at this time";
                else
                    error_box.text = ex.Message;
                error_box.Show();
            }
            Attendance_Clear();
        }

        private void Attendance_Clear()
        {
            Lecture_note.ResetText();
            Lec_start_time.ResetText();
            Lec_end_time.ResetText();
            P_radio.Checked = false;
        }

        private void Update_Attendance_btn_Click(object sender, EventArgs e)
        {
            up_std_id.ResetText();
            Lecture_date_drop.ResetText();
            Lec_date_text.ResetText();
            lec_time.ResetText();
            Atte_drop.ResetText();
            Transition.ShowSync(Update_Att_Panel);
        }

        private void Lecture_date_drop_ValueChanged(object sender, EventArgs e)
        {
            Lec_date_text.Text = Lecture_date_drop.Text;
        }

        private void Update_Att_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Update_Att_Panel);
        }

        private void Update_the_Att_Click(object sender, EventArgs e)
        {
            if(Class_Name.Text == "Theory")
                updateAttandance("Theory");
            else if(Class_Name.Text == "Lab")
                updateAttandance("Lab");
            else
            {
                error_box = new ErrorBox();
                error_box.text = "Please enter class name";
                error_box.Show();
            }
                Transition.HideSync(Update_Att_Panel);
        }

        
        private void updateAttandance(string lec)
        {
            try
            {
                faculty.Update_Attendance( lec,faculty.Assigned_course, up_std_id.Text, Lec_date_text.Text, lec_time.Text, Atte_drop.Text);
                
                success_box = new SuccessBox();
                success_box.text = "Updated Successfully";
                success_box.Show();

            }
            catch (SqlException ex)
            {
                Connection.Connection.con.Close();
                error_box = new ErrorBox();
                if (ex.Number == 2627)
                    error_box.text = "Attendance has already been Updated";
                else
                    error_box.text = ex.Message;
                error_box.Show();
            }
        }

        private void selectDeliverableName(BunifuDropdown deliverableNameDropdown)
        {
            deliverableNameDropdown.Items.Clear();
            if (Marks_Class_name.Text == "Theory")
            {

                deliverableNameDropdown.Items.AddRange(
                    new String[]
                    {
                        "Assignments","Quizes","Mid Exam","Final Exam"
                    }
                );
                    
            }
            else if (Marks_Class_name.Text == "Lab")
            {

                deliverableNameDropdown.Items.AddRange(
                       new String[]
                        {
                            "Lab Assignments","Lab Mid Exam","Lab Final Exam"
                        }

                   );
            }

        }

        
        private void selectDeliverable(BunifuDropdown deliverableNameDropdown,BunifuDropdown deliverableNoDropdown,string forWhich)
        {
            deliverableNoDropdown.Items.Clear();
            Connection.Connection.con.Open();
            string deliverable = null;
            if (deliverableNameDropdown.Text == "Assignments")
            {
                deliverable = "Assignments";
            }
            else if (deliverableNameDropdown.Text == "Quizes")
            {
                deliverable = "Quizes";
            }
            else if (deliverableNameDropdown.Text == "Mid Exam")
            {
                deliverable = "Mid";
                deliverableNoDropdown.Items.Add("MID-Term Exam");
                Connection.Connection.con.Close();
                return;

            }
            else if (deliverableNameDropdown.Text == "Final Exam")
            {
                deliverable = "Final";
                deliverableNoDropdown.Items.Add("Final Exam");
                Connection.Connection.con.Close();
                return;
            }
            else if (deliverableNameDropdown.Text == "Lab Assignments")
            {
                deliverable = "Lab_Assignments";
            }
            else if (deliverableNameDropdown.Text == "Lab Mid Exam")
            {
                deliverable = "Lab_Mid";
                deliverableNoDropdown.Items.Add("Lab MID-Term Exam");
                Connection.Connection.con.Close();
                return;

            }
            else if (deliverableNameDropdown.Text == "Lab Final Exam")
            {
                deliverable = "Lab_Final";
                deliverableNoDropdown.Items.Add("Lab Final Exam");
                Connection.Connection.con.Close();
                return;

            }
            else
            {
                Connection.Connection.con.Close();
                error_box = new ErrorBox();
                error_box.text = "Please select deliverable name";
                error_box.Show();
                return;
            }
            String query = null;
            if(forWhich == "insert")
                query = "select " + deliverable + " from Whole_Semester_Deliverables where " + deliverable + " not in (select " + deliverable + "_Marks_Id from " + deliverable + "_Marks where Course_Code = '"+faculty.Assigned_course+"')";
            else
                query = "select " + deliverable + " from Whole_Semester_Deliverables";

            SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                deliverableNoDropdown.Items.Add(reader[deliverable].ToString());

            }
            Connection.Connection.con.Close();
        }
        private void Deliverable_name_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            selectDeliverable(Deliverable_name_drop,deliverable_no_drop,"insert");
        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {
            
            showStudentList("Marks");
            Update_marks_panel.Hide();

            Faculty_pages.SetPage(3);
        }
        private void Mark_Sheet_Clear()
        {
            Deliverable_name_drop.Text = "Which Deliverable";
            deliverable_no_drop.Text = "Deliverable No.";
            Total_Marks.ResetText();
            for(int i = 0;i < Marks_sheet.Rows.Count;i++)
            {
                textBox[i].ResetText();
            }
        }
        private void Submit_the_Marks_Click(object sender, EventArgs e)
        {
            if (Marks_Class_name.Text == "Theory" || Marks_Class_name.Text == "Lab")
                submitTheMarks();
            
            else
            {
                error_box = new ErrorBox();
                error_box.text = "Please enter class name";
                error_box.Show();
            }
        }

        private void submitTheMarks()
        {
            try
            {
                float obtained_Marks = 0, total_Marks = float.Parse(Total_Marks.Text);
                int flag = 0;
                for(int i = 0; i < Marks_sheet.Rows.Count; i++)
                {
                    if (float.Parse(textBox[i].Text) < 0 || float.Parse(textBox[i].Text) > total_Marks)
                    {
                        flag = 1;
                        break;
                    }
                    

                }
                if (flag == 1)
                {
                    error_box = new ErrorBox();
                    error_box.text = "You have enter invalid obtained marks";
                    error_box.Show();
                    return;
                }

                if (Deliverable_name_drop.Text == "Which Deliverable" || deliverable_no_drop.Text == "Deliverable No.")
                {
                    error_box = new ErrorBox();
                    error_box.text = "Please enter Deliverable Name or Deliverable No.";
                    error_box.Show();
                    return;
                }
                for (int i = 0; i < Marks_sheet.Rows.Count; i++)
                {
                    string student_id = Marks_sheet.Rows[i].Cells[0].Value.ToString();
                    obtained_Marks = float.Parse(textBox[i].Text);
                    faculty.UploadMarks(faculty.Assigned_course, student_id, Deliverable_name_drop.Text, deliverable_no_drop.Text, obtained_Marks, total_Marks);

                }
                success_box = new SuccessBox();
                success_box.text = "Marks has been Uploaded";
                success_box.Show();
            }
            catch (SqlException ex)
            {

                Connection.Connection.con.Close();
                error_box = new ErrorBox();
                if (ex.Number == 2627)
                    error_box.text = "Marks has already been submited of this deliverable";
                else
                    error_box.text = ex.Message;
                error_box.Show();
            }
            Mark_Sheet_Clear();
        }
        private void Marks_Class_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectDeliverableName(Deliverable_name_drop);
        }

        private void update_marks_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Update_marks_panel);
        }

        private void marksPanelClear()
        {
            ud_marks_std_id.ResetText();
            up_deliv_name.Text = "Which Deliverable";
            up_deli_no.Text = "Deliverable No.";
            up_obtained_marks.ResetText();
            up_total_marks.Text = "Total Marks";
        }
        private void show_the_update_marks_panel_Click(object sender, EventArgs e)
        {
            marksPanelClear();
            Transition.ShowSync(Update_marks_panel);
            selectDeliverableName(up_deliv_name);

        }

        

        private void update_the_marks_Click(object sender, EventArgs e)
        {
            getSetUpdateMarks("set");
        }

        private void up_deliv_name_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectDeliverable(up_deliv_name, up_deli_no, "update");
            
        }

        private void getSetUpdateMarks(string get_set)
        {
            string deliverable = up_deliv_name.Text;
            if (up_deliv_name.Text == "Mid Exam")
                deliverable = "Mid";
            else if(up_deliv_name.Text == "Final Exam")
                deliverable = "Final";
            else if (up_deliv_name.Text == "Lab Assignments")
                deliverable = "Lab_Assignments";
            else if (up_deliv_name.Text == "Lab Mid Exam")
                deliverable = "Lab_Mid";
            else if (up_deliv_name.Text == "Lab final Exam")
                deliverable = "Lab_final";
            
            
            string query = null;
            if (get_set == "get")
            {
                Connection.Connection.con.Open();
                query = "select " + deliverable + "_total_Marks from " + deliverable + "_Marks where " + deliverable + "_Marks_Id = '" + up_deli_no.Text + "' and Course_Code = '" + faculty.Assigned_course + "'";
                SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                    up_total_marks.Text = reader[deliverable + "_total_Marks"].ToString();
                Connection.Connection.con.Close();
            }
            else
            {
                float obtained = float.Parse(up_obtained_marks.Text);
                if(up_deliv_name.Text == "Which Deliverable" || up_deli_no.Text == "Deliverable No.")
                {
                    error_box = new ErrorBox();
                    error_box.text = "Please select deliverable";
                    error_box.Show();
                    return;
                }
                else if(obtained > float.Parse(up_total_marks.Text))
                {
                    error_box = new ErrorBox();
                    error_box.text = "Enter Valid Marks";
                    error_box.Show();
                    return;
                }
                else
                {
                    try
                    {
                        Connection.Connection.con.Open();
                        query = "update " + deliverable + "_Marks set " + deliverable + "_Marks = " + obtained + " where Std_ID = '" + ud_marks_std_id.Text + "' AND Course_Code = '" + faculty.Assigned_course + "' and  " + deliverable + "_Marks_Id = '" + up_deli_no.Text + "'";
                        SqlCommand cmd = new SqlCommand(query, Connection.Connection.con);
                        cmd.ExecuteNonQuery();
                        Transition.HideSync(Update_marks_panel);
                        success_box = new SuccessBox();
                        success_box.text = "Updated Successfully";
                        success_box.Show();
                        Connection.Connection.con.Close();

                    }
                    catch(SqlException ex)
                    {
                        error_box = new ErrorBox();
                        error_box.text = ex.Message;
                        error_box.Show();
                    }
                 }
               
            }
        }

        private void up_deli_no_SelectedIndexChanged(object sender, EventArgs e)
        {
            getSetUpdateMarks("get");
        }

        private void Faculty_Logout_Click(object sender, EventArgs e)
        {
            new LogIn_Interface().Show();
            this.Hide();

        }

        private void Admin_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Admin_home_Click(object sender, EventArgs e)
        {
            Faculty_pages.SetPage(0);
        }

        private void faculty_Profile_Click(object sender, EventArgs e)
        {
            Faculty_pages.SetPage(4);
        }
    }
}
