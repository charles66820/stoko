using stoko_class_BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stoko_db_BLL {
    public static class DbOrder {
        public static List<Order> GetOrders() {
            List<Order> Orders = new List<Order>();

            Orders.Add(new Order(1,2,25,62,755,new DateTime(),0,"toto",new Address(1,"test", "truc", "666820", "perpi")));
            Orders.Add(new Order(1,2,25,62,755,new DateTime(),1,"toto",new Address("test", "", "666820", "perpi")));
            Orders.Add(new Order(1,2,25,62,755,new DateTime(),0,"toto",new Address()));
            Orders.Add(new Order(1,2,25,62,755,new DateTime(),0,"toto",new Address()));
            Orders.Add(new Order(1,2,25,62,755,new DateTime(),2,"toto",new Address()));

            return Orders;
        }
    }
}
