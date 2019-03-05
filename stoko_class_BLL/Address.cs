using System;

namespace stoko_class_BLL {
    public class Address {
        public int Id { get; set; }
        public String Way { get; set; }
        public String Complement { get; set; }
        public String ZipCode { get; set; }
        public String City { get; set; }
        public Client Client { get; set; }
        public String FullAddress {
            get {
                String complement = String.Empty;

                if (Complement != null && Complement != String.Empty) complement = " " + Complement;

                return Way + complement + ", " + ZipCode + " " + City;
            }
        }

        public Address() { }
        public Address(int pId) {
            Id = pId;
        }

        /// <summary>
        /// Surcharged constructor without Id
        /// </summary>
        /// <param name="pWay"></param>
        /// <param name="pComplement"></param>
        /// <param name="pZipCode"></param>
        /// <param name="pCity"></param>
        /// <param name="pClient"></param>
        public Address(String pWay, String pComplement, String pZipCode, String pCity, Client pClient = null) {
            Way = pWay;
            Complement = pComplement;
            ZipCode = pZipCode;
            City = pCity;
            Client = pClient;
        }

        /// <summary>
        /// Surcharged constructor with Id
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pWay"></param>
        /// <param name="pComplement"></param>
        /// <param name="pZipCode"></param>
        /// <param name="pCity"></param>
        /// <param name="pClient"></param>
        public Address(int pId, String pWay, String pComplement, String pZipCode, String pCity, Client pClient = null) {
            Id = pId;
            Way = pWay;
            Complement = pComplement;
            ZipCode = pZipCode;
            City = pCity;
            Client = pClient;
        }
    }
}
