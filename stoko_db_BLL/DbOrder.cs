using MySql.Data.MySqlClient;
using stoko_class_BLL;
using System;
using System.Collections.Generic;

namespace stoko_db_BLL {
    public static class DbOrder {
        public static List<Order> GetOrders() {
            List<Order> Orders = new List<Order>();

            /*sql reqest for get order infos */
            String sql = "SELECT o.id_command, o.id_client, o.shipping, o.total_HT, o.total_HT + o.tax_on_command as total_TTC, o.date, o.status, " +
                /* part for get client name */
                "(SELECT " +
                    "CASE " +
                        "WHEN NOT ISNULL(c.last_name) AND NOT ISNULL(c.first_name) THEN CONCAT(c.last_name, \" \", c.first_name) " +
                        "WHEN NOT ISNULL(c.last_name) AND ISNULL(c.first_name) THEN CONCAT(c.last_name, \" (\", c.login, \")\") " +
                        "WHEN NOT ISNULL(c.first_name) AND ISNULL(c.last_name) THEN CONCAT(c.first_name, \" (\", c.login, \")\") " +
                        "WHEN ISNULL(c.last_name) and ISNULL(c.first_name) THEN c.login " +
                        "ELSE CONCAT(c.last_name, \" \", c.first_name) " +
                    "END " +
                    "FROM client as c WHERE c.id_client = o.id_client " +
                ") as client_name, " +

                /* part for get address */
                "a.id_address, a.way, a.complement, a.zip_code, a.city " +
                /* and of request */
                "FROM command as o LEFT JOIN address as a ON a.id_address = o.id_address_delivery";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            MySqlDataReader res = req.ExecuteReader();

            if (res.HasRows) {
                while (res.Read()) {
                    Orders.Add(
                        new Order(
                            res.GetInt32("id_command"),
                            res.IsDBNull(1) ? -1 : res.GetInt32("id_client"),
                            res.GetInt32("shipping"),
                            res.GetInt32("total_HT"),
                            res.GetInt32("total_TTC"),
                            res.GetDateTime("date"),
                            res.GetInt32("status"),
                            res.IsDBNull(7) ? null : res.GetString("client_name"),
                            res.IsDBNull(8) ? new Address(-1) : new Address(
                                res.GetInt32("id_address"),
                                res.GetString("way"),
                                res.IsDBNull(10) ? null : res.GetString("complement"),
                                res.GetString("zip_code"),
                                res.GetString("city")
                                )
                            )
                        );
                }
            }

            res.Close();

            return Orders;
        }

        public static List<OrderContent> GetOrdersContent(Order order) {
            List<OrderContent> OrderContents = new List<OrderContent>();

            String sql = "SELECT c.id_product, p.reference, p.product_title, c.unit_price_HT, c.quantity " +
                "FROM command_content as c LEFT JOIN product as p ON c.id_product = p.id_product " +
                "WHERE c.id_command = @orderId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@orderId", order.Id));

            MySqlDataReader res = req.ExecuteReader();

            if (res.HasRows) {
                while (res.Read()) {
                    OrderContents.Add(
                        new OrderContent(
                            res.IsDBNull(0) ? -1 : res.GetInt32("id_product"),
                            res.IsDBNull(1) ? null : res.GetString("reference"),
                            res.IsDBNull(2) ? null : res.GetString("product_title"),
                            res.GetInt32("unit_price_HT"),
                            res.GetInt32("quantity"),
                            order
                            )
                        );
                }
            }

            res.Close();

            return OrderContents;
        }

        public static void UpdateOrderedAddress(Order order) {

            String sql = "UPDATE command SET id_address_delivery=@addressId WHERE id_command = @orderId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@orderId", order.Id));
            req.Parameters.Add(new MySqlParameter("@addressId", order.Address.Id));

            req.ExecuteNonQuery();
        }

        public static void UpdateShipOrder(Order order) {

            String sql = "UPDATE command SET status=1 WHERE id_command = @orderId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@orderId", order.Id));

            req.ExecuteNonQuery();
        }
    }
}
