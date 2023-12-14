using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLSANBONG
{
    public class UserLoginInfo
    {   
        public static string username { get; set; }
        public static string password { get; set; }
        public static bool isAdmin(string Username)
        {
            return Username == "admin";
        }
    }
}
