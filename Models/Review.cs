using System;

namespace MoviesRating.Models
{
    public class Review
    {
        public int Id { get; set; }
        public double Rate { get; set; }
        public string Comment { get; set; }
        public DateTime AddDate { set; get; }
        public Movie Movie { set; get; }
        public User User { set; get; }
    }
}
