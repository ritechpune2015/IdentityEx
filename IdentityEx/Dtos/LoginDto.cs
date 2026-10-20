using System.ComponentModel.DataAnnotations;

namespace IdentityEx.Dtos
{
    public class LoginDto
    {
        [EmailAddress]
        [Required]
        public string EmailID { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
