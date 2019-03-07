using System;
using System.Collections.Generic;

namespace stoko_class_BLL {
    public class Product {
        public int Id { get; set; }
        public String Title { get; set; }
        public double PriceHT { get; set; }
        public String Reference { get; set; }
        private int quantity;
        public int Quantity {
            get { return quantity; }
            set {
                QuantityStock = (QuantityStock - quantity) + value;
                quantity = value;
            }
        }
        public int QuantityStock { get; set; }
        public String Description { get; set; }
        public Category Category { get; set; }
        public List<Picture> Pictures { get; set; }

        public Product() { }

        /// <summary>
        /// Surcharged constructor without Id
        /// </summary>
        /// <param name="pTitle"></param>
        /// <param name="pPriceHT"></param>
        /// <param name="pReference"></param>
        /// <param name="pQuantity"></param>
        /// <param name="pQuantityStock"></param>
        /// <param name="pDescription"></param>
        /// <param name="pCategory"></param>
        public Product(String pTitle, double pPriceHT, String pReference, int pQuantity, int pQuantityStock, String pDescription, Category pCategory = null) {
            Title = pTitle;
            PriceHT = pPriceHT;
            Reference = pReference;
            Quantity = pQuantity;
            QuantityStock = pQuantityStock;
            Description = pDescription;
            Category = pCategory;
        }

        /// <summary>
        /// Surcharged constructor with Id
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pTitle"></param>
        /// <param name="pPriceHT"></param>
        /// <param name="pReference"></param>
        /// <param name="pQuantity"></param>
        /// <param name="pQuantityStock"></param>
        /// <param name="pDescription"></param>
        /// <param name="pCategory"></param>
        public Product(int pId, String pTitle, double pPriceHT, String pReference, int pQuantity, int pQuantityStock, String pDescription, Category pCategory = null) {
            Id = pId;
            Title = pTitle;
            PriceHT = pPriceHT;
            Reference = pReference;
            Quantity = pQuantity;
            QuantityStock = pQuantityStock;
            Description = pDescription;
            Category = pCategory;
        }
    }
}
