﻿using System.ComponentModel.DataAnnotations;

namespace PracticeTest.Models
{
    public class CustomerRegistration
    {
        public string Id { get; set; }    
         public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }


    }
}
