using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.ViewModels
{
    public class UserVM
    {
    }
    public class InsertUserVM
    {
        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
    public class UpdateUserPassVM
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string NewPassword { get; set; }
    }
    public class UpdateUserUserNameVM
    {
        [Required]
        public string OldUserName { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string NewUserName { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
