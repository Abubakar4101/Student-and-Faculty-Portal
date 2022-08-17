using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuOnline_Portal.src.Courses
{
    internal class Courses
    {
        private String course_code;
        private String course_name;
        private int credit_hours;
        private String course_Depart;


        public String COURSE_CODE
        {
            get { return course_code; }
            set { course_code = value; }
        }
        public String COURSE_NAME
        {
            get { return course_name; }
            set { course_name = value; }
        }
        public int CREDIT_HOURS
        {
            get { return credit_hours; }
            set { credit_hours = value; }
        }
        public String COURSE_DEPART
        {
            get { return course_Depart; }
            set { course_Depart = value; }
        }

    }
}
