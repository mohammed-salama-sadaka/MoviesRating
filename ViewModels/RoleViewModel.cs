using System.ComponentModel.DataAnnotations;

namespace MoviesRating.ViewModels
{
	public class RoleViewModel
	{
		public string RoleId { get; set; }
		[Required(ErrorMessage ="Please add Role Name Firest"),StringLength(50)]
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
