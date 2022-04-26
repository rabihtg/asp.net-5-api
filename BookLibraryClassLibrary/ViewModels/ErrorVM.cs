using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.ViewModels
{
    public class ErrorVM
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string HelpLink { get; set; } = "https://google.com";
    }
}
