using System.ComponentModel.DataAnnotations;

namespace CBD.Administration.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// UserName
        /// </summary>
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        /// Remember me
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string Message { get; set; }
    }
}