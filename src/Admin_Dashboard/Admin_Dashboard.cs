using CuOnline_Portal.src.Boxes;
using CuOnline_Portal.src.Connection;
using CuOnline_Portal.src.Courses;
using CuOnline_Portal.src.Users;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace CuOnline_Portal.Admin
{
    public partial class Admin_Dashboard : Form
    {
        ErrorBox error_box = null;
        SuccessBox success_box = null;
        SqlCommand cmd = null;
        Admins admin = new Admins();
        DateTime dateTime = DateTime.Now;
        private int C_Index;
        string imageLocation = "";
        string image_path;
        public Admin_Dashboard()
        {
            InitializeComponent();
        }
        private void showCoursesData()
        {
            Connection.con.Open();
            Added_Courses_Data.Rows.Clear();
            String query = "Select * from Courses";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                C_Index = 1;
                while (reader.Read())
                {
                    Added_Courses_Data.Rows.Add
                    (
                       new Object[]
                       {
                            C_Index,
                            reader["Course_Code"].ToString(),
                            reader["Course_Name"].ToString(),
                            reader["Credit_Hours"].ToString(),
                            reader["Course_Department"].ToString(),
                       }
                    );
                    C_Index++;
                }
            }
            else
                Added_Courses_Data.Hide();


            Connection.con.Close();
        }
        private void showStudentData()
        {
            Connection.con.Open();
            Added_student_data.Rows.Clear();
            String query = "Select * from Student";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Added_student_data.Rows.Add
                    (
                       new Object[]
                       {
                            
                            reader["Std_ID"].ToString(),
                            reader["Std_Name"].ToString(),
                            reader["Std_Gender"].ToString(),
                            reader["Std_DOB"].ToString(),
                            reader["Std_Department"].ToString(),
                            reader["Std_Programme"].ToString(),
                            reader["Std_Address"].ToString(),
                            reader["Std_PhoneNo"].ToString(),
                       }
                    );
                }
            }
            else
                Added_student_data.Hide();


            Connection.con.Close();
        }
        private void showAssignedData()
        {
            Connection.con.Open();
            Assigned_data.Rows.Clear();
            String query = "SELECT Assign_Course.Fac_ID,Faculty.Fac_Name,Assign_Course.Course_Code,Courses.Course_Name FROM ((Assign_Course INNER JOIN Faculty ON Faculty.Fac_ID = Assign_Course.Fac_ID) INNER JOIN Courses ON Assign_Course.Course_Code = Courses.Course_Code);";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Assigned_data.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Fac_ID"].ToString(),
                            reader["Fac_Name"].ToString(),
                            reader["Course_Code"].ToString(),
                            reader["Course_Name"].ToString(),
                       }
                    );
                }
            }
            else
                Assigned_data.Hide();


            Connection.con.Close();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void showingProfile()
        {
            Connection.con.Open();
            admin.viewProfile(admin);
            Connection.con.Close();
            MemoryStream ms = new MemoryStream(admin.User_image);
            Admin_Pic.Image = Image.FromStream(ms);
            Admin_Name.Text = admin.User_name;
            adm_id.Text = admin.User_id;
            adm_name.Text = admin.User_name;
            adm_gender.Text = admin.User_gender;
            adm_dob.Text = admin.User_DOB;
            adm_email.Text = admin.User_official_email;
            adm_address.Text = admin.User_address;
        }
        private void Admin_Dashboard_Load(object sender, EventArgs e)
        {
            DOB_Picker.Size = new Size(200, 36);
            Date.Text = dateTime.ToString("MM/dd/yyyy");
            search_std_panel.Hide();
            drop_student_panel.Hide();
            showingProfile();

            /*for (int i = 0; i <= 5; i++)
             {
                 Added_Courses_Data.Rows.Add
                 (
                     new Object[]
                     {
                         i,
                         $"30{i}",
                         "DataBase",
                         4
                     }
                 );
             }*/



        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Admin_Logout_Click(object sender, EventArgs e)
        {
            new LogIn_Interface().Show();
            this.Hide();

        }

        private void Admin_Courses_Click(object sender, EventArgs e)
        {
            Add_Course_Panel.Hide();
            Drop_Course_Panel.Hide();
            showCoursesData();
            bunifuPages1.SetPage(1);

        }

        private void Admin_home_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(0);
        }
        


        private void Admin_Students_Click(object sender, EventArgs e)
        {
            Register_std_panel.Hide();
            search_std_panel.Hide();
            drop_std_panel.Hide();
            showStudentData();
            bunifuPages1.SetPage(2);
        }

        private void Add_Courses_Click(object sender, EventArgs e)
        {

            Course_Code_Text.Text = "";
            Course_Name_Text.Text = "";
            Credit_Hours_Text.Text = "";
            Course_Department.Text = "";
            Transition.ShowSync(Add_Course_Panel);
            
            /* Courses course = new Courses();
             course.COURSE_CODE = Course_Code_Text.Text;
             course.COURSE_NAME = Course_Name_Text.Text;
             course.CREDIT_HOURS = int.Parse(Credit_Hours_Text.Text);*/
        }

        private void Course_ok_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                Courses course = new Courses();
                course.COURSE_CODE = Course_Code_Text.Text;
                course.COURSE_NAME = Course_Name_Text.Text;
                course.COURSE_DEPART = Course_Department.Text;
                course.CREDIT_HOURS = int.Parse(Credit_Hours_Text.Text);
                cmd = admin.addCourse(course);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showCoursesData();
                Transition.HideSync(Add_Course_Panel);
                success_box = new SuccessBox();
                success_box.text = "Course Added Successfully";
                success_box.Show();
                

            }
            catch (SqlException ex)
            {

                Add_Course_Panel.Hide();    
                if (ex.Number == 2627)
                {
                    error_box = new ErrorBox();
                    error_box.text = "This Course Already Exist";
                    error_box.Show();
                }

                else if (ex.State == 0)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Credit Hours must be 0 to 4";
                    error_box.Show();
                }
                else if (ex.State == 2)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else 
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
                Add_Course_Panel.Hide();
                Connection.con.Close();
            }

        }

        private void Add_Course_Panel_Close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Add_Course_Panel);
            Connection.con.Close();
        }

        private void Drop_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Drop_Course_Panel);
            Connection.con.Close();
        }

        private void Drop_Course_OK_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.dropCourse(Drop_Course_ID.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showCoursesData();
                Transition.HideSync(Drop_Course_Panel);
                success_box = new SuccessBox();
                success_box.text = "Course Dropped Successfully";
                success_box.Show();
                
                

            }
            catch (SqlException ex)
            {

                Drop_Course_Panel.Hide();
                if (ex.State == 2)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                Drop_Course_Panel.Hide();
                Connection.con.Close();
            }
        }

        private void Drop_Course_Click(object sender, EventArgs e)
        {
            Drop_Course_ID.Text = "";
            Transition.ShowSync(Drop_Course_Panel);
        }

        private void DOB_Picker_ValueChanged(object sender, EventArgs e)
        {
            Std_DOB_text.Text = DOB_Picker.Text;
        }

        private void upload_img_button_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        image_path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        student_image.Image = new Bitmap(openFileDialog1.FileName);
                        imageLocation = openFileDialog1.FileName.ToString();
                        student_image.ImageLocation = imageLocation;
                        student_image.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
            catch (Exception ex)
            {
                error_box = new ErrorBox();
                error_box.text = "File already Exist " + ex.Source;
                error_box.Show();
            }
        }

        private void Register_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Register_std_panel);
        }

        private void Register_std_button_Click(object sender, EventArgs e)
        {
           

            std_id_text.Text = "";
            std_password_text.Text = "";
            std_name_text.Text = "";
            std_address_text.Text = "";
            Std_DOB_text.Text = "";
            std_per_email_text.Text = "";
            std_off_email_text.Text = "";
            std_age_text.Text = "";
            std_gender_drop.Text = "";
            std_depart_text.Text = "";
            std_program_text.Text = "";
            std_phoneNo_text.Text = "";
            Program_drop.Items.Clear();
            
            Transition.ShowSync(Register_std_panel);

        }

        
        private void Registered_std_Click(object sender, EventArgs e)
        {
            
            try
            {
                Connection.con.Open();
                Student student = new Student();
                student.User_id = std_id_text.Text;
                student.User_password = std_password_text.Text;
                student.User_name = std_name_text.Text;
                student.User_address = std_address_text.Text;
                student.User_DOB = Std_DOB_text.Text;
                student.User_personal_email = std_per_email_text.Text;
                student.User_official_email = std_off_email_text.Text;
                student.User_age = std_age_text.Text;
                student.User_gender = std_gender_drop.Text;
                student.Student_department = std_depart_text.Text;
                student.Student_programme = std_program_text.Text;
                student.User_phoneNo = std_phoneNo_text.Text;
                Image temp = new Bitmap(image_path);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                student.User_image = ms.ToArray();
                cmd = admin.registerStudent(student);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showStudentData();
                Transition.HideSync(Register_std_panel);
                sendConfirmationMail("Student", student.User_id, student.User_name, student.User_password, student.User_personal_email, student.Student_programme);
                success_box = new SuccessBox();
                success_box.text = "Student Registered Successfully";
                success_box.Show();


            }
            catch (SqlException ex)
            {

                Register_std_panel.Hide();
               
                if (ex.Number == 2627)
                {
                    error_box = new ErrorBox();
                    error_box.text = "This student Already Exist";
                    error_box.Show();
                }
                else if (ex.Number == 515)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
               
                Connection.con.Close();
            }
            

        }

        
        private void sendConfirmationMail(string person,string id, string name, string pass, string mailAddress, string applied)
        {
            MailMessage mail = new MailMessage()
            {
                BodyEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.Default
            };
            mail.To.Add(mailAddress);
            mail.From = new MailAddress("cuonline.portal@gmail.com");
            mail.Subject = "COMSATS Registration Confirmation";
            if(person == "Student")
                mail.Body = "<b>Dear " + name + ",<b><br><br>You are recently applied for admission in COMSATS Lahore. So, your application has been approved.<br>Now, you are enrolled in " + applied + " Programme.<br><br>Your CuOnline Credentials are: <br><br>LogIn ID: " + id + "<br>Password: " + pass + "<b><br><br>Regards<br><b>CuOnline Team<b>";
            else
                mail.Body = "<b>Dear " + name + ",<b><br><br>You are recently applied for lecturer in COMSATS Lahore. So, your application has been approved.<br>Now, you are posted at " + applied + " designation.<br><br>Your CuOnline Credentials are: <br><br>LogIn ID: " + id + "<br>Password: " + pass + "<b><br><br>Regards<br><b>CuOnline Team<b>";

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.EnableSsl = true;
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential("cuonline.portal@gmail.com", "qmprkvplpspngfkh");
            smtp.Send(mail);
        }
        private void Search_std_button_Click(object sender, EventArgs e)
        {
            search_std_id.Text = "";
            Transition.ShowSync(search_std_panel);
        }

        private void std_search_ok_Click(object sender, EventArgs e)
        {
            Connection.con.Open();
            Added_student_data.Rows.Clear();
            String query = "Select * from Student where Std_ID = '" + search_std_id.Text + "'";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Added_student_data.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Std_ID"].ToString(),
                            reader["Std_Name"].ToString(),
                            reader["Std_Gender"].ToString(),
                            reader["Std_DOB"].ToString(),
                            reader["Std_Department"].ToString(),
                            reader["Std_Programme"].ToString(),
                            reader["Std_Address"].ToString(),
                            reader["Std_PhoneNo"].ToString(),
                       }
                    );
                }
            }
            else
            {
                error_box = new ErrorBox();
                error_box.text = "Sorry, No such Student is registered";
                error_box.Show();
            }
            Transition.HideSync(search_std_panel);
            Connection.con.Close();
        }

        private void search_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(search_std_panel);
        }

        private void drop_student_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(drop_student_panel);
        }
        private void Drop_the_std_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.dropStudent(drop_studentid.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showStudentData();
                Transition.HideSync(drop_student_panel);
                success_box = new SuccessBox();
                success_box.text = "Student Dropped Successfully";
                success_box.Show();

            }
            catch (SqlException ex)
            {

                if (ex.State == 2)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
                drop_student_panel.Hide();
                Connection.con.Close();
            }
        }

        private void drop_std_button_Click(object sender, EventArgs e)
        {
            drop_studentid.Text = "";
            Transition.ShowSync(drop_student_panel);
        }

        private void Admin_Teachers_Click(object sender, EventArgs e)
        {
            search_faculty_panel.Hide();
            drop_faculty_panel.Hide();
            register_faculty_panel.Hide();
            showFacultyData();
            bunifuPages1.SetPage(3);
        }

        private void search_faculty_panel_close_Click_1(object sender, EventArgs e)
        {
            Transition.HideSync(search_faculty_panel);
        }

        private void drop_faculty_panel_close_Click_1(object sender, EventArgs e)
        {
            Transition.HideSync(drop_faculty_panel);
        }

        private void register_faculty_btn_Click(object sender, EventArgs e)
        {
            faculty_id_text.Text = "";
            faculty_password_text.Text = "";
            facult_name_text.Text = "";
            faculty_address_text.Text = "";
            faculty_dob.Text = "";
            faculty_personal_text.Text = "";
            faculty_official_text.Text = "";
            faculty_age_text.Text = "";
            faculty_gender_text.Text = "";
            faculty_edu_text.Text = "";
            faculty_design_text.Text = "";
            faculty_phone_text.Text = "";
            faculty_address_text.Text = "";
            Transition.ShowSync(register_faculty_panel);
        }

        private void register_faculty_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(register_faculty_panel);
        }

        private void search_faculty_btn_Click(object sender, EventArgs e)
        {
            Transition.ShowSync(search_faculty_panel);
        }

        private void drop_faculty_btn_Click(object sender, EventArgs e)
        {
            Transition.ShowSync(drop_faculty_panel);
        }

        private void std_gender_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            std_gender_text.Text = std_gender_drop.Text;
        }

        private void department_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            std_depart_text.Text = department_drop.Text;
            fetchPrograms(std_depart_text.Text);
        }

        private void fetchPrograms(string department)
        {
            Program_drop.Items.Clear();
            Connection.con.Open();
            String query = "select program from programs where department = '" + department + "'";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Program_drop.Items.Add(reader["program"].ToString());

            }
            Connection.con.Close();
        }

        private void Program_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            std_program_text.Text = Program_drop.Text;
        }

        

        private void c_depart_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Course_Department.Text = c_depart_drop.Text;

        }

        private void credit_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Credit_Hours_Text.Text = credit_drop.Text;
        }

        private void assign_course_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Assign_course_panel);
        }

        private void Assign_Course_Click(object sender, EventArgs e)
        {
            Assign_course_panel.Hide();
            drop_assigned_course_panel.Hide();
            showAssignedData();
            bunifuPages1.SetPage(4);
        }

        private void Available_courses_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Available_courses.Text = Available_courses_drop.Text;
        }

        private void assigning_btn_Click(object sender, EventArgs e)
        {
            assign_facultyid.Text = "";
            Available_courses.Text = "";
            Available_courses_drop.Items.Clear();
            facid_drop.Items.Clear();
            Connection.con.Open();
            String query = "select Course_Code from Courses where Course_Code not in (select Course_Code from Assign_Course)";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                Available_courses_drop.Items.Add(reader["Course_Code"].ToString());

            }
            Connection.con.Close();
            Connection.con.Open();
            String query2 = "select Fac_ID from Faculty where Fac_ID not in (select Fac_ID from Assign_Course)";
            cmd = new SqlCommand(query2, Connection.con);
            SqlDataReader reader2 = cmd.ExecuteReader();
            while (reader2.Read())
            {

                facid_drop.Items.Add(reader2["Fac_ID"].ToString());

            }


            Transition.ShowSync(Assign_course_panel);
            Connection.con.Close();
        }

        private void Assigned_button_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.assignCourseToFaculty(assign_facultyid.Text,Available_courses.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showAssignedData();
                Transition.HideSync(Assign_course_panel);
                success_box = new SuccessBox();
                success_box.text = "Course Successfully assigned";
                success_box.Show();


            }
            catch (SqlException ex)
            {

                Assign_course_panel.Hide();

                if (ex.Number == 2627)
                {
                    error_box = new ErrorBox();
                    error_box.text = "This course Already assigned";
                    error_box.Show();
                }
                else if (ex.Number == 547)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Sorry, Faculty ID id incorrect";
                    error_box.Show();
                }
                else if (ex.Number == 515)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }

                Connection.con.Close();
            }
        }

        private void registered_faculty_data_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fac_gender_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            faculty_gender_text.Text = fac_gender_drop.Text;
        }

        private void upload_faulty_image_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "C://Desktop";
            openFileDialog1.Title = "Select image to be upload.";
            openFileDialog1.Filter = "Image Only(*.jpg; *.jpeg; *.gif; *.bmp; *.png)|*.jpg; *.jpeg; *.gif; *.bmp; *.png";
            openFileDialog1.FilterIndex = 1;
            try
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (openFileDialog1.CheckFileExists)
                    {
                        image_path = System.IO.Path.GetFullPath(openFileDialog1.FileName);
                        student_image.Image = new Bitmap(openFileDialog1.FileName);
                        imageLocation = openFileDialog1.FileName.ToString();
                        faculty_img.ImageLocation = imageLocation;
                        faculty_img.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                }
            }
            catch (Exception ex)
            {
                error_box = new ErrorBox();
                error_box.text = "File already Exist " + ex.Source;
                error_box.Show();
            }
        }

        private void faculty_dob_picker_ValueChanged(object sender, EventArgs e)
        {
            faculty_dob.Text = faculty_dob_picker.Text;
        }

        private void register_faculty_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                Faculty faculty = new Faculty();
                faculty.User_id = faculty_id_text.Text;
                faculty.User_password = faculty_password_text.Text;
                faculty.User_name = facult_name_text.Text;
                faculty.User_address = faculty_address_text.Text;
                faculty.User_DOB = faculty_dob.Text;
                faculty.User_personal_email = faculty_personal_text.Text;
                faculty.User_official_email = faculty_official_text.Text;
                faculty.User_age = faculty_age_text.Text;
                faculty.User_gender = fac_gender_drop.Text;
                faculty.Faculty_designation = faculty_design_text.Text;
                faculty.Faculty_education = faculty_edu_text.Text;
                faculty.User_phoneNo = faculty_phone_text.Text;
                Image temp = new Bitmap(image_path);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                faculty.User_image = ms.ToArray();
                cmd = admin.registerFaculty(faculty);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showFacultyData();
                Transition.HideSync(register_faculty_panel);
                sendConfirmationMail("Faculty", faculty.User_id, faculty.User_name, faculty.User_password, faculty.User_personal_email, faculty.Faculty_designation);
                success_box = new SuccessBox();
                success_box.text = "Faculty Registered Successfully";
                success_box.Show();



            }
            catch (SqlException ex)
            {

                Register_std_panel.Hide();

                if (ex.Number == 2627)
                {
                    error_box = new ErrorBox();
                    error_box.text = "This student Already Exist";
                    error_box.Show();
                }
                else if (ex.Number == 515)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }


                Connection.con.Close();
            }
        }

        private void showFacultyData()
        {
            Connection.con.Open();
            registered_faculty_data.Rows.Clear();
            String query = "Select * from Faculty";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    registered_faculty_data.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Fac_ID"].ToString(),
                            reader["Fac_Name"].ToString(),
                            reader["Fac_Gender"].ToString(),
                            reader["Fac_DOB"].ToString(),
                            reader["Fac_Education"].ToString(),
                            reader["Fac_designation"].ToString(),
                            reader["Fac_Address"].ToString(),
                            reader["Fac_PhoneNo"].ToString(),
                       }
                    );
                }
            }
            else
                registered_faculty_data.Hide();


            Connection.con.Close();
        }

        private void search_faculty_ok_Click(object sender, EventArgs e)
        {
            Connection.con.Open();
            registered_faculty_data.Rows.Clear();
            String query = "Select * from Faculty where Fac_ID = '" + search_faculty_id.Text + "'";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    registered_faculty_data.Rows.Add
                    (
                       new Object[]
                       {

                            reader["Fac_ID"].ToString(),
                            reader["Fac_Name"].ToString(),
                            reader["Fac_Gender"].ToString(),
                            reader["Fac_DOB"].ToString(),
                            reader["Fac_Education"].ToString(),
                            reader["Fac_designation"].ToString(),
                            reader["Fac_Address"].ToString(),
                            reader["Fac_PhoneNo"].ToString(),
                       }
                    );
                }
            }
            else
            {
                error_box = new ErrorBox();
                error_box.text = "Sorry, No such Faculty is registered";
                error_box.Show();
            }
            Transition.HideSync(search_faculty_panel);
            Connection.con.Close();
        }

        private void drop_faculty_ok_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.dropFaculty(drop_faculty_id.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showFacultyData();
                Transition.HideSync(drop_faculty_panel);
                success_box = new SuccessBox();
                success_box.text = "Faculty Dropped Successfully";
                success_box.Show();

            }
            catch (SqlException ex)
            {

                if (ex.State == 2)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
                drop_faculty_panel.Hide();
                Connection.con.Close();
            }
        }

        private void drop_assign_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(drop_assigned_course_panel);
        }

        private void drop_assign_course_Click(object sender, EventArgs e)
        {
            drop_faculty_id.Text = "";
            drop_facid_drop.Items.Clear();
            Connection.con.Open();
            String query = "select Fac_ID from Assign_Course";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                drop_facid_drop.Items.Add(reader["Fac_ID"].ToString());

            }
            Connection.con.Close();
            Transition.ShowSync(drop_assigned_course_panel);
        }

        private void d_assigned_facid_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.dropAssignedCourse(d_assign_facid.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showAssignedData();
                Transition.HideSync(drop_assigned_course_panel);
                success_box = new SuccessBox();
                success_box.text = "Assigned Course Dropped Successfully";
                success_box.Show();

            }
            catch (SqlException ex)
            {

                if (ex.State == 2)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
                drop_assigned_course_panel.Hide();
                Connection.con.Close();
            }
        }

        private void drop_facid_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            d_assign_facid.Text = drop_facid_drop.Text;
        }

        private void facid_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            assign_facultyid.Text = facid_drop.Text;
        }

        private void Std_C_Assig_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(Std_Assign_C_Panel);
        }

        private void assign_course_to_std_Click(object sender, EventArgs e)
        {
            Std_Assign_C_Panel.Hide();
            std_Assigned_data.Hide();
            View_enrolled_panel.Hide();
            drop_s_assign_panel.Hide();
            Enrolled_label.Hide();
            bunifuPages1.SetPage(5);

        }

        private void aassign_course_code_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            Assign_course_code.Text = aassign_course_code_drop.Text;
        }

        private void assigning_std_course_btn_Click(object sender, EventArgs e)
        {
            Assign_course_code.Text = "";
            assign_std_id.Text = "";
            std_Assigned_data.Hide();
            Enrolled_label.Hide();
            aassign_course_code_drop.Items.Clear();
            Connection.con.Open();
            String query = "select Course_Code from Courses";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                aassign_course_code_drop.Items.Add(reader["Course_Code"].ToString());

            }
            Transition.ShowSync(Std_Assign_C_Panel);
            Connection.con.Close();
        }

        private void assigned_course_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.assignCourseToStudent(Assign_course_code.Text, assign_std_id.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                Transition.HideSync(Std_Assign_C_Panel);
                success_box = new SuccessBox();
                success_box.text = "Course Successfully assigned";
                success_box.Show();


            }
            catch (SqlException ex)
            {

                Assign_course_panel.Hide();

                if (ex.Number == 2627)
                {
                    error_box = new ErrorBox();
                    error_box.text = "This course Already assigned";
                    error_box.Show();
                }
                else if (ex.Number == 547)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Sorry, Student ID is incorrect";
                    error_box.Show();
                }
                else if (ex.Number == 515)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }

                Connection.con.Close();
            }
        }

        private void drop_s_assign_course_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(drop_s_assign_panel);
        }

       

        private void drop_s_assign_courseid_drop_SelectedIndexChanged(object sender, EventArgs e)
        {
            drop_s_assign_courseid.Text = drop_s_assign_courseid_drop.Text;
        }

        private void drop_s_course_btn_Click(object sender, EventArgs e)
        {
            drop_s_assign_courseid.Text = "";
            drop_s_assign_stdid.Text = "";
            std_Assigned_data.Hide();
            Enrolled_label.Hide();
            drop_s_assign_courseid_drop.Items.Clear();
            Connection.con.Open();
            String query = "select Course_Code from Courses";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                drop_s_assign_courseid_drop.Items.Add(reader["Course_Code"].ToString());

            }
            Transition.ShowSync(drop_s_assign_panel);
            Connection.con.Close();
        }

        private void drop_s_assigned_btn_Click(object sender, EventArgs e)
        {
            try
            {
                Connection.con.Open();
                cmd = admin.dropStudentAssignedCourse(drop_s_assign_courseid.Text, drop_s_assign_stdid.Text);
                cmd.ExecuteNonQuery();
                Connection.con.Close();
                showAssignedData();
                Transition.HideSync(drop_s_assign_panel);
                success_box = new SuccessBox();
                success_box.text = "Assigned Course Dropped Successfully";
                success_box.Show();

            }
            catch (SqlException ex)
            {

                if (ex.State == 2)
                {
                    error_box = new ErrorBox();
                    error_box.text = "Something is missing";
                    error_box.Show();
                }
                else
                {
                    error_box = new ErrorBox();
                    error_box.text = ex.Message;
                    error_box.Show();
                }
                drop_s_assign_panel.Hide();
                Connection.con.Close();
            }
        }

        private void View_enrolled_panel_close_Click(object sender, EventArgs e)
        {
            Transition.HideSync(View_enrolled_panel);
        }
        
        private void show_view_panel_Click(object sender, EventArgs e)
        {
            view_srdid.Text = "";
            std_Assigned_data.Hide();
            Enrolled_label.Hide();
            Transition.ShowSync(View_enrolled_panel);
        }

        private void showAssignedCoursesData(string StudentID)
        {
            Connection.con.Open();
            std_Assigned_data.Rows.Clear();
            //Enrolled_label.Text = StudentID + " enrolled in: ";
            String query = "select Student_Registration.Std_ID, Student_Registration.Course_Code, Courses.Course_Name, Courses.Credit_Hours,Courses.Course_Department from Student_Registration inner join Courses on Student_Registration.Course_Code = Courses.Course_Code where Std_ID = '" + StudentID + "'";
            cmd = new SqlCommand(query, Connection.con);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                C_Index = 1;
                
                while (reader.Read())
                {
                    Enrolled_label.Text = reader["Std_ID"].ToString() + " enrolled in: ";
                    std_Assigned_data.Rows.Add
                    (
                       new Object[]
                       {
                            C_Index,
                            
                            reader["Course_Code"].ToString(),
                            reader["Course_Name"].ToString(),
                            reader["Credit_Hours"].ToString(),
                            reader["Course_Department"].ToString(),
                       }
                    );
                    C_Index++;
                }
                std_Assigned_data.Show();
                Enrolled_label.Show();
            }
            else
                std_Assigned_data.Hide();


            Connection.con.Close();
        }
        private void View_course_list_btn_Click(object sender, EventArgs e)
        {
            showAssignedCoursesData(view_srdid.Text);
            Transition.HideSync(View_enrolled_panel);
        }

        private void Admin_Profile_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(6);

        }

        private void Admin_Exit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
