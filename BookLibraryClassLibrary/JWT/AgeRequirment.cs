using Microsoft.AspNetCore.Authorization;

namespace BookLibraryClassLibrary.JWT
{
    public class AgeRequirment : IAuthorizationRequirement
    {
        public AgeRequirment(int age)
        {
            Age = age;
        }

        public int Age { get; set; }


    }
}