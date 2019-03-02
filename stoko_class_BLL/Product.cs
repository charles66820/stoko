﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stoko_class_BLL {
    class Product {
        public int Id { get; set; }
        public String Title { get; set; }
        public int PriceHT { get; set; }
        public String Reference { get; set; }
        public String Quantity { get; set; }
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
        /// <param name="pDescription"></param>
        /// <param name="pCategory"></param>
        public Product(String pTitle, int pPriceHT, String pReference, String pQuantity, String pDescription, Category pCategory) {
            Title = pTitle;
            PriceHT = pPriceHT;
            Reference = pReference;
            Quantity = pQuantity;
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
        /// <param name="pDescription"></param>
        /// <param name="pCategory"></param>
        public Product(int pId, String pTitle, int pPriceHT, String pReference, String pQuantity, String pDescription, Category pCategory) {
            Id = pId;
            Title = pTitle;
            PriceHT = pPriceHT;
            Reference = pReference;
            Quantity = pQuantity;
            Description = pDescription;
            Category = pCategory;
        }
    }
}
