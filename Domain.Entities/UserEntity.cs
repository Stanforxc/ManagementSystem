using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEntity
    {
        [Key]
        public int UserID { get; set; }
        public string account { get; set; }
        public string passwd { get; set; }
    }
}
