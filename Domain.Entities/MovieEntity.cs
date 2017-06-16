using System.ComponentModel.DataAnnotations;
using System;

namespace Domain.Entities
{
    public class MovieEntity
    {
        [Key]
        [Required]
        [Display(Name = " movie name" )]
        public string movie_name { get; set; }

        [Display(Name = "online name")]
        public DateTime online_time { get; set; }
        public int star { get; set; }
        public string director { get; set; }
        public string cast { get; set; }
        public int price { get; set; }
        public string runtime { get; set; }
        public string description { get; set; }
 
    }
}
