using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using stoko_class_BLL;

namespace stoko_db_BLL
{
    public static class Data
    {
        public static string DbURL { get; set; }
        public static MySqlConnection DbConn { get; set; }
        public static String DbConnStatus { get; set; }
        public static String DbConnMsg { get; set; }
        public static List<Product> Products { get; set; }
        public static List<Client> Clients { get; set; }
        public static List<Order> Orders { get; set; }

        public static bool DbConnect() {
            
            DbConn = new MySqlConnection(DbURL);

            try {
                DbConn.Open();

                MessageBox.Show(DbConn.State.ToString());
                DbConnStatus = "sGoodDbConnexion";
                DbConnMsg = "";
                return true;
            } catch (MySqlException uneSqlException) {
                DbConnMsg = uneSqlException.InnerException.Message;
                DbConnStatus = "sErrorAuthDbConnexion";
            } catch (Exception uneException) {
                DbConnMsg = uneException.InnerException.Message;
                DbConnStatus = "sErrorUnknownDbConnexion";
            }
            return false;
        }
    }
}
