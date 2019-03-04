using stoko_class_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stoko_db_BLL {
    public static class DbClient {
        public static List<Client> GetClients() {
            List<Client> Clients = new List<Client>();

            Clients.Add(new Client(2, "toto", "toto@toto.fr", "", "", "", "https://scontent-mrs1-1.xx.fbcdn.net/v/t1.0-9/23844688_1730458603665154_1432580857537994705_n.jpg?_nc_cat=106&_nc_ht=scontent-mrs1-1.xx&oh=e65ecd7abbf8e5ebb42ca2e89d272d9c&oe=5D15DE6D", new DateTime()));
            Clients.Add(new Client(3, "tutu", "toto@toto.fr", "zefzef", "fzefzef", "", "", new DateTime()));
            Clients.Add(new Client(7, "titi", "toto@toto.fr", "fzef", "fzef", "0606060606", "https://scontent-mrs1-1.xx.fbcdn.net/v/t1.0-9/23844688_1730458603665154_1432580857537994705_n.jpg?_nc_cat=106&_nc_ht=scontent-mrs1-1.xx&oh=e65ecd7abbf8e5ebb42ca2e89d272d9c&oe=5D15DE6D", new DateTime()));
            Clients.Add(new Client(6, "tete", "toto@toto.fr", "fzefe", "", "0606060606", "https://scontent-mrs1-1.xx.fbcdn.net/v/t1.0-9/23844688_1730458603665154_1432580857537994705_n.jpg?_nc_cat=106&_nc_ht=scontent-mrs1-1.xx&oh=e65ecd7abbf8e5ebb42ca2e89d272d9c&oe=5D15DE6D", new DateTime()));
            Clients.Add(new Client(8, "to0a", "to0a66@toto.fr", "lagier", "mathieu", "0606060606", "https://scontent-mrs1-1.xx.fbcdn.net/v/t1.0-9/23844688_1730458603665154_1432580857537994705_n.jpg?_nc_cat=106&_nc_ht=scontent-mrs1-1.xx&oh=e65ecd7abbf8e5ebb42ca2e89d272d9c&oe=5D15DE6D", new DateTime()));

            return Clients;
        }

        public static List<Address> GetAddresses(Client client) {
            List<Address> Addresses = new List<Address>();

            Addresses.Add(new Address(5, "truc", "", "66000", "perpi", client));
            Addresses.Add(new Address(1, "fzef", "", "66000", "perpi", client));
            Addresses.Add(new Address(3, "zefe", "", "66000", "perpi", client));

            if (client.Addresses != null) client.Addresses.Clear();
            client.Addresses = Addresses;

            return Addresses;
        }
    }
}
