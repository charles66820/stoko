using System;
using System.Collections.Generic;

namespace stoko_class_BLL {
    public class Client {
        public int Id { get; set; }
        public String Login { get; set; }
        public String Email { get; set; }
        public String LastName { get; set; }
        public String FirstName { get; set; }
        public String PhoneNumber { get; set; }
        public String Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Address> Addresses { get; set; }

        public Client() { }
        public Client(int pId) {
            Id = pId;
        }

        /// <summary>
        /// Surcharged constructor without Id
        /// </summary>
        /// <param name="pLogin"></param>
        /// <param name="pEmail"></param>
        /// <param name="pLastName"></param>
        /// <param name="pFirstName"></param>
        /// <param name="pPhoneNumber"></param>
        /// <param name="pAvatar"></param>
        /// <param name="pCreatedDate"></param>
        public Client(String pLogin, String pEmail, String pLastName, String pFirstName, String pPhoneNumber, String pAvatar, DateTime pCreatedDate) {
            Login = pLogin;
            Email = pEmail;
            LastName = pLastName;
            FirstName = pFirstName;
            PhoneNumber = pPhoneNumber;
            Avatar = pAvatar;
            CreatedDate = pCreatedDate;
        }

        /// <summary>
        /// Surcharged constructor with Id
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pLogin"></param>
        /// <param name="pEmail"></param>
        /// <param name="pLastName"></param>
        /// <param name="pFirstName"></param>
        /// <param name="pPhoneNumber"></param>
        /// <param name="pAvatar"></param>
        /// <param name="pCreatedDate"></param>
        public Client(int pId, String pLogin, String pEmail, String pLastName, String pFirstName, String pPhoneNumber, String pAvatar, DateTime pCreatedDate) {
            Id = pId;
            Login = pLogin;
            Email = pEmail;
            LastName = pLastName;
            FirstName = pFirstName;
            PhoneNumber = pPhoneNumber;
            Avatar = pAvatar;
            CreatedDate = pCreatedDate;
        }
    }
}
