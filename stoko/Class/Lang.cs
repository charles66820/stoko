using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stoko {
    public class Lang {
        public String Code { get; }
        public String Name { get; }

        public Lang(String pCode, String pName) {
            Code = pCode;
            Name = pName;
        }
    }
}
