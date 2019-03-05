using MySql.Data.MySqlClient;
using stoko_class_BLL;
using System;
using System.Collections.Generic;

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
                "FROM product as p LEFT JOIN category as c ON p.id_category = c.id_category";

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
                            new Category((res.IsDBNull(7) ? -1 : res.GetInt32("id_category")), (res.IsDBNull(8) ? null : res.GetString("title_category")))
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

        public static int CreateProduct(Product product) {
            String sql = "INSERT INTO product(product_title, unit_price_HT, reference, quantity, description, id_category)" +
                "VALUE(@productTitle, @productPrice, @productRef, @productQuantity, @productDes, @productIdCat)";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@productTitle", product.Title));
            req.Parameters.Add(new MySqlParameter("@productPrice", product.PriceHT));
            req.Parameters.Add(new MySqlParameter("@productRef", product.Reference));
            req.Parameters.Add(new MySqlParameter("@productQuantity", product.Quantity));
            req.Parameters.Add(new MySqlParameter("@productDes", product.Description));

            if (product.Category == null || product.Category.Id == -1) {
                req.Parameters.Add(new MySqlParameter("@productIdCat", null));
            } else {
                req.Parameters.Add(new MySqlParameter("@productIdCat", product.Category.Id));
            }

            req.ExecuteNonQuery();
            return int.Parse(req.LastInsertedId.ToString());
        }

        public static void UpdateProduct(Product product) {
            String sql = "UPDATE product SET product_title = @productTitle, unit_price_HT = @productPrice, " +
                "reference = @productRef, quantity = @productQuantity, description = @productDes, id_category = @productIdCat " +
                "WHERE id_product = @productId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@productTitle", product.Title));
            req.Parameters.Add(new MySqlParameter("@productPrice", product.PriceHT));
            req.Parameters.Add(new MySqlParameter("@productRef", product.Reference));
            req.Parameters.Add(new MySqlParameter("@productQuantity", product.Quantity));
            req.Parameters.Add(new MySqlParameter("@productDes", product.Description));

            if (product.Category == null || product.Category.Id == -1) {
                req.Parameters.Add(new MySqlParameter("@productIdCat", null));
            } else {
                req.Parameters.Add(new MySqlParameter("@productIdCat", product.Category.Id));
            }

            req.Parameters.Add(new MySqlParameter("@productId", product.Id));

            req.ExecuteNonQuery();
        }

        public static void DeleteProduct(Product product) {
            String sql = "DELETE FROM product WHERE id_product = @productId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@productId", product.Id));

            req.ExecuteNonQuery();
        }
    }
}
