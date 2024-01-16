using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesRating.ViewModels
{
	public class UserViewModel
	{
		public string UserId { get; set; }
		[Required]
		public string FirstName { get; set; }
		[Required]
		public string LastName { get; set; }
		public string UserName { get; set; }
		[DataType(DataType.EmailAddress)]
		[Required]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		[Required]
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }

		public List<RoleViewModel> RolesViewModel{ get; set; }

		
	}
}
