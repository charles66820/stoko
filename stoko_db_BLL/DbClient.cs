using MySql.Data.MySqlClient;
using stoko_class_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stoko_db_BLL {
    public static class DbClient {
        public static List<Client> GetClients() {
            List<Client> Clients = new List<Client>();

            String sql = "SELECT c.id_client, c.login, c.email, c.last_name, c.first_name, c.phone_number, c.avatar_url, c.creation_date FROM client as c";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            MySqlDataReader res = req.ExecuteReader();

            if (res.HasRows) {
                while (res.Read()) {
                    Clients.Add(
                        new Client(
                            res.GetInt32("id_client"),
                            res.GetString("login"),
                            res.GetString("email"),
                            res.IsDBNull(3) ? null : res.GetString("last_name"),
                            res.IsDBNull(4) ? null : res.GetString("first_name"),
                            res.IsDBNull(5) ? null : res.GetString("phone_number"),
                            res.IsDBNull(6) ? null : res.GetString("avatar_url"),
                            res.GetDateTime("creation_date")
                            )
                        );
                }
            }

            res.Close();

            return Clients;
        }

        public static List<Address> GetAddresses(Client client) {
            List<Address> Addresses = new List<Address>();

            String sql = "SELECT a.id_address, a.way, a.complement, a.zip_code, a.city FROM address as a WHERE a.id_client = @clientId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@clientId", client.Id));

            MySqlDataReader res = req.ExecuteReader();

            if (res.HasRows) {
                while (res.Read()) {
                    Addresses.Add(
                        new Address(
                            res.GetInt32("id_address"),
                            res.GetString("way"),
                            res.IsDBNull(2) ? null : res.GetString("complement"),
                            res.GetString("zip_code"),
                            res.GetString("city"),
                            client
                            )
                        );
                }
            }

            res.Close();

            if (client.Addresses != null) client.Addresses.Clear();
            client.Addresses = Addresses;

            return Addresses;
        }
    }
}
