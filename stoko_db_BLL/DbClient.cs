using MySql.Data.MySqlClient;
using stoko_class_BLL;
using System;
using System.Collections.Generic;

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

        public static int CreateClient(Client client) {
            String sql = "INSERT INTO client(login, email, first_name, last_name, phone_number) " +
                "VALUE(@clientLogin, @clientEmail, @clientFirstName, @clientLastName, @clientPhoneNumber)";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@clientLogin", client.Login));
            req.Parameters.Add(new MySqlParameter("@clientEmail", client.Email));
            req.Parameters.Add(new MySqlParameter("@clientFirstName", client.FirstName));
            req.Parameters.Add(new MySqlParameter("@clientLastName", client.LastName));
            req.Parameters.Add(new MySqlParameter("@clientPhoneNumber", client.PhoneNumber));

            req.ExecuteNonQuery();
            return int.Parse(req.LastInsertedId.ToString());
        }

        public static void UpdateClient(Client client) {
            String sql = "UPDATE client SET login = @clientLogin, email = @clientEmail, " +
                "first_name = @clientFirstName, last_name = @clientLastName, phone_number = @clientPhoneNumber " +
                "WHERE id_client = @clientId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@clientLogin", client.Login));
            req.Parameters.Add(new MySqlParameter("@clientEmail", client.Email));
            req.Parameters.Add(new MySqlParameter("@clientFirstName", client.FirstName));
            req.Parameters.Add(new MySqlParameter("@clientLastName", client.LastName));
            req.Parameters.Add(new MySqlParameter("@clientPhoneNumber", client.PhoneNumber));
            req.Parameters.Add(new MySqlParameter("@clientId", client.Id));

            req.ExecuteNonQuery();
        }

        public static void DeleteClient(Client client) {
            String sql = "DELETE FROM client WHERE id_client = @clientId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@clientId", client.Id));

            req.ExecuteNonQuery();
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

        public static int CreateAddress(Address address) {
            String sql = "INSERT INTO address(way, complement, zip_code, city, id_client) " +
                "VALUE(@addressWay, @addressComplement, @addressZipCode, @addressCity, @addressClientId)";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@addressWay", address.Way));
            req.Parameters.Add(new MySqlParameter("@addressComplement", address.Complement));
            req.Parameters.Add(new MySqlParameter("@addressZipCode", address.ZipCode));
            req.Parameters.Add(new MySqlParameter("@addressCity", address.City));
            req.Parameters.Add(new MySqlParameter("@addressClientId", address.Client.Id));

            req.ExecuteNonQuery();
            return int.Parse(req.LastInsertedId.ToString());
        }

        public static void UpdateAddress(Address address) {
            String sql = "UPDATE address SET way = @addressWay, complement = @addressComplement, " +
                "zip_code = @addressZipCode, city = @addressCity " +
                "WHERE id_address = @addressId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@addressWay", address.Way));
            req.Parameters.Add(new MySqlParameter("@addressComplement", address.Complement));
            req.Parameters.Add(new MySqlParameter("@addressZipCode", address.ZipCode));
            req.Parameters.Add(new MySqlParameter("@addressCity", address.City));
            req.Parameters.Add(new MySqlParameter("@addressId", address.Id));

            req.ExecuteNonQuery();
        }

        public static void DeleteAddress(Address address) {
            String sql = "DELETE FROM address WHERE id_address = @addressId";

            MySqlCommand req = new MySqlCommand(sql, Data.DbConn);

            req.Parameters.Add(new MySqlParameter("@addressId", address.Id));

            req.ExecuteNonQuery();
        }
    }
}
