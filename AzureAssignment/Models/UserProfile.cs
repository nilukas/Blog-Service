using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AzureAssignment.Models
{
    public class UserProfile
    {
        [Key]
        public string ProfileId { get; set; }

       public string FirstName { get; set; }

       public string LastName { get; set; }
        
       public string Address { get; set; }

      public string Email { get; set; }

      public string Telephone { get; set; }

      public string ImageUrl { get; set; }
    }

}
