using System;

namespace stoko_class_BLL {
    public class Picture {
        public int Id { get; set; }
        public String PictureName { get; set; }
        public DateTime UpdateAt { get; set; }
        public Product Product { get; set; }

        public Picture() { }

        /// <summary>
        /// Surcharged constructor
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pPictureName"></param>
        /// <param name="pUpdateAt"></param>
        /// <param name="pProduct"></param>
        public Picture(int pId, String pPictureName, DateTime pUpdateAt, Product pProduct) {
            Id = pId;
            PictureName = pPictureName;
            UpdateAt = pUpdateAt;
            Product = pProduct;
        }
    }
}
