using CuOnline_Portal.Admin;
using CuOnline_Portal.src.Boxes;
using CuOnline_Portal.src.Faculty_Dashboard;
using CuOnline_Portal.src.Student_Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuOnline_Portal
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Splash splash = new Splash();
            //LogIn_Interface log = new LogIn_Interface();
            //SuccessBox box = new SuccessBox();
            //Admin_Dashboard admin = new Admin_Dashboard();
            //Student_Dashboard std = new Student_Dashboard("FA20-BSE-084");*/
            //Faculty_Dashboard faculty = new Faculty_Dashboard("CS-13");
            Application.Run(splash);
            
            


            


        }
   
    }
}
