using System;

namespace stoko_class_BLL {
    public class OrderContent {
        public int ProductId { get; }
        public String ProductReference { get; }
        public String ProductName { get; }
        public int PriceHT { get; }
        public int Qantity { get; }
        public Order Order { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pProductId"></param>
        /// <param name="pProductReference"></param>
        /// <param name="pProductName"></param>
        /// <param name="pPriceHT"></param>
        /// <param name="pQantity"></param>
        /// <param name="pOrder"></param>
        public OrderContent(int pProductId, String pProductReference, String pProductName, int pPriceHT, int pQantity, Order pOrder) {
            ProductId = pProductId;
            ProductReference = pProductReference;
            ProductName = pProductName;
            PriceHT = pPriceHT;
            Qantity = pQantity;
        }
    }
}
