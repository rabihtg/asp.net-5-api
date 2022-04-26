using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Structs
{
    public readonly struct NoAuthRoutes
    {
        public static readonly List<string> Routes = new()
        {
            "/api/user/login",
            "/api/user/signup"
        };
    }
}
