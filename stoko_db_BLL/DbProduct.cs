using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using stoko_class_BLL;

namespace stoko_db_BLL {
    public static class DbProduct {
        public static List<Product> GetProducts() {
            List<Product> products = new List<Product>();

            products.Add(new Product(2, "test", 20, "fzefozef", 8, "fzfzkeffje zefjlzkefjlfk jezeezlk", new Category(5, "truc")));
            products.Add(new Product(3, "fzefzef", 34, "fzef", 2, "ffzefzefezff zefzez fzeef efzef", new Category(5, "truc")));
            products.Add(new Product(4, "fzefzef", 52, "fzef", 50, "g sdfgrytrjzh zty zerzeryre", new Category(1, "teu")));
            products.Add(new Product(6, "fzefzef", 9, "fzef", 3, "dsfgdg sdgfdg sdgfsdg", new Category(3, "heu")));

            return products;
        }
    }
}
