using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stoko_class_BLL {
    public class Category {
        public int Id { get; }
        public String Title { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pTitle"></param>
        public Category(int pId, String pTitle) {
            Id = pId;
            Title = pTitle;
        }
    }
}
