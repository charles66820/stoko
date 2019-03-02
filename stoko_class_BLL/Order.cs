using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stoko_class_BLL {
    public class Order {
        public int Id { get; }
        public int ClientId { get; }
        public int Shipping { get; }
        public int PriceHT { get; }
        public int PriceTTC { get; }
        public DateTime OrderedDate { get; }
        public int Shipped { get; set; }
        public String ClientName { get; }
        public Address Address { get; set; }

        private List<OrderContent> orderContents;
        public List<OrderContent> OrderContents {
            get {
                if (orderContents == null) {
                    /* TODO: BDD GET */
                    //orderContents = new OrderContent();
                    return orderContents;
                } else {
                    return orderContents;
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pClientId"></param>
        /// <param name="pShipping"></param>
        /// <param name="pPriceHT"></param>
        /// <param name="pPriceTTC"></param>
        /// <param name="pOrderedDate"></param>
        /// <param name="pShipped"></param>
        /// <param name="pClientName"></param>
        /// <param name="pAddress"></param>
        public Order(int pId, int pClientId, int pShipping, int pPriceHT, int pPriceTTC, DateTime pOrderedDate, int pShipped, String pClientName, Address pAddress) {
            Id = pId;
            ClientId = pClientId;
            Shipping = pShipping;
            PriceHT = pPriceHT;
            PriceTTC = pPriceTTC;
            OrderedDate = pOrderedDate;
            Shipped = pShipped;
            ClientName = pClientName;
            Address = pAddress;
        }

        //TODO
        public List<Address> GetClientAddresses() {
            List<Address> clientAddresses = new List<Address>();

            //get address with
            var oui = ClientId;

            return clientAddresses;
        } 
    }
}
