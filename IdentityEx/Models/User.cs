using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityEx.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FullName
        {
          get {
               return LastName + " " + FirstName;
              }
        }
        public string MobileNo { get; set; }
        public string Address { get; set; }

    }
}
