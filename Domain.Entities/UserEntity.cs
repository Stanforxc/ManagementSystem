using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class UserEntity
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }
}
