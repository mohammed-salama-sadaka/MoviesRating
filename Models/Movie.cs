using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System;

namespace MoviesRating.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Add Movie Name")]
        public string Name { set; get; }
        public string Description { set; get; }
        public string Publisher { set; get; }
       
        public double Rank { set; get; } = 0;
        public string PosterPath { set; get; }
  
        [NotMapped]
        public IFormFile PosterFile { set; get; }
        [DataType(DataType.Url, ErrorMessage = "Please Add A Vaild URL")]
        public string Tailer { set; get; }

        public virtual ICollection<Review> Reviews { set; get; } = new List<Review>();


        public double Rate => Math.Round((double)(Reviews?.Sum(i => i.Rate) / Reviews?.Count),2);

    }
}
