using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(15)]
        public string Password { get; set; }
    }
}
