using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuOnline_Portal.src.Users
{
    internal class Users
    {
        private string user_id;
        private string user_password;
        private string user_name;
        private string user_DOB;
        private string user_official_email;
        private string user_personal_email;
        private byte[] user_image;
        private string user_address;
        private string user_age;
        private string user_gender;
        private string user_phoneNo;

        public string User_id { get => user_id; set => user_id = value; }
        public string User_password { get => user_password; set => user_password = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public string User_DOB { get => user_DOB; set => user_DOB = value; }
        public string User_official_email { get => user_official_email; set => user_official_email = value; }
        public byte[] User_image { get => user_image; set => user_image = value; }
        public string User_address { get => user_address; set => user_address = value; }
        public string User_phoneNo { get => user_phoneNo; set => user_phoneNo = value; }
        public string User_personal_email { get => user_personal_email; set => user_personal_email = value; }
        public string User_age { get => user_age; set => user_age = value; }
        public string User_gender { get => user_gender; set => user_gender = value; }

        public void viewProfile(Users user)
        {}

    }
}
