using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CuOnline_Portal.src.Connection
{
    public class Connection
    {
        public static String dbConnect = "Data Source=DESKTOP-O5A4DC9\\SQLEXPRESS;Initial Catalog=COMSATS;Integrated Security=True";
        public static SqlConnection con = new SqlConnection(dbConnect);
    }
}
