using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using stoko_class_BLL;

namespace stoko_db_BLL {
    public static class DbProduct {
        public static List<Product> GetProducts() {
            List<Product> products = new List<Product>();

            String sql = "SELECT p.id_product, p.product_title, p.unit_price_HT, p.reference, p.quantity, " +
                "IFNULL(" +
                    "p.quantity+(SELECT SUM(quantity) as qteCommanded " +
                    "FROM command_content, command " +
                    "WHERE command.id_command = command_content.id_command AND command.status = 0 AND command_content.id_product = p.id_product), " +
                    "p.quantity) as qteStock, " +
                "p.description, c.title_category, c.id_category " +
                "FROM product as p, category as c " +
                "WHERE p.id_category = c.id_category";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            MySqlDataReader res = req.ExecuteReader();
            
            if (res.HasRows) {
                while (res.Read()) {
                    products.Add(
                        new Product(
                            res.GetInt32("id_product"),
                            res.GetString("product_title"),
                            res.GetInt32("unit_price_HT"),
                            res.GetString("reference"),
                            res.GetInt32("quantity"),
                            res.GetInt32("qteStock"),
                            res.GetString("description"),
                            new Category(res.GetInt32("id_category"), res.GetString("title_category"))
                            )
                        );
                }
            }

            res.Close();

            return products;
        }

        public static List<Category> GetCategories() {
            List<Category> categories = new List<Category>();

            String sql = "SELECT id_category, title_category FROM category";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            MySqlDataReader res = req.ExecuteReader();

            if (res.HasRows) {
                while (res.Read()) {
                    categories.Add(
                        new Category(
                            res.GetInt32("id_category"),
                            res.GetString("title_category")
                            )
                        );
                }
            }

            res.Close();

            return categories;
        }
    }
}
