using Microsoft.AspNetCore.Identity;

namespace MoviesRating.Models
{
    public class User:IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { set; get; }
        public string ProfileImagePath { set; get; }
        public byte[] ProfileImage { set; get; }
    }
}
