using System;
using System.Collections.Generic;

namespace stoko_class_BLL {
    public class Order {
        public int Id { get; }
        public int ClientId { get; }
        public double Shipping { get; }
        public double PriceHT { get; }
        public double PriceTTC { get; }
        public DateTime OrderedDate { get; }
        public int Shipped { get; set; }
        public String ClientName { get; }
        public Address Address { get; set; }

        public String ShippingAddress {
            get {
                return (Address.Id == -1) ? String.Empty : Address.FullAddress;
            }
        }
        
        public List<OrderContent> OrderContents { get; set; }

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
        public Order(int pId, int pClientId, double pShipping, double pPriceHT, double pPriceTTC, DateTime pOrderedDate, int pShipped, String pClientName, Address pAddress) {
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
    }
}
